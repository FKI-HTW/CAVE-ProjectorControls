using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaveCalibrator.Util
{
    public class MathUtil
    {
        public static int Mod(int value, int mod)
        {
            return (value % mod + mod) % mod;
        }
    }
}
