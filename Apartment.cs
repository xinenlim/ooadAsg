using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_ASG
{
    class Apartment : Resort
    {
        public int Level { get; set; }
        public int UnitNo { get; set; }
        public bool Seaview { get; set; }

        public Apartment(int id,string block,int lvl,int unitno,bool seaview, double rate) :base(id,block,rate)
        {
            Level = lvl;
            UnitNo = unitno;
            Seaview = seaview;
        }

        public override double ComputeResortCost(int day)
        {
            if (Seaview == true)
            {
                return (Rate * day) + ((Level - 1) * 8* day) + (day * 12);
            }
            else
            {
                return (Rate * day) + ((Level - 1) * 8* day );
            }
           
        }

        public override string ToString()
        {
            return base.ToString() + "Level: " + Level + "\tUnitNo: " + UnitNo + "Seaview: " + Seaview;
        }
    }
}
