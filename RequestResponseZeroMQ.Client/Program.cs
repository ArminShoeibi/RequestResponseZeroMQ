using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using NetMQ;
using NetMQ.Sockets;
using RequestResponseZeroMQ.Shared.DTOs;

namespace RequestResponseZeroMQ.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using RequestSocket requestSocket = new(">tcp://localhost:5500");
            List<int> userIds = new()
            {
                1,
                2,
                3
            };
            string userIdsJson = JsonSerializer.Serialize(userIds);
            Console.WriteLine($"Sending Request to server for getting users with these ids: {userIdsJson}");
            requestSocket.SendFrame(userIdsJson, false);

            string usersJson = requestSocket.ReceiveFrameString();


            List<UserSummaryDto> users = JsonSerializer.Deserialize<List<UserSummaryDto>>(usersJson);
            foreach (UserSummaryDto user in users)
            {
                Console.WriteLine($" {user.UserId + 10} {user.FirstName + 10} {user.LastName + 10} {user.Email + 10}");
            }
        }
    }
}
