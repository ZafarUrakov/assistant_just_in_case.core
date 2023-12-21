//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Linq;
using System.Threading.Tasks;
using assistant_just_in_case.core.Brokers.Storages;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace doviz_bot.Services.Foundations.TelegramUsers
{
    public class TelegramUserService : ITelegramUserService
    {
        private readonly IStorageBroker storageBroker;

        public TelegramUserService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }
        public async ValueTask<TelegramUser> AddTelegramUserAsync(TelegramUser user) =>
            await this.storageBroker.InsertTelegramUserAsync(user);

        public IQueryable<TelegramUser> RetriveAllTelegramUsers() =>
            this.storageBroker.SelectAllTelegramUsers();

        public ValueTask<TelegramUser> RetrieveTelegramUserByIdAsync(Guid telegramUserId) =>
            this.storageBroker.SelectTelegramUserByIdAsync(telegramUserId);

        public ValueTask<TelegramUser> ModifyTelegramUserAsync(TelegramUser telegramUser) =>
            this.storageBroker.UpdateTelegramUserAsync(telegramUser);
    }
}
