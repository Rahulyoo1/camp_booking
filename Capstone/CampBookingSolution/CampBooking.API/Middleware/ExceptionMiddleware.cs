using System.Net;
using System.Text.Json;
namespace CampBooking.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context) {

            try
            {
                await next(context);

            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                var response = new
                {
                    Success = false,
                    Message = ex.Message
                };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}

