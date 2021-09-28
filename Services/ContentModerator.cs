using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace cognitive_services.Services
{
    public class ContentModerator : IContentModerator
    {
        private AzureParams _azureParams;
        private string _uriProcessText;
        private string _uriProcessImage;

        public ContentModerator(AzureParams azureParams)
        {
            _azureParams = azureParams;
            _uriProcessText = azureParams.Url + "/contentmoderator/moderate/v1.0/ProcessText/Screen";
            _uriProcessImage = azureParams.Url + "/contentmoderator/moderate/v1.0/ProcessImage/Evaluate";
        }

        public string Text(string text)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _azureParams.Key);

            byte[] byteData = Encoding.UTF8.GetBytes(text);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                HttpResponseMessage response = client.PostAsync(_uriProcessText, content).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }

        public string Image(string pathImagem)
        {
            if (string.IsNullOrEmpty(pathImagem))
            {
                pathImagem = "https://cdn.dol.com.br/img/Artigo-Destaque/670000/foto_00674297_0_.jpg?xid=1619969";
            }
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _azureParams.Key);

            //URL da Imagem
            var jsonContent = "{'DataRepresentation':'URL','Value':'" + pathImagem + "'}";

            byte[] byteData = Encoding.UTF8.GetBytes(jsonContent);

            using (var content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
                HttpResponseMessage response = client.PostAsync(_uriProcessImage, content).Result;

                return response.Content.ReadAsStringAsync().Result;
            }
        }
    }
}
