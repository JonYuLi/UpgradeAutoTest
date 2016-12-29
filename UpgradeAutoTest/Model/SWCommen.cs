using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradeAutoTest.Model
{
    public class SWCommen
    {
        public byte[] head { get; set; }
        public byte[] body { get; set; }
        public byte[] tail { get; set; }

        public byte[] response { get; set; }

        public byte[] GetPackage()
        {
            byte[] package = new byte[head.Length + body.Length + tail.Length];

            for (int i = 0; i < head.Length; i++)
            {
                package[i] = head[i];
            }

            for (int i = 0; i < body.Length; i++)
            {
                package[i + head.Length] = body[i];
            }

            for (int i = 0; i < tail.Length; i++)
            {
                package[i + head.Length + body.Length] = tail[i];
            }

            return package;
        }
    }
}
