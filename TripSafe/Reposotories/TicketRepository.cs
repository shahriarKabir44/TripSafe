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
                string query = $@"with route_bus as
                             ( select route.name as routeName,
                             route.id as routeId,
                            route.busId as busId,
                            bus.name as busName,
                            bus.rem_vacancy as remVacancy 
                            from route,bus  
                            where bus.Id=route.busId) 
                            select route_bus.routeId,
                            route_bus.routeName,route_bus.busId,
                            route_bus.remVacancy,route_bus.busName,schedule.terminalId
                            from route_bus,schedule 
                            where schedule.arrivalTime<={arrivalTime} and 
                            schedule.terminalId={start_terminal} and 
                            schedule.routeId=route_bus.routeId and 
                            route_bus.remVacancy>={vacancy} and 
                            schedule.routeId in 
                            (select S.routeId from schedule as S, 
                            schedule as T  where T.terminalId={start_terminal}
                            and S.terminalId={end_terminal} 
                            and S.routeId=T.routeId 
                            and S.stoppageIndex>T.stoppageIndex);";
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

        public Object getDailyBoardingUnboardingInfo(int date)
        {
            List<Object> data = new List<object>();

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = $@"with ticket_time as(
	                                select ticket.Id,ticket. passengerCount,ticket. startTerminal,ticket. endTerminal,ticket. passengerId,ticket. tripId,
                                    trip.date as tripDate from trip, ticket 
                                    where ticket.tripId=trip.Id and trip.date={date}
                                ),
                                boardCount as(
	                                select terminal.Id, terminal.name, terminal.districtId,
                                (select name from district where district.Id=terminal.districtId) as districtName,
                                    (case 
		                                when (select sum(passengerCount) from ticket_time where terminal.Id=ticket_time.startTerminal) is null then 0
                                        else (select sum(passengerCount) from ticket_time where terminal.Id=ticket_time.startTerminal)
	                                end
                                    ) as boardCnt,
                                    (
                                    case 
		                                when (select sum(passengerCount)  from ticket_time where terminal.Id=ticket_time.endTerminal) is null then 0
                                        else (select sum(passengerCount) from ticket_time where terminal.Id=ticket_time.endTerminal)
	                                end
    
                                    ) as unBoardCnt
    
    
	                                from terminal
                                ) 
                                select * from boardCount;";

                using (MySqlCommand newCommand = new MySqlCommand(query))
                {
                    newCommand.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = newCommand.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            data.Add(new
                            {


                                Id = Convert.ToInt32(sdr["Id"]),
                                name = sdr["name"].ToString(),
                                districtId = Convert.ToInt32(sdr["districtId"]),
                                districtName = sdr["districtName"].ToString(),
                                boardCnt = Convert.ToInt32(sdr["boardCnt"]),
                                unBoardCnt = Convert.ToInt32(sdr["unBoardCnt"])
                            });
                        }
                    }
                    con.Close();
                }


            }


            return data;
        }
    }