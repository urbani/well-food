using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TRPO.View
{
    public interface IAuthentification : IInterractable
    {
        String getLogin();
        String getPassword();
        void hideErrorText();
        void clearAuthData(); 
        void hideForm();
        void showForm();
    }
}
