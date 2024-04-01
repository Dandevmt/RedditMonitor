using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RedditMonitor.ConsoleApp;

Console.WriteLine("Starting Reddit Monitor...");

var host = Configuration.BuildHost(args);

var loop = host.Services.GetRequiredService<InputLoopService>();

loop.StartInputLoop();

await host.RunAsync();