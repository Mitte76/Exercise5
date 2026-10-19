using System;
using System.Threading.Tasks;

namespace Exercise5.Menus
{
    public class MenuItem(
        string text,
        Menu? submenu = null,
        Func<string?>? action = null,
        Func<Task<string?>>? taskAction = null,
        bool backUpWhenDone = false
    )
    {
        public string Text { get; } = text;
        public Menu? Submenu { get; } = submenu;
        public Func<string?>? Action { get; } = action;
        public Func<Task<string?>>? TaskAction { get; } = taskAction;
        public bool BackOutWhenDone { get; } = backUpWhenDone;
    }
}
