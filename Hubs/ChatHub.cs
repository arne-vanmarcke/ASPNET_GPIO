using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Device.Gpio;

namespace aspnetcoreapp_oefening.Hubs
{
    public interface IChat
    {
        Task ReceiveMessage(string user, string message);
        Task setPin(int pinNumber, bool value);
    }
    public class ChatHub : Hub<IChat>
    {
        private GpioController controller = new GpioController();

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.ReceiveMessage(user, message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task setPin(int pinNumber, bool value)
        {
            await Clients.All.ReceiveMessage($"Gpio {pinNumber}", $"{value}");
            controller.OpenPin(pinNumber,PinMode.Output);
            if(value)
                controller.Write(pinNumber,PinValue.High);
            else
                controller.Write(pinNumber,PinValue.Low);
        }
    }
}