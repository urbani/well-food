using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms; 
using System.Data.OleDb;
using System.Data.Common;
using System.Collections;
using TRPO.Controller;
using TRPO.Structures;
using System.IO;

namespace TRPO.View
{
    public partial class ChiefForm : Form, IOrderViewable, IDishManagable
    {
        OrderCookController ordCookContr;
        DishesManagementController dishesManagementContr;

        public ChiefForm(OrderCookController occ, DishesManagementController dmc)
        {
            InitializeComponent();
            ordCookContr = occ;
            dishesManagementContr = dmc;
            setDishInfo(new Dish());

        }

        public void updateOrderList(List<ChiefListEntry> list)
        {//TODO: отладить изменение количества элементов в заказе
            int selectedItem = 0;
            if (listView1.SelectedIndices.Count > 0)
            {
                selectedItem = listView1.SelectedIndices[0];
            }

            listView1.Items.Clear();
            String[] s;


            foreach (ChiefListEntry entry in list)
            {
                s = new String[] { entry.name, entry.need.ToString(), entry.ready.ToString(), entry.left.ToString() };
                ListViewItem tmp = new ListViewItem(s);
                listView1.Items.Add(tmp);
            }

            if ((listView1.Items.Count > 0) &&(!listView1.Focused || listView1.SelectedItems.Count <= 0))
            {
                if (listView1.Items.Count <= selectedItem)
                {
                    selectedItem = listView1.Items.Count - 1;
                }
                this.listView1.Focus();
                this.listView1.Items[selectedItem].Selected = true;
            }
            dishesManagementContr.updateDishInfo();
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
            switch (el)
            {
                case GlobalObj.ErrorLevels.Critical:
                    MessageBox.Show(msg);
                    break;
                case GlobalObj.ErrorLevels.Info:
                    toolStripStatusLabel.Text = msg;
                    toolStripStatusLabel.Visible = true;
                    break;
            }
            
        }

        public void showMsg(String msg, String header)
        {
            MessageBox.Show(msg, header);
        }

        public String getSelectedDishName()
        {
            String result = "";
            switch (mainTab.SelectedIndex)
            {
                case 0:
                    result = listView1.SelectedItems.Count > 0 ? listView1.SelectedItems[0].Text : "";
                    break;
                case 1:
                    result = dishesDataGrid.SelectedRows.Count > 0 ? dishesDataGrid.SelectedRows[0].Cells[0].Value.ToString() : "";
                    break;
                case 2:
                    result = "";
                    break;
            }
            return result;
        }

        public void setDishInfo(Dish d)
        {
          
            dishName.Text = d.Name;
            dishTypeLabel.Text = d.DishType;
            receipeText.Text = d.Recipe;
            if (d.LinkToPhoto != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto))
            {

                dishPicture.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto);
            }
            else
            {
                dishPicture.Image = null;
            }

