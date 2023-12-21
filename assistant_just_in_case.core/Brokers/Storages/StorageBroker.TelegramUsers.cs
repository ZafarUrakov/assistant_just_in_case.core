//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUsers;
using Microsoft.EntityFrameworkCore;

namespace assistant_just_in_case.core.Brokers.Storages
{
    public partial class StorageBroker
    {
        public DbSet<TelegramUser> TelegramUsers { get; set; }

        public async ValueTask<TelegramUser> InsertTelegramUserAsync(TelegramUser telegramUser) =>
            await InsertAsync(telegramUser);

        public IQueryable<TelegramUser> SelectAllTelegramUsers() =>
        SelectAll<TelegramUser>();

        public async ValueTask<TelegramUser> SelectTelegramUserByIdAsync(Guid telegramUserId) =>
        await SelectAsync<TelegramUser>(telegramUserId);

        public async ValueTask<TelegramUser> UpdateTelegramUserAsync(TelegramUser telegramUser) =>
        await UpdateAsync(telegramUser);

        public async ValueTask<TelegramUser> DeleteTelegramUserAsync(TelegramUser telegramUser) =>
            await DeleteAsync(telegramUser);
    }
}
