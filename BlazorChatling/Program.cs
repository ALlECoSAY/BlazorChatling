using BlazorChatling.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

var services = builder.Services;


var mvcBuilder = services.AddRazorPages();
services.AddServerSideBlazor();
//services.AddScoped<NavigationManager>(); Error


services.AddBlazorContextMenu();


services.AddScoped<UserValidationService>();
services.AddScoped<ChatsDAOService>();


services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
services.AddSingleton<HttpClient>();
services.AddSingleton<UserInfoGlobalClass>();


//delete later
//services.AddScoped<IHttpContextAccessor>();
services.AddHttpContextAccessor();

services.AddBlazorContextMenu();



if (builder.Environment.IsDevelopment()) {
    mvcBuilder.AddRazorRuntimeCompilation();
}



services.AddAuthentication("BasicCookieAuth").AddCookie("BasicCookieAuth", options => {
    options.Cookie.Name = "BasicCookieAuth";
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";


});

services.AddAuthorization(options => {
    options.AddPolicy("MustBeAnAdmin", policy => policy.RequireClaim("Role", "Admin"));

});


//DataBase
services.AddDbContext<BlazorChatling.Data.DBContextFiles.ChatlingDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();

//between app.UseRouting and app.UseEndpoints
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => {
    endpoints.MapRazorPages(); //Routes for pages
    endpoints.MapControllers(); //Routes for my API controllers
});




app.UseHttpsRedirection();

app.UseStaticFiles();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();