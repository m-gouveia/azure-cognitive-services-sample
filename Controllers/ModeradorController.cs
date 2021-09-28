using cognitive_services.Services;
using Microsoft.AspNetCore.Mvc;

namespace cognitive_services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModeradorController : ControllerBase
    {
        private IComputeVision _computeVision;
        private IContentModerator _contentModerator;

        public ModeradorController(IComputeVision computeVision, IContentModerator contentModerator)
        {
            _computeVision = computeVision;
            _contentModerator = contentModerator;
        }

        [HttpGet]
        public string Get()
        {
            string textoDaImagem = _computeVision.TextToImage();

            if (textoDaImagem.Trim().Length > 0)
            {
                return _contentModerator.Text(textoDaImagem);
            }

            //TODO: Responder em formato JSON
            return "Imagem não possui texto";
        }



        [HttpGet("Image")]
        public string GetImage(string pathImagem)
        {
           return _contentModerator.Image(pathImagem);
        }
    }
}
