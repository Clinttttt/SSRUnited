using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SSRUnited.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.SharedClient(builder.Configuration);


await builder.Build().RunAsync();
