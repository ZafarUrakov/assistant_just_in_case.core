//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using assistant_just_in_case.core.Services.Foundations.Converters;
using assistant_just_in_case.core.Services.Foundations.Telegrams;
using assistant_just_in_case.core.Services.Foundations.TelegramUsers;
using assistant_just_in_case.Services.Processings.Telegrams;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.core.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService : ITelegramUserOrchestrationService
    {
        private readonly ITelegramUserProcessingService telegramUserProcessingService;
        private readonly ITelegramService telegramService;
        private readonly ITelegramUserService telegramUserService;
        private readonly IConverterService converterService;

        public TelegramUserOrchestrationService(
            ITelegramUserProcessingService telegramUserProcessingService,
            ITelegramService telegramService,
            ITelegramUserService telegramUserService,
            IConverterService converterService)
        {
            this.telegramUserProcessingService = telegramUserProcessingService;
            this.telegramService = telegramService;
            this.telegramUserService = telegramUserService;
            this.converterService = converterService;
        }

        private const string startCommand = "/start";
        private const string convertCommand = "💰 Convert";
        private const string weatherCommand = "🌦 Weather";
        private const string backToMenuCommand = "⬅️ Menu";
        private const string menuCommand = "🧩 Menu";
        private const string connectWithUseCommand = "ℹ️ Connect with us";
        private const string feedbackCommand = "✍️ Leave feedback";

        public async ValueTask<TelegramUserMessage> ProcessTelegramUserAsync(TelegramUserMessage telegramUserMessage)
        {
            telegramUserMessage.TelegramUser =
                await telegramUserProcessingService
                    .UpsertTelegramUserProcessingService(telegramUserMessage.TelegramUser);

            if (await StartAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await MenuAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await BackToMenu(telegramUserMessage))
                return telegramUserMessage;

            if (await ConvertAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await WeatherAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await ConnectWithUsAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await FeedbackAsync(telegramUserMessage))
                return telegramUserMessage;

            if (await RegisterAsync(telegramUserMessage))
                return telegramUserMessage;

            return telegramUserMessage;
        }

        public void ListenTelegramUserMessage()
        {
            this.telegramService.RegisterTelegramEventHandler(async (telegramUserMessage) =>
            {
                await this.ProcessTelegramUserAsync(telegramUserMessage);
            });
        }

        private static ReplyKeyboardMarkup MainMarkupEng()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[]{new KeyboardButton("🧩 Menu") },
                new KeyboardButton[]
                {
                    new KeyboardButton("✍️ Leave feedback"),
                    new KeyboardButton("ℹ️ Connect with us")
                },
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}
