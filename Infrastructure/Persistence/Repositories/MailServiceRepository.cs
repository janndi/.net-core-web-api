using Domain.Models.DTO;
using Infrastructure.Persistence.Interface;
using Infrastructure.ThirdpartyService.MailService;
using MimeKit;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class MailServiceRepository : IMailServiceRepository
    {
        private readonly HttpClient _httpClient;

        public MailServiceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> SendAsync(EmailDTO emailDTO)
        {
            try
            {
                var smtpServer = MailServiceConfiguration.Instance.SMTPSERVER;
                var smtpPort = MailServiceConfiguration.Instance.PORT;
                var smtpUsername = MailServiceConfiguration.Instance.USERNAME;
                var smtpPassword = MailServiceConfiguration.Instance.PASSWORD;
                emailDTO.Template = Path.Combine(Directory.GetCurrentDirectory(), "EmailTemplates/UserCreationTemplate.html");

                if (string.IsNullOrEmpty(emailDTO.To)) return false;

                var mail = new MailMessage();

                mail.To.Add(new MailAddress(emailDTO.To, ""));
                mail.From = new MailAddress(smtpUsername, "");
                mail.Subject = emailDTO.Subject;
                mail.ReplyToList.Add(new MailAddress(MailServiceConfiguration.Instance.REPLYTO, "noreply"));
                mail.IsBodyHtml = true;


                using (StreamReader sourceReader = File.OpenText(emailDTO.Template))
                {
                    emailDTO.Content = sourceReader.ReadToEnd();
                }

                MailServiceConfiguration.EmailBodyReplacements(emailDTO, emailDTO.Subject);

                mail.Body = emailDTO.Content;

                using (var client = new SmtpClient(smtpServer, Convert.ToInt32(smtpPort)))
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                    
                    await client.SendMailAsync(mail);
                    client.Dispose();

                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
