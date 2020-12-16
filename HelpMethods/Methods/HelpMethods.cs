using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HelpMethods
{
    public static class ConsoleHelperMethod
    {
        private static ConsoleColor SaveOldColor { get; set; }
        public static void Print(ConsoleColor messageColor, string message)
        {
            ConsoleColor saveColor = Console.ForegroundColor;
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
            Console.ForegroundColor = saveColor;
        }
        public static void PrintAndNoSaveColor(ConsoleColor messageColor, string message)
        {
            Console.ForegroundColor = messageColor;
            Console.WriteLine(message);
        }
        public static void SaveColor(ConsoleColor consoleColor)
        {
            SaveOldColor = consoleColor;
        }
        public static void LoadColor()
        {
            if (SaveOldColor.ToString() == null)
            {
                ErrorMessage("Сохраненных цветов нету!");
            }
            else
            {
                Console.ForegroundColor = SaveOldColor;
            }
        }
        public static void ErrorMessage(string message) => Print(ConsoleColor.Red, message);
        public static void WarningMessage(string message) => Print(ConsoleColor.Yellow, message);
        public static void CompletedMessage(string message) => Print(ConsoleColor.Green, message);
    }
    public static class HelperMethod
    {
        #region Array
        public static void PrintArray<type>(type[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(array[i]);
            }
        }
        public static void PrintArray<type>(type[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Console.Write(array[i, j] + " ");
                }
                Console.WriteLine();
            }
        }
        public static void PrintArray<type>(type[,,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    for (int h = 0; h < array.GetLength(2); h++)
                    {
                        Console.Write(array[i, j, h] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
        }
        public static void SortArray(ref int[] array)
        {
            if (array.Length <= 0)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                int[] GetNewArray = (from sorted in array orderby sorted ascending select sorted).ToArray<int>();
                for (int i = 0; i < GetNewArray.Length; i++)
                {
                    array[i] = GetNewArray[i];
                }
            }
        }
        public static void SortArray(ref double[] array)
        {
            if (array.Length <= 0)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                double[] GetNewArray = (from sorted in array orderby sorted ascending select sorted).ToArray<double>();
                for (int i = 0; i < GetNewArray.Length; i++)
                {
                    array[i] = GetNewArray[i];
                }
            }
        }
        public static void ReverseArray(ref int[] array)
        {
            if (array.Length <= 0)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                int[] GetReverseArray = (from sorted in array orderby sorted descending select sorted).ToArray<int>();
                for (int i = 0; i < GetReverseArray.Length; i++)
                {
                    array[i] = GetReverseArray[i];
                }
            }
        }
        public static void ReverseArray(ref double[] array)
        {
            if (array.Length <= 0)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                double[] GetNewArray = (from sorted in array orderby sorted descending select sorted).ToArray<double>();
                for (int i = 0; i < GetNewArray.Length; i++)
                {
                    array[i] = GetNewArray[i];
                }
            }
        }
        public static void SeachToArray(string[] array, string seach)
        {
            if (array.Length <= 0)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                string[] ReadSeachResult =
                    (from ReadItemsToArray in array where ReadItemsToArray.Contains(seach) select ReadItemsToArray).ToArray<string>();
                foreach (var PrintResult in ReadSeachResult)
                {
                    Console.WriteLine(PrintResult);
                }
            }
        }
        public static void CopyArray<Type>(Type[] arrayOne, ref Type[] arrayTwo)
        {
            if (arrayOne.Length <= 0 || arrayOne is null)
            {
                ConsoleHelperMethod.ErrorMessage("Массив пустой!");
            }
            else
            {
                arrayTwo = (from ReadArray in arrayOne select ReadArray).ToArray<Type>();
            }
        }
        #endregion Array
        public static void Swap(ref object Item1, ref object Item2)
        {
            (Item1, Item2) = (Item2, Item1);
        }

        #region Process
        public static void PrintAllProcess()
        {
            try
            {
                var ReadAllProcess = from GetProcess in Process.GetProcesses(".") select GetProcess;
                foreach (var getResult in ReadAllProcess)
                {
                    Console.WriteLine(getResult.ProcessName + ".exe");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void KillProcess(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                ConsoleHelperMethod.ErrorMessage("Название процесса не может быть пустым!");
            }
            else
            {
                Process process = null;
                try
                {
                    processName = Path.GetFileNameWithoutExtension(processName);
                    process = Process.GetProcessesByName(processName)[0];
                    process.Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void KillProcess(int PID)
        {
            if (PID < 0)
            {
                ConsoleHelperMethod.ErrorMessage("PID не может быть меньше 0!");
            }
            else
            {
                try
                {
                    Process process = Process.GetProcessById(PID);
                    process.Kill();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void PrintProcessModule(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                ConsoleHelperMethod.ErrorMessage("Название процесса не может быть пустым!");
            }
            else
            {
                try
                {
                    processName = Path.GetFileNameWithoutExtension(processName);
                    Process process = Process.GetProcessesByName(processName)[0];
                    ProcessModuleCollection processModuleCollection = process.Modules;
                    foreach (ProcessModule processModules in processModuleCollection)
                    {
                        Console.WriteLine(processModules.ModuleName);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static ProcessModule GetProcessModule(Process process,string moduleName)
        {
            ProcessModuleCollection processModuleCollection = process.Modules;
            ProcessModule processModule = default;
            for(int i = 0; i < processModuleCollection.Count; i++)
            {
                if (processModuleCollection[i].ModuleName == moduleName)
                {
                    processModule = processModuleCollection[i];
                    break;
                }
                else
                {
                    continue;
                }
            }
            return processModule;
        }
        public static void PrintProcessThreadInfo(string processName)
        {
            if (string.IsNullOrEmpty(processName))
            {
                ConsoleHelperMethod.ErrorMessage("Название процесса не может быть пустым!");
            }
            else
            {
                try
                {
                    processName = Path.GetFileNameWithoutExtension(processName);
                    Process process = Process.GetProcessesByName(processName)[0];
                    ProcessThreadCollection processThreadCollection = process.Threads;

                    foreach (ProcessThread processThreads in processThreadCollection)
                    {
                        Console.WriteLine($"ID: {processThreads.Id} | StartTime: {processThreads.StartTime} | State: {processThreads.ThreadState}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static void GetAssemblyCurrentDomain()
        {
            AppDomain appDomain = AppDomain.CurrentDomain;
            Assembly[] assembly = appDomain.GetAssemblies();

            foreach (var ReadAssebly in assembly)
            {
                Console.WriteLine(ReadAssebly.GetName().Name);
                Console.WriteLine(ReadAssebly.GetName().Version);
            }
        }
        #endregion Process
    }
}

