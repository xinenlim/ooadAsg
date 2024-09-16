using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_ASG
{
    abstract class Resort
    {
        public int ResortId { get; set; }
        public string Block { get; set; }
        public double Rate { get; set; }

        public Resort (int id, string block,double rate)
        {
            ResortId = id;
            Block = block;
            Rate = rate;
        }

        public abstract double ComputeResortCost(int day);
        public override string ToString()
        {
            return "Resort ID: " + ResortId + "\tBlock: " + Block + "\tRate: " + Rate;
        }
    }
}