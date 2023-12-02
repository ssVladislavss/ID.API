using ID.Core.ApiResources;
using ID.Core.ApiResources.Abstractions;
using ID.Core.ApiScopes;
using ID.Core.ApiScopes.Abstractions;
using ID.Core.Clients;
using ID.Core.Clients.Abstractions;
using ID.Core.Users;
using ID.Core.Users.Abstractions;
using ID.Data.Configurations;
using ID.Data.Configurations.Users.Profile;
using ID.Data.EF;
using ID.Data.EF.Repositories;
using ID.Host.Infrastracture;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.AspNetIdentity;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

_ = new AppSettings(builder.Configuration);

var migrationsAssembly = typeof(UserIDContext).GetTypeInfo().Assembly.GetName().Name;

builder.Services.AddScoped<IClientRepository, ClientRepository>(opt => new ClientRepository
                (opt.GetRequiredService<ConfigurationDbContext>(),
                new System.Linq.Expressions.Expression<Func<IdentityServer4.EntityFramework.Entities.Client, object>>[]
                {
                    prop => prop.AllowedCorsOrigins,
                    prop => prop.Properties,
                    prop => prop.AllowedScopes,
                    prop => prop.AllowedGrantTypes,
                    prop => prop.Claims,
                    prop => prop.ClientSecrets,
                    prop => prop.IdentityProviderRestrictions,
                    prop => prop.PostLogoutRedirectUris,
                    prop => prop.RedirectUris
                }));
builder.Services.AddScoped<IClientService, ClientService>();

builder.Services.AddScoped<IApiResourceRepository, ApiResourceRepository>(opt => new ApiResourceRepository
                (opt.GetRequiredService<ConfigurationDbContext>(),
                new System.Linq.Expressions.Expression<Func<IdentityServer4.EntityFramework.Entities.ApiResource, object>>[]
                {
                    prop => prop.UserClaims,
                    prop => prop.Scopes,
                    prop => prop.Properties,
                    prop => prop.Secrets
                }));
builder.Services.AddScoped<IApiResourceService, ApiResourceService>();

builder.Services.AddScoped<IApiScopeRepository, ApiScopeRepository>(opt => new ApiScopeRepository
                (opt.GetRequiredService<ConfigurationDbContext>(),
                new System.Linq.Expressions.Expression<Func<IdentityServer4.EntityFramework.Entities.ApiScope, object>>[]
                {
                    prop => prop.UserClaims,
                    prop => prop.Properties
                }));
builder.Services.AddScoped<IApiScopeService, ApiScopeService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddDbContext<UserIDContext>(options =>
{
    options.UseNpgsql(DbConnections.Npgsql!, sql => sql.MigrationsAssembly(migrationsAssembly));
    options.UseLazyLoadingProxies();
});

builder.Services.Configure<IdentityOptions>(options =>
{
    options.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
    options.ClaimsIdentity.EmailClaimType = JwtClaimTypes.Email;
    options.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
    options.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.PreferredUserName;
});

builder.Services.AddIdentity<UserID, IdentityRole>(config =>
{
    config.Password.RequiredLength = 4;
    config.Password.RequireDigit = false;
    config.Password.RequireNonAlphanumeric = false;
    config.User.RequireUniqueEmail = true;

    config.ClaimsIdentity.UserNameClaimType = JwtClaimTypes.PreferredUserName;
    config.ClaimsIdentity.UserIdClaimType = JwtClaimTypes.Subject;
    config.ClaimsIdentity.RoleClaimType = JwtClaimTypes.Role;
    config.ClaimsIdentity.EmailClaimType = JwtClaimTypes.Email;
})
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<UserIDContext>()
                .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.UserInteraction.LoginUrl = "/Account/Login";
    options.UserInteraction.LogoutUrl = "/Account/Logout";
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseSuccessEvents = true;
})
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = builder =>
        {
            builder.UseNpgsql(DbConnections.Npgsql!,
            sql => sql.MigrationsAssembly(migrationsAssembly));
        };

        options.DefaultSchema = "configuration_store";
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = builder =>
        {
            builder.UseNpgsql(DbConnections.Npgsql!,
            sql => sql.MigrationsAssembly(migrationsAssembly));
        };

        options.DefaultSchema = "persisted_store";
        options.EnableTokenCleanup = true;
        options.TokenCleanupInterval = 1800;
    })
    .AddProfileService<IDProfileService>()
    .AddResourceOwnerValidator<ResourceOwnerPasswordValidator<UserID>>()
    .AddDeveloperSigningCredential()
    .AddAspNetIdentity<UserID>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = "https://localhost:44338";
                    options.SaveToken = true;
                    options.Audience = "localhost:44338";
                    options.RequireHttpsMetadata = false;
                    options.ClaimsIssuer = "identity_server";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        NameClaimType = JwtClaimTypes.Name,
                        RoleClaimType = JwtClaimTypes.Role,
                        ClockSkew = TimeSpan.Zero
                    };
                });

var app = builder.Build();

await Task.Run(async () =>
{
    await ClientService.StartInitializerAsync(app.Services);
    await UserService.StartInitializerAsync(app.Services);
}).ConfigureAwait(false);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
