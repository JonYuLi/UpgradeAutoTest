using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpgradeAutoTest.Comm;
using UpgradeAutoTest.Model;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--------厦门忻德485油感自动升级测试-------- V1.0 --------\n");
            Console.WriteLine("输入要测试的次数：");


            var line = Console.ReadLine();

            try
            {
                int count = Int32.Parse(line);
                for (int i = 1; i <= count; i++)
                {
                    if (ProcessUpgrade.Excute())
                    {
                        Console.WriteLine("升级成功，第 {0} 次，总共 {1} 次...", i, count);
                        CommHelper.ClearLog();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("升级失败，第 {0} 次，总共 {1} 次...", i, count);
                        Console.ResetColor();
                        CommHelper.SaveLog();

                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("异常！" + ex.Message + ex);
            }

            Console.WriteLine("\n测试结束，按任意键退出...");
            Console.ReadKey();
        }
    }
}
