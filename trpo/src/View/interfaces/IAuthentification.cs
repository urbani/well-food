using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace trpo.src.View.interfaces
{
    interface IAuthentification
    {
        void showError(String msg);
        void showMsg(String msg);
        String getLogin();
        String getPassword();
        void clearAuthData();
    }
}
