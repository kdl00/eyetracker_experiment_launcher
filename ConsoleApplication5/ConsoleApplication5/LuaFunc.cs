/*
 * LuaFunc.cs
 * 
 * This class contains the defition for the LuaFunc attributes used in binding C# methods to Lua functions or vice versa.
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication5
{
    class LuaFunc : Attribute
    {
        // Name of the function in the Lua file
        private String functionName;

        public LuaFunc(String strFunctionName)
        {
            functionName = strFunctionName;
        }

        public String getFunctionName()
        {
            return functionName;
        }
    }
}
