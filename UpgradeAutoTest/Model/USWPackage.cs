using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest.Model
{
    public class USWPackage : SWCommen
    {
        public USWPackage()
        {
            head = new byte[] { 0x55, 0x53, 0x57, 0x3A };
            tail = new byte[] { 0x45, 0x4E, 0x44 };
        }

        public byte[] GetResponse()
        {
            var tmp = ByteHelper.Add(head, new byte[] { body[0], 0x00, body[0]});
            return ByteHelper.Add(tmp, tail);
        }
    }
}
