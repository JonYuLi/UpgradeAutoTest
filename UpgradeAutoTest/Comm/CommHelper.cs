using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Configuration;
using System.Threading;
using UpgradeAutoTest.Utility;
using System.IO;

namespace UpgradeAutoTest.Comm
{
    public class CommHelper
    {
        static SerialPort sp;
        public static byte[] recvBuff;
        public static int recvLen;
        public static List<byte[]> log;

        public static bool Open()
        {
            string comm = ConfigurationManager.AppSettings["comm"];
            string baudrate = ConfigurationManager.AppSettings["baudrate"];

            if (sp == null)
            {
                sp = new SerialPort(comm, Int32.Parse(baudrate));
                sp.Open();
                sp.DataReceived += Sp_DataReceived;
                recvBuff = new byte[20000];
                recvLen = 0;
                log = new List<byte[]>();
            }
            return sp.IsOpen;
        }

        public static void Send(byte[] send)
        {
            sp.Write(send, 0, send.Length);
            log.Add(send);
        }

        private static void Sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            int len = sp.BytesToRead;
            byte[] rec = new byte[len];
            sp.Read(rec, 0, len);
            log.Add(rec);
            ByteHelper.Append(recvBuff, recvLen, rec, len);
            recvLen += len;
        }

        public static void ClearBuff()
        {
            for (int i = 0; i < recvLen; i++)
            {
                recvBuff[i] = 0;
            }
            recvLen = 0;
        }

        public static void SaveLog()
        {
            string name = DateTime.Now.ToString("yyMMddHHMMss");
            FileStream fs = File.OpenWrite(name);
            foreach (Byte[] bs in log)
            {
                fs.Write(bs, 0, bs.Length);
            }
            fs.Close();
        }

        public static void ClearLog()
        {
            log.Clear();
        }
    }
}
