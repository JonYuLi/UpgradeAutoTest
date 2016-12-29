using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UpgradeAutoTest.Model;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest.Comm
{
    public class ProcessUpgrade
    {
        static UpgradeRepository ur;
        static bool flag;

        /// <summary>
        /// 执行一次升级
        /// </summary>
        public static bool Excute()
        {
            string file1 = flag ? ConfigurationManager.AppSettings["file1"] : ConfigurationManager.AppSettings["file2"];
            if (flag)
                flag = false;
            else
                flag = true;
            Console.WriteLine("升级文件：" + file1);
            ur = new UpgradeRepository(file1);

            CommHelper.Open();

            int i;
            for (i = 0; i < 5; i++)
            {
                if (SendSSW(ur.ssw)) break;
            }

            if (i >= 5)
            {
                return false;
            }

            foreach (USWPackage usw in ur.uswList)
            {
                Console.Write(".");
                for (i = 0; i < 5; i++)
                {
                    if (SendUSW(usw)) break;
                }

                if (i >= 5)
                {
                    return false;
                }
            }
            Console.WriteLine();

            //普通升级
            //Thread.Sleep(10000);

            //死机代码
            //byte[] did = new byte[] { 0xF5, 0x40, 0x04, 0x20, 0x02, 0x01, 0x01, 0x3D };
            //for (int k = 0; k < 1000; k++)
            //{
            //    CommHelper.Send(did);
            //    Thread.Sleep(10);
            //}

            //smart模拟
            byte[] did = new byte[] { 0xF5, 0x40, 0x04, 0x20, 0x02, 0x01, 0x01, 0x3D };
            for (int k = 0; k < 400; k++)
            {
                CommHelper.Send(did);
                Thread.Sleep(30);
            }

            CommHelper.Send(ur.qsw.GetPackage());

            Thread.Sleep(500);

            CommHelper.Send(ur.qsw.GetPackage());

            Thread.Sleep(1000);

            QSWResult qswResult = new QSWResult(CommHelper.recvBuff);
            if (qswResult.result == 0 && qswResult.status == 5)
                return true;

            Console.WriteLine(qswResult.result);
            Console.WriteLine(qswResult.status);
            ByteHelper.ConsoleWriteByteArray(CommHelper.recvBuff, CommHelper.recvLen);
            return false;
        }

        private static bool SendSSW(SSWPackage ssw)
        {
            CommHelper.Send(ssw.GetPackage());
            int tmout = 0;
            while (CommHelper.recvLen <= 8 && tmout < 1000)
            {
                Thread.Sleep(1);
                tmout++;
            }
            if (tmout >= 1000) return false;
            var ret =  ByteHelper.Search(CommHelper.recvBuff, ssw.GetResponse(), 9);
            CommHelper.ClearBuff();
            return ret;
        }

        private static bool SendUSW(USWPackage usw)
        {
            CommHelper.Send(usw.GetPackage());
            int tmout = 0;
            while (CommHelper.recvLen <= 8 && tmout < 1000)
            {
                Thread.Sleep(1);
                tmout++;
            }
            if (tmout >= 1000) return false;
            var ret = ByteHelper.Search(CommHelper.recvBuff, usw.GetResponse(), 10);
            CommHelper.ClearBuff();
            return ret;
        }
    }
}
