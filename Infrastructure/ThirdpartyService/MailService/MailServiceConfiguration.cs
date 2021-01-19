using Domain.Models.DTO;
using Microsoft.Extensions.Configuration;
using System;

namespace Infrastructure.ThirdpartyService.MailService
{
    public class MailServiceConfiguration
    {
        private static MailServiceConfiguration _instance = null;

        public string SMTPSERVER;
        public string PORT;
        public string USERNAME;
        public string PASSWORD;
        public string EMAILTEMPLATE;
        public string REPLYTO;

        public const string EmailSmtpServer = "EmailConfiguration:SmtpServer";
        public const string EmailSmtpPort = "EmailConfiguration:SmtpPort";
        public const string EmailSmtpUserName = "EmailConfiguration:SmtpUsername";
        public const string EmailSmtpPassword = "EmailConfiguration:SmtpPassword";
        public const string EmailTemplate = "Data\\Templates\\EmailTemplate.html";
        public const string EmailReplyTo = "EmailConfiguration:EmailNoReply";

        public static MailServiceConfiguration Instance
        {
            get
            {
                if (_instance == null)
                    throw new Exception("Email Service Settings not initialized. Load settings before usage.");

                return _instance;
            }
        }

        private MailServiceConfiguration(IConfiguration configuration)
        {
            InitMailServiceConfiguration(configuration);
        }

        private void InitMailServiceConfiguration(IConfiguration configuration)
        {
            SMTPSERVER = configuration[EmailSmtpServer] ?? EmailSmtpServer;
            PORT = configuration[EmailSmtpPort] ?? EmailSmtpPort;
            USERNAME = configuration[EmailSmtpUserName] ?? EmailSmtpUserName;
            PASSWORD = configuration[EmailSmtpPassword] ?? EmailSmtpPassword;
            REPLYTO = configuration[EmailReplyTo] ?? EmailReplyTo;
            EMAILTEMPLATE = EmailTemplate;
        }

        public static void LoadMailServiceSettings(IConfiguration configuration)
        {
            _instance = new MailServiceConfiguration(configuration);
        }

        public static void EmailBodyReplacements(EmailDTO emailDTO, string title)
        {
            emailDTO.Content = emailDTO.Content.Replace("#UserName", emailDTO.Username);
            emailDTO.Content = emailDTO.Content.Replace("#Action", title);
        }
    }
}
