using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Graph.Models;

namespace Azure.Mail.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class WeatherForecastController : ControllerBase
  {
    private readonly GraphServiceClient _client;
    public WeatherForecastController(GraphServiceClient client)
    {
      _client = client;
    }

    [HttpGet("weather")]
    public async Task<IActionResult> Get()
    {
      await _client.Users["minh-0@minhdd.onmicrosoft.com"].SendMail.PostAsync(new Microsoft.Graph.Users.Item.SendMail.SendMailPostRequestBody
      {
        Message = new Message
        {
          ToRecipients =
          [
            new()
            {
              EmailAddress = new EmailAddress()
              {
                Address = "minhdd148@gmail.com"
              }
            }
          ],
          Body = new ItemBody
          {
            ContentType = BodyType.Text,
            Content = "Test send mail"
          }
        }
      });

      return Ok(new {});
    }
  }
}
