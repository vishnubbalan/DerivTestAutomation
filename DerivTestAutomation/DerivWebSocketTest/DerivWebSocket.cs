using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DerivWebSocketTest
{
    public class DerivWebSocket
    {
        public string responceData = null;


        static async Task<string> ReceiveDataAsysnc(ClientWebSocket clientWebSocket)
        {
            byte[] buffer = new byte[1024];
            StringBuilder stringBuilder = new StringBuilder();

            WebSocketReceiveResult result = null;
            do
            {
                result = await clientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                stringBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));
            } while (!result.EndOfMessage);

            return stringBuilder.ToString();
        }

        async public Task ReceiveData(string endPoint, string message)
        {
            Uri url = new Uri(endPoint);
            using (ClientWebSocket clientWebSocket = new ClientWebSocket())
            {
                try
                {
                    await clientWebSocket.ConnectAsync(url, CancellationToken.None);
                    await SendDataAsync(clientWebSocket, message);
                    responceData = await ReceiveDataAsysnc(clientWebSocket);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception : " + ex.Message);
                }
                finally
                {
                    if (clientWebSocket.State == WebSocketState.Open)
                    {
                        await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    }
                }
            }

            static async Task SendDataAsync(ClientWebSocket clientWebSocket, string message)
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
