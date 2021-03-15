using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using RequestResponseZeroMQ.Shared.DTOs;

namespace RequestResponseZeroMQ.Server
{
    class Program
    {
        static void Main(string[] args)
        {

            using ResponseSocket responseSocket = new("@tcp://localhost:5500");
            string userIdsJson =  responseSocket.ReceiveFrameString();

            List<int> userIds = JsonSerializer.Deserialize<List<int>>(userIdsJson);

            List<UserSummaryDto> users = new();
            foreach (var userId in userIds)
            {
                UserSummaryDto userSummaryDto = new()
                {
                    UserId = userId,
                    FirstName = $"Armin {userId}",
                    LastName = $"Shoeibi {userId}",
                    Email = $"Email{userId}@gmail.com"
                };
                users.Add(userSummaryDto);
            }
            string usersJson = JsonSerializer.Serialize(users);
            responseSocket.SendFrame(usersJson, false);

        }

    }
}
