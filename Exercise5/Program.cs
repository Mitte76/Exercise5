using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Exercise5.Data;
using Exercise5.Facilities;
using Exercise5.Menus;

namespace Exercise5
{
    internal class Program
    {
        private static List<Garage> facilities = [];

        static async Task Main()
        {
            Menu menu = new MenuFactory(facilities).CreateMainMenu();
            await menu.ShowMenu();
        }
    }
}
