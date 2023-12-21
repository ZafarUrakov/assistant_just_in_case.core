//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using assistant_just_in_case.core.Models.TelegramUsers;

namespace assistant_just_in_case.core.Models.Converters
{
    public class Converter
    {
        public Guid Id { get; set; }
        public string FirstCurrency { get; set; }
        public decimal Amount { get; set; }
        public string SecondCurrency { get; set; }
        public float Result { get; set; }

        public Guid TelegramUserId { get; set; }
        public TelegramUser TelegramUser { get; set; }
    }
}
