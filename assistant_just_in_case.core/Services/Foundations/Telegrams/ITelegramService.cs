//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.Services.Foundations.Telegrams
{
    public interface ITelegramService
    {
        void RegisterTelegramEventHandler(Func<TelegramUserMessage, ValueTask> eventHandler);
        ValueTask<Message> SendMessageAsync(long userTelegramId,
           string message,
           int? replyToMessageId = null,
           ParseMode? parseMode = null,
           IReplyMarkup? replyMarkup = null);

        ValueTask DeleteMessageAsync(
            long userTelegramId,
            int messageId);
    }
}
