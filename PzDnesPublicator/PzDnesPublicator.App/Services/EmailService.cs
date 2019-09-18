using OpenPop.Mime;
using OpenPop.Pop3;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PzDnesPublicator.App.Services
{
    public class EmailService
    {
        public async Task<MailMessage> GetEmail(string username, string password, string senderFilter)
        {
            using (Pop3Client client = new Pop3Client())
            {
                client.Connect("pop3.abv.bg", 995, true);
                client.Authenticate(username, password);
                int messageCount = client.GetMessageCount();
                List<MailMessage> allMessages = new List<MailMessage>(messageCount);

                for (int i = messageCount; i > 0; i--)
                {
                    var message = client.GetMessage(i).ToMailMessage();
                    if (message.From.Address.Contains(senderFilter))
                    {
                        return message;
                    }
                }
                return null;
            }


            //Pop3Client pop3Client = new Pop3Client();
            //await pop3Client.ConnectAsync("pop3.abv.bg", username, password, 995, true);
            //try
            //{
            //    var messageHeaders = await pop3Client.ListAndRetrieveHeaderAsync();
            //    var messages = await pop3Client.ListAndRetrieveAsync();
            //    var lastMessage = messages.Where(x => x.From.Contains(senderFilter)).OrderByDescending(x => x.Date).First();
            //    return lastMessage;
            //}
            //finally
            //{
            //    await pop3Client.DisconnectAsync();
          //}
        }
    }
}
