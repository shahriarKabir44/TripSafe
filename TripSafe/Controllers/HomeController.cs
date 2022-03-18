using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using TripSafe.Models;

namespace TripSafe.Controllers
{
    public class HomeController : Controller
    {
        public Object Index()
        {
            string constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            List<District> districts = new List<District>();

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "select * from district";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            districts.Add(new District
                            {
                                Id = Convert.ToInt32(sdr["Id"]),
                                name = sdr["name"].ToString(),
                                 
                            });
                        }
                    }
                    con.Close();
                }
            }
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public object insertDistrict()
        {
            string constr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
            List<District> districts = new List<District>();

            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "insert into district(name) values(?1)";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    
                    cmd.Parameters.AddWithValue("?1", "Ramgamati");
                    con.Open();
                    int res=cmd.ExecuteNonQuery();
                    con.Close();
                    using(MySqlCommand newCommand= new MySqlCommand("select * from district  where district.Id=(select max(Id) from district)")) {
                        newCommand.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = newCommand.ExecuteReader())
                        {
                            while (sdr.Read())
                            {
                                districts.Add(new District
                                {
                                    Id = Convert.ToInt32(sdr["Id"]),
                                    name = sdr["name"].ToString(),

                                });
                            }
                        }
                        con.Close();
                    }
                }
            }
            return Json(districts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeLogin()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}