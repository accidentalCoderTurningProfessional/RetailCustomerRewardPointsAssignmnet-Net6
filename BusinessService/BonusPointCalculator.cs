using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public class BonusPointCalculator
    {
        public decimal GetBonusPoint(decimal Amount)
        {
            decimal bonusPoint = 0;
            bonusPoint =  (Amount-50) >0 ? (Amount-50)*1 : bonusPoint;
            bonusPoint = (Amount - 100) > 0 ? bonusPoint + (Amount - 100) * 1 : bonusPoint;
            return bonusPoint;
        }
    }
}
