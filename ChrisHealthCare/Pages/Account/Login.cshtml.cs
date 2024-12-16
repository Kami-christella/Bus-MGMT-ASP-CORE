using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ChrisHealthCare.Pages.Account
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "All fields are required.";
                return Page();
            }

            try
            {
                string connectionString = "Data Source=DESKTOP-2042M6B\\SQLEXPRESS;Initial Catalog=BusMNGMTSystem;Integrated Security=True;TrustServerCertificate=True";
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();
                    string sqlQuery = "SELECT Role FROM Users WHERE Username=@Username AND Password=@Password";
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, con))
                    {
                        cmd.Parameters.AddWithValue("@Username", Username);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            string role = reader.GetString(0);
                            // Authentication successful
                            // You can set session or cookies here based on your requirement
                            return RedirectToPage("/Drivers/Index");
                        }
                        else
                        {
                            // Authentication failed
                            ErrorMessage = "Invalid username or password";
                            return Page();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return Page();
            }
        }
    }
}
