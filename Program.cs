using System;
using System.IO;
using System.Text.RegularExpressions;
using PersonalIdentityNumberSweden;
using SocialSecurityNumberSweden;

namespace PersonalIdentityNumberSweden
{
    class Program
    {
        static void Main(string[] args)
        {
            bool clientAnswer = false;
            do
            {
                string firstName = args[0];
                string lastName = args[1];
                string SSNumber = args[2];
            
                if (args.Length > 0)
                {
                    Console.WriteLine(" You have already entered this information:\n Name    : {0} {1}\n SSNumber: {2}", firstName, lastName, SSNumber);
                }
                else
                {
                    askClient(out firstName, out lastName, out SSNumber);
                }

                if (SSNumber.Length != 13)
                {
                    Console.Clear();
                    askClient(out firstName, out lastName, out SSNumber);
                }
            
                do
                {
                    Console.Clear();
                    bool isMyIformation = false;
                    bool bla = true;
                    Console.Write($@" 
                You have already entered this information:

                Name: {firstName} {lastName} 
                Social Security Number: {SSNumber} 

                Would you verify this informations? Yes/No : ");
                    string Answer = Console.ReadLine();
                    switch (Answer.ToUpper())
                    {
                        case "YES":
                        case "Y":
                            clientAnswer = isMyIformation;
                            break;
                        case "NO":
                        case "N":
                            Console.Clear();
                            askClient(out firstName, out lastName, out SSNumber);
                            break;                            
                        default:
                            Console.WriteLine("\n\t\t Your answer is invalid. Please try again...");
                            Console.Beep(300, 800);                            
                            clientAnswer = bla;
                            break;
                            //goto UserVerification;
                    }
                } while (clientAnswer);
                // In this stage try to verify the data insert to the system. 
                //It is important to manage the data from the beginning.
                //Split data to Birthday and Security Parts

                bool verify = true;
                string[] SSNumbersSplit = SSNumber.Split("-");
                if (SSNumber.Length != 13)
                {
                    Console.WriteLine("Invalid SSnumber. Pleas try again! ");
                    askClient(out firstName, out lastName, out SSNumber);
                }
                
                if (SSNumbersSplit[0].Length == 8)
                {
                    SSNumbersSplit[0] = SSNumbersSplit[0].Insert(4, "-");
                    SSNumbersSplit[0] = SSNumbersSplit[0].Insert(7, "-");
                }
                else
                {
                    askClient(out firstName, out lastName, out SSNumber);
                }

                try
                {
                    Convert.ToDateTime(SSNumbersSplit[0]);
                    Convert.ToInt32(SSNumbersSplit[1]);
                }
                catch
                {
                    Console.WriteLine("\n ::::::::::::::: Exception :::::::::::::");
                    Console.WriteLine("\n   Invalid SSnumber. Pleas try again!  \n");
                    Console.WriteLine(" :::::::::::::::::::::::::::::::::::::::\n");
                    askClient(out firstName, out lastName, out SSNumber);
                }
                Console.WriteLine(" ===============================================================");

                VerifySSN.verifySSN(SSNumber, out verify);
                if (verify == false) { askClient(out firstName, out lastName, out SSNumber); }
                //End of Verification

                DateTime birthDay = Convert.ToDateTime(SSNumbersSplit[0]);

                DateTime SecDate = DateTime.Today;

                int age = int.Parse((DateTime.Today - birthDay).TotalDays.ToString());
                int ageYear = age / 365;
                int ageMonth = 0;
                int ageDay = 0;

                if (age % 365 > 30)
                {
                    ageMonth = (int)(age % 365.25) / 30;
                    ageDay = (int)(age % 365.25 % 30);
                }
                else
                {
                    ageDay = (int)(age % 365.25 % 30);
                }
                
                int birthYear = Convert.ToDateTime(SSNumbersSplit[0]).Year;
                string gender = Convert.ToInt32(SSNumbersSplit[1][2].ToString()) % 2 == 0 ? "Female" : "Male";

                Console.Clear();
                Console.WriteLine($@" 
                
                       Social Scurity Number (SWEDEN)
                       Information about {firstName} {lastName} {SSNumber} 
                =====================================================
                 Name                  : {firstName} {lastName}
                 Social Scurity Number : {SSNumber}
                 Birth Date            : { birthDay.ToString("dd MMM yyyy")}                
                 Gender                : {gender}
                 Age                   : {ageYear} years {ageMonth} Months {ageDay} Days
                 Generation            : {Generation(birthYear)}
                =====================================================");                
            
                bool userAnswer = false;
                do
                {
                    Console.Write("\n\t\t Do you want to rerun this program? Y/N :");
                    string answer = Console.ReadLine();
                    switch (answer.ToUpper())
                    {
                        case "Y":
                            clientAnswer = true;
                            userAnswer = false;
                            Console.Clear();
                            break;
                        case "N":
                            clientAnswer = false;
                            userAnswer = false;
                            break;
                        default:
                            Console.WriteLine("Your answer is invalid. Please try again...");
                            userAnswer = true;
                            break;
                    }
                } while (userAnswer);
            } while (clientAnswer);
        }

        public static void askClient(out string firstName, out string lastName, out string SSNumber)
        {
            Console.Write("\n\t\t Please Enter your First name: ");
            firstName = Console.ReadLine();
            Console.Write("\t\t Please Enter your Last name : ");
            lastName = Console.ReadLine();
            Console.Write($@" 
                 Pleas Enter your Social Security Number in this format 
                 Exampel:
                 Year    yyyy = 1986
                 Month     mm = 02 
                 They      dd = 07
                 Scurity xxxx = 1234
                 Enter (yyyymmdd-xxxx): ");
            SSNumber = Console.ReadLine();
        }

        public static string Generation(int birthYear)
        {
            string generation = null;
            if (birthYear > 2000) { generation = "Z"; }
            else if (birthYear >= 1985) { generation = "Millennial"; }
            else if (birthYear > 1964) { generation = "X"; }
            else if (birthYear > 1945) { generation = "Baby Boomers"; }
            else if (birthYear >= 1901) { generation = "Greatest"; }
            return generation;
        }
    }    
}

