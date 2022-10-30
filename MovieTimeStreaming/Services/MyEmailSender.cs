
using System.Net.Mail;
using System.Net;
namespace MovieTimeStreaming.Services;


public class MyEmailSender
{
    private static string recepiant { get; set; }
    private static string subject { get; set; }
    private static string body { get; set; }
    public MyEmailSender(string to, string Subject, string Body)
    {
        recepiant = to;
        subject = Subject;
        body = Body;
    }

    public static void SendEmail()
    {
        var client = new SmtpClient("smtp.mailtrap.io",2525)
        {
            Credentials = new NetworkCredential("15f40ac89aab5a","a84122b19d4d79"),
            EnableSsl = true
        };
        client.Send("MovieTime@example.com", recepiant,subject,body);
        Console.WriteLine("Sent");
        Console.ReadLine();
    }
    
}