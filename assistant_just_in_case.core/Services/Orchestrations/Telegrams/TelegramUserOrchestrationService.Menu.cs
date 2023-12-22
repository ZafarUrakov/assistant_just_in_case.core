using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.core.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        private async ValueTask<bool> MenuAsync(TelegramUserMessage telegramUserMessage)
        {
            if (telegramUserMessage.Message.Text == menuCommand)
            {
                var markup = ConvertMarkupEng();

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: $"Assitant 🎭\n\nChoose any function you like: ",
                    replyMarkup: markup);

                return true;
            }

            return false;
        }

        private static ReplyKeyboardMarkup ConvertMarkupEng()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[]{new KeyboardButton("💰 Convert"), new KeyboardButton("🌦 Weather") },
                new KeyboardButton[]
                {
                    new KeyboardButton("⬅️ Menu")
                },
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}
