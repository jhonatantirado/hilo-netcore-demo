using EnterprisePatterns.Api.Common.Application.Dto;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EnterprisePatterns.Api.Common.Controllers
{
    [Produces("application/json")]
    [Route("")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public Object Get()
        {
            return new ApiStringResponseDto("api root endpoint");
        }
    }
}
