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
        public bool Output18 { get; set; }

        public void OnPost()
        {
            controller.Write(18, Output18? PinValue.High : PinValue.Low);
            Console.WriteLine($"Output18 is: {Output18}");
        }
    }
}
