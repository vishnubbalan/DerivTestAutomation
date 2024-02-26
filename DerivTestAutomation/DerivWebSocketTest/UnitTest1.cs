using System.Net.WebSockets;
using System.Text;

namespace DerivWebSocketTest
{
    [TestClass]
    public class UnitTest1
    {
        string responceData = null;
        

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
            using(ClientWebSocket clientWebSocket = new ClientWebSocket())
            {
                try
                {
                    await clientWebSocket.ConnectAsync(url, CancellationToken.None);
                    await SendDataAsync(clientWebSocket, message);
                    responceData = await ReceiveDataAsysnc(clientWebSocket);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception : " + ex.Message);
                }
                finally{
                    if(clientWebSocket.State == WebSocketState.Open)
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

        [TestMethod]
        public void Test_WebSocket_StatusId()
        {
            ReceiveData("wss://ws.derivws.com/websockets/v3?app_id=1089", "{\r\n \"states_list\": \"id\"\r\n}");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Rootobject responceObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(responceData);
            Assert.IsTrue(!String.IsNullOrEmpty(responceData), $"Responce is : {responceData}");
            Assert.IsTrue(responceObject.states_list.Where(t => t.text.Equals("Sumatra Selatan")).ToList()[0].value.Equals("SS"));

        }

    }
}