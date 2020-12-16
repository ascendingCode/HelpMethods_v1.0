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
            //Пример использования новой функции

            Process process = Process.GetProcessById(12208); //explorer.exe (проводник)
            HelperMethod.PrintProcessModule(process.ProcessName); //Выводим все модули

            ProcessModule processModule = HelperMethod.GetProcessModule(process, "icu.dll"); //Получаем модуль

            Console.WriteLine(processModule.ModuleName); //Теперь у нас есть этот модуль.
        }
    }
}