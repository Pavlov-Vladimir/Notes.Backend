namespace Notes.WebApi;
public class ConfigureSwaggerPasswordOptions : IConfigureOptions<SwaggerGenOptions>
{

    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("oauth2",
            new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Password = new OpenApiOAuthFlow
                    {
                        TokenUrl = new Uri("https://localhost:7123/connect/token"),
                        Scopes = new Dictionary<string, string>
                        {
                            { "NotesWebAPI", "Web API" }
                        }
                    }
                }
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "oauth2",
                            Type = ReferenceType.SecurityScheme
                        },
                        In = ParameterLocation.Header,
                        Name = "Bearer",
                        Scheme = "oauth2"
                    },
                    new List<string>()
                }
            });
    }
}
