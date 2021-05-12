using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedOne.Rewards.WebApi.Controllers.Consumer
{
    [Route("consumer/[controller]")]
    [Authorize(Roles = "consumer")]
    public class BaseConsumerApiController : BaseApiController
    {
    }
}
