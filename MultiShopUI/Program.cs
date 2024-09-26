using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Dependency.Autofac;
using Core.Helpers.Security.Encryption;
using Core.Helpers.Security.JWT;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login"; // Giriş yolu
        options.LogoutPath = "/Auth/Logout"; // Çıkış yolu
        options.ExpireTimeSpan = TimeSpan.FromDays(7); // Cookie'nin geçerlilik süresi
        options.SlidingExpiration = true; // Cookie süresi dolmadan kullanıcı etkileşiminde sürenin uzatılması
        options.Cookie.HttpOnly = true; // JavaScript ile erişimi engelle
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Sadece HTTPS üzerinden gönderilsin
    });
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{
	builder.RegisterModule<AutofacBusinessModule>();
});

var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{

	option.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
		RequireExpirationTime = true,
		ValidateAudience = true,
		ValidateIssuer = true,
		ValidAudience = tokenOptions.Audience,
		ValidIssuer = tokenOptions.Issuer,
	};
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
			name: "areas",
			pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
		  );
app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
