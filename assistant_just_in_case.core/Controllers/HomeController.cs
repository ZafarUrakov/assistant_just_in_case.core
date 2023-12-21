//===========================
// Copyright (c) Good Engineers
// Fast assistant everywhere
//===========================

using Microsoft.AspNetCore.Mvc;

namespace assistant_just_in_case.core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> AssistantIsWorking() =>
            Ok("Assistant is working...");
    }
}
