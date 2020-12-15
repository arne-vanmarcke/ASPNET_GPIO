using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;

using aspnetcoreapp_oefening.Hubs;
using System.Device.Gpio;
namespace aspnetcoreapp_oefening
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private GpioController _controller;
        private readonly IHubContext<ChatHub, IChat> _chatHub;

        public Worker(ILogger<Worker> logger, GpioController controller, IHubContext<ChatHub,IChat> chatHub)
        {
            _logger = logger;
            _controller=controller;
            _chatHub=chatHub;
            PinEventTypes bothPinEventTypes = PinEventTypes.Falling | PinEventTypes.Rising;
            _controller.RegisterCallbackForPinValueChangedEvent(17, bothPinEventTypes, valueChangeHandler);
            _controller.RegisterCallbackForPinValueChangedEvent(27, bothPinEventTypes, valueChangeHandler);
            _controller.RegisterCallbackForPinValueChangedEvent(22, bothPinEventTypes, valueChangeHandler);
        }

        public async void valueChangeHandler(object sender, PinValueChangedEventArgs pinValueChangedEventArgs)
        {
          string rising_falling = pinValueChangedEventArgs.ChangeType == PinEventTypes.Rising ? "High" : "Low";
          await _chatHub.Clients.All.ReceiveMessage($"GPIO {pinValueChangedEventArgs.PinNumber}:", rising_falling);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}