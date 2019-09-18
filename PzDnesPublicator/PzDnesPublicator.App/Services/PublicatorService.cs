using PzDnesPublicator.App.DTOs;
using Spire.Doc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PzDnesPublicator.App.Services
{
    public class PublicatorService
    {
        private readonly EmailService _emailService;

        public PublicatorService(EmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task<NewsDTO> GetNews(CredentialsDTO credentials)
        {
            string text = GetContentFromEmail(credentials.EmailUsername, credentials.EmailPassword, credentials.EmailSenderFilter);
            NewsDTO news = GetNews(text);
            return news;
        }

        private static NewsDTO GetNews(string text)
        {
            var articles = text
                .Split(
                    new string[] { "\n", Environment.NewLine },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select((x, i) => new ArticleDTO
                {
                    Title = string.Empty,
                    Content = x,
                    ArticleNumber = i + 1
                })
                .ToList();

            var news = new NewsDTO { Articles = articles };
            return news;
        }

        private string GetContentFromEmail(string username, string password, string senderFilter)
        {
            var email = _emailService.GetEmail(username, password, senderFilter);

            var text = string.Empty;
            if (email.Attachments.Any())
            {
                Document document = new Document();
                document.LoadFromStream(email.Attachments[0].ContentStream, FileFormat.Doc);
                text = document.GetText();
            }
            else
            {
                text = email.Body;
            }

            return text;
        }
    }
}
