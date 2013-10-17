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
using TRPO.GlobalObj;
using TRPO.Properties;

namespace TRPO.View
{
    public partial class ChiefForm : Form, IOrderViewable, IDishManagable, IMenuManagable
    {
        OrderCookController ordCookContr;
        DishesManagementController dishesManagementContr;
        MenuManagementConroller menuController;

        Timer refreshTimer;

        public ChiefForm(OrderCookController occ, DishesManagementController dmc, MenuManagementConroller mmc)
        {
            InitializeComponent();
            ordCookContr = occ;
            dishesManagementContr = dmc;
            menuController = mmc;
            setDishInfo(new Dish());
            
            refreshTimer = new Timer();
            refreshTimer.Interval = Settings.Default.refresh_rate_sec * 1000;
            refreshTimer.Tick += new EventHandler(refreshTimer_Tick);
            refreshTimer.Enabled = true;
        }

        private void refreshTimer_Tick(Object myObject, EventArgs myEventArgs)
        {
            if (listView1 != null && mainTab.SelectedIndex == 0) 
            {
                ordCookContr.updateOrderList();
                dishesManagementContr.updateAbleToCookDishes();
                dishesManagementContr.updateRedundantDishesAmount();
            }
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

        public void setRedundantDishes(int n)
        {
            ableToGetTextBox.Text = n.ToString();
            readyDishesAmount2.Maximum = getSelectedDishAmount();
        }

        public void setDishInfo(Dish d)
        {
            switch (mainTab.SelectedIndex)
            {
                case 0:
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
                    break;
                case 1:
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
                        createDishImage.ImageLocation = "";
                    }

                    createDishContentsDataGrid.Rows.Clear();
                    foreach (KeyValuePair<String, Double> p in d.Consistance)
                    {
                        createDishContentsDataGrid.Rows.Add(p.Key, p.Value);
                    }

                    break;
                case 2:
                    label11.Text = d.Name;
                    label12.Text = d.DishType;
                    if (d.LinkToPhoto != "" && File.Exists(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto))
                    {

                        pictureBox1.Image = Image.FromFile(Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto);
                        pictureBox1.ImageLocation = Properties.Settings.Default.dishesImagesFolderPath + d.LinkToPhoto;
                    }
                    else
                    {
                        pictureBox1.Image = null;
                        pictureBox1.ImageLocation = "";
                    }

                    dataGridView5.Rows.Clear();
                    foreach (KeyValuePair<String, Double> p in d.Consistance)
                    {
                        dataGridView5.Rows.Add(p.Key, p.Value);
                    }
                    break;
            }
            

        }

        public void setDishesList(Dictionary<String, String> dishes)
        {
            switch (mainTab.SelectedIndex)
            {
                case 0:
                    
                    break;
                case 1:
                    dishesDataGrid.Rows.Clear();

                    if (dishes != null && dishes.Count != 0)
                    {
                        foreach (KeyValuePair<String, String> p in dishes)
                        {
                            dishesDataGrid.Rows.Add(p.Key, p.Value);
                        }
                    }

                    break;
                case 2:
                    menuCreationDishes.Rows.Clear();

                    if (dishes != null && dishes.Count != 0)
                    {
                        foreach (KeyValuePair<String, String> p in dishes)
                        {
                            menuCreationDishes.Rows.Add(p.Key, p.Value);
                        }
                    }
                    break;
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
                productsDataGrid.Sort(productsDataGrid.Columns[0], ListSortDirection.Ascending);
                productsDataGrid.Rows[0].Selected= true;
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

            if ((listView1.Items.Count > 0) && (!listView1.Focused || listView1.SelectedItems.Count <= 0))
            {
                if (listView1.Items.Count <= selectedItem)
                {
                    selectedItem = listView1.Items.Count - 1;
                }
                
                //this.listView1.Focus();
                this.listView1.Items[selectedItem].Selected = true;
            }
            dishesManagementContr.updateDishInfo();
        }

        public void addProductToContence(String s, Double d)
        {
            createDishContentsDataGrid.Rows.Add(s, d);
        }

        public void addDishToMenu1(String dish)
        {
            dataGridView1.Rows.Add(dish);
        }

        public void addDishToMenu2(String dish)
        {
            dataGridView2.Rows.Add(dish);
        }

        public void addDishToMenu3(String dish)
        {
            dataGridView3.Rows.Add(dish);
        }

        public void addDishToSpecialMenu(String dish, String type)
        {
            dataGridView4.Rows.Add(dish, type);
        }

        public void clearMenu()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            complexDishCheckbox.Checked = false;
        }

