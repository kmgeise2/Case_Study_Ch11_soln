using System.Linq;

namespace IC09_Ch11_SportsPro_rev.Models
{
    public static class Check
    {
        //Receive the database and the email string
        public static string EmailExists(SportsProContext context, string email)
        {
            string msg = "";
            if (!string.IsNullOrEmpty(email))
            {
                var customer = context.Customers.FirstOrDefault(
                    c => c.Email.ToLower() == email.ToLower());

                if (customer != null)
                    msg = "Email address already in use.";
            }
            return msg;
        }
    }
}
