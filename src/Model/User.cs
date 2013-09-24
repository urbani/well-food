using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace TRPO.Model
{
    public class User
    {
        private int Id;
        private String Name;
        private String Surname;
        private String Patronymic;
        private Roles Role;
        private String LinkToPhoto;

        public String name { get { return Name; } }
        public String surname { get { return Surname; } }
        public String patronymic { get { return Patronymic; } }
        public int id { get { return Id; } }
        public Roles role { get { return Role; } }
        public String linkToPhoto;

        

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
                    Id = Convert.ToInt32(reader[0]);
                    Name = reader[1].ToString();
                    Surname = reader[2].ToString();
                    Patronymic = reader[3].ToString();
                    LinkToPhoto = reader[4].ToString();
                    switch(reader[5].ToString())
                    {
                        case ("Administrator"):
                            Role = Roles.Administrator;
                            break;
                        case ("Chief"):
                            Role = Roles.Chief;
                            break;
                        case ("Manager"):
                            Role = Roles.Manager;
                            break;
                        case ("Courier"):
                            Role = Roles.Courier;
                            break;
                    }

                   // Console.WriteLine("" + id + " " + name + " " + surname + " " + patronymic + " " + linkToPhoto + " " + role);
                }
                reader.Close();
            } else
            {
                Id = -1;
                Name = null;
                Surname = null;
                Patronymic = null;
                Role = Roles.Unnown;
                LinkToPhoto = null;
            }
        }

        public bool isAuthenticated()
        {
            return authenticated;
        }

    }
}
