namespace WebAPI.Utils.Mail
{
    public interface IEmailService
    {
        //Metodo asincrono para envio de email que recebe MailRequest
        Task SendEmailAsync(MailRequest mailRequest);
    }
}
