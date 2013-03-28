using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using LuaInterface;
using System.Threading;
namespace ConsoleApplication5
{
    public class Program
    {
        private LuaEngine luaEng;
       
        static void Main(string[] args)
        {
            Program p = new Program();
            
            p.run(); // have to do this because static methods can't access non-static fields (luaEng) without a reference to an object (Program object)

        }

        public void run()
        {
            luaEng = new LuaEngine();
            luaEng.registerLuaFunctions(this); // binds lua function say() to the printSaying() below
            LuaInterpreter interp = new LuaInterpreter(luaEng);
            
        }
       
       
       

        // This method can be called by a Lua script
        [LuaFunc("say")]
        public void printSaying(string msg)
        {
            Console.WriteLine(msg);
        }

       
    }
}
