using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RoadFix;
using RoadFix.Services;
using Supabase;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//SypaBase Client to Dependency Injection
var url = builder.Configuration["SupabaseUrl"] ?? "";
var key = builder.Configuration["SupabaseKey"] ?? "";

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
    {
       throw new Exception("Supabase URL or Key is missing from appsettings.json");
    }
    

    builder.Services.AddScoped(provider =>
     new Supabase.Client(url, key, new Supabase.SupabaseOptions 
    { 
        AutoConnectRealtime = true 
    }));
    var supabase = new Supabase.Client(url, key, new Supabase.SupabaseOptions 
    { 
        AutoConnectRealtime = true 
    });
await supabase.InitializeAsync();
    
builder.Services.AddScoped<AuthService>();
await builder.Build().RunAsync();
