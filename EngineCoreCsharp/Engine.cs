using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EngineCoreCsharp
{
    public class InvalidEngineException : Exception
    {

        public InvalidEngineException() { }

        public InvalidEngineException(string message)
            : base(message) { }

        public InvalidEngineException(string message, Exception inner)
            : base(message, inner) { }

    }

    public static class Helper
    {
        const string DllPath = "EngineCoreCpp64";

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern MySafeHandle CreateEngine(uint pid, uint[] offsets, uint base_address);

        [DllImport(DllPath, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool ActivateCheat(MySafeHandle engine);

        public static MySafeHandle CreateEngineWrapper(uint pid, uint[] offsets, uint base_address)
        {
            try
            {
                MySafeHandle engine = CreateEngine(pid, offsets, base_address);
                if (engine.IsInvalid)
                {
                    throw new InvalidEngineException("Invalid pointer to engine");
                }
                return engine;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in CreateEngine: " + ex.Message);
                throw;
            }
        }

        public static bool ActivateCheatWrapper(MySafeHandle engine)
        {
            try
            {
                bool result = ActivateCheat(engine);
                if (!result)
                {
                    Console.WriteLine("Error: Failed to activate cheat");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in ActivateCheat: " + ex.Message);
                throw;
            }
        }
    }
    public class MySafeHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        const string path = "EngineCoreCpp64";

        [DllImport(path, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool DestroyEngine(IntPtr engine);

        public MySafeHandle(nint handle, bool ownsHandle) : this(ownsHandle)
        {
            SetHandle(handle);
        }

        public MySafeHandle() : this(true)
        { }


        private MySafeHandle(bool ownsHandle) : base(ownsHandle)
        { }

        override protected bool ReleaseHandle()
        {
            return DestroyEngine(handle);
        }
    }

    public static class Engine
    {
        static MySafeHandle engine;
        public static bool ActivateCheat(uint[] offsets, string processName)
        {
            uint pid;
            uint base_address;
            Process? processes = Process.GetProcessesByName(processName).FirstOrDefault();
            if (processes != null)
            {
                pid = (uint)processes.Id;
                base_address = (uint)processes.MainModule.BaseAddress.ToInt32();
            }
            else
            {
                Console.WriteLine("No process with the name " + processName + " was found.");
                return false;
            }
            engine = Helper.CreateEngineWrapper(pid, offsets, base_address);
            return Helper.ActivateCheatWrapper(engine);
        }

        public static void DeactivateCheat()
        {
            engine.Dispose();
        }
    }
}
