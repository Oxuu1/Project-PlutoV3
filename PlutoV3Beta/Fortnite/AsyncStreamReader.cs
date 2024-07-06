using System;
using System.Diagnostics;
using System.IO;

namespace PlutoV3Beta
{
    // Token: 0x02000007 RID: 7
    public class AsyncStreamReader
    {
        // Token: 0x14000001 RID: 1
        // (add) Token: 0x06000024 RID: 36 RVA: 0x00002EE4 File Offset: 0x000010E4
        // (remove) Token: 0x06000025 RID: 37 RVA: 0x00002F1C File Offset: 0x0000111C
        public event AsyncStreamReader.EventHandler<string> DataReceived;

        // Token: 0x17000005 RID: 5
        // (get) Token: 0x06000026 RID: 38 RVA: 0x00002F51 File Offset: 0x00001151
        // (set) Token: 0x06000027 RID: 39 RVA: 0x00002F59 File Offset: 0x00001159
        public bool Active { get; private set; }

        // Token: 0x06000028 RID: 40 RVA: 0x00002F62 File Offset: 0x00001162
        public AsyncStreamReader(StreamReader reader)
        {
            this._reader = reader;
            this.Active = false;
        }

        // Token: 0x06000029 RID: 41 RVA: 0x00002F8C File Offset: 0x0000118C
        public void Start()
        {
            bool flag = !this.Active;
            if (flag)
            {
                this.Active = true;
                this.BeginReadAsync();
            }
        }

        // Token: 0x0600002A RID: 42 RVA: 0x00002FB8 File Offset: 0x000011B8
        public void Stop()
        {
            this.Active = false;
        }

        // Token: 0x0600002B RID: 43 RVA: 0x00002FC4 File Offset: 0x000011C4
        protected void BeginReadAsync()
        {
            bool active = this.Active;
            if (active)
            {
                this._reader.BaseStream.BeginRead(this._buffer, 0, this._buffer.Length, new AsyncCallback(this.ReadCallback), null);
            }
        }

        // Token: 0x0600002C RID: 44 RVA: 0x0000300C File Offset: 0x0000120C
        private void ReadCallback(IAsyncResult asyncResult)
        {
            bool flag = this._reader == null;
            if (!flag)
            {
                int num = this._reader.BaseStream.EndRead(asyncResult);
                string data = null;
                bool flag2 = num > 0;
                if (flag2)
                {
                    data = this._reader.CurrentEncoding.GetString(this._buffer, 0, num);
                }
                else
                {
                    this.Active = false;
                }
                AsyncStreamReader.EventHandler<string> dataReceived = this.DataReceived;
                if (dataReceived != null)
                {
                    dataReceived(this, data);
                }
                this.BeginReadAsync();
            }
        }

        // Token: 0x0400000F RID: 15
        protected readonly byte[] _buffer = new byte[4096];

        // Token: 0x04000010 RID: 16
        private StreamReader _reader;

        // Token: 0x02000009 RID: 9
        // (Invoke) Token: 0x0600002E RID: 46
        public delegate void EventHandler<args>(object sender, string data);
    }
}