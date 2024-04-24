using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextToSpeechController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Main(string text)
        {
            try
            {
                var speechKey = "9d1b809846f5428e93c3a3d12500d8a0";
                var speechRegion = "speechRegion";

                var speechConfig = SpeechConfig.FromSubscription(speechKey, speechRegion);

                // The neural multilingual voice can speak different languages based on the input text.
                speechConfig.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";

                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
                {
                    var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                    return Ok(speechSynthesisResult);
                }

                
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }

        }
    }
}
