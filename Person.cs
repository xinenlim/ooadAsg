using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRG_2_ASG
{
    class Person
    {
        public string Nric { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public bool Member { get; set; }
        public List<Rental> RentalList { get; set; }

        public Person(string nric,string name,string tel,bool member)
        {
            Nric = nric;
            Name = name;
            Tel = tel;
            Member = member;
            RentalList = new List<Rental>();
        }

        public void AddRental(Rental a)
        {
            RentalList.Add(a);
        }

        public override string ToString()
        {
            return "Nric: " + Nric + "\tName: " + Name + "\tTel: " + Tel + "Member: " + Member;
        }


    }
}
