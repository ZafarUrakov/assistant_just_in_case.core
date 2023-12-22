//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using assistant_just_in_case.core.Services.Orchestrations.Telegrams;
using Microsoft.AspNetCore.Mvc;

namespace assistant_just_in_case.core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ITelegramUserOrchestrationService userOrchestrationService;

        public HomeController(ITelegramUserOrchestrationService userOrchestrationService)
        {
            this.userOrchestrationService = userOrchestrationService;
        }

        [HttpPost]
        public void AssistantIsWorking()
        {
            this.userOrchestrationService.ListenTelegramUserMessage();
        }

    }
}
