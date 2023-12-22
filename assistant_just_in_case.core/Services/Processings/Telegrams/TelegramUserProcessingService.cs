//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUsers;
using doviz_bot.Services.Foundations.TelegramUsers;
using Microsoft.EntityFrameworkCore;

namespace assistant_just_in_case.Services.Processings.Telegrams
{
    public class TelegramUserProcessingService : ITelegramUserProcessingService
    {
        private readonly ITelegramUserService telegramUserService;

        public TelegramUserProcessingService(ITelegramUserService telegramUserService)
        {
            this.telegramUserService = telegramUserService;
        }

        public async ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser) =>
            await this.telegramUserService.ModifyTelegramUserAsync(telegramUser);

        public async ValueTask<TelegramUser> UpsertTelegramUserProcessingService(TelegramUser telegramUser)
        {
            TelegramUser maybeTelegramUser =
                await this.telegramUserService.RetriveAllTelegramUsers()
                .FirstOrDefaultAsync(user => user.TelegramId == telegramUser.TelegramId);

            return maybeTelegramUser is null
                ? await this.telegramUserService.AddTelegramUserAsync(telegramUser)
                : maybeTelegramUser;
        }
    }
}
