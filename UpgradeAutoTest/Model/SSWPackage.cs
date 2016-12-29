using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest.Model
{
    public class SSWPackage : SWCommen
    {
        public SSWPackage()
        {
            head = new byte[] { 0x53, 0x53, 0x57, 0x3A };
            tail = new byte[] { 0x45, 0x4E, 0x44 };
        }

        public byte[] GetResponse()
        {
            var tmp =  ByteHelper.Add(head, new byte[]{ 0x00, 0x00});
            return ByteHelper.Add(tmp, tail);
        }
    }
}
