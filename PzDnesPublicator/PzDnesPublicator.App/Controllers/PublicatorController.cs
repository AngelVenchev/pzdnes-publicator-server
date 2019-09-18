using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PzDnesPublicator.App.DTOs;
using PzDnesPublicator.App.Services;
using Spire.Doc;

namespace PzDnesPublicator.App.Controllers
{
    [Route("api/[controller]")]
    public class PublicatorController : Controller
    {
        private readonly PublicatorService _publicatorService;

        public PublicatorController(PublicatorService publicatorService)
        {
            _publicatorService = publicatorService;
        }

        [HttpPost("RetrieveEmails")]
        public async Task<NewsDTO> RetrieveEmails([FromBody]CredentialsDTO credentials)
        {
            var news = await _publicatorService.GetNews(credentials);
            return news;
        }
    }
}
