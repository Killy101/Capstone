using Capstone_Csite.Models;
using MySql.Data.MySqlClient;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MySqlCommand = MySql.Data.MySqlClient.MySqlCommand;
using MySqlConnection = MySql.Data.MySqlClient.MySqlConnection;

namespace Capstone_Csite.Controllers
{
    public class HomeController : Controller
    {
     
        string connectionString = "server=localhost;user id=root;database=databasecsite";

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult OwnerRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OwnerRegister(SignUp Models)
        {

            using (var connection = new MySqlConnection(connectionString))
            {
                string usertype = "Owner";
                string query = "INSERT INTO `users`(`firstName`, `lastName`, `phone`, `age`, `email`, `username`, `password`, `usertype`) " +
                "VALUES (@firstname, @lastname, @phone, @age, @email, @username, @password, @usertype)";
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@firstname", Models.fName);
                command.Parameters.AddWithValue("@lastname", Models.lName);
                command.Parameters.AddWithValue("@phone", Models.phone);
                command.Parameters.AddWithValue("@age", Models.age);
                command.Parameters.AddWithValue("@email", Models.email);
                command.Parameters.AddWithValue("@username", Models.username);
                command.Parameters.AddWithValue("@password", Models.password);
                command.Parameters.AddWithValue("@usertype", usertype);

                connection.Open();
                command.ExecuteNonQuery();
            }
            
            return RedirectToAction("CampInfo");
        }

        public ActionResult CampInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CampInfo(ImageFile objImage, ImageFile Models)
        {
            foreach (var file in objImage.files)
            {
                if (file != null && file.ContentLength > 0)
                {
                    file.SaveAs(Path.Combine(Server.MapPath("/Uploads"), Guid.NewGuid() + Path.GetExtension(file.FileName)));
                }
            }
            using (var connection = new MySqlConnection(connectionString))
            {

                string query = "INSERT INTO `campinfo`(`CampName`, `CAmp_Location`,`Camp_Photo`, `Camp_Desc`) " +
                    "VALUES (@name, @location, @img,@desc)";
                var command = new MySqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", Models.name);
                command.Parameters.AddWithValue("@location", Models.location);
                command.Parameters.AddWithValue("@img", objImage.files);
                command.Parameters.AddWithValue("@desc", Models.desc);


                connection.Open();
                command.ExecuteNonQuery();
            }

            return RedirectToAction("Login");

            //foreach (var file in objImage.files)
            //{
            //    string filename = Path.GetFileName(file.FileName);
            //    string contentType = file.ContentType;
            //    using (Stream fs = file.InputStream)
            //    {
            //        using (BinaryReader br = new BinaryReader(fs))
            //        {
            //            byte[] bytes = br.ReadBytes((Int32)fs.Length);

            //            using (var connection = new MySqlConnection(connectionString))
            //            {
            //                 string query = "INSERT INTO `campinfo`(`CampName`, `CAmp_Location`,`Camp_Photo`, `Camp_Desc`) " +
            //    "VALUES (@name, @location, @img,@desc)";
            //                var command = new MySqlCommand(query, connection);

            //                command.Parameters.AddWithValue("@name", Models.name);
            //                command.Parameters.AddWithValue("@location", Models.location);
            //                command.Parameters.AddWithValue("@img", objImage.files);
            //                command.Parameters.AddWithValue("@desc", Models.desc);

            //                connection.Open();
            //                command.ExecuteNonQuery();
            //                connection.Close();
            //                }
            //            }
            //        }
            //    }

            //return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login models)
        {

            //if statement para error either user or pass(KUWANG)
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT password FROM users WHERE username = @Username";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", models.username);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var storedPassword = reader.GetString("Password");
                                if (VerifyPassword(models.password, storedPassword))
                                {
                                    // Successful login
                                    //string userType = GetUserType(username);
                                    //Session["email"] = userType;
                                 

                                    // You can create a session or authentication token here to maintain the user's login state
                                    return RedirectToAction("Index", "Home");
                                }

                            }
                        }
                    }
                }
            }
            // Invalid login
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(models);
        }
        private bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            // Implement your password verification logic (e.g., using bcrypt or other secure hashing algorithm)
            // Return true if the entered password matches the stored password
            return enteredPassword == storedPassword;
            // You can use BCrypt.Net.BCrypt.Verify() or any other library for password verification

        }

        public ActionResult LoginEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginEmail(EmailLogin models)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT password FROM users WHERE email = @email";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", models.email);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var storedPassword = reader.GetString("Password");
                                if (VerifyPassword(models.password, storedPassword))
                                {
                                    // Successful login
                                    //string userType = GetUserType(username);
                                    //Session["email"] = userType;


                                    // You can create a session or authentication token here to maintain the user's login state
                                    return RedirectToAction("Index", "Home");
                                }

                            }
                        }
                    }
                }
            }
            // Invalid login
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(models);
        }

        /*--------------------------------------------------SAMPLE -----------------------------------------------------*/
        /*--------------------------------------------------SAMPLE -----------------------------------------------------*/
        public ActionResult Samp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Samp(ImageFile objImage, ImageFile Models)
        {
            return View();
        }

        /*--------------------------------------------------ADMIN -----------------------------------------------------*/
        /*--------------------------------------------------ADMIN -----------------------------------------------------*/

        public ActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AdminLogin(Login models)
        {
            if (ModelState.IsValid)
            {
                using (var connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    var query = "SELECT password FROM admin WHERE username = @Username";
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", models.username);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var storedPassword = reader.GetString("Password");
                                if (VerifyPassword(models.password, storedPassword))
                                {
                                    // Successful login
                                    //string userType = GetUserType(username);
                                    //Session["email"] = userType;

                                    // You can create a session or authentication token here to maintain the user's login state
                                    return RedirectToAction("Index", "Home");
                                }
                            }
                        }
                    }
                }
            }
            // Invalid login
            ModelState.AddModelError(string.Empty, "Invalid username or password.");
            return View(models);
        }
    
    }
}