        public TRPO.Structures.Menu getCreatedMenu()
        {
            TRPO.Structures.Menu m;
            if (!complexDishCheckbox.Checked)
            {
                List<String> menu1 = new List<String>();
                List<String> menu2 = new List<String>();
                List<String> menu3 = new List<String>();
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    menu1.Add(r.Cells[0].Value.ToString());
                }
                foreach (DataGridViewRow r in dataGridView2.Rows)
                {
                    menu2.Add(r.Cells[0].Value.ToString());
                }
                foreach (DataGridViewRow r in dataGridView3.Rows)
                {
                    menu3.Add(r.Cells[0].Value.ToString());
                }
                m = new TRPO.Structures.Menu(menu1, menu2, menu3, false, dateTimePicker.Value);
            }
            else
            {
                List<String> menu1 = new List<String>();
                List<String> menu2 = new List<String>();
                List<String> menu3 = new List<String>();
                foreach (DataGridViewRow r in dataGridView4.Rows)
                {
                    switch (r.Cells[1].Value.ToString())
                    {
                        case "Первое":
                            menu1.Add(r.Cells[0].Value.ToString());
                            break;
                        case "Второе":
                            menu2.Add(r.Cells[0].Value.ToString());
                            break;
                        case "Третье":
                            menu3.Add(r.Cells[0].Value.ToString());
                            break;
                    }
                    
                }

                m = new TRPO.Structures.Menu(menu1, menu2, menu3, true, dateTimePicker.Value);
            }
            return m;
        }

