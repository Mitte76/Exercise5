using System.Numerics;

namespace Exercise5
{
    internal class Program
    {

        static void Main(string[] args)
        {
            bool exitProgram = false;

            Menu menu = new(
            [
                "Show all facilities",
                "Create new facility",
                "Load facilities from database",
                "Save facilities to database",
                "Exit"
            ]);

            while (!exitProgram)
            {
                int itemSelected = menu.ShowMenu();

                exitProgram = HandleSelection(itemSelected);

                Console.WriteLine($"Item selected = {itemSelected}");
                Console.ReadKey();
            }



        }
        static bool HandleSelection(int selectedItem)
        {

            switch (selectedItem)
            {
                case 0:
                    return true;
                default:
                    return false;

            }

        }



    }
}
