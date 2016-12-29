using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest.Model
{
    public class QSWPackage : SWCommen
    {
        public QSWPackage()
        {
            head = new byte[] { 0x51, 0x53, 0x57, 0x3A };
            tail = new byte[] { 0x45, 0x4E, 0x44 };
            body = new byte[] { 0x00 };
        }

        public byte[] GetResponse()
        {
            byte[] resBody = new byte[25];

            var tmp = ByteHelper.Add(head, resBody);
            return ByteHelper.Add(tmp, tail);
        }
    }
}
