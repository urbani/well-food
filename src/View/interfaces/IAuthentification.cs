using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IAuthentification
    {
        String getLogin();
        String getPassword();
        void showErrorText(String s);
        void hideErrorText();
        void clearAuthData();
    }
}
