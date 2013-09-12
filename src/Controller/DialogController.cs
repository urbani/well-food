using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;

namespace TRPO.Controller
{
    public class DialogController
    {
        public DialogController(){}
        ClientManagerDialog cmd;
        String name, surname, patronimic, companyName;


        public void addForm(View.ClientManagerDialog cmd)
        {
            this.cmd = cmd;
        }


    }
}
