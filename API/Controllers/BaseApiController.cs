using System;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")] // /api/users this introduces the "/api/*" routing
public class BaseApiController : ControllerBase
{

}
