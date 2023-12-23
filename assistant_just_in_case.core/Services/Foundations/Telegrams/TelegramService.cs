//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Threading.Tasks;
using assistant_just_in_case.core.Brokers.Telegrams;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using assistant_just_in_case.core.Models.TelegramUsers;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.core.Services.Foundations.Telegrams
{
    public class TelegramService : ITelegramService
    {
        private readonly ITelegramBroker telegramBroker;

        public TelegramService(ITelegramBroker telegramBroker)
        {
            this.telegramBroker = telegramBroker;
        }

        public void RegisterTelegramEventHandler(Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            this.telegramBroker.RegisterTelegramEventHandler(async message =>
                await ProcessTelegramTaskAsync(message, eventHandler));
        }

        private async ValueTask ProcessTelegramTaskAsync(Update update, Func<TelegramUserMessage, ValueTask> eventHandler)
        {
            if (update.Type == UpdateType.Message)
            {
                var telegramUser = new TelegramUser
                {
                    Id = Guid.NewGuid(),
                    TelegramId = update.Message.From.Id,
                    FirstName = update.Message.From.FirstName,
                };

                var telegramUserMessage = new TelegramUserMessage
                {
                    TelegramUser = telegramUser,
                    Message = update.Message
                }; 

                await eventHandler(telegramUserMessage);
            }
        }

        public async ValueTask<Message> SendMessageAsync(
            long userTelegramId,
            string message,
            int? replyToMessageId = null,
            ParseMode? parseMode = null,
            IReplyMarkup? replyMarkup = null)
        {
            return await this.telegramBroker.SendTextMessageAsync(
                    userTelegramId: userTelegramId,
                    message: message,
                    replyToMessageId: replyToMessageId,
                    parseMode: parseMode,
                    replyMarkup: replyMarkup);

        }

        public async ValueTask DeleteMessageAsync(
            long userTelegramId,
            int messageId)
        {
            await this.telegramBroker.DeleteMessageAsync(
               userTelegramId: userTelegramId,
               messageId: messageId);
        }
    }
}
