using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5.Menus
{
    public class Menu(string menuName, MenuItem[] menuItems, bool isMainMenu = false)
    {
        private readonly string _menuName = menuName;
        private readonly MenuItem[] _items = menuItems;
        private readonly bool _isMainMenu = isMainMenu;

        private int returnValue = 1;
        int currentItem = 0;
        private string currentInput = "";

        public bool ShowMenu()
        {
            while (true)
            {
                ShowMainMenu();
                ConsoleKeyInfo input = Console.ReadKey(true);

                if (HandleInput(input))
                {
                    if (returnValue == -1)
                        return false;

                    MenuItem selected = _items[returnValue];

                    if (selected.Submenu != null)
                    {
                        selected.Submenu.ShowMenu();
                        continue;
                    }
                    selected.Action?.Invoke();
                    return false;
                }
            }
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine(_menuName);
            Console.WriteLine("----------");

            for (int i = 0; i < _items.Length; i++)
            {
                bool selected = i == currentItem;
                int menuNumber = (i + 1) % _items.Length;
                PrintMenuItem(menuNumber, _items[i].Text, selected);
            }
            Console.Write(currentInput);
        }

        static void PrintMenuItem(int number, string text, bool selected)
        {
            Console.Write($"{number}.[");

            Console.ForegroundColor = selected ? ConsoleColor.Green : ConsoleColor.DarkGray;

            Console.Write(selected ? "#" : " ");

            Console.ResetColor();
            Console.WriteLine($"] {text}");
        }

        private bool HandleInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.DownArrow:
                    currentItem++;
                    if (currentItem >= _items.Length)
                        currentItem = 0;
                    currentInput = "";
                    return false;
                case ConsoleKey.UpArrow:
                    currentItem--;
                    if (currentItem < 0)
                        currentItem = _items.Length - 1;
                    currentInput = "";

                    return false;
                case ConsoleKey.Backspace:
                    currentInput = currentInput[..^1];
                    return false;

                case ConsoleKey.Enter:
                    if (currentInput == "0" && !_isMainMenu)
                    {
                        returnValue = -1;
                        return true;
                    }

                    if (
                        currentInput != ""
                        && int.TryParse(currentInput, out int currentNumber)
                        && currentNumber >= 1
                        && currentNumber <= _items.Length
                    )
                    {
                        returnValue = currentNumber - 1;
                    }
                    else
                    {
                        returnValue = currentItem;
                    }

                    return true;
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                    currentInput += input.KeyChar;
                    if (int.TryParse(currentInput, out int current))
                    {
                        if (current < _items.Length && current >= 0)
                            currentItem = (current + _items.Length - 1) % _items.Length;
                        else
                        {
                            currentItem = 0;
                            currentInput = "";
                        }
                    }
                    return false;
                default:
                    Console.WriteLine("Incorrect input, please try again\n");
                    return false;
            }
        }
    }

    public class MenuItem(string text, Menu? submenu = null, Action? action = null)
    {
        public string Text { get; } = text;
        public Menu? Submenu { get; } = submenu;
        public Action? Action { get; } = action;
    }
}
