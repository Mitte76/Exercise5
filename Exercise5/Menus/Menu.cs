using System;

namespace Exercise5.Menus
{
    public class Menu(string menuName, Func<MenuItem[]> menuItems, bool isMainMenu = false)
    {
        private readonly string _menuName = menuName;

        private readonly Func<MenuItem[]> _items = menuItems;

        private readonly bool _isMainMenu = isMainMenu;

        private string currentInput = "";

        public bool ShowMenu()
        {
            int selectedIndex = 0;

            while (true)
            {
                MenuItem[] items = _items();
                ShowMainMenu(items, selectedIndex);

                if (HandleInput(Console.ReadKey(true), items, selectedIndex, out selectedIndex))
                {
                    if (selectedIndex == -1)
                        return false;

                    MenuItem selected = items[selectedIndex];

                    if (selected.Submenu != null)
                    {
                        selected.Submenu.ShowMenu();
                        continue;
                    }

                    if (selected.Action != null)
                    {
                        selected.Action.Invoke();
                        continue;
                    }

                    if (selected.Submenu == null)
                        return false;
                }
            }
        }

        public void ShowMainMenu(MenuItem[] items, int selectedIndex)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;

            string title = _menuName;
            string border = new('*', title.Length + 8);

            Console.WriteLine($"{border}\n*   {title}   *\n{border}\n");
            Console.ResetColor();

            for (int i = 0; i < items.Length; i++)
            {
                bool selected = i == selectedIndex;
                int menuNumber = (i + 1) % items.Length;
                PrintMenuItem(menuNumber, items[i].Text, selected);
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

        private bool HandleInput(
            ConsoleKeyInfo input,
            MenuItem[] items,
            int currentIndex,
            out int selectedIndex
        )
        {
            selectedIndex = currentIndex;

            switch (input.Key)
            {
                case ConsoleKey.DownArrow:
                    selectedIndex++;
                    selectedIndex = (currentIndex + 1) % items.Length;
                    currentInput = "";
                    return false;
                case ConsoleKey.UpArrow:
                    selectedIndex--;
                    selectedIndex = (currentIndex - 1 + items.Length) % items.Length;
                    currentInput = "";

                    return false;
                case ConsoleKey.Backspace:
                    currentInput = currentInput[..^1];
                    return false;

                case ConsoleKey.Enter:
                    if (currentInput == "0" && !_isMainMenu)
                    {
                        selectedIndex = -1;
                        return true;
                    }

                    if (
                        currentInput != ""
                        && int.TryParse(currentInput, out int currentNumber)
                        && currentNumber >= 1
                        && currentNumber <= items.Length
                    )
                    {
                        selectedIndex = currentNumber - 1;
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
                        if (current < items.Length && current >= 0)
                            selectedIndex = (current + items.Length - 1) % items.Length;
                        else
                        {
                            selectedIndex = 0;
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
}
