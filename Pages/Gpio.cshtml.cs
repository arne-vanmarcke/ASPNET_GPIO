using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using System.Device.Gpio;

namespace MyApp.Namespace
{
    public class GpioModel : PageModel
    {
        private GpioController controller;

        public GpioModel(GpioController controller)
        {
            this.controller=controller;
        }
        
        public void OnGet()
        {
        }

        [BindProperty]
        public bool Gpio18 { get; set; }
        [BindProperty]
        public bool Gpio23 { get; set; }
        [BindProperty]
        public bool Gpio24 { get; set; }

        public PinValue Gpio17 { get; set; }
        public PinValue Gpio27 { get; set; }
        public PinValue Gpio22 { get; set; }

        public void OnPost()
        {
            controller.Write(18, Gpio18? PinValue.High : PinValue.Low);
            controller.Write(23, Gpio23? PinValue.High : PinValue.Low);
            controller.Write(24, Gpio24? PinValue.High : PinValue.Low);

            Gpio17=controller.Read(17);
            Gpio27=controller.Read(27);
            Gpio22=controller.Read(22);
            Console.WriteLine($"Gpio18 is: {Gpio18}");
            Console.WriteLine($"Gpio23 is: {Gpio23}");
            Console.WriteLine($"Gpio24 is: {Gpio24}");
        }
    }
}
