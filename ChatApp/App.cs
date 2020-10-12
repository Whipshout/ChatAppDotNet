/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: App.cs                                    *
 *     Description: manages the initial state of the app   *
 ***********************************************************/

using System;
using ChatApp.Interfaces;

namespace ChatApp
{
    public class App : IApp
    {
        /// <summary>
        /// Ask for a port and save the input
        /// </summary>
        /// <returns></returns>
        public int GetPort()
        {
            while (true)
            {
                Console.WriteLine("Type a port number between 1 and 65500 and then press Enter");
                var input = Console.ReadLine();

                // Check if input is a number
                if (int.TryParse(input, out var port))
                {
                    if (port >= 1 && port <= 65500)
                    {
                        return port;
                    }
                }

                Console.WriteLine("Please, type a number between 1 and 65500");
            }
        }

        /// <summary>
        /// Ask for an username and save the input
        /// </summary>
        /// <returns></returns>
        public string GetUserName()
        {
            while (true)
            {
                Console.WriteLine("Type a nickname and then press Enter");
                var userName = Console.ReadLine();

                // Check if input is not empty
                if (!string.IsNullOrEmpty(userName))
                {
                    return userName;
                }
                
                Console.WriteLine("Please, type something");
            }
        }
    }
}