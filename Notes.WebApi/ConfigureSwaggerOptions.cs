namespace Notes.WebApi;
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition($"AuthToken",
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer",
                Name = "Authorization",
                Description = "Authorization token"
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = $"AuthToken",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                new string[] { }
                }
            });

        options.CustomOperationIds(apiDescription =>
            apiDescription.TryGetMethodInfo(out MethodInfo methodInfo)
            ? methodInfo.Name
            : null);
    }
}
