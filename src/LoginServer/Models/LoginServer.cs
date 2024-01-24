using System.Net;
using System.Net.Sockets;
using Core.Logging;
using NetCoreServer;

namespace LoginServer.Models;

internal class LoginServer : TcpServer
{
    private readonly SmartSpamProtector _spamProtector = new();

    public LoginServer(IPAddress address, int port) : base(address, port) {}

    public LoginServer(string address, int port) : base(address, port) {}

    public LoginServer(DnsEndPoint endpoint) : base(endpoint) {}

    public LoginServer(IPEndPoint endpoint) : base(endpoint) {}

    protected override TcpSession CreateSession()
    {
        Log.Info("[TCP_SERVER] CreateSession");
        return new LoginSession(this);
    }

    protected override void OnConnected(TcpSession session)
    {
        try
        {
            if ( session.IsSocketDisposed )
                return;

            if ( session.Socket.RemoteEndPoint is not IPEndPoint ip )
            {
                session.Disconnect();
                return;
            }

            if ( !_spamProtector.CanConnect($"{ip.Address}") )
                return;

            Log.Info($"[TCP_SERVER] Connected : {ip.Address}");
        }
        catch ( Exception e )
        {
            Log.Error("[TCP_SERVER] OnConnected", e);
            session.Dispose();
        }
    }

    protected override void OnStarted()
    {
        Log.Info("[TCP-SERVER] Started!");
    }

    protected override void OnError(SocketError error)
    {
        Log.Info("[TCP-SERVER] SocketError");
        Stop();
    }
}