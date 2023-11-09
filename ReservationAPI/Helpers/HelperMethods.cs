using System.Net.Mail;

namespace ReservationAPI.Helpers
{
    public static class HelperMethods
    {
        public static IConfiguration GetConfiguration()
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new string[] { @"bin\" }, StringSplitOptions.None)[0];

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            return configuration;

        }

        public static bool IsValidEmail(this string emailaddress)
        {
            try
            {
                if (emailaddress.IsNullOrEmpty()) return false;
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}