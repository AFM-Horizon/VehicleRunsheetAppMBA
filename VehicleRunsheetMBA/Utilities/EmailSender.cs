using System;
using System.IO;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;
using VehicleRunsheetMBAProj.Models;

namespace VehicleRunsheetMBAProj.Utilities
{
    //Work In Progress - Alex
    public static class EmailSender
    {
        //public static async Task SendEmail()
        //{
        //    var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        //    var client = new SendGridClient(apiKey);
        //    var from = new EmailAddress("speakerbugsound@gmail.com", "Alex");
        //    var subject = "Sending with SendGrid is Fun";
        //    var to = new EmailAddress("alex_musumeci@yahoo.com", "Myself");
        //    var plainTextContent = "and easy to do anywhere, even with C#";
        //    var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        //    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //    var response = await client.SendEmailAsync(msg);
        //}

        public static async Task SendEmail(Runsheet runsheet, string email)
        {
            var from = new EmailAddress("speakerbugsound@gmail.com", "Alex");
            var to = new EmailAddress(email, "Myself");

            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);

            var subject = $"Runsheet For {runsheet.Driver} {runsheet.Date.ToShortDateString()}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, " ", "<strong></strong>");

            var bytes = File.ReadAllBytes("wwwroot/CsvFiles/OutputPdf.pdf");
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment($"Runsheet{runsheet.Driver}.pdf", file);

            var response = await client.SendEmailAsync(msg);
        }
    }
}