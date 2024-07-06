using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlutoV3Beta.Fortnite
{
    internal class PlutoInjector
    {
        public static void Inject(int procId, string path)
        {
            bool flag = !File.Exists(path);
            if (flag)
            {
                MessageBox.Show("DLL Not Found! Please make sure Both Framework.dll and PlutoV3.dll exists inside the folder!");
            }
            else
            {
                IntPtr handle = FortniteV1.OpenProcess(1082, false, procId);
                IntPtr loadLib = FortniteV1.GetProcAddress(FortniteV1.GetModuleHandle("kernel32.dll"), "LoadLibraryA");
                uint size = (uint)((path.Length + 1) * Marshal.SizeOf(typeof(char)));
                IntPtr address = FortniteV1.VirtualAllocEx(handle, IntPtr.Zero, size, 12288U, 4U);
                UIntPtr bytesWritten;
                FortniteV1.WriteProcessMemory(handle, address, Encoding.Default.GetBytes(path), size, out bytesWritten);
                FortniteV1.CreateRemoteThread(handle, IntPtr.Zero, 0U, loadLib, address, 0U, IntPtr.Zero);
            }
        }

    }
}
