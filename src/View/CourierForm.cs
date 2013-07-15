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


namespace TRPO.View
{
    public partial class CourierForm : Form, ICourierForm
    {
        CourierController controller;

        public CourierForm(CourierController c)
        {
            InitializeComponent();
            controller = c;
            controller.addForm(this);
        }

        public void showMsg(String msg, GlobalObj.ErrorLevels el)
        {
        }

    }
}
