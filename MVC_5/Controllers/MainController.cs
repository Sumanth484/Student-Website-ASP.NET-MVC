using MVC_5.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_5.Controllers
{
    public class MainController : Controller
    {

        string ConnectionString = ConfigurationManager.ConnectionStrings["DatabaseConnectionString"].ConnectionString;
        // GET: Main
        public ActionResult Index(int? page)
        {
            List<Registration> reg = new List<Registration>();
            int totalpages = 0;
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_getstudents", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@page", page==null?1:page);

                        SqlDataReader reader = command.ExecuteReader();
                        
                            
                            while (reader.Read())
                            {
                                Registration registration = new Registration();
                                registration.id = (int)reader["id"];
                                registration.name = (string)reader["name"];
                                registration.gender = (string)reader["gender"];
                                registration.age = (int)reader["age"];
                                registration.Section = (string)reader["section"];
                                totalpages = (int)reader["totalpage"];
                                reg.Add(registration);
                            }
                        
                        
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = "DataBase Error please contact Admin";

            }
            ViewData["message"] = TempData["message"];
            ViewBag.totalpages = totalpages;
            ViewBag.currentpage = page==null ? 1 : page;
            return View(reg);
        }

        public ActionResult Create()
        {
            List<SelectListItem> citieslist = new List<SelectListItem>();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("sp_getcities", connection))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        
                        using (SqlDataReader reader = command.ExecuteReader())
                        {


                            while (reader.Read())
                            {
                                SelectListItem item = new SelectListItem();
                                item.Value =Convert.ToString(reader["value"]);
                                item.Text =Convert.ToString(reader["text"]);
                                citieslist.Add(item);
                            }
                        }

                    }
                }


            }
            catch(Exception ex)
            {
                TempData["message"] = "DataBase Error while loading data to cities";
            }


            ViewData["message"] = TempData["message"];
            ViewData["city"] = citieslist;
            var model = new Registration();
            return View(model);
        }


        [HttpPost]
        public ActionResult Create(Registration Reg)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_savestudents", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", Reg.name);
                        cmd.Parameters.AddWithValue("@gender", Reg.gender);
                        cmd.Parameters.AddWithValue("@age", Reg.age);
                        cmd.Parameters.AddWithValue("@id", 0);
                        cmd.Parameters.AddWithValue("@section", Reg.Section);
                        cmd.Parameters.AddWithValue("@city", Reg.city);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        TempData["message"] = "Data saved successfully";
                        
                    }

                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "DataBase Error please contact Admin";
            }
            ViewData["message"] = TempData["message"];
            return RedirectToAction("Index", "Main");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {

            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                Registration registration = new Registration();
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("sp_getstudentbyid", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {

                                
                                while (reader.Read())
                                {
                                    registration.id = (int)reader["id"];
                                    registration.name = (string)reader["name"];
                                    registration.gender = (string)reader["gender"];
                                    registration.age = (int)reader["age"];
                                    registration.Section = (string)reader["section"];
                                    registration.city = reader["city"].ToString();

                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = "Id not found in DataBase please contact Admin";

                }
                ViewData["message"] = TempData["message"];
                return View(registration);
            }
            


        }
        [HttpPost]
        public ActionResult Edit(Registration Reg)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("sp_savestudents", con))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", Reg.name);
                        cmd.Parameters.AddWithValue("@gender", Reg.gender);
                        cmd.Parameters.AddWithValue("@age", Reg.age);
                        cmd.Parameters.AddWithValue("@id", Reg.id);
                        cmd.Parameters.AddWithValue("@section", Reg.Section);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        TempData["message"] = "Data Updated successfully";
                        
                    }

                }

            }
            catch (Exception ex)
            {
                TempData["message"] = "Error occured while updating please contact Admin";
            }
            ViewData["message"] = TempData["message"];
            return RedirectToAction("Index", "Main");
        }

        public ActionResult Details(int id) 
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                Registration registration = new Registration();
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("sp_getstudentbyid", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {


                                while (reader.Read())
                                {
                                    registration.id = (int)reader["id"];
                                    registration.name = (string)reader["name"];
                                    registration.gender = (string)reader["gender"];
                                    registration.age = (int)reader["age"];
                                    registration.Section = (string)reader["section"];

                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = "DataBase Error please contact Admin";

                }
                ViewData["message"] = TempData["message"];
                return View(registration);
            }

        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                Registration registration = new Registration();
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("sp_getstudentbyid", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id", id);
                            using (SqlDataReader reader = command.ExecuteReader())
                            {


                                while (reader.Read())
                                {
                                    registration.id = (int)reader["id"];
                                    registration.name = (string)reader["name"];
                                    registration.gender = (string)reader["gender"];
                                    registration.age = (int)reader["age"];
                                    registration.Section = (string)reader["section"];

                                }
                            }

                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = "DataBase Error please contact Admin";   

                }
                ViewData["message"] = TempData["message"];
                return View(registration);
            }

        }

        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmation(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                
                try
                {
                    using (SqlConnection connection = new SqlConnection(ConnectionString))
                    {
                        connection.Open();
                        using (SqlCommand command = new SqlCommand("sp_deletebyid", connection))
                        {
                            command.CommandType = System.Data.CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@id", id);
                            command.ExecuteNonQuery();
                            connection.Close();
                            TempData["message"] = "Record deleted Successfully";
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["message"] = "DataBase Error please contact Admin";

                }
                ViewData["message"] = TempData["message"];
                return RedirectToAction("Index", "Main");

            }

        }


    }
}