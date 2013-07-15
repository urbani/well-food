namespace TRPO.View
{
    partial class AdminForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminForm));
            this.mainTable = new System.Windows.Forms.TableLayoutPanel();
            this.headerTable = new System.Windows.Forms.TableLayoutPanel();
            this.headerLabel1 = new System.Windows.Forms.Label();
            this.headerLabel2 = new System.Windows.Forms.Label();
            this.headerList1 = new System.Windows.Forms.ComboBox();
            this.headerList2 = new System.Windows.Forms.ComboBox();
            this.headerSearchButton1 = new System.Windows.Forms.Button();
            this.headerSearchButton2 = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rightBodyTabel = new System.Windows.Forms.TableLayoutPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.leftBodyTable = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainTable.SuspendLayout();
            this.headerTable.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.rightBodyTabel.SuspendLayout();
            this.leftBodyTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTable
            // 
            this.mainTable.ColumnCount = 1;
            this.mainTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTable.Controls.Add(this.headerTable, 0, 0);
            this.mainTable.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.mainTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTable.Location = new System.Drawing.Point(0, 0);
            this.mainTable.Name = "mainTable";
            this.mainTable.RowCount = 2;
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.13097F));
            this.mainTable.Size = new System.Drawing.Size(721, 462);
            this.mainTable.TabIndex = 0;
            // 
            // headerTable
            // 
            this.headerTable.ColumnCount = 4;
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 98F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 41.74434F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 44F));
            this.headerTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.25566F));
            this.headerTable.Controls.Add(this.headerLabel1, 0, 0);
            this.headerTable.Controls.Add(this.headerLabel2, 0, 1);
            this.headerTable.Controls.Add(this.headerList1, 1, 0);
            this.headerTable.Controls.Add(this.headerList2, 1, 1);
            this.headerTable.Controls.Add(this.headerSearchButton1, 2, 0);
            this.headerTable.Controls.Add(this.headerSearchButton2, 2, 1);
            this.headerTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.headerTable.Location = new System.Drawing.Point(3, 3);
            this.headerTable.Name = "headerTable";
            this.headerTable.RowCount = 2;
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headerTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.headerTable.Size = new System.Drawing.Size(715, 84);
            this.headerTable.TabIndex = 2;
            // 
            // headerLabel1
            // 
            this.headerLabel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.headerLabel1.AutoSize = true;
            this.headerLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerLabel1.Location = new System.Drawing.Point(44, 11);
            this.headerLabel1.Name = "headerLabel1";
            this.headerLabel1.Size = new System.Drawing.Size(51, 20);
            this.headerLabel1.TabIndex = 0;
            this.headerLabel1.Text = "label1";
            // 
            // headerLabel2
            // 
            this.headerLabel2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.headerLabel2.AutoSize = true;
            this.headerLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerLabel2.Location = new System.Drawing.Point(44, 53);
            this.headerLabel2.Name = "headerLabel2";
            this.headerLabel2.Size = new System.Drawing.Size(51, 20);
            this.headerLabel2.TabIndex = 1;
            this.headerLabel2.Text = "label2";
            // 
            // headerList1
            // 
            this.headerList1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.headerList1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerList1.FormattingEnabled = true;
            this.headerList1.Location = new System.Drawing.Point(101, 7);
            this.headerList1.Name = "headerList1";
            this.headerList1.Size = new System.Drawing.Size(233, 28);
            this.headerList1.TabIndex = 2;
            // 
            // headerList2
            // 
            this.headerList2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.headerList2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerList2.FormattingEnabled = true;
            this.headerList2.Location = new System.Drawing.Point(101, 49);
            this.headerList2.Name = "headerList2";
            this.headerList2.Size = new System.Drawing.Size(233, 28);
            this.headerList2.TabIndex = 3;
            // 
            // headerSearchButton1
            // 
            this.headerSearchButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.headerSearchButton1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerSearchButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerSearchButton1.Location = new System.Drawing.Point(340, 4);
            this.headerSearchButton1.Name = "headerSearchButton1";
            this.headerSearchButton1.Size = new System.Drawing.Size(37, 34);
            this.headerSearchButton1.TabIndex = 4;
            this.headerSearchButton1.Text = "b1";
            this.headerSearchButton1.UseVisualStyleBackColor = true;
            // 
            // headerSearchButton2
            // 
            this.headerSearchButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.headerSearchButton2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.headerSearchButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.headerSearchButton2.Location = new System.Drawing.Point(340, 45);
            this.headerSearchButton2.Name = "headerSearchButton2";
            this.headerSearchButton2.Size = new System.Drawing.Size(37, 35);
            this.headerSearchButton2.TabIndex = 5;
            this.headerSearchButton2.Text = "b2";
            this.headerSearchButton2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.rightBodyTabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.leftBodyTable, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 93);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(715, 366);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // rightBodyTabel
            // 
            this.rightBodyTabel.AllowDrop = true;
            this.rightBodyTabel.ColumnCount = 1;
            this.rightBodyTabel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 58.68545F));
            this.rightBodyTabel.Controls.Add(this.button1, 0, 1);
            this.rightBodyTabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rightBodyTabel.Location = new System.Drawing.Point(360, 3);
            this.rightBodyTabel.Name = "rightBodyTabel";
            this.rightBodyTabel.RowCount = 2;
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 41.97531F));
            this.rightBodyTabel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.rightBodyTabel.Size = new System.Drawing.Size(352, 360);
            this.rightBodyTabel.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.Location = new System.Drawing.Point(232, 327);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 30);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // leftBodyTable
            // 
            this.leftBodyTable.Controls.Add(this.tabPage1);
            this.leftBodyTable.Controls.Add(this.tabPage2);
            this.leftBodyTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.leftBodyTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.leftBodyTable.Location = new System.Drawing.Point(3, 3);
            this.leftBodyTable.Name = "leftBodyTable";
            this.leftBodyTable.SelectedIndex = 0;
            this.leftBodyTable.Size = new System.Drawing.Size(351, 360);
            this.leftBodyTable.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(343, 323);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 33);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(343, 323);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 462);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(721, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // AdminForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(721, 484);
            this.Controls.Add(this.mainTable);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AdminForm";
            this.Text = "Администратор";
            this.mainTable.ResumeLayout(false);
            this.headerTable.ResumeLayout(false);
            this.headerTable.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.rightBodyTabel.ResumeLayout(false);
            this.leftBodyTable.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.TableLayoutPanel mainTable;
        protected System.Windows.Forms.TableLayoutPanel headerTable;
        protected System.Windows.Forms.StatusStrip statusStrip1;
        protected System.Windows.Forms.Label headerLabel1;
        protected System.Windows.Forms.Label headerLabel2;
        protected System.Windows.Forms.ComboBox headerList1;
        protected System.Windows.Forms.ComboBox headerList2;
        protected System.Windows.Forms.Button headerSearchButton1;
        protected System.Windows.Forms.Button headerSearchButton2;
        protected System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        protected System.Windows.Forms.Button button1;
        protected System.Windows.Forms.TabControl leftBodyTable;
        protected System.Windows.Forms.TabPage tabPage1;
        protected System.Windows.Forms.TabPage tabPage2;
        protected System.Windows.Forms.TableLayoutPanel rightBodyTabel;
    }
}