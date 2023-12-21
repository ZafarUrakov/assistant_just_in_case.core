//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Collections.Generic;
using assistant_just_in_case.core.Models.Converters;

namespace assistant_just_in_case.core.Models.TelegramUsers
{
    public class TelegramUser
    {
        public Guid Id { get; set; }
        public long TelegramId { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public TelegramUserStatus Status { get; set; }
        public Guid HelperId { get; set; }
        public ICollection<Converter> Converts { get; set; }
    }
}
