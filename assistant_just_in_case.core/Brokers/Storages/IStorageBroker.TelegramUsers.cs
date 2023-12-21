//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace assistant_just_in_case.core.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser);
        IQueryable<TelegramUser> SelectAllTelegramUsers();
        ValueTask<TelegramUser> SelectTelegramUserByIdAsync(Guid telegramUserId);
        ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser);
        ValueTask<TelegramUser> DeleteTelegramUserAsync(TelegramUser telegramUser);
    }
}
