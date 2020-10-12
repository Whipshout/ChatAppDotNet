/***********************************************************
 *     Author: Eduardo Sánchez Sánchez                     *
 *     Email: whipshout@gmail.com                          *
 *     Filename: IClient.cs                                *
 *     Description: interface for Client service           *
 ***********************************************************/

using System.Threading.Tasks;

namespace ChatApp.Interfaces
{
    public interface IClient
    {
        /// <summary>
        /// Create a client using a given port and username
        /// </summary>
        /// <param name="port"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task CreateClient(int port, string userName);
    }
}