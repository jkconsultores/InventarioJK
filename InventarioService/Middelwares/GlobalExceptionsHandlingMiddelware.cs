using Model_Inventario.InventarioDTO.Error;
using Newtonsoft.Json;
using System.Net;

namespace InventarioService.Middelwares
{
    public class GlobalExceptionsHandlingMiddelware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionsHandlingMiddelware> _logger;

        public GlobalExceptionsHandlingMiddelware(ILogger<GlobalExceptionsHandlingMiddelware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);

            }
            catch (ExceptionDTO ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string error = JsonConvert.SerializeObject(ex.error);
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(error);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                string error = ex.Message;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(error);
            }
        }
    }
}