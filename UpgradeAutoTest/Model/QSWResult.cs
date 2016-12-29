using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpgradeAutoTest.Model
{
    public class QSWResult
    {
        public bool valid { get; set; }
        public int result { get; set; }
        public int status { get; set; }
        public bool isSuccess { get; set; }

        public QSWResult(byte[] source)
        {
            valid = false;
            for (int i = 0; i < source.Length - 31; i ++)
            {
                if (source[i] == 'Q' && source[i + 1] == 'S' && source[i + 2] == 'W' &&
                    source[i + 29] == 'E' && source[i + 30] == 'N' && source[i + 31] == 'D')
                {
                    status = source[4 + i];
                    result = source[25 + i];
                    valid = true;
                    return;
                }
            }
        }
    }
}
