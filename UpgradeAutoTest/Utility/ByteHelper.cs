using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradeAutoTest.Utility
{
    public class ByteHelper
    {
        public static byte[] Add(byte[] b1, byte[] b2)
        {
            byte[] ret = new byte[b1.Length + b2.Length];

            for (int i = 0; i < b1.Length; i++)
            {
                ret[i] = b1[i];
            }

            for (int i = 0; i < b2.Length; i ++)
            {
                ret[i + b1.Length] = b2[i];
            }

            return ret;
        }

        public static void Append(byte[] src, int offset, byte[] dec, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (offset + i >= src.Length) return;
                src[offset + i] = dec[i];
            }
        }

        public static void MakeReverse(byte[] source, int offset, int len)
        {
            byte[] tmp = new byte[len];

            for (int i = 0; i < len; i++)
            {
                tmp[i] = source[offset + len - i - 1];
            }

            for (int i = 0; i < len; i++)
            {
                source[offset + i] = tmp[i];
            }
        }

        public static bool Search(byte[] src, byte[] dec, int len)
        {
            for (int i = 0; i < src.Length - len + 1; i++)
            {
                if (src[i] == dec[0])
                {
                    if (Compare(src, i, dec, len))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
   

        public static bool Compare(byte[] src, int offset, byte[] dec, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (src[i + offset] != dec[i]) return false;
            }
            return true;
        }

        public static void ConsoleWriteByteArray(byte[] array, int len)
        {
            Console.WriteLine();
            for (int i = 0; i < len; i++)
            {
                Console.Write(array[i].ToString("X2") + " ");
            }
            Console.WriteLine();
        }
    }
}
