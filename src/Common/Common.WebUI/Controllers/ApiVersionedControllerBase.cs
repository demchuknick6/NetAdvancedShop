namespace Common.WebUI.Controllers;

[ApiController]
[ApiExceptionFilter]
[Route("api/v{version:apiVersion}/[controller]")]
public class ApiVersionedControllerBase : ControllerBase
{

}
