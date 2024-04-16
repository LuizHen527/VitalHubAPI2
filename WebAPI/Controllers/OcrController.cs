﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Domains;
using WebAPI.Utils.OCR;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : ControllerBase
    {
        private readonly OcrService _ocrService;
        public OcrController(OcrService ocrService)
        {
            _ocrService = ocrService;
        }

        [HttpPost]

        public async Task<IActionResult> ReconizeText([FromForm]FileUploadModel fileUploadModel)
        {
            try
            {
                //Verifica se a imagem foi recebida
                if (fileUploadModel == null || fileUploadModel.Image == null)   
                {
                    return BadRequest("Nenhuma imagem foi fornecida");
                }

                //Abre a conexao com o recurso
                using(var stream = fileUploadModel.Image.OpenReadStream())
                {
                    //Chama o metodo para reconhecer a imagem
                    var result = await _ocrService.RecognizeTextAsync(stream);

                    //Retorna o resultado
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {

                return BadRequest("Erro ao processar a imagem" + ex.Message);
            }
        }
    }
}
