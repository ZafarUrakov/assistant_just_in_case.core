//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;

namespace assistant_just_in_case.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        private async ValueTask<bool> ConnectWithUsAsync(TelegramUserMessage telegramUserMessage)
        {
            if (telegramUserMessage.Message.Text == connectWithUseCommand)
            {
                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Doviz ⚡️\n\nHere are our contacts my dear friend: @zafar_urakov | @Johnnysenior");

                return true;
            }

            return false;
        }
    }
}
