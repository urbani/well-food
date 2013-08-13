using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace TRPO.Model
{
    public class User
    {
        private int id;
        private String name;
        private String surname;
        private String patronymic;
        public Roles role;

        private String linkToPhoto;

        private bool authenticated;

        private String login;
        private String password;

        public User(String l, String p)
        {
            login = l;
            password = p;
            authenticated = false;
            updateInformation();
        }

        //Авториует пользователя и вызывает метод updateInformation(); для заполнения данных в случае успешной апвотризации
        public bool authenticate()
        {

            DBConnector connector = new DBConnector();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT Password FROM Users WHERE Login = \"" + login + "\"");
            if (reader.Read())
            {
                authenticated = password.Equals(reader[0].ToString());//true если пароль в базе такой же как введенный пользователем пароль
            }
            reader.Close();
            connector.closeConnection();

            updateInformation();

            return authenticated;
        }

        //Заполнение данных пользователя данными из базы ID, Имя, Фамилия, Отчество, путь к фотографии, роль  
        private void updateInformation()
        {
            if (authenticated)
            {
                DBConnector connector = new DBConnector();
                connector.openConnection();
                //OleDbDataReader reader = connector.executeQuery("SELECT * FROM Users WHERE Login = \"" + login + "\"");
                OleDbDataReader reader = connector.executeQuery("SELECT u.ID_Us, u.Name, u.Surname, u.Patronymic, u.Link_To_Photo, r.Role FROM Users u INNER JOIN Roles r ON r.ID_R = u.Role WHERE u.Login = \"" + login + "\"");
                
                while (reader.Read())
                {
                    id = Convert.ToInt32(reader[0]);
                    name = reader[1].ToString();
                    surname = reader[2].ToString();
                    patronymic = reader[3].ToString();
                    linkToPhoto = reader[4].ToString();
                    switch(reader[5].ToString())
                    {
                        case ("Administrator"):
                            role = Roles.Administrator;
                            break;
                        case ("Chief"):
                            role = Roles.Chief;
                            break;
                        case ("Manager"):
                            role = Roles.Manager;
                            break;
                        case ("Courier"):
                            role = Roles.Courier;
                            break;
                    }

                   // Console.WriteLine("" + id + " " + name + " " + surname + " " + patronymic + " " + linkToPhoto + " " + role);
                }
                reader.Close();
            } else
            {
                id = -1;
                name = null;
                surname = null;
                patronymic = null;
                role = Roles.Unnown;
                linkToPhoto = null;
            }
        }

        public bool isAuthenticated()
        {
            return authenticated;
        }

    }
}
