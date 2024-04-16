using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using System.Diagnostics;

namespace WebAPI.Utils.OCR
{
    public class OcrService
    {
        private readonly string _subscriptionKey = "";
        private readonly string _endpoint = "https://cvvitalhubg2.cognitiveservices.azure.com/";

        public async Task<string> RecognizeTextAsync(Stream imageStream)
        {
            try
            {
                //Cria um client para a API de Computer Vision
                var client = new ComputerVisionClient(new ApiKeyServiceClientCredentials(_subscriptionKey))
                {
                    Endpoint = _endpoint
                };

                //Faz a chamada para a API
                var ocrResult = await client.RecognizePrintedTextInStreamAsync(true, imageStream);

                //Processa o resultado e retorna o texto reconhecido
                return ProcessRecognitionResult(ocrResult);
            }
            catch (Exception error)
            {

                return "Erro ao reconhecer o texto" + error.Message;
            }
        }

        private static string ProcessRecognitionResult(OcrResult result)
        {
            string recognizedText = "";

            //percorre todas as regioes 
            foreach (var region in result.Regions)
            {
                //para cada regiao percorre as linhas
                foreach (var line in region.Lines)
                {
                    //para cada lina percorre as palavras 
                    foreach (var word in line.Words)
                    {
                        //adiciona cada palavra ao texto, separando com espaco
                        recognizedText += word.Text + " ";
                    }
                    //quebra de linha ao final de cada linha 
                    recognizedText += "\n";
                }
            }

            //retorna o texto
            return recognizedText;
        }
    }
}
