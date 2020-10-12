/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: CheckPort.cs                              *
 *     Description: check if a specific port is in use     *
 ***********************************************************/

using System.Linq;
using System.Net.NetworkInformation;
using ChatApp.Interfaces;

namespace ChatApp.Helpers
{
    public class CheckPort : ICheckPort
    {
        /// <summary>
        /// Check if a given port is in use
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public bool CheckPortConnection(int port)
        {
            var ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            var ipEndPoints = ipProperties.GetActiveTcpListeners();

            return ipEndPoints.Any(endPoint => endPoint.Port == port);
        }
    }
}