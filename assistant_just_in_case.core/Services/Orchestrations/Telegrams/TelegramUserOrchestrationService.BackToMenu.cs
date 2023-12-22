//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace assistant_just_in_case.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        public async ValueTask<bool> BackToMenu(TelegramUserMessage telegramUserMessage)
        {
            if (telegramUserMessage.Message.Text == menuCommand
                || telegramUserMessage.Message.Text == startCommand)
            {
                var markup = MainMarkupEng();
                string message = "Doviz 👀\n\nMenu my dear friend 👇🏼";

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: message,
                    replyMarkup: markup);

                if (telegramUserMessage.TelegramUser.Status == TelegramUserStatus.First
                    || telegramUserMessage.TelegramUser.Status == TelegramUserStatus.Amount
                    || telegramUserMessage.TelegramUser.Status == TelegramUserStatus.Last)
                {
                    var converter = this.converterService.RetriveAllConverters()
                        .FirstOrDefault(c => c.Id == telegramUserMessage.TelegramUser.HelperId);
                    await this.converterService.RemoveConverterAsync(converter);
                }

                telegramUserMessage.TelegramUser.Status = TelegramUserStatus.Active;
                telegramUserMessage.TelegramUser.HelperId = default;
                await this.telegramUserProcessingService
                    .ModifyTelegramUserAsync(telegramUserMessage.TelegramUser);

                return true;
            }

            return false;
        }
    }
}
