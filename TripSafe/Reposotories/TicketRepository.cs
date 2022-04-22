using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TripSafe.Models;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace TripSafe.Reposotories
{
    public class TicketRepository
    {
        private string constr;

        public TicketRepository()
        {
            this.constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        }
        public Object searchBus(int start_terminal, int end_terminal, int arrivalTime, int vacancy)
        {
            List<Object> results = new List<Object>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = $"with route_bus as( select route.name as routeName,route.id as routeId,route.busId as busId,bus.name as busName,bus.rem_vacancy as remVacancy from route,bus  where bus.Id=route.busId) select route_bus.routeId, route_bus.routeName,route_bus.busId,route_bus.remVacancy,route_bus.busName,schedule.terminalId from route_bus,schedule where schedule.arrivalTime<={arrivalTime} and schedule.terminalId={start_terminal} and schedule.routeId=route_bus.routeId and route_bus.remVacancy>={vacancy} and schedule.routeId in (select S.routeId from schedule as S, schedule as T  where T.terminalId={start_terminal} and S.terminalId={end_terminal} and S.routeId=T.routeId and S.stoppageIndex>T.stoppageIndex);";
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
                                results.Add(new
                                {
                                    routeId = Convert.ToInt32(sdr["routeId"]),

                                    routeName = sdr["routeName"].ToString(),
                                    busId = Convert.ToInt32(sdr["busId"]),
                                    remVacancy = Convert.ToInt32(sdr["remVacancy"]),
                                    busName = sdr["busName"].ToString(),
                                    terminalId = Convert.ToInt32(sdr["terminalId"]),
                                });
                            }
                        }
                        con.Close();
                    }

                }
            }
            return results;
        }
   
         
    
    }
}