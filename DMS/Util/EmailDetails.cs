
namespace Util
{
    public class EmailDetails
    {
        public string EmailFromDisplayName { get; set; }
        public string NewUserEmailSubject { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Port { get; set; }
        public string Server { get; set; }

        public bool IsHTMLResponse { get; set; }
        public bool IsSSLEnable { get; set; }
        public bool IsMailSend { get; set; }
        public bool EmailUseDefaultCredentials { get; set; }
        public string EmailHost { get; set; }
    }
}
