using Azure.Storage.Blobs;

namespace WebAPI.Utils.BlobStorage
{
    public static class AzureBlobStorageHelper
    {
        public static async Task<string> UploadImageBlobAsync(IFormFile arquivo, string stringConexao, string nomeContainer)
        {
            try
            {
                //verifica se existe um arquivo
                if (arquivo != null)
                {
                    //gerar um nome unico para a imagem
                    var blobName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(arquivo.FileName);

                    //Cria uma instancia do BlobServiceClient passando a string de conexao com o blob da azure
                    var blobServiceClient = new BlobServiceClient(stringConexao);

                    //obtem dados do container client
                    var blobContainerClient = blobServiceClient.GetBlobContainerClient(nomeContainer);

                    //obtem um blobClient usando o blob name
                    var blobClient = blobContainerClient.GetBlobClient(blobName);

                    //abre o fluxo de entrada do arquivo (foto)
                    using (var stream = arquivo.OpenReadStream())
                    {
                        await blobClient.UploadAsync(stream, true);
                    }

                    //Retorna a uri do blob como uma string
                    return blobClient.Uri.ToString();
                }
                else
                {
                    //Retorna uri de uma imagem padrao caso nenhuma imagem seja enviada
                    return "https://blobvitalhub3dmg2.blob.core.windows.net/blobvitalcontainer/imagemPadrao.jpg";

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
