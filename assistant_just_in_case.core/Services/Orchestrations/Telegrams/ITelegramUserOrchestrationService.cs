//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;

namespace assistant_just_in_case.Services.Orchestrations.Telegrams
{
    public interface ITelegramUserOrchestrationService
    {
        ValueTask<TelegramUserMessage> ProcessTelegramUserAsync(TelegramUserMessage telegramUserMessage);
        void ListenTelegramUserMessage();
    }
}
