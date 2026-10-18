using System;
using System.Collections.Generic;
using System.Text;

namespace Exercise5
{
    public class Menu(string[] menuItems)
    {

        private readonly string[] _items = menuItems;
        private int? returnValue = null;
        private bool itemSelected = false;
        int currentItem = 0;
        private string currentInput  = "";
        public int ShowMenu()
        {
            while (!itemSelected)
            {
                ShowMainMenu();
                ConsoleKeyInfo input = Console.ReadKey(true);
                HandleInput(input);
            }
            itemSelected = false;
            return returnValue.Value;
        }

        public void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("MainMenu: Exersice 4");
            Console.WriteLine("----------");

            for (int i = 0; i < _items.Length; i++)
            {
                bool selected = i == currentItem;
                int menuNumber = (i + 1) % _items.Length;
                PrintMenuItem(menuNumber, _items[i], selected);
            }
            Console.Write(currentInput);
        }
        static void PrintMenuItem(int number, string text, bool selected)
        {
            Console.Write($"{number}.[");

            Console.ForegroundColor = selected
                ? ConsoleColor.Green
                : ConsoleColor.DarkGray;

            Console.Write(selected ? "#" : " ");

            Console.ResetColor();
            Console.WriteLine($"] {text}");
        }

        private void HandleInput(ConsoleKeyInfo input)
        {
            switch (input.Key)
            {
                case ConsoleKey.DownArrow:
                    currentItem++;
                    if (currentItem >= _items.Length)
                        currentItem = 0;
                    currentInput = "";
                    break;
                case ConsoleKey.UpArrow:
                    currentItem--;
                    if (currentItem < 0)
                        currentItem = _items.Length - 1;
                    currentInput = "";

                    break;
                case ConsoleKey.Backspace:
                    currentInput = currentInput.Remove(currentInput.Length - 1);
                    break;
                case ConsoleKey.Enter:
                    if (currentInput != "" && int.TryParse(currentInput, out int currentNumber))
                    {
                        if (currentNumber <= _items.Length - 1)
                        {
                            returnValue = currentNumber;
                            Console.WriteLine("fffff");
                        }
                        currentInput = "";

                    }
                    else
                    {
                        returnValue = (currentItem + 1) % _items.Length;
                    }
                    itemSelected = true;
                    break;
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
                    break;
                default:
                    Console.WriteLine("Incorrect input, please try again\n");
                    break;
            }

        }



    }
}