using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using TRPO.Model;

namespace TRPO.Model
{
    public class ClientManager
    {
        private DBConnector connector;
        

        /// <summary>
        /// 
        /// </summary>
        public ClientManager()
        {
            connector = new DBConnector();
        }

        /// <summary>
        /// Забираем из БД компании и вставляет их в форму
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, String> getCompanies()
        {
            Dictionary<int, String> result = new Dictionary<int, string>();
            connector.openConnection();

            OleDbDataReader reader = connector.executeQuery("SELECT ID_Comp, Name_Comp FROM Company");
            while (reader.Read())
            {

                result.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
            }
            reader.Close();
            connector.closeConnection();
            return result;
        }

        /// <summary>
        /// запрос к БД на получение списка сотрудников текущей компании
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, String> getEmployers(int id)
        {
            Dictionary<int, String> result = new Dictionary<int, string>();
            connector.openConnection();
            OleDbDataReader reader = connector.executeQuery("SELECT e.ID_Emp, e.Surname, e.Name_Emp, e.Patronymic FROM Employee AS e WHERE e.Company=" + id.ToString());

            while (reader.Read())
            {
                result.Add(Convert.ToInt32(reader[0]), reader[1].ToString() + " " + reader[2].ToString() + " " + reader[3].ToString());
            }
            reader.Close();

            connector.closeConnection();

            return result;
        }

        public void updateEmployData(List<String> data, int emplId)
        {
            connector.openConnection();
            connector.executeNonQuery(String.Format("UPDATE Employee SET Surname={0}, Name_Emp={1}, Patronymic={2} WHERE ID_Emp={3}", data[1], data[2], data[3], emplId));
            connector.closeConnection();
        }

    }
}
