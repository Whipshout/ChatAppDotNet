/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: IApp.cs                                   *
 *     Description: interface for App service              *
 ***********************************************************/

namespace ChatApp.Interfaces
{
    public interface IApp
    {
        /// <summary>
        /// Get port for create server or connect client
        /// </summary>
        /// <returns></returns>
        int GetPort();

        /// <summary>
        /// Get username for client/server
        /// </summary>
        /// <returns></returns>
        string GetUserName();
    }
}