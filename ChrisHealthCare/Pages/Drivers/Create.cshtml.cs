using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using static ChrisHealthCare.Pages.Drivers.IndexModel;

namespace ChrisHealthCare.Pages.Drivers
{
    public class CreateModel : PageModel
    {
        public DriverInfo driverInfo = new DriverInfo();

        public String errorMessage = "";
        public String successMessage = "";


        public void OnGet()
        {
        }
        public void OnPost()
        {
            driverInfo.driverId = Request.Form["driverId"];
            driverInfo.Name = Request.Form["Name"];
            driverInfo.Licence = Request.Form["Licence"];
            driverInfo.Email = Request.Form["Email"];
            driverInfo.Phone = Request.Form["Phone"];

            if (driverInfo.driverId.Length == 0 || driverInfo.Name.Length == 0 ||
                driverInfo.Licence.Length == 0 || driverInfo.Email.Length == 0 
                || driverInfo.Phone.Length == 0)
            {
                errorMessage = "All fields are required";
                return;
            }
            // save data
            try
            {
                String connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    String sqlQuery = "INSERT INTO Drivers(driverId,Name,Licence,Email,Phone) VALUES (@driverId,@Name,@Licence,@Email,@Phone)";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@driverId", driverInfo.driverId);
                        cmd.Parameters.AddWithValue("@Name", driverInfo.Name);
                        cmd.Parameters.AddWithValue("@Licence", driverInfo.Licence);
                        cmd.Parameters.AddWithValue("@Email", driverInfo.Email);
                        cmd.Parameters.AddWithValue("@Phone", driverInfo.Phone);

                        cmd.ExecuteNonQuery();
                        successMessage = "New Driver Added successfully";
                    }
                }

            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            driverInfo.driverId = ""; driverInfo.Name= "";
            driverInfo.Licence = ""; driverInfo.Email = "";
            driverInfo.Phone = "";


            Response.Redirect("/Drivers/index");
        }


    }
}
