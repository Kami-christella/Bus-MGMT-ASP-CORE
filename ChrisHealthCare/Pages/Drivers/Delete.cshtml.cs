using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ChrisHealthCare.Pages.Drivers
{
    public class DeleteModel : PageModel
    {
        public DriverInfo DriverInfo = new DriverInfo();
        public string ErrorMessage = "";
        public string SuccessMessage = "";

        public void OnGet()
        {
            string driverId = Request.Query["driverId"];
            try
            {
                string connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Name, Licence, Email, Phone FROM Drivers WHERE DriverId=@driverId";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DriverInfo.driverId = driverId;
                                DriverInfo.Name = reader.GetString(0);
                                DriverInfo.Licence = reader.GetString(1);
                                DriverInfo.Email = reader.GetString(2);
                                DriverInfo.Phone = reader.GetString(3);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
        }

        public IActionResult OnPost()
        {
            string driverId = Request.Form["DriverId"];
            try
            {
                string connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "DELETE FROM Drivers WHERE DriverId=@driverId";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverId);
                        cmd.ExecuteNonQuery();
                    }
                }
                SuccessMessage = "Driver deleted successfully.";
                return RedirectToPage("/Drivers/Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
