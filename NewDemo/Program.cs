using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NewDemo.Services.Interface;
using NewDemo.Services.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using NewDemo;

using NewDemo.ViewModel;
using NewDemo.Middleware;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddCustomServices();





await builder.Build().RunAsync();