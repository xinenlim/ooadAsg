using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;

namespace PRG_2_ASG
{
    class Program
    {

        public static void ReadGuest(List<Person>gList)
        {
            Person p;
            bool m;

            string[] lines = File.ReadAllLines("Guests.csv");
            for (int i = 1; i < lines.Count(); i++)
            {
                string[] data = lines[i].Split(',');
                if (data[3] == "Yes")
                {
                    m = true;
                }
                else 
                { 
                    m = false; 
                }
                p = new Person(data[0], data[1], data[2], m);
                gList.Add(p);
            }
        }
        public static void ReadResortList(List<Resort> rList)
        {
            Apartment a;
            Bungalow b;
            bool c;
            string[] lines = File.ReadAllLines("Resorts.csv");
            
            for (int i =1; i < lines.Length; i++)
            {

                string[] data = lines[i].Split(',');
                if (data[0] == "Apartment")
                {
                    if (data[6] == "Yes")
                    {
                        c = true;
                    }
                    else
                    {
                        c = false;
                    }
                    a = new Apartment(Convert.ToInt32(data[1]),data[2],Convert.ToInt32(data[3]),Convert.ToInt32(data[4]),c,Convert.ToDouble(data[6]));//convert data 
                    rList.Add(a);
                }
                else if (data[0] == "Bungalow")
                {
                    if (data[3] == "Yes")
                    {
                        c = true;
                    }
                    else
                    {
                        c = false;
                    }
                    b = new Bungalow(Convert.ToInt32(data[1]),data[2],c,Convert.ToDouble(data[6]));//convert data 
                    rList.Add(b);
                }
                
            }
        }
        public static void RegisterGuest(List<Person> gList)
        {
            Console.WriteLine("Option 3. Register Guest");

            Console.Write("Enter NRIC Number: ");
            string n = Console.ReadLine();

            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Tel No: ");
            string tel = Console.ReadLine();

            Console.Write("Enrol as a Member (Enter 1 for Yes, 0 for No)?");
            int m = Convert.ToInt32(Console.ReadLine());
        
            Person a = new Person(n, name, tel, Convert.ToBoolean(m));
            gList.Add(a);
            string c;
            if (a.Member == true)
            {
                c = "Yes";
            }
            else
            {
                c = "No";
            }
            using (StreamWriter sw = new StreamWriter("Guests.csv", true))
            {
                 sw.WriteLine(a.Nric+","+a.Name+","+a.Tel+","+c);
                
            }
            Console.WriteLine("~Registration Successful~");
        }
        public static void Menu() 
        {
            Console.WriteLine("==MENU==");
            Console.WriteLine("1.List all guests");
            Console.WriteLine("2.List all resorts");
            Console.WriteLine("3.Register guest");
            Console.WriteLine("4.Rent a resort");
            Console.WriteLine("5.List all rental details for a guest");
            Console.WriteLine("6.List all rental details for bungalows or apartments");
            Console.WriteLine("0.Exit");
            Console.WriteLine("-----------------------");

        }
        static void DisplayGuests(List<Person> guestList)
        {
            //Console.WriteLine("Option 1. List all guest");
            Console.WriteLine("{0,-10}{1,5}{2,10}{3,11}{4,15}", "S/No", "NRIC Number", "Name", "Tel", "Member");
            string a;
            for (int i = 0; i < guestList.Count; i++)
            {
                if (guestList[i].Member == true)
                {
                    a = "Yes";
                }
                else
                {
                    a = "No";
                }
                Console.WriteLine("{0,-10}{1,5}{2,13}{3,13}{4,12}", i + 1, guestList[i].Nric, guestList[i].Name, guestList[i].Tel, a);
            }
        }

        static void DisplayResort(List<Resort> rlist) 
        {
            Console.WriteLine("{0,-5}{1,5}{2,10}{3,11}{4,17}{5,15}{6,15}", "S/No", "Resort ID", "Block", "Level", "Unit Number", "Pool/Seaview", "Rate($)");
            for (int i = 0; i < rlist.Count; i++)
            {
                Resort r = rlist[i];
                if (r is Apartment)
                {
                    Apartment a = (Apartment)r;
                    Console.WriteLine("{0,-5}{1,5}{2,15}{3,9}{4,13}{5,15}{6,17}", i + 1, a.ResortId, a.Block, a.Level, a.UnitNo,a.Seaview,a.Rate) ;
                }
                else
                {
                    Bungalow b = (Bungalow)r;
                    Console.WriteLine("{0,-5}{1,5}{2,15}{3,9}{4,13}{5,15}{6,17}", i + 1, b.ResortId, b.Block, " ", " ", b.Pool, b.Rate);
                }
            }
        }

