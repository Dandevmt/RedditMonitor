using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Security;

namespace RedditMonitor.ConsoleApp
{
    internal class InputLoopService
    {
        private readonly CancellationToken cancellationToken;
        private readonly IStore store;

        public InputLoopService(IHostApplicationLifetime applicationLifetime, IStore store)
        {
            this.cancellationToken = applicationLifetime.ApplicationStopping;
            this.store = store;
        }

        public void StartInputLoop()
        {
            var subreddit = "technology";            

            PrintHelp();

            Console.WriteLine($"\nStarting to monitor '{subreddit}' subreddit. Enter monitor [subreddit] to change.\n");

            Monitor(subreddit);

            Task.Run(async () => await MonitorAsync());
            Task.Run(async () => await PrintPeriodicTotals());
        }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async ValueTask MonitorAsync()
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var input = Console.ReadLine()?.Split(' ') ?? Array.Empty<string>();

                switch (input[0])
                {
                    case "help":
                        PrintHelp();
                        break;
                    case "monitor":
                        if (input.Length > 1)
                        {
                            Monitor(input[1]);
                        }
                        break;
                    case "statistics":
                        PrintStatistics();
                        break;
                }
            }
        }

        private async Task PrintPeriodicTotals()
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                PrintTotalPosts();
            }
        }

        private void Monitor(string subreddit)
        {
            store.SetSubredditToMonitor(subreddit);
            Console.WriteLine($"\nMonitoring {subreddit}\n\n");
        }

        private void PrintHelp()
        {
            var helpText = @"
--Reddit Monitor Help--
  Commands
    help: Print help text
    monitor [subreddit]: Monitor specified subreddit
    statistics: Output monitoring statistics
-----------------------


                ";

            Console.WriteLine(helpText);
            
        }

        private void PrintStatistics()
        {
            var statistics = new SubredditStatistics(store.GetAllSubredditPosts(store.GetSubredditToMonitor()));

            var totalPosts = statistics.GetTotalPosts();
            Console.WriteLine($"\nTotal Posts: {totalPosts}");

            var topTenPosts = statistics.GetTopTenPosts();
            Console.WriteLine("\n--Top Ten Posts--");
            foreach (var post in topTenPosts)
            {
                Console.WriteLine($"{post.Ups} Upvotes\n{post.Title}\n");
            }

            var topTenAuthors = statistics.GetTopTenAuthors();
            Console.WriteLine("\n--Top Ten Authors--");
            foreach (var author in topTenAuthors)
            {
                Console.WriteLine($"{author.Author}: {author.PostCount} Posts");
            }

            Console.WriteLine("\n\n");
        }

        private void PrintTotalPosts()
        {
            var statistics = new SubredditStatistics(store.GetAllSubredditPosts(store.GetSubredditToMonitor()));

            var currentPosition = Console.GetCursorPosition();

            Console.SetCursorPosition(0, Console.CursorTop - 3);
            Console.WriteLine($"\nTotal Posts: {statistics.GetTotalPosts()}\nTotal UpVotes: {statistics.GetTotalUpVotes()}");
            Console.SetCursorPosition(currentPosition.Left, currentPosition.Top);
        }

    }
}

