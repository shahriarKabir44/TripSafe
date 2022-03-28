using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripSafe.Models;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace TripSafe.Repositories
{
    public class TerminalRepository
    {
        private string constr;
        public TerminalRepository()
        {
            this.constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        }
        public List<Terminal> getTerminals()
        {
            List<Terminal> terminals = new List<Terminal>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT * FROM terminal";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    using (MySqlCommand newCommand = new MySqlCommand(query))
                    {
                        newCommand.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = newCommand.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                terminals.Add(new Terminal
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    name = sdr["name"].ToString(),
                                    districtId = Convert.ToInt32(sdr["districtId"]),
                                     
                                }); ;
                            }
                        }
                        con.Close();
                    }

                }
            }
            return terminals;
        }

    }
}