using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ChrisHealthCare.Pages.Drivers.IndexModel;

namespace ChrisHealthCare.Pages.Drivers
{
    public class EditModel : PageModel
    {
        public DriverInfo driverInfo = new DriverInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String driverId = Request.Query["driverId"];
            try
            {
                String connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "SELECT Name, Licence, Email, Phone FROM Drivers WHERE driverId=@driverId";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                driverInfo.driverId = driverId;
                                driverInfo.Name = reader.GetString(0);
                                driverInfo.Licence = reader.GetString(1);
                                driverInfo.Email = reader.GetString(2);
                                driverInfo.Phone = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            driverInfo.driverId = Request.Form["driverId"];
            driverInfo.Name = Request.Form["Name"];
            driverInfo.Licence = Request.Form["Licence"];
            driverInfo.Email = Request.Form["Email"];
            driverInfo.Phone = Request.Form["Phone"];

            if (string.IsNullOrEmpty(driverInfo.driverId) || string.IsNullOrEmpty(driverInfo.Name) ||
                string.IsNullOrEmpty(driverInfo.Licence) || string.IsNullOrEmpty(driverInfo.Email) || string.IsNullOrEmpty(driverInfo.Phone))
            {
                errorMessage = "All fields are required.";
                return;
            }

            try
            {
                String connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "UPDATE Drivers SET Name=@Name, Licence=@Licence, Email=@Email, Phone=@Phone WHERE driverId=@driverId";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Name", driverInfo.Name);
                        cmd.Parameters.AddWithValue("@Licence", driverInfo.Licence);
                        cmd.Parameters.AddWithValue("@Email", driverInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", driverInfo.Phone);
                        cmd.Parameters.AddWithValue("@driverId", driverInfo.driverId);

                        cmd.ExecuteNonQuery();
                    }
                }
                successMessage = "Driver updated successfully.";
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
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
