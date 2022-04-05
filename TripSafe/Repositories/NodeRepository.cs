using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TripSafe.Models;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace TripSafe.Repositories
{
    public class NodeRepository
    {
        private string constr;
        public NodeRepository()
        {
            this.constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

        }
        public void insert(Node newNode)
        {
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO node (routeId,terminalId,stoppageIndex)VALUES(?1,?2,?3);";

                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;

                    cmd.Parameters.AddWithValue("?1", newNode.routeId);

                    cmd.Parameters.AddWithValue("?2", newNode.terminalId);
                    cmd.Parameters.AddWithValue("?3", newNode.stoppageIndex);
                    con.Open();
                    int res = cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        public List<Object> getNodes(int routeId)
        {
            List<Object> nodes = new List<Object>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = $"select routeId, terminalId,stoppageIndex, (select name from terminal where terminal.Id= terminalid) as terminalName from node where routeId= {routeId}  order by stoppageIndex;";
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
                                nodes.Add(new
                                {
                                    Id = Convert.ToInt32(sdr["routeId"]),
                                    terminalId = Convert.ToInt32(sdr["terminalId"]),
                                    stoppageIndex = Convert.ToInt32(sdr["stoppageIndex"]),

                                    terminalName = sdr["terminalName"].ToString(),
                                     
                                }); ;
                            }
                        }
                        con.Close();
                    }

                }
            }
            return nodes;
        }
    }
}