/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: Program.cs                                *
 *     Description: control the flow of the app and create *
 *                  the client of the server in each case  *
 ***********************************************************/

using ChatApp.Helpers;
using ChatApp.Interfaces;
using ChatApp.Services;
using SimpleInjector;

namespace ChatApp
{
    internal static class Program
    {
        private static readonly Container Container;

        /// <summary>
        /// Initialize the dependency injection of the services
        /// Using SimpleInjector framework
        /// </summary>
        static Program()
        {
            Container = new Container();

            // Register all services
            Container.Register<IApp, App>();
            Container.Register<ICheckPort, CheckPort>();
            Container.Register<IServer, Server>();
            Container.Register<IClient, Client>();

            Container.Verify();
        }

        private static void Main(string[] args)
        {
            // Get instances of services
            var handlerApp = Container.GetInstance<IApp>();
            var handlerCheckPort = Container.GetInstance<ICheckPort>();
            var handlerServer = Container.GetInstance<IServer>();
            var handlerClient = Container.GetInstance<IClient>();

            var port = handlerApp.GetPort();
            var userName = handlerApp.GetUserName();
            var portInUse = handlerCheckPort.CheckPortConnection(port);

            // Check if create server or client
            if (!portInUse)
            {
                handlerServer.CreateServer(port, userName);
            }
            else
            {
                handlerClient.CreateClient(port, userName).Wait();
            }
        }
    }
}