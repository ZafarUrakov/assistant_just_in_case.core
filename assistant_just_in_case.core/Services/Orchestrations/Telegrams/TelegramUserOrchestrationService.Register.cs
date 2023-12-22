//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using Telegram.Bot.Types.Enums;

namespace assistant_just_in_case.core.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        private async ValueTask<bool> RegisterAsync(TelegramUserMessage telegramUserMessage)
        {
            if (string.IsNullOrWhiteSpace(telegramUserMessage.TelegramUser.PhoneNumber))
            {
                if (telegramUserMessage.Message.Type == MessageType.Contact &&
                    telegramUserMessage.TelegramUser.TelegramId == telegramUserMessage.Message.Contact.UserId)
                {
                    string phoneNumber = telegramUserMessage.Message.Contact.PhoneNumber;
                    telegramUserMessage.TelegramUser.PhoneNumber = phoneNumber;

                    await this.telegramUserProcessingService
                        .ModifyTelegramUserAsync(telegramUserMessage.TelegramUser);

                    var markup = MainMarkupEng();

                    await this.telegramService.SendMessageAsync(
                        userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                        message: $"Doviz 👻\n\nThank you for registering {telegramUserMessage.TelegramUser.FirstName}, use it for good!",
                        replyMarkup: markup);

                    return true;
                }

                return false;
            }

            return false;
        }
    }
}
