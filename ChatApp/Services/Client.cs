/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: Client.cs                                 *
 *     Description: create a client and his services       *
 ***********************************************************/

using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ChatApp.Interfaces;

namespace ChatApp.Services
{
    public class Client : IClient
    {
        /// <summary>
        /// Create client with a given port and UserName
        /// Executing until server is closed or user exits
        /// </summary>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        public async Task CreateClient(int port, string userName)
        {
            Thread.Sleep(1000);

            ClientWebSocket webSocket = null;

            // try to create a client in the given port
            try
            {
                webSocket = new ClientWebSocket();

                await webSocket.ConnectAsync(new Uri("ws://localhost:" + port + "/" + userName),
                    CancellationToken.None);

                Console.WriteLine("You can begin to chat (Type '$exit' to stop chat app client)");

                await Task.WhenAny(Receive(webSocket), Send(webSocket, userName));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            }
            finally
            {
                // when server stops or client closes
                webSocket?.Dispose();
                Console.WriteLine("Press enter to close chat app client");
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Handles the send service of the client
        /// Check if socket is open and if the user uses the key word to close
        /// Then send the message buffering it
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        private static async Task Send(WebSocket webSocket, string userName)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var stringToSend = Console.ReadLine();

                if (string.IsNullOrEmpty(stringToSend)) continue;
                
                if (stringToSend == "$exit")
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                        CancellationToken.None);

                    break;
                }
                
                var buffer = Encoding.UTF8.GetBytes($"'{userName}': {stringToSend}");
                await webSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true,
                    CancellationToken.None);
                await Task.Delay(1000);
            }
        }

        /// <summary>
        /// Handles the receive service of the client
        /// Check if the socket is open
        /// Then buffers the received message
        /// If the server closes, write a specific message
        /// </summary>
        /// <param name="webSocket"></param>
        /// <returns></returns>
        private static async Task Receive(WebSocket webSocket)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                var buffer = new byte[1024];
                await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                // check for server message to close client too
                if (Encoding.UTF8.GetString(buffer).TrimEnd('\0') == "Closing server")
                {
                    Console.WriteLine("Server closed, press Enter to further instructions");

                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty,
                        CancellationToken.None);

                    break;
                }

                Console.WriteLine(Encoding.UTF8.GetString(buffer).TrimEnd('\0'));
            }
        }
    }
}