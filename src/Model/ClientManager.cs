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
        private DBConnector connector =null;
        private OleDbDataReader reader;
        public ClientManager()
        {
            connector = new DBConnector();
            
        }
        public Dictionary<int, String> getCompanies()
        {
            Dictionary<int, String> result = new Dictionary<int, string>();
            connector.openConnection();
            reader = connector.executeQuery("SELECT ID_Comp, Name_Comp FROM Company");
            while (reader.Read())
            {

                result.Add(Convert.ToInt32(reader[0]), reader[1].ToString());
            }
            connector.closeConnection();
            return result;
        }
    }
}
