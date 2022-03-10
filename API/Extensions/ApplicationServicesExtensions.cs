using System.Linq;
using API.Errors;
using core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepositoy>();
            services.AddScoped(typeof(IGenericRepository<>), (typeof(GenericRepositoy<>)));

            services.Configure<ApiBehaviorOptions>(options =>
          {
              options.InvalidModelStateResponseFactory = actionContext =>
              {
                  var errors = actionContext.ModelState
                  .Where(e => e.Value.Errors.Count > 0)
                  .SelectMany(x => x.Value.Errors)
                  .Select(x => x.ErrorMessage).ToArray();

                  var errorResponse = new ApiValidationErrorResponse
                  {
                      Errors = errors
                  };
                  return new BadRequestObjectResult(errorResponse);
              };

               
          });
           return services;
        }
    }
}