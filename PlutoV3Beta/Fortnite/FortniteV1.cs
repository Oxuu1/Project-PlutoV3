using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using PlutoV3Beta.Fortnite;

namespace PlutoV3Beta
{
    // Token: 0x02000006 RID: 6
    public static class FortniteV1
    {
        // Token: 0x06000017 RID: 23 RVA: 0x00002CD8 File Offset: 0x00000ED8
        public static void Launch(string path, string username)
        {
            string shipping = "/FortniteGame/Binaries/Win64/FortniteClient-Win64-Shipping.exe";
            string args = "-log -epicapp=Fortnite -epicenv=Prod -epiclocale=en-us -epicportal -skippatchcheck -nobe -fromfl=eac -fltoken=3db3ba5dcbd2e16703f3978d -AUTH_LOGIN=" + username + "@fortnite.com -AUTH_PASSWORD=FortniteLauncher -AUTH_TYPE=epic";
            Process FN = new Process();
            string fullpath = path + shipping;
            FN.StartInfo.Arguments = args;
            FN.StartInfo.FileName = fullpath;
            FN.StartInfo.UseShellExecute = false;
            FN.StartInfo.RedirectStandardOutput = true;
            FN.Start();
            int id = FN.Id;
            string Backend = Directory.GetCurrentDirectory() + "\\PlutoV3.dll";
            PlutoInjector.Inject(id, Backend);
            for (; ; )
            {
                string Output = FN.StandardOutput.ReadLine();
                bool init = Output.Contains("Game Engine Initialized");
                bool flag = init;
                if (flag)
                {
                    string console = Directory.GetCurrentDirectory() + "\\Framework.dll";
                    string Leaker = Directory.GetCurrentDirectory() + "\\MemoryLeaker.dll";
                    Thread.Sleep(3000);
                    PlutoInjector.Inject(id, console);
                    PlutoInjector.Inject(id, Leaker);
                }
            }
        }
        // Token: 0x06000018 RID: 24 RVA: 0x00002DBC File Offset: 0x00000FBC



        // Token: 0x06000019 RID: 25
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(IntPtr hHandle);

        // Token: 0x0600001A RID: 26
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        // Token: 0x0600001B RID: 27
        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        // Token: 0x0600001C RID: 28
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string lpModuleName);

        // Token: 0x0600001D RID: 29
        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr VirtualAllocEx(IntPtr hProcess, IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);

        // Token: 0x0600001E RID: 30
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out UIntPtr lpNumberOfBytesWritten);

        // Token: 0x0600001F RID: 31
        [DllImport("kernel32.dll")]
        public static extern IntPtr CreateRemoteThread(IntPtr hProcess, IntPtr lpThreadAttributes, uint dwStackSize, IntPtr lpStartAddress, IntPtr lpParameter, uint dwCreationFlags, IntPtr lpThreadId);

        // Token: 0x06000020 RID: 32
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(FortniteV1.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        // Token: 0x06000021 RID: 33
        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);

        // Token: 0x06000022 RID: 34
        [DllImport("kernel32.dll")]
        private static extern int ResumeThread(IntPtr hThread);

        // Token: 0x06000023 RID: 35 RVA: 0x00002E60 File Offset: 0x00001060
        public static void SuspendProcess(this Process process)
        {
            foreach (object obj in process.Threads)
            {
                ProcessThread thread = (ProcessThread)obj;
                IntPtr pOpenThread = FortniteV1.OpenThread(FortniteV1.ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);
                bool flag = pOpenThread == IntPtr.Zero;
                if (flag)
                {
                    break;
                }
                FortniteV1.SuspendThread(pOpenThread);
            }
        }

        // Token: 0x02000008 RID: 8
        [Flags]
        public enum ThreadAccess
        {
            // Token: 0x04000012 RID: 18
            TERMINATE = 1,
            // Token: 0x04000013 RID: 19
            SUSPEND_RESUME = 2,
            // Token: 0x04000014 RID: 20
            GET_CONTEXT = 8,
            // Token: 0x04000015 RID: 21
            SET_CONTEXT = 16,
            // Token: 0x04000016 RID: 22
            SET_INFORMATION = 32,
            // Token: 0x04000017 RID: 23
            QUERY_INFORMATION = 64,
            // Token: 0x04000018 RID: 24
            SET_THREAD_TOKEN = 128,
            // Token: 0x04000019 RID: 25
            IMPERSONATE = 256,
            // Token: 0x0400001A RID: 26
            DIRECT_IMPERSONATION = 512
        }
    }
}