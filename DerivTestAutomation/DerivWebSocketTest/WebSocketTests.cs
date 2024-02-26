using System.Net.WebSockets;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DerivWebSocketTest
{
    [TestClass]
    public class WebSocketTests
    {
        
        DerivWebSocket derivWebSockets = new DerivWebSocket();
        [TestMethod]
        public void Test_WebSocket_StatusId()
        {
            derivWebSockets.ReceiveData("wss://ws.derivws.com/websockets/v3?app_id=1089", "{\r\n \"states_list\": \"id\"\r\n}");
            Thread.Sleep(TimeSpan.FromSeconds(10));
            Rootobject responceObject = Newtonsoft.Json.JsonConvert.DeserializeObject<Rootobject>(derivWebSockets.responceData);
            Assert.IsTrue(!String.IsNullOrEmpty(derivWebSockets.responceData), $"Responce is : {derivWebSockets.responceData}");
            Assert.IsTrue(responceObject.states_list.Where(t => t.text.Equals("Sumatra Selatan")).ToList()[0].value.Equals("SS"));

        }

    }
}