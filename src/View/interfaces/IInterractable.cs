using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TRPO.View
{
    public interface IInterractable
    {
        void showMsg(String msg, GlobalObj.ErrorLevels levels);
        void showMsg(String msg, String header);
    }
}
