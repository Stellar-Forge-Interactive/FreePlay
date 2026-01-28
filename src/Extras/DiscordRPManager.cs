using DiscordRPC;
using DiscordRPC.Message;
using FreePlay.Core;

namespace FreePlay.Extras;

internal class DiscordRPManager
{
    const long ID = 1466187702180384910;

    DiscordRpcClient client;

    public DiscordRPManager()
    {
        client = new DiscordRpcClient($"{ID}");

        client.OnReady += OnReady;
        
        client.Initialize();
    }

    void OnReady(object sender, ReadyMessage args)
    {
        var debug = Program.DebugMode;

        RichPresence presence = new RichPresence();
        presence.Type = ActivityType.Listening;
        presence.Details = "Details debug";
        presence.State = "State debug";
        presence.StatusDisplay = StatusDisplayType.State;
        
        presence.Timestamps = Timestamps.FromTimeSpan(120);
        
        client.SetPresence(presence);
    }
}