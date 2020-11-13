using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HelpMethods;
namespace TestHelpMethods
{
    public static class Program
    {
        public static void Main(string[]args)
        {
            //Пример использования.

            string[] games = { "Grand Theft Auto V", "Counter-Strike", "Fallout 3", "Minecraft", "Fortnite" };
            ConsoleHelperMethod.SaveColor(ConsoleColor.White);
            ConsoleHelperMethod.PrintAndNoSaveColor(ConsoleColor.Green,"Hello!Select menu item.");
            Console.WriteLine("1.Print all process.");
            Console.WriteLine("2.Seach item to array.");

            int ReadMenu = int.Parse(Console.ReadLine());

            if(ReadMenu==1)
            {
                HelperMethod.PrintAllProcess();
                ConsoleHelperMethod.LoadColor();
            }
            else if(ReadMenu==2)
            {
                Console.Clear();
                Console.Write("Enther game name: ");
                string ReadGameName = Console.ReadLine();

                if(string.IsNullOrEmpty(ReadGameName))
                {
                    ConsoleHelperMethod.ErrorMessage("Error! The name of the game cannot be empty!");
                    ConsoleHelperMethod.LoadColor();
                }
                else
                {
                    HelperMethod.SeachToArray(games, ReadGameName);
                    ConsoleHelperMethod.LoadColor();
                }
            }
            else
            {
                ConsoleHelperMethod.ErrorMessage("Error! Invalid menu item!");
            }
        }
    }
}