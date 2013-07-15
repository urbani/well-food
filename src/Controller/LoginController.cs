using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TRPO.View;
using TRPO.Model;
using System.Windows.Forms;

namespace TRPO.Controller
{
    public class LoginController
    {
        private IAuthentification authView;

        //TODO: add IAuthentification into constructor
        public LoginController(IAuthentification i)
        {
            authView = i;
        }
        
        //обработчик  авторизации
        public void Login()
        {
            User user = new User(authView.getLogin(), authView.getPassword());
            try
            {
                user.authenticate();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Diagnostics.Debug.WriteLine("Original error: " + ex.ToString());
                authView.showMsg("Ошиба базы данных!", GlobalObj.ErrorLevels.Critical);
                return;
            }

            if (user.isAuthenticated())
            {
                switch (user.role)
                {
                    case (Roles.Administrator):
                        AdminForm af = new AdminForm();
                        af.Show();
                        break;
                    case (Roles.Chief):
                        ChiefForm chief = new ChiefForm();
                        chief.Show();
                        break;
                    case (Roles.Manager):
                        ManagerForm mf = new ManagerForm();
                        mf.Show();
                        break;
                    case (Roles.Courier):

                        ClientManagementConroller cmc = new ClientManagementConroller(user);
                        OrdersConroller oc = new OrdersConroller(user);
                        CourierForm couF = new CourierForm(cmc, oc);
                        couF.Show();
                        break;
                }
            }
            else
            {
                authView.showMsg("Неверный логин/пароль", GlobalObj.ErrorLevels.Info);
            }
        }
    }
}
