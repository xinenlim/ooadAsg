using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_ASG
{
    class Bungalow : Resort
    {
        public bool Pool { get; set; }

        public Bungalow(int id,string block,bool pool, double rate) :base(id,block,rate)
        {
            Pool = pool;
        }
        public override double ComputeResortCost(int day)
        {
            if (Pool == true)
            {
                return (day*Rate)+(day*10.5);
            }
            else
            {
                return (day * Rate);
            }
        }

        public override string ToString()
        {
            return base.ToString()+"Pool :"+ Pool;
        }
    }
}