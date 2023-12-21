//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace doviz_bot.Services.Foundations.TelegramUsers
{
    public interface ITelegramUserService
    {
        ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser user);
        IQueryable<TelegramUser> RetriveAllTelegramUsers();
        ValueTask<TelegramUser> RetrieveTelegramUserByIdAsync(Guid telegramUserId);
        ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser);
    }
}
