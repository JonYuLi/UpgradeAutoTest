using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpgradeAutoTest.Utility;

namespace UpgradeAutoTest.Model
{
    public class UpgradeRepository
    {
        public SSWPackage ssw { get; private set; }
        public List<USWPackage> uswList { get; private set; }
        public QSWPackage qsw { get; private set; }

        public UpgradeRepository(string path)
        {
            ssw = new SSWPackage();
            ssw.body = GetUpgrade.GetSSWBody(path);

            int nPackageNum = ssw.body[11] << 8 | ssw.body[12];

            uswList = new List<USWPackage>();

            for (int i = 0; i < nPackageNum; i++)
            {
                USWPackage usw = new USWPackage();
                usw.body = GetUpgrade.GetUSWBody(path, i);
                uswList.Add(usw);
            }

            qsw = new QSWPackage();
        }
    }
}
