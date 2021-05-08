using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RedOne.Rewards.WebApi.Controllers.Admin
{
    [Route("admin/[controller]")]
    [Authorize(Roles = "admin")]
    public class BaseAdminApiController : BaseApiController
    {
    }
}
