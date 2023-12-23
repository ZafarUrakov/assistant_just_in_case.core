//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using System;
using System.Net.Http;
using System.Threading.Tasks;
using assistant_just_in_case.core.Models.TelegramUserMessages;
using assistant_just_in_case.core.Models.TelegramUsers;
using Newtonsoft.Json.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace assistant_just_in_case.core.Services.Orchestrations.Telegrams
{
    public partial class TelegramUserOrchestrationService
    {
        private async ValueTask<bool> WeatherAsync(TelegramUserMessage telegramUserMessage)
        {
            if (telegramUserMessage.Message?.Text == weatherCommand)
            {
                telegramUserMessage.TelegramUser.Status = TelegramUserStatus.Weather;
                await this.telegramUserProcessingService.ModifyTelegramUserAsync(telegramUserMessage.TelegramUser);

                var markUp = new ReplyKeyboardMarkup(new KeyboardButton[][]
{
                    new KeyboardButton[]{ new KeyboardButton("Location 📍") { RequestLocation = true } },
                    new KeyboardButton[]{ new KeyboardButton(backToMenuCommand) }
})
                {
                    ResizeKeyboard = true
                };


                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: "Assitant ☃️\n\nSend your location my friend",
                    replyMarkup: markUp);

                return true;
            }
            if (telegramUserMessage.Message.Type == MessageType.Location &&
                telegramUserMessage.TelegramUser.Status == TelegramUserStatus.Weather)
            {
                var response = await GetCurrentWeatherAsync(telegramUserMessage);
                var markUp = MainMarkupEng();

                await this.telegramService.SendMessageAsync(
                    userTelegramId: telegramUserMessage.TelegramUser.TelegramId,
                    message: $"Assitant ☃️\n\n{response}",
                    replyMarkup: markUp);

                telegramUserMessage.TelegramUser.Status = TelegramUserStatus.Active;
                await this.telegramUserProcessingService.ModifyTelegramUserAsync(telegramUserMessage.TelegramUser);

                return true;
            }

            return false;
        }

        private async ValueTask<string> GetCurrentWeatherAsync(TelegramUserMessage telegramUserMessage)
        {
            double longitude = telegramUserMessage.Message.Location.Longitude;
            double latitude = telegramUserMessage.Message.Location.Latitude;

            using (var httpClient = new HttpClient())
            {

                string apiKey = "e2e8a7f702ae48b0b602f87993c98955";
                string apiUrlForLocation = $"https://api.opencagedata.com/geocode/v1/json?key={apiKey}&q={latitude}+{longitude}";
                HttpResponseMessage responseForLocation = await httpClient.GetAsync(apiUrlForLocation);

                string apiUrl = $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current_weather=true";
                HttpResponseMessage response = await httpClient.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string contentForLocation = await responseForLocation.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(contentForLocation);

                    string city = result.results[0].components.city;
                    string street = result.results[0].components.road;
                    string houseNumber = result.results[0].components.house_number;

                    string content = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(content);
                    double temperature = (double)jsonResponse["current_weather"]["temperature"];

                    if (temperature != null)
                    {
                        if (string.IsNullOrEmpty(city))
                        {
                            city = "Unknown City";
                        }
                    }
                    else
                    {
                        return "Could not find information by your location!!!";
                    }
                    return $"Temperature of {city}, {street}, {houseNumber}: {temperature:F1}°C";
                }
                else
                {
                    return $"Error getting weather forecast. Status code: {response.StatusCode}";
                }
            }
        }
    }
}
