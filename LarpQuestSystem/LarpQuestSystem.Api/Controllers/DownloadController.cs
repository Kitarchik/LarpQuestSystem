using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using LarpQuestSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LarpQuestSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DownloadController : ControllerBase
    {
        readonly QuestContext _db;
        private static DriveService _driveService;

        public DownloadController(QuestContext context)
        {
            _db = context;
            _driveService = GetDriveService();
        }

        [HttpGet("artistic/{id}")]
        public async Task<IActionResult> DownloadQuestArtisticDocument(int id)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();

            var fileId = GetArtisticFileId(quest.ArtisticTextLink);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            var request = _driveService.Files.Export(fileId, @"application/vnd.openxmlformats-officedocument.wordprocessingml.document").ExecuteAsStream();

            return File(request,
                @"application/vnd.ms-word", $"{quest.Name} Artistic.docx");
        }

        [HttpGet("artistic/ready/{id}")]
        public async Task<IActionResult> CheckArtisticLink(int id)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();

            var fileId = GetArtisticFileId(quest.ArtisticTextLink);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("technical/{id}")]
        public async Task<IActionResult> DownloadQuestTechnicalDocument(int id)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();

            var fileId = GetArtisticFileId(quest.TechnicalDescriptionLink);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            var request = _driveService.Files.Export(fileId, @"application/vnd.openxmlformats-officedocument.wordprocessingml.document").ExecuteAsStream();

            return File(request,
                @"application/vnd.ms-word", $"{quest.Name} Technical.docx");
        }

        [HttpGet("technical/ready/{id}")]
        public async Task<IActionResult> CheckTechnicalLink(int id)
        {
            var quest = await _db.Quests.FirstOrDefaultAsync(x => x.Id == id);
            if (quest == null)
                return NotFound();

            var fileId = GetArtisticFileId(quest.TechnicalDescriptionLink);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpGet("technicalItem/{id}")]
        public async Task<IActionResult> DownloadTechnicalItemDocument(int id)
        {
            var questItem = await _db.QuestItems.FirstOrDefaultAsync(x => x.Id == id);
            var startingNpc = await _db.Npcs.FirstOrDefaultAsync(x => x.Id == questItem.StartingNpcId);
            var item = await _db.Items.FirstOrDefaultAsync(x => x.Id == questItem.ItemId);
            if (questItem == null)
                return NotFound();

            var fileId = GetArtisticFileId(questItem.TechnicalDocumentForNpc);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            var request = _driveService.Files.Export(fileId, @"application/vnd.openxmlformats-officedocument.wordprocessingml.document").ExecuteAsStream();

            return File(request,
                @"application/vnd.ms-word", $"{item.Name} для {startingNpc.Name} Technical document.docx");
        }

        [HttpGet("technicalItem/ready/{id}")]
        public async Task<IActionResult> CheckTechnicalItemLink(int id)
        {
            var questItem = await _db.QuestItems.FirstOrDefaultAsync(x => x.Id == id);
            if (questItem == null)
                return NotFound();

            var fileId = GetArtisticFileId(questItem.TechnicalDocumentForNpc);
            if (string.IsNullOrEmpty(fileId))
            {
                return NotFound();
            }

            return Ok();
        }

        private DriveService GetDriveService()
        {
            string[] scopes = { DriveService.Scope.DriveReadonly };

            var keyFilePath = "LarpQuestSystem.p12";
            var serviceAccountEmail = "larpquestsystemserviceaccount@larpquestsystem.iam.gserviceaccount.com";
            //loading the Key file
            var certificate = new X509Certificate2(keyFilePath, "notasecret", X509KeyStorageFlags.Exportable);
            var credential = new ServiceAccountCredential(new ServiceAccountCredential.Initializer(serviceAccountEmail)
            {
                Scopes = scopes
            }.FromCertificate(certificate));
            return new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "LarpQuestSystem",
            });
        }

        private string GetArtisticFileId(string textLink)
        {
            if (string.IsNullOrEmpty(textLink))
            {
                return string.Empty;
            }
            string result = Regex.Match(textLink, @"[-\w]{25,}").Value;

            return result;
        }
    }
}