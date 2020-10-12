/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: ICheckPort.cs                             *
 *     Description: interface for CheckPort helper         *
 ***********************************************************/

namespace ChatApp.Interfaces
{
    public interface ICheckPort
    {
        /// <summary>
        /// Check if someone is using a given port
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        bool CheckPortConnection(int port);
    }
}