        static void RentResort(int rentalNo, List<Resort> resortList, List<Person> guestList, List<Rental> rentalList)
        {
            Console.WriteLine("Rent resort");
            DisplayGuests(guestList);
            Console.Write("Enter S/No of Guest:");
            int sn = Convert.ToInt32(Console.ReadLine());
            // display resort 
            DisplayResort(resortList);
            Console.Write("Enter S/No of Resort:");
            int snResort = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Arrival Date: ");
            DateTime arrival = Convert.ToDateTime(Console.ReadLine());

            Console.Write("Enter Departure Date: ");
            DateTime departure = Convert.ToDateTime(Console.ReadLine());

            //create rental object based on resort and guest
            //person guest ==
            Person guest = guestList[sn - 1];
            Resort resort = resortList[snResort - 1];
            Rental r = new Rental(rentalNo, guest, arrival, departure,resort);
            //add to rental list 
            rentalList.Add(r);
            //guest . add rental
            guest.AddRental(r);

            Console.WriteLine("~Process Successful~");
        }

        static void ListRentDetailGuest(List<Person>guestList,List<Rental>rentalList,List<Resort>resortList) 
        {
            Console.WriteLine("Option 5. List Rental Detail for Guest");
            DisplayGuests(guestList);
            Console.Write("Enter S/No of Guest:");
            int sn = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < rentalList.Count; i++)
            {
                Console.WriteLine("{0,-10}{1,10}{2,13}{3,12}{4,15}{5,15}", "Resort No", "Block", "Resort ID","Guest", "Arrival", "Depature");
                Console.WriteLine("{0,-14}{1,7}{2,11}{3,12}{4,15}{5,15}",rentalList[i].RentalNo, rentalList[i].Resort.Block, rentalList[i].Resort.ResortId,rentalList[i].Guest.Name, rentalList[i].ArrivalDate.ToString("dd/MM/yyyy"), rentalList[i].DepartureDate.ToString("dd/MM/yyyy"));
            }
        }


        static void ListRentDetailResort(List<Rental> rentalList, List<Resort> resortList)
        {
            Console.WriteLine("Option 6. List Rental Detail for bungalows or apartments");
            Console.Write("Enter 1 for apartment, 2 for Bungalows: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            if (choice == 1)
            {
                Console.WriteLine("Rentals for Apartment: ");
                Console.WriteLine("{0,-10}{1,10}{2,13}{3,12}{4,15}{5,15}{6,15}{7,15}", "Resort ID", "Block", "Unit No", "Level", "Rental No", "Guest", "Arrival", "Depature");
                for (int i = 0; i < rentalList.Count; i++)
                {
                    Resort r = rentalList[i].Resort;
                    if (r is Apartment)
                    {
                        Apartment a = (Apartment)r;
                        Console.WriteLine("{0,-10}{1,10}{2,10}{3,15}{4,16}{5,16}{6,16}{7,16}", a.ResortId, a.Block, a.UnitNo, a.Level, rentalList[i].RentalNo, rentalList[i].Guest.Name,
                            rentalList[i].ArrivalDate.ToString("dd/MM/yyyy"), rentalList[i].DepartureDate.ToString("dd/MM/yyyy"));
                    }
                }
            }


            else
            {
                Console.WriteLine("Rentals for Bungalow: ");
                Console.WriteLine("{0,-10}{1,10}{2,13}{3,12}{4,15}{5,15}", "Resort ID", "Block", "Rental No", "Guest", "Arrival", "Depature");
                for (int i = 0; i < rentalList.Count; i++)
                {
                    Resort r = rentalList[i].Resort;
                    if (r is Bungalow)
                    {
                        Bungalow b = (Bungalow)r;
                        Console.WriteLine("{0,-10}{1,10}{2,10}{3,15}{4,16}{5,16}", b.ResortId, b.Block, rentalList[i].RentalNo, rentalList[i].Guest.Name,
                            rentalList[i].ArrivalDate.ToString("dd/MM/yyyy"), rentalList[i].DepartureDate.ToString("dd/MM/yyyy"));
                    }
                }
            }

        }









        static void Main(string[] args)
        {
            List<Person> guestList = new List<Person>();// create method for guest 
            List<Resort> resortList = new List<Resort>();
            List<Rental> rentalList = new List<Rental>();
            ReadGuest(guestList);
            ReadResortList(resortList);
            int rentalNo = 0;
            
            while (true)
            {
                
                Menu();
                Console.Write("Enter your option:");
                int option = Convert.ToInt32(Console.ReadLine());
                if (option == 0)
                {
                    break;
                }

                else if (option == 1)
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Option 1. List all guest");
                    DisplayGuests(guestList);
                }
                else if (option == 2)
                {
                    Console.WriteLine("-----------------------");
                    DisplayResort(resortList);
                }
                else if (option == 3)
                {
                    Console.WriteLine("-----------------------");
                    RegisterGuest(guestList);
                }
                else if (option == 4)
                {
                    Console.WriteLine("-----------------------");
                    rentalNo++;
                    RentResort(rentalNo,resortList, guestList,rentalList);
                }

                else if (option == 5) 
                {
                    Console.WriteLine("-----------------------");
                    ListRentDetailGuest( guestList, rentalList,resortList);
                }

                else if (option == 6) 
                {

                    Console.WriteLine("-----------------------");
                    ListRentDetailResort(rentalList, resortList);

                }

                else if (option == 7)
                {
                 
                }

                else
                {
                    Console.WriteLine("-----------------------");
                    Console.WriteLine("Invalid option. Please try again");
                }

            }

        }
    }
}