            dishConsistanceDataGrid.Rows.Clear();
            foreach (KeyValuePair<String, Double> p in d.Consistance)
            {
                dishConsistanceDataGrid.Rows.Add(p.Key, p.Value);
            }

        }

        public void setCreateDishInfo(Dish d)
        {

            createDishName.Text = d.Name;
            createDishType.Text = d.DishType;
            createDishRecipe.Text = d.Recipe;
            if (d.LinkToPhoto != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto))
            {

                createDishImage.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto);
                createDishImage.ImageLocation = Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto;
            }
            else
            {
                createDishImage.Image = null;
            }

            createDishContentsDataGrid.Rows.Clear();
            foreach (KeyValuePair<String, Double> p in d.Consistance)
            {
                createDishContentsDataGrid.Rows.Add(p.Key, p.Value);
            }
        }

        public int getReadyDishesAmount()
        {
            return Convert.ToInt32(readyDishesAmount.Value);
        }

        public void setDishesList(Dictionary<String, String> dishes)
        {
            dishesDataGrid.Rows.Clear();

            if (dishes != null && dishes.Count != 0)
            {
                foreach (KeyValuePair<String, String> p in dishes)
                {
                    dishesDataGrid.Rows.Add(p.Key, p.Value);
                }
            }
        }

        public void setProductsList(List<String> pList)
        {
            productsDataGrid.Rows.Clear();
            if (pList != null && pList.Count != 0)
            {
                foreach (String s in pList)
                {
                    productsDataGrid.Rows.Add(s);
                }
            }
        }

        public void updateContents(Dictionary<String, Double> cont)
        {
            createDishContentsDataGrid.Rows.Clear();
            foreach(KeyValuePair<String, Double> p in cont)
            {
                createDishContentsDataGrid.Rows.Add(p.Key, p.Value);
            }
        }


        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            dishesManagementContr.updateDishInfo();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Visible = false;
            dishesManagementContr.addReadyDishes();
            ordCookContr.updateOrderList();
        }

        private void mainTab_Selected(object sender, TabControlEventArgs e)
        {
            toolStripStatusLabel.Visible = false;
            dishesManagementContr.fillDishProd();
        }

        private void readyDishesAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                readyButton_Click(sender, new EventArgs());
                readyDishesAmount.Focus();
            }
        }

        private void dishesDataGrid_SelectionChanged(object sender, EventArgs e)
        {
            //dishesManagementContr.updateContents();
            dishesManagementContr.updateCreationDishInfo();
        }

        private void createDishCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (createDishCheckBox.Checked)
            {
                tableLayoutPanel5.Enabled = false;
                createDishButton.Text = "Создать";

                createDishName.Text = "";
                createDishType.Text = "";
                createDishRecipe.Text = "";
                createDishImage.Image = null;
                createDishContentsDataGrid.Rows.Clear();

                dishesDataGrid.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dishesDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
                createDishName.ReadOnly = false;
                createDishName.BackColor = System.Drawing.Color.White;
            }
            else
            {
                tableLayoutPanel5.Enabled = true;
                createDishButton.Text = "Обновить";
                dishesManagementContr.updateCreationDishInfo();

                dishesDataGrid.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                dishesDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
                createDishName.ReadOnly = true;
                createDishName.BackColor = System.Drawing.SystemColors.Control;
            }
        }

        private void createDishButton_Click(object sender, EventArgs e)
        {
            if (createDishCheckBox.Checked)
            {
                dishesManagementContr.createNewDish();
            }
            else
            {
                dishesManagementContr.updateDish();
            }
        }

        private void createDishImage_Click(object sender, EventArgs e)
        {// Изменяет изображение блюда
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files (*.*)|*.*";
            ofd.InitialDirectory = Properties.Settings.Default.dishesImagesFolderPath;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String fileName = Path.GetFileName(ofd.FileName);
                if (fileName != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + fileName))
                {
                    createDishImage.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + fileName);
                    createDishImage.ImageLocation = Properties.Settings.Default.dishesImagesFolderPath + fileName;
                }
                else
                {
                    createDishImage.Image = null;
                    showMsg("Такого файла нет в директории с изображениями: " + Properties.Settings.Default.dishesImagesFolderPath, "Ошибка!");
                }
            }
        }

        private void productsDataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {//Добавить кликнутый продукт к составу
            dishesManagementContr.addProductToDish();
        }

        private void createDishContentsDataGrid_KeyDown(object sender, KeyEventArgs e)
        {//удалить активный продукт из состава
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
            {
                dishesManagementContr.deleteProductFromDish();
                
            }
        }

        public void addProductToContence(String s, Double d)
        {
            createDishContentsDataGrid.Rows.Add(s, d);
        }

        public String getCreationImageLocation()
        {
            return Path.GetFileName(createDishImage.ImageLocation);
        }

        public String getSelectedProductName()
        {
            String result = "";

            result = productsDataGrid.SelectedRows.Count > 0 ? productsDataGrid.SelectedRows[0].Cells[0].Value.ToString() : "";
            return result;
        }

        public String getCreationDishType()
        {
            return createDishType.Text;
        }

        public String getCreationDishName()
        {
            return createDishName.Text;
        }

        public String getCreationDishReceipe()
        {
            return createDishRecipe.Text;
        }

        public Dictionary<String, Double> getDishConsistance()
        {
            Dictionary<String, Double> res = new Dictionary<String, Double>();
            int i = 0;
            try
            {
                for (i = 0; i < createDishContentsDataGrid.Rows.Count; i++)
                {
                    res.Add(createDishContentsDataGrid.Rows[i].Cells[0].Value.ToString(), Convert.ToDouble(createDishContentsDataGrid.Rows[i].Cells[1].Value.ToString()));
                }
                return res;
            }
            catch (FormatException ex)
            {
                showMsg("Неверный формат количества продукта в составе в строке номер " + (i + 1), "Ошибка!");
                return new Dictionary<String, Double>();
            }
            
        }

        private void ChiefForm_Load(object sender, EventArgs e)
        {
            var dishTypes = new string[] { "Первое", "Второе", "Третье" };
            createDishType.DataSource = dishTypes;
            createDishType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            createDishType.AutoCompleteSource = AutoCompleteSource.ListItems;
            createDishType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        public Dish getCreatedDish()
        {
            Dish res = new Dish(-1, getCreationDishName(), getCreationImageLocation(), getCreationDishType(), -1, getCreationDishReceipe(), getDishConsistance());
            return res;
        }

    }
}
