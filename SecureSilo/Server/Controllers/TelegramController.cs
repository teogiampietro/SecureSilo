using Microsoft.AspNetCore.Mvc;
using SecureSilo.Server.Data;
using SecureSilo.Shared;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace SecureSilo.Server.Controllers
{
    [ApiController]
    [Route("api/bot")]
    public class TelegramController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Telegram.Bot.Types.Update update)
        {
            TelegramBotClient client = new TelegramBotClient(Constants.idTelegram);
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await client.SendTextMessageAsync(update.Message.From.Id, "Hola soy un crack programando. Teo.");
            }
            return Ok();
        }
      
    }
}
