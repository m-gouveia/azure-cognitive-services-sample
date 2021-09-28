using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;

namespace cognitive_services.Services
{
    public class ComputeVision : IComputeVision
    {
        private AzureParams _azureParams;

        public ComputeVision(AzureParams azureParams)
        {
            _azureParams = azureParams;
        }

        public string TextToImage()
        {
            var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_azureParams.Key));
            client.Endpoint = _azureParams.Url;

            //var myfile = System.IO.File.OpenRead(@"C:\Imagem_Moderar\img-sem-texto.jpeg");
            //var myfile = System.IO.File.OpenRead(@"C:\Imagem_Moderar\img-sem-palavrao.jpeg");
            var myfile = System.IO.File.OpenRead(@"C:\Imagem_Moderar\img-com-palavrao.jpeg");

            var result = client.RecognizePrintedTextInStreamAsync(false, myfile);

            result.Wait();

            var rst = result.Result;

            string retorno = "";

            foreach (var r in rst.Regions)
            {
                foreach (var t in r.Lines)
                {
                    foreach (var w in t.Words)
                    {
                        retorno += " " + w.Text;
                    }
                }
            }

            retorno = retorno.Trim();

            return retorno;
        }
    }
}