        public bool inMenu1(String dishName)
        {
            bool res = false;
            foreach (DataGridViewRow r in dataGridView1.Rows)
            {
                if (r.Cells[0].Value.ToString() == dishName)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool inMenu2(String dishName)
        {
            bool res = false;
            foreach (DataGridViewRow r in dataGridView2.Rows)
            {
                if (r.Cells[0].Value.ToString() == dishName)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool inMenu3(String dishName)
        {
            bool res = false;
            foreach (DataGridViewRow r in dataGridView3.Rows)
            {
                if (r.Cells[0].Value.ToString() == dishName)
                {
                    res = true;
                }
            }
            return res;
        }


        public bool inSpecialMenu(String dishName)
        {
            bool res = false;
            foreach (DataGridViewRow r in dataGridView4.Rows)
            {
                if (r.Cells[0].Value.ToString() == dishName)
                {
                    res = true;
                }
            }
            return res;
        }

        public bool addingToComplexMenu()
        {
            return complexDishCheckbox.Checked;
        }

        public int getReadyDishesAmount()
        {
            return Convert.ToInt32(readyDishesAmount.Value);
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
                    if (dataGridView1.Focused)
                    {
                        result = dataGridView1.SelectedRows.Count > 0 ? dataGridView1.SelectedRows[0].Cells[0].Value.ToString() : "";
                    }
                    else
                        if (dataGridView2.Focused)
                        {
                            result = dataGridView2.SelectedRows.Count > 0 ? dataGridView2.SelectedRows[0].Cells[0].Value.ToString() : "";
                        }
                        else
                            if (dataGridView3.Focused)
                            {
                                result = dataGridView3.SelectedRows.Count > 0 ? dataGridView3.SelectedRows[0].Cells[0].Value.ToString() : "";
                            }
                            else
                                if (dataGridView4.Focused)
                                {
                                    result = dataGridView4.SelectedRows.Count > 0 ? dataGridView4.SelectedRows[0].Cells[0].Value.ToString() : "";
                                } 
                                else
                                {
                                    result = menuCreationDishes.SelectedRows.Count > 0 ? menuCreationDishes.SelectedRows[0].Cells[0].Value.ToString() : "";
                                }
                    
                    break;
            }
            return result;
        }

        public String getSelectedDishType()
        {
            String result = "";
            switch (mainTab.SelectedIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    if (dataGridView1.Focused)
                    {

                    }
                    else
                        if (dataGridView2.Focused)
                        {

                        }
                        else
                            if (dataGridView3.Focused)
                            {

                            }
                            else
                                if (dataGridView4.Focused)
                                {
                                    result = dataGridView4.SelectedRows.Count > 0 ? dataGridView4.SelectedRows[0].Cells[1].Value.ToString() : "";
                                }
                                else
                                {
                                    result = menuCreationDishes.SelectedRows.Count > 0 ? menuCreationDishes.SelectedRows[0].Cells[1].Value.ToString() : "";
                                }
                    break;
            }
            return result;
        }

        public int getSelectedDishAmount()
        {
            int res = 0;
            switch (mainTab.SelectedIndex)
            {
                case 0:
                    res = Convert.ToInt32(listView1.SelectedItems[0].SubItems[3].Text);
                    break;
                case 1:
                    break;
                case 2:
                    break;
            }
            return res;
        }

        public int getReadyStockDishesAmount()
        {
            return (int) readyDishesAmount2.Value;
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

        public String getSelectedContenceName()
        {
            String result = "";

            result = createDishContentsDataGrid.SelectedRows.Count > 0 ? createDishContentsDataGrid.SelectedRows[0].Cells[0].Value.ToString() : "";
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
                String tmpProdName = "";
                for (i = 0; i < createDishContentsDataGrid.Rows.Count; i++)
                {
                    tmpProdName = createDishContentsDataGrid.Rows[i].Cells[0].Value.ToString();
                    if (!res.ContainsKey(tmpProdName))
                    {
                        res.Add(tmpProdName, Convert.ToDouble(createDishContentsDataGrid.Rows[i].Cells[1].Value.ToString()));
                    }
                }
                return res;
            }
            catch (FormatException)
            {
                showMsg("Неверный формат количества продукта в составе в строке номер " + (i + 1), "Ошибка!");
                return new Dictionary<String, Double>();
            }

        }

        public Dish getCreatedDish()
        {
            Dish res = new Dish(-1, getCreationDishName(), getCreationImageLocation(), getCreationDishType(), -1, getCreationDishReceipe(), getDishConsistance());
            return res;
        }

        public String getAddingProductName()
        {
            return Microsoft.VisualBasic.Interaction.InputBox("Введите название продукта:", "Добавление продукта", "", -1, -1);
        }

        public void setDishCreationPrice(Double price)
        {
            priceTextBox.Text = price.ToString();
        }

        private void listView1_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            dishesManagementContr.fillDishProd();
            dishesManagementContr.updateDishInfo();
            dishesManagementContr.updateAbleToCookDishes();
            dishesManagementContr.updateRedundantDishesAmount();

        }

        private void readyButton_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Visible = false;
            dishesManagementContr.addReadyDishes();
            ordCookContr.updateOrderList();
            dishesManagementContr.updateAbleToCookDishes();
            dishesManagementContr.updateRedundantDishesAmount();

            readyDishesAmount.Value = 0;
            readyDishesAmount2.Value = 0;
        }

        private void mainTab_Selected(object sender, TabControlEventArgs e)
        {
            toolStripStatusLabel.Visible = false;
            ordCookContr.updateOrderList();
            dishesManagementContr.updateAbleToCookDishes();
            dishesManagementContr.updateRedundantDishesAmount();
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
                createDishImage.ImageLocation = "";
                createDishContentsDataGrid.Rows.Clear();

                dishesDataGrid.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                dishesDataGrid.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
                createDishName.ReadOnly = false;
                createDishName.BackColor = System.Drawing.Color.White;

                //labelPrice.Visible = true;
               // priceTextBox.Visible = true;
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

             //   labelPrice.Visible = false;
              //  priceTextBox.Visible = false;
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

        private void ChiefForm_Load(object sender, EventArgs e)
        {
            var dishTypes = new string[] { "Первое", "Второе", "Третье" };
            createDishType.DataSource = dishTypes;
            createDishType.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            createDishType.AutoCompleteSource = AutoCompleteSource.ListItems;
            createDishType.DropDownStyle = ComboBoxStyle.DropDownList;

            //Создание меню
            dateTimePicker.Value = DateTime.Now.AddDays(1);
        }

        public void setAbleToCookDishes(int amount)
        {
            ableToCookTextBox.Text = amount.ToString();
        }

        private void addProdButton_Click(object sender, EventArgs e)
        {
            dishesManagementContr.addProduct();
        }

        private void productsDataGrid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar >= 'а' && e.KeyChar <= 'я') || (e.KeyChar >= 'А' && e.KeyChar <= 'Я'))
            {
                e.KeyChar = Char.ToUpper(e.KeyChar);

                for (int i = 0; i < productsDataGrid.Rows.Count; i++)
                {

                    if (Char.ToUpper(productsDataGrid.Rows[i].Cells[0].Value.ToString()[0]) == e.KeyChar)
                    {
                        productsDataGrid.Rows[i].Selected = true;
                        productsDataGrid.CurrentCell = productsDataGrid.Rows[i].Cells[0];
                        productsDataGrid.FirstDisplayedScrollingRowIndex = i;
                        i = productsDataGrid.Rows.Count;
                    }
                }

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker.Value = dateTimePicker1.Value.AddDays(1);
        }

