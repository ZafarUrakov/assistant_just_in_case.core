﻿//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using assistant_just_in_case.core.Models.TelegramUsers;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.core.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        private async ValueTask<bool> FeedbackAsync(TelegramUserMessage telegramUserMessage)
        {
            if (telegramUserMessage.Message.Text == feedbackCommand)
            {
                telegramUserMessage.TelegramUser.Status = TelegramUserStatus.Feedback;
                await this.telegramUserProcessingService
                       .ModifyTelegramUserAsync(telegramUserMessage.TelegramUser);

                var markup = FeedbackMarkupEng();

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Doviz 📝\n\nMy dear friend, leave your feedback as a message.",
                    replyMarkup: markup);

                return true;
            }
            else if (telegramUserMessage.TelegramUser.Status == TelegramUserStatus.Feedback)
            {
                await SendfeedbackAcceptanceMessage(telegramUserMessage);

                return true;
            }

            return false;
        }

        private async ValueTask SendfeedbackAcceptanceMessage(TelegramUserMessage telegramUserMessage)
        {
            await this.telegramService.SendMessageAsync(
                    userTelegramId: 1924521160,
                    $"Doviz 🌚\n\nYou have a recieved a review from @{telegramUserMessage.Message.Chat.Username}\n\nFeedback:\n{telegramUserMessage.Message.Text}");

            var markup = MainMarkupEng();

            await this.telegramService.SendMessageAsync(
               userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
               "Doviz 🌚\n\nYour review has been accepted😁",
               replyMarkup: markup);

            System.Timers.Timer timer = new System.Timers.Timer(30000);
            timer.Elapsed += async (sender, e) =>
            {

                await this.telegramService.SendMessageAsync(
                   userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                   "Doviz administration 😎\n\nWe thank you for your feedback, have a nice day.");

                timer.Stop();

            };

            timer.Start();
        }

        private static ReplyKeyboardMarkup FeedbackMarkupEng()
        {
            return new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[]
                {
                    new KeyboardButton("⬅️ Menu")
                },
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}
