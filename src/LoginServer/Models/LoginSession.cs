using System.Net;
using System.Net.Sockets;
using NetCoreServer;

namespace LoginServer.Models;

internal class LoginSession(TcpServer server) : TcpSession(server)
{
    public string? IpAddress { get; private set; }

    protected override void OnConnected()
    {
        try
        {
            if ( IsDisposed )
                return;

            if ( IsSocketDisposed )
            {
                Disconnect();
                return;
            }

            if ( Socket == null )
                return;

            if ( Socket?.RemoteEndPoint is IPEndPoint ip )
                IpAddress = ip.Address.ToString();
        }
        catch
        {
            Disconnect();
        }
    }

    protected override void OnReceived(byte[] buffer, long offset, long size)
    {
        try
        {
            //TODO: Packet incoming
            var packetSpan = buffer.AsSpan((int)offset, (int)size);
        }
        catch
        {
            Disconnect();
        }
    }

    protected override void OnError(SocketError error)
    {
        Disconnect();
    }
}