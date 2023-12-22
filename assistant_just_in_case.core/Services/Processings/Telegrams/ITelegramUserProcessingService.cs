//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace assistant_just_in_case.Services.Processings.Telegrams
{
    public interface ITelegramUserProcessingService
    {
        ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser);
        ValueTask<TelegramUser> UpsertTelegramUserProcessingService(TelegramUser telegramUser);
    }
}
