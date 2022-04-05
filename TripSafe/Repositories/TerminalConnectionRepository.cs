using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripSafe.Models;
using System.Configuration;
using MySql.Data.MySqlClient;
namespace TripSafe.Repositories
{
    public class TerminalConnectionRepository
    {
        private string constr;
        public TerminalConnectionRepository()
        {
            this.constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        }
        public void create(TerminalConnection connection)
        {
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO terminal_connection(terminal1,terminal2,roadId) VALUES (?1,?2,?3);";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("?1", connection.terminal1);
                    cmd.Parameters.AddWithValue("?2", connection.terminal2);
                    cmd.Parameters.AddWithValue("?3", connection.roadId);
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}