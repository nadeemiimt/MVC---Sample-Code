namespace EPCommon.Contracts
{
    public interface IEmailSender
    {
        /// <summary>
        /// Used to send Emails
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        void SendMail(string destination, string subject, string body);
    }
}
