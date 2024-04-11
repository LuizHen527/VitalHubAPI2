
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WebAPI.Utils.Mail
{
    public class EmailService : IEmailService
    {
        //Variavel privada com as configs do email
        private readonly EmailSettings emailSettings;
        public EmailService(IOptions<EmailSettings> options) 
        {
            //obtem as configs do email e armazena na variavel privada
            emailSettings = options.Value;
        }
        public async Task SendEmailAsync(MailRequest mailRequest)
        {
            try
            {
                //Objeto que representa o email
                var email = new MimeMessage();

                //define o remetente do email
                email.Sender = MailboxAddress.Parse(emailSettings.Email);

                //adiciona um destinatario do email
                email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));

                //define o assunto do email
                email.Subject = mailRequest.Subject;

                //cria o corpo do email
                var builder = new BodyBuilder();

                //Define o corpo do email como html
                builder.HtmlBody = mailRequest.Body;

                //Defineo corpo do email no obj MimeMessage
                email.Body = builder.ToMessageBody();

                //Cria um cliente SMTP para envio de email
                using(var smtp = new SmtpClient())
                {
                    //Conecta ao servidor SMTP usando os dados do emailSettings
                    smtp.Connect(emailSettings.Host, emailSettings.Port, SecureSocketOptions.StartTls);

                    //Autentica-se no servidor SMTP usando os dados do emailSettings
                    smtp.Authenticate(emailSettings.Email, emailSettings.Password);

                    //Envia o email de forma assincrona
                    await smtp.SendAsync(email);
                }
            }
            catch 
            { 
            
            }
        }
    }
}
