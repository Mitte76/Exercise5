using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Menus
{
    public class MenuItem(string text, Menu? submenu = null, Action? action = null)
    {
        public string Text { get; } = text;
        public Menu? Submenu { get; } = submenu;
        public Action? Action { get; } = action;
    }
}
