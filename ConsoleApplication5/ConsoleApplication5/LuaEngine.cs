/*
 * LuaEngine.cs
 * 
 * The LuaEngine class extends the functionality of the Lua class provided by the LuaInterface. The extended functionality of this
 * class includes registering many Lua functions at once through the use of reflection (getting information about methods).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using LuaInterface;

namespace ConsoleApplication5
{
   /*
    * Registers all methods of a particular object (usually a class instance) in Lua
    * This method binds the Lua function name to the C# method that it's supposed to call upon execution
    * 
    * objTarget - the object containing LuaFunc attributes (Lua function -> C# method binding)
    * 
    * Exanple:
    *   [LuaFunc("myFunctionName")]
    *   public void foobar() 
    *   {
    *       Console.WriteLine("You called the C# method foobar() !");
    *   }
    *   If you called myFunctionName() from the Lua file and executed it
    *   the console would call foobar() resulting in the console printing:
    *   You called the C# method foobar() !
    *   
    */
    class LuaEngine : Lua
    {
        public LuaEngine()
        {
            init();
        }

        private void init()
        {
            // this might configure global variables for example
            this["luaVersion"] = "LuaInterface - Lua version 5.1";
        }

        public void registerLuaFunctions(Object objTarget)
        {
            Type tTarget = objTarget.GetType();

            foreach (MethodInfo methInfo in tTarget.GetMethods())
            {
                foreach (Attribute attr in Attribute.GetCustomAttributes(methInfo))
                {
                    // if the method has an attribute that is of type LuaFunc then we want to register the method in Lua allowing
                    // that C# method to be called by calling the function name, the content of lFunc, in Lua
                    if (attr is LuaFunc)
                    {
                        LuaFunc lFunc = attr as LuaFunc;
                        Trace.WriteLine("Registering exposed Lua function: " + methInfo + " as " + lFunc.getFunctionName());
                        RegisterFunction(lFunc.getFunctionName(), objTarget, methInfo);
                    }
                }
            }
        }
    }
}
