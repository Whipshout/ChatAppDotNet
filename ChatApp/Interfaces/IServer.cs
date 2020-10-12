/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: IServer.cs                                *
 *     Description: interface for the Server service       *
 ***********************************************************/

namespace ChatApp.Interfaces
{
    public interface IServer
    {
        /// <summary>
        /// Create the server using a given port and username
        /// </summary>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        void CreateServer(int port, string userName);
    }
}