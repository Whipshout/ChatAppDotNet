/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: Server.cs                                 *
 *     Description: create server for app and manage msg   *
 ***********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using ChatApp.Interfaces;
using Fleck;

namespace ChatApp.Services
{
    public class Server : IServer
    {
        private static bool _isServerClosed;
        
        /// <summary>
        /// Create the server using a given port and username
        /// Manage sockets status and wait for inputs
        /// </summary>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        public void CreateServer(int port, string userName)
        {
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:" + port) {RestartAfterListenError = true};

            // start and configure server
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    if (_isServerClosed) return;
                    
                    Console.WriteLine(socket.ConnectionInfo.Path.Substring(1) + " has logged in!");

                    foreach (var s in allSockets.ToList())
                    {
                        s.Send(socket.ConnectionInfo.Path.Substring(1) + " has logged in!");
                    }

                    allSockets.Add(socket);
                };

                socket.OnClose = () =>
                {
                    if (_isServerClosed) return;
                    
                    Console.WriteLine(socket.ConnectionInfo.Path.Substring(1) + " has logged out!");

                    foreach (var s in allSockets.ToList())
                    {
                        s.Send(socket.ConnectionInfo.Path.Substring(1) + " has logged out!");
                    }

                    allSockets.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    if (_isServerClosed) return;
                    
                    Console.WriteLine(message);

                    foreach (var s in allSockets.ToList().Where(s => s != socket))
                    {
                        s.Send(message);
                    }
                };
            });

            Console.WriteLine("You can begin to chat (Type '$exit' to stop chat app server)");

            WaitForInputs(allSockets, userName);

            CloseServer(allSockets);
        }

        /// <summary>
        /// Send own inputs to all the other sockets with an username
        /// </summary>
        /// <param name="allSockets"></param>
        /// <param name="userName"></param>
        private static void WaitForInputs(IReadOnlyCollection<IWebSocketConnection> allSockets, string userName)
        {
            var input = Console.ReadLine();
            // waiting for input until user wants to finish
            while (input != "$exit")
            {
                if (string.IsNullOrEmpty(input))
                {
                    input = Console.ReadLine();
                    continue;
                }
                
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send($"'{userName}': {input}");
                }

                input = Console.ReadLine();
            }
        }

        /// <summary>
        /// Close server using the key word $exit and sending message to all sockets
        /// </summary>
        /// <param name="allSockets"></param>
        private static void CloseServer(IEnumerable<IWebSocketConnection> allSockets)
        {
            // send message to all sockets connected
            foreach (var socket in allSockets.ToList())
            {
                socket.Send("Closing server");
            }

            _isServerClosed = true;
            
            Console.WriteLine("Press enter to close chat app server");
            Console.ReadLine();
        }
    }
}