using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using trpo.View;
using trpo.Model;

namespace trpo.Controller
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
            user.authenticate();
            if (user.isAuthenticated())
            {
                switch (user.role)
                {
                    case (Roles.Administrator):
                        AdminForm af = new AdminForm();//Как передать в форму User-a?
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
                        CourierForm couF = new CourierForm();
                        couF.Show();
                        break;
                }
            }
            else
            {
                authView.showErrorText("Неверный логин/пароль");
            }
        }
    }
}
