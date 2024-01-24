using Core.Logging;

namespace LoginServer.Models;

public class SmartSpamProtector
{
    private const int ConnectionAttemptsBeforeBlacklist = 4;
    private static readonly TimeSpan TimeBetweenConnection = TimeSpan.FromMilliseconds(125);

    private static readonly HashSet<string> BlacklistedIps = new();
    private static readonly Dictionary<string, List<DateTime>> ConnectionsByIp = new();

    public bool CanConnect(string ipAddress)
    {
        if ( BlacklistedIps.Contains(ipAddress) )
            return false;

        if ( !ConnectionsByIp.TryGetValue(ipAddress, out var dates) )
        {
            dates = new List<DateTime>();
            ConnectionsByIp[ipAddress] = dates;
        }

        var lastConnection = dates.LastOrDefault();
        dates.Add(DateTime.UtcNow);

        if ( dates.Count > ConnectionAttemptsBeforeBlacklist )
        {
            BlacklistedIps.Add(ipAddress);
            Log.Warn($"[SPAM_PROTECTOR] Blacklisted {ipAddress}");
            return false;
        }

        if ( lastConnection.Add(TimeBetweenConnection) >= DateTime.UtcNow )
            return false;

        dates.Clear();
        return true;
    }
}