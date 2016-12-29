using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradeAutoTest.Utility
{
    public class GetUpgrade
    {
        /// <summary>
        /// 从文件中取到升级的SSW升级数据包
        /// </summary>
        /// <param name="file">文件路径+文件名</param>
        /// <returns></returns>
        public static byte[] GetSSWBody(string file)
        {
            var body = new byte[18];

            FileStream fs = File.OpenRead(file);
            var len = fs.Read(body, 1 , 12);

            if (len < 12) return new byte[] { 0 };

            body[0] = 0;
            body[13] = body[12];
            body[14] = body[11];
            body[15] = 0;
            body[16] = 0;

            int fileLength = body[2] << 8 | body[1];
            int nDesFileLength = (fileLength + (fileLength % 16 > 0 ? 16 - fileLength % 16 : 0));
            int nPackageNum = (nDesFileLength % 512 > 0 ? nDesFileLength / 512 + 1 : nDesFileLength / 512);
            body[11] = (byte)(nPackageNum / 256);
            body[12] = (byte)(nPackageNum % 256);

            byte verify = 0;
            for (int i = 0; i < 17; i++)
            {
                verify ^= body[i];
            }
            body[17] = verify;

            ByteHelper.MakeReverse(body, 1, 2);
            ByteHelper.MakeReverse(body, 3, 2);
            ByteHelper.MakeReverse(body, 5, 2);
            ByteHelper.MakeReverse(body, 7, 4);
            ByteHelper.MakeReverse(body, 13, 2);

            return body;
        }

        /// <summary>
        /// 从文件中取到升级的USW升级数据包
        /// </summary>
        /// <param name="path">文件路径+文件名</param>
        /// <param name="num">要取的包序号</param>
        /// <returns>返回USW升级数据包内容</returns>
        public static byte[] GetUSWBody(string path, int num)
        {
            byte[] body = new byte[516];

            FileStream fs = File.OpenRead(path);
            fs.Seek(12 + num * 512, SeekOrigin.Begin);
            var len = fs.Read(body, 3, 512);

            if (len < 512)
            {
                for (int i = len + 3; i < 512 + 3; i++)
                {
                    body[i] = 0xFF;
                }
            }

            body[0] = (byte)num;
            body[1] = (byte)(len / 256);
            body[2] = (byte)(len % 256);

            byte verify = 0;
            for (int i = 0; i < 17; i++)
            {
                verify ^= body[i];
            }
            body[515] = Verify(body, 515);

            return body;
        }

        /// <summary>
        /// 异或校验
        /// </summary>
        /// <param name="body">校验数组</param>
        /// <param name="len">要进行校验的长度</param>
        /// <returns></returns>
        private static byte Verify(byte[] body, int len)
        {
            byte verify = 0;
            for (int i = 0; i < len; i++)
            {
                verify ^= body[i];
            }
            return verify;
        }
    }
}
