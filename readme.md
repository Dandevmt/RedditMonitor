# Reddit Monitor
Application that monitors and provides statistics for  a selected subreddit. Prior to running the application, certain environment variables need to be configured. Refer to the Setup section below for the environment variables setup.

The application will automatically start to monitor a subreddit. This can be changed by using the `monitor` command. 

`monitor [subreddit]`

To view statistics for the selected subreddit you can enter the `statistics` command.

You can view the help text by entering `help`.

## Setup
The following environment varibables should be setup to run the application.
These can be updated in Visual Studio project settings or in launchSettings.json.


| Environment Variable          | Value                                      |
| ----------------------------- | ------------------------------------------ |
| RedditApiOptions:ClientId     | Reddit Api Client Id                       |
| RedditApiOptions:ClientSecret | Reddit Api Secret                          |
| RedditApiOptions:GrantType    | client_credentials                         |
| RedditApiOptions:UserAgent    | reddit-monitor                             |
| RedditApiOptions:BaseUrl      | https://oauth.reddit.com                   |
| RedditApiOptions:TokenUrl     | https://www.reddit.com/api/v1/access_token |

