namespace EcommerceAPIS.Extentions
{
    public static class AddSwaggerServices
    {
        public static IServiceCollection SwaggerServices(this IServiceCollection services)
        {
            //allow dj for swagger services
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }
        public static WebApplication SwaggerMiddleware(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
