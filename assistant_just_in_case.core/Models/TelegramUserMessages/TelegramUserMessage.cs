//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using assistant_just_in_case.core.Models.TelegramUsers;
using Telegram.Bot.Types;

namespace assistant_just_in_case.core.Models.TelegramUserMessages
{
    public class TelegramUserMessage
    {
        public TelegramUser TelegramUser { get; set; }
        public Message Message { get; set; }
    }
}
