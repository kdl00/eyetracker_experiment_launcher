using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LuaInterface;

namespace ConsoleApplication5
{
    class LuaInterpreter
    {
        private LuaEngine engine;
        
        public LuaInterpreter(LuaEngine eng)
        {
            engine = eng;
            showMainMenu();
        }

        public void showMainMenu()
        {
            Console.WriteLine("Main menu:");
            Console.WriteLine("\t1. Run a Lua file");
            Console.WriteLine("\t2. Interactive Interpreter");
            Console.WriteLine(Environment.NewLine+"What do you want to do?");
            ConsoleKey k;
            do
            {
                k = Console.ReadKey(true).Key;
            } while (k != ConsoleKey.D1 && k != ConsoleKey.D2);

            doOption(k);
        }

        private void doOption(ConsoleKey k)
        {
            switch (k)
            {
                case ConsoleKey.D1:
                    executeLuaFile();
                    break;
                case ConsoleKey.D2:
                    enterInteractiveInterpreter();
                    break;
                default:
                    throw new Exception("Invalid console key in doOption()!");
            }            
        }

        private void executeLuaFile()
        {
            Console.WriteLine("Lua file to execute: ");
            string file;
            do
            {
                file = Console.ReadLine();
            } while (!System.IO.File.Exists(file));

            try
            {
                engine.DoFile(file);
            }
            catch (LuaException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            showMainMenu();
        }

        // TODO: the interpreter is not accumulative ie. i can't define a bunch of functions then call them 
        // use stringbuilder to accumulate each line (append) then once the user types "\n" (blank new line) 
        // run what's been accumulated in the stringbuilder
        private void enterInteractiveInterpreter()
        {
            Console.Clear();
            Console.WriteLine("Commands available:\n\tmenu - Return to the main menu\n\trun - executes previously entered Lua code\n\tclear - clears previously entered Lua code\n\texit - exit the program\n");
            StringBuilder b = new StringBuilder();
            while (true)
            {
                Console.Write("Lua interpreter > ");
                string ln = Console.ReadLine();

                if (ln.Trim().ToLower().Equals("quit"))
                    break;
                else if (ln.Trim().ToLower().Equals("run"))
                {
                    try
                    {
                        Console.Write(b.ToString());
                        engine.DoString(b.ToString());
                    }
                    catch (LuaException ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                    }
                }
                else if (ln.Trim().ToLower().Equals("clear"))
                    b.Clear();
                else if (ln.Trim().ToLower().Equals("exit"))
                    return;
                else
                    b.AppendLine(ln);
            }
            showMainMenu();
        }
    }
}
