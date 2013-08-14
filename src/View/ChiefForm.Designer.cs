namespace TRPO.View
{
    partial class ChiefForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        protected System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChiefForm));
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "one",
            "1"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("three");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("two");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
            "Картошка",
            "23"}, -1);
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
            "Гречка",
            "10"}, -1);
            this.leftBodyTable = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rightBodyTabel = new System.Windows.Forms.TableLayoutPanel();
            this.dishLayout = new System.Windows.Forms.TableLayoutPanel();
            this.dishLayoutHeader = new System.Windows.Forms.TableLayoutPanel();
            this.dishPicture = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.dishName = new System.Windows.Forms.Label();
            this.dishTypeLabel = new System.Windows.Forms.Label();
            this.receipeLayout = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.receipeText = new System.Windows.Forms.RichTextBox();
            this.dishAddLayout = new System.Windows.Forms.TableLayoutPanel();
            this.readyDishesAmount = new System.Windows.Forms.NumericUpDown();
            this.readyButton = new System.Windows.Forms.Button();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.storageFoodList = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label3 = new System.Windows.Forms.Label();
            this.dishFoodList = new System.Windows.Forms.ListView();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.leftBodyTable.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.rightBodyTabel.SuspendLayout();
            this.dishLayout.SuspendLayout();
            this.dishLayoutHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dishPicture)).BeginInit();
            this.tableLayoutPanel2.SuspendLayout();
            this.receipeLayout.SuspendLayout();
            this.dishAddLayout.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.readyDishesAmount)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // leftBodyTable
            // 
            this.leftBodyTable.Controls.Add(this.tabPage1);
            this.leftBodyTable.Controls.Add(this.tabPage2);
            this.leftBodyTable.Controls.Add(this.tabPage3);
            this.leftBodyTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftBodyTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftBodyTable.Location = new System.Drawing.Point(0, 0);
            this.leftBodyTable.Name = "leftBodyTable";
            this.leftBodyTable.SelectedIndex = 0;
            this.leftBodyTable.Size = new System.Drawing.Size(740, 470);
            this.leftBodyTable.TabIndex = 4;
            this.leftBodyTable.Selected += new System.Windows.Forms.TabControlEventHandler(this.leftBodyTable_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.tableLayoutPanel3);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(732, 441);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Текущий заказ";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.rightBodyTabel, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.listView1, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 91.80723F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(726, 435);
            this.tableLayoutPanel3.TabIndex = 1;
            // 
            // rightBodyTabel
            // 
            this.rightBodyTabel.AllowDrop = true;
            this.rightBodyTabel.ColumnCount = 1;
            this.rightBodyTabel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.68545F));
            this.rightBodyTabel.Controls.Add(this.dishLayout, 0, 0);
            this.rightBodyTabel.Controls.Add(this.dishAddLayout, 0, 1);
            this.rightBodyTabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightBodyTabel.Location = new System.Drawing.Point(366, 3);
            this.rightBodyTabel.Name = "rightBodyTabel";
            this.rightBodyTabel.RowCount = 2;
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.97531F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.rightBodyTabel.Size = new System.Drawing.Size(357, 429);
            this.rightBodyTabel.TabIndex = 5;
            // 
            // dishLayout
            // 
            this.dishLayout.ColumnCount = 1;
            this.dishLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishLayout.Controls.Add(this.dishLayoutHeader, 0, 0);
            this.dishLayout.Controls.Add(this.receipeLayout, 0, 1);
            this.dishLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishLayout.Location = new System.Drawing.Point(3, 3);
            this.dishLayout.Name = "dishLayout";
            this.dishLayout.RowCount = 2;
            this.dishLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 112F));
            this.dishLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishLayout.Size = new System.Drawing.Size(351, 381);
            this.dishLayout.TabIndex = 1;
            // 
            // dishLayoutHeader
            // 
            this.dishLayoutHeader.ColumnCount = 2;
            this.dishLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.dishLayoutHeader.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishLayoutHeader.Controls.Add(this.dishPicture, 0, 0);
            this.dishLayoutHeader.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.dishLayoutHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishLayoutHeader.Location = new System.Drawing.Point(3, 3);
            this.dishLayoutHeader.Name = "dishLayoutHeader";
            this.dishLayoutHeader.RowCount = 1;
            this.dishLayoutHeader.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishLayoutHeader.Size = new System.Drawing.Size(345, 106);
            this.dishLayoutHeader.TabIndex = 0;
            // 
            // dishPicture
            // 
            this.dishPicture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishPicture.Image = ((System.Drawing.Image)(resources.GetObject("dishPicture.Image")));
            this.dishPicture.InitialImage = null;
            this.dishPicture.Location = new System.Drawing.Point(3, 3);
            this.dishPicture.Name = "dishPicture";
            this.dishPicture.Size = new System.Drawing.Size(100, 100);
            this.dishPicture.TabIndex = 0;
            this.dishPicture.TabStop = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.dishName, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.dishTypeLabel, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(109, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 81F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 19F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(233, 100);
            this.tableLayoutPanel2.TabIndex = 2;
            // 
            // dishName
            // 
            this.dishName.AutoSize = true;
            this.dishName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishName.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dishName.Location = new System.Drawing.Point(3, 0);
            this.dishName.Name = "dishName";
            this.dishName.Size = new System.Drawing.Size(227, 81);
            this.dishName.TabIndex = 1;
            this.dishName.Text = "Борщ по-пацански. Подавать с семками в подогретой барсетке";
            // 
            // dishTypeLabel
            // 
            this.dishTypeLabel.AutoSize = true;
            this.dishTypeLabel.Location = new System.Drawing.Point(3, 81);
            this.dishTypeLabel.Name = "dishTypeLabel";
            this.dishTypeLabel.Size = new System.Drawing.Size(66, 19);
            this.dishTypeLabel.TabIndex = 2;
            this.dishTypeLabel.Text = "Первое";
            // 
            // receipeLayout
            // 
            this.receipeLayout.ColumnCount = 1;
            this.receipeLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.receipeLayout.Controls.Add(this.label1, 0, 0);
            this.receipeLayout.Controls.Add(this.receipeText, 0, 1);
            this.receipeLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receipeLayout.Location = new System.Drawing.Point(3, 115);
            this.receipeLayout.Name = "receipeLayout";
            this.receipeLayout.RowCount = 2;
            this.receipeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 22F));
            this.receipeLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.receipeLayout.Size = new System.Drawing.Size(345, 263);
            this.receipeLayout.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Рецепт:";
            // 
            // receipeText
            // 
            this.receipeText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.receipeText.Location = new System.Drawing.Point(3, 25);
            this.receipeText.Name = "receipeText";
            this.receipeText.Size = new System.Drawing.Size(339, 235);
            this.receipeText.TabIndex = 1;
            this.receipeText.Text = resources.GetString("receipeText.Text");
            // 
            // dishAddLayout
            // 
            this.dishAddLayout.ColumnCount = 2;
            this.dishAddLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishAddLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 127F));
            this.dishAddLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.dishAddLayout.Controls.Add(this.readyDishesAmount, 0, 0);
            this.dishAddLayout.Controls.Add(this.readyButton, 1, 0);
            this.dishAddLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishAddLayout.Location = new System.Drawing.Point(3, 390);
            this.dishAddLayout.Name = "dishAddLayout";
            this.dishAddLayout.RowCount = 1;
            this.dishAddLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.dishAddLayout.Size = new System.Drawing.Size(351, 36);
            this.dishAddLayout.TabIndex = 2;
            // 
            // readyDishesAmount
            // 
            this.readyDishesAmount.Dock = System.Windows.Forms.DockStyle.Right;
            this.readyDishesAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readyDishesAmount.Location = new System.Drawing.Point(159, 3);
            this.readyDishesAmount.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.readyDishesAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.readyDishesAmount.Name = "readyDishesAmount";
            this.readyDishesAmount.Size = new System.Drawing.Size(62, 30);
            this.readyDishesAmount.TabIndex = 1;
            this.readyDishesAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.readyDishesAmount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.readyDishesAmount_KeyDown);
            // 
            // readyButton
            // 
            this.readyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.readyButton.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.readyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.readyButton.Location = new System.Drawing.Point(227, 3);
            this.readyButton.Name = "readyButton";
            this.readyButton.Size = new System.Drawing.Size(121, 30);
            this.readyButton.TabIndex = 0;
            this.readyButton.Text = "Готово";
            this.readyButton.UseVisualStyleBackColor = true;
            this.readyButton.Click += new System.EventHandler(this.readyButton_Click);
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView1.FullRowSelect = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView1.Location = new System.Drawing.Point(3, 3);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(357, 429);
            this.listView1.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.listView1_ItemSelectionChanged);
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Блюдо";
            this.columnHeader1.Width = 89;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Требуется";
            this.columnHeader2.Width = 85;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Приготовлено";
            this.columnHeader3.Width = 85;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Осталось";
            this.columnHeader4.Width = 85;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(732, 441);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Создание блюда";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.22222F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 55.55556F));
            this.tableLayoutPanel1.Controls.Add(this.storageFoodList, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dishFoodList, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 4.597701F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.4023F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(726, 435);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // storageFoodList
            // 
            this.storageFoodList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader6});
            this.storageFoodList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.storageFoodList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.storageFoodList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem4,
            listViewItem5});
            this.storageFoodList.LabelEdit = true;
            this.storageFoodList.Location = new System.Drawing.Point(3, 22);
            this.storageFoodList.Name = "storageFoodList";
            this.storageFoodList.Size = new System.Drawing.Size(155, 410);
            this.storageFoodList.TabIndex = 0;
            this.storageFoodList.UseCompatibleStateImageBehavior = false;
            this.storageFoodList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Продукт";
            this.columnHeader5.Width = 80;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "На складе";
            this.columnHeader6.Width = 69;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(164, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Состав блюда:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // dishFoodList
            // 
            this.dishFoodList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader8});
            this.dishFoodList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dishFoodList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dishFoodList.Location = new System.Drawing.Point(164, 22);
            this.dishFoodList.Name = "dishFoodList";
            this.dishFoodList.Size = new System.Drawing.Size(155, 410);
            this.dishFoodList.TabIndex = 1;
            this.dishFoodList.UseCompatibleStateImageBehavior = false;
            this.dishFoodList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Продукт";
            this.columnHeader7.Width = 69;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Количество";
            this.columnHeader8.Width = 77;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Доступные продукты:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // tabPage3
            // 
            this.tabPage3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(732, 441);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Создание меню";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 470);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(740, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(57, 17);
            this.toolStripStatusLabel.Text = "Error Text";
            this.toolStripStatusLabel.Visible = false;
            // 
            // ChiefForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 492);
            this.Controls.Add(this.leftBodyTable);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChiefForm";
            this.Text = "Шеф-повар";
            this.leftBodyTable.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.rightBodyTabel.ResumeLayout(false);
            this.dishLayout.ResumeLayout(false);
            this.dishLayoutHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dishPicture)).EndInit();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.receipeLayout.ResumeLayout(false);
            this.receipeLayout.PerformLayout();
            this.dishAddLayout.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.readyDishesAmount)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.StatusStrip statusStrip1;
        protected System.Windows.Forms.Button readyButton;
        protected System.Windows.Forms.TabControl leftBodyTable;
        protected System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.TableLayoutPanel rightBodyTabel;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.TableLayoutPanel dishLayout;
        private System.Windows.Forms.TableLayoutPanel dishLayoutHeader;
        private System.Windows.Forms.PictureBox dishPicture;
        private System.Windows.Forms.Label dishName;
        private System.Windows.Forms.TableLayoutPanel receipeLayout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel dishAddLayout;
        private System.Windows.Forms.NumericUpDown readyDishesAmount;
        private System.Windows.Forms.RichTextBox receipeText;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label dishTypeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListView storageFoodList;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView dishFoodList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
    }
}