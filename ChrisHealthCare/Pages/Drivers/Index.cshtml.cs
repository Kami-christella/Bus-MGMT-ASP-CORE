using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Data.SqlClient;

namespace ChrisHealthCare.Pages.Drivers
{
    public class IndexModel : PageModel
    {
        public List<DriverInfo> ListDrivers = new List<DriverInfo>();

        public void OnGet()
        {
            ListDrivers.Clear();
            try
            {
                string conString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";

                using (SqlConnection con = new SqlConnection(conString))  // Correct instantiation
                {
                    con.Open();
                    string sqlQuery = "SELECT * FROM Drivers";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DriverInfo driverInfo = new DriverInfo
                                {
                                    driverId = reader.GetString(0),                // Convert int to string
                                    Name = reader.GetString(1),
                                    Licence = reader.GetString(2),
                                    Email = reader.GetString(3),
                                    Phone = reader.GetString(4)     
                                };

                                ListDrivers.Add(driverInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
        public class DriverInfo
        {
            public string driverId { get; set; }
            public string Name { get; set; }
            public string Licence { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
           
        }
    }
}