        private void dishesDataGrid_SelectionChanged_1(object sender, EventArgs e)
        {
            dishesManagementContr.updateCreationDishInfo();
        }

        private void menuCreationDishes_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            menuController.addDish();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            switch (complexDishCheckbox.CheckState)
            {
                case CheckState.Checked:
                    tableLayoutPanel19.Enabled = false;

                    dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
                    dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    dataGridView2.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
                    dataGridView3.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
                    dataGridView3.DefaultCellStyle.ForeColor = Color.FromArgb(200, 200, 200);
                    break;
                case CheckState.Unchecked:
                    tableLayoutPanel19.Enabled = true;

                    dataGridView1.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                    dataGridView1.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
                    dataGridView2.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                    dataGridView2.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
                    dataGridView3.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
                    dataGridView3.DefaultCellStyle.ForeColor = Color.FromArgb(0, 0, 0);
                    break;
            }
            
            
            //TODO создание комплексного меню. деактивируются другие поля
        }

        private void menuCreationDishes_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space))
            {
                if (complexDishCheckbox.Checked)
                {
                    this.addDishToSpecialMenu(this.getSelectedDishName(), getSelectedDishType());
                }
                else
                {
                    menuController.addDish();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            menuController.addMenu();
        }

        private void createDishContentsDataGrid_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            createDishContentsDataGrid.Rows.Remove(createDishContentsDataGrid.SelectedRows[0]);

         //       dishesManagementContr.deleteProductFromDish();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
        }

        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
        }

        private void dataGridView3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Rows.Remove(dataGridView3.SelectedRows[0]);
        }

        private void dataGridView4_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView4.Rows.Remove(dataGridView4.SelectedRows[0]);
        }

        private void createDishContentsDataGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dishesManagementContr.updateDishPrice();
        }

        private void createDishContentsDataGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            dishesManagementContr.updateDishPrice();
        }

        private void createDishContentsDataGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dishesManagementContr != null)
            {
                dishesManagementContr.updateDishPrice();
            }
        }

        private void ableToCookTextBox_TextChanged(object sender, EventArgs e)
        {
            readyDishesAmount.Maximum = Convert.ToInt32(ableToCookTextBox.Text);
        }

        private void ableToGetTextBox_TextChanged(object sender, EventArgs e)
        {
            readyDishesAmount2.Maximum = Convert.ToInt32(ableToGetTextBox.Text);
        }

        private void ChiefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            refreshTimer.Enabled = false;
        }

        private void getFromReadyButton_Click(object sender, EventArgs e)
        {
            dishesManagementContr.addDishesFromStock();

            ordCookContr.updateOrderList();
            dishesManagementContr.updateAbleToCookDishes();
            dishesManagementContr.updateRedundantDishesAmount();

            readyDishesAmount.Value = 0;
            readyDishesAmount2.Value = 0;
        }
    }
}
