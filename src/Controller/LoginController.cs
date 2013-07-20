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

        private void showAuthForm(object sender, FormClosedEventArgs e)
        {
            authView.showForm();
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
                authView.showMsg("Ошибка базы данных!", "Ошибка БД");
                return;
            }

            if (user.isAuthenticated())
            {
                switch (user.role)
                {
                    case (Roles.Administrator):
                        AdminForm af = new AdminForm();
                        af.FormClosed += new FormClosedEventHandler(showAuthForm);
                        af.Show();
                        authView.hideForm();
                        break;
                    case (Roles.Chief):
                        ChiefForm chief = new ChiefForm();
                        chief.FormClosed += new FormClosedEventHandler(showAuthForm);
                        chief.Show();
                        authView.hideForm();
                        break;
                    case (Roles.Manager):
                        ManagerForm mf = new ManagerForm();
                        mf.FormClosed += new FormClosedEventHandler(showAuthForm);
                        mf.Show();
                        authView.hideForm();
                        break;
                    case (Roles.Courier):

                        ClientManagementConroller cmc = new ClientManagementConroller(user);
                        OrdersConroller oc = new OrdersConroller(user);
                        CourierForm cf = new CourierForm(cmc, oc);
                        cf.FormClosed += new FormClosedEventHandler(showAuthForm);
                        cf.Show();
                        authView.hideForm();
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
