using System;
using System.IO;
using System.Numerics;
using System.Text.RegularExpressions;
using SocialSecurityNumberSweden;

namespace SocialSecurityNumberSweden
{
    class Program
    {
        static void Main(string[] args)
        {
            string socialSecurityNumber =null;
            do
            {
            Start: //Label to send the user to the beginning to insert data.
                if (args.Length > 0)
                {
                    foreach (string argument in args)
                    {
                        if (argument.Length == 13)
                        {
                            Console.WriteLine("You enter this Social Security Number {0}", argument);
                            socialSecurityNumber = argument;
                        }
                        else
                            continue;
                    }
                }
                AskClient:
                 if (socialSecurityNumber == null)
                {
                    Console.Write(" Pleas insert a Social Security Number in this format \n Exampel: \n Year    yyyy = 1986 \n Month     mm = 02 \n They      dd = 07 \n Scurity xxxx = 1234  \n\n Enter (yyyymmdd-xxxx)/ Quit: ");
                    socialSecurityNumber = Console.ReadLine();
                }
                // Data kontrol 
                if (socialSecurityNumber.ToUpper() == "QUIT" || socialSecurityNumber.ToUpper() == "Q")
                {
                    break;
                }
            else if (socialSecurityNumber.Length != 13)
                {
                        Console.Clear();
                    Main(null);
                }
                

            UserVerification:
                Console.Write("\n\n Is this {0} your Social Security Number  Yes/No : ", socialSecurityNumber);
                string Answer = Console.ReadLine();
                    switch (Answer.ToUpper())
                    {
                            case "YES":
                            case "Y":
                                break;
                            case "NO":                      
                            case "N":
                                Console.Clear();
                        socialSecurityNumber = null;
                                goto AskClient;
                            default:
                                Console.WriteLine("Your answer is invalid. Please try again...");
                                goto UserVerification;
                    }
                
            // In this stage try to verify the data insert to the system. 
            //It is important to manage the data from the beginning.
            //Split data to Birthday and Security Parts

                bool verify = true;
                string[] socialSecurityNumbersSplit = socialSecurityNumber.Split("-");
                
            if (socialSecurityNumbersSplit[0].Length == 8)
                {
                        socialSecurityNumbersSplit[0] = socialSecurityNumbersSplit[0].Insert(4, "-");
                        socialSecurityNumbersSplit[0] = socialSecurityNumbersSplit[0].Insert(7, "-");
                }
            else
                {
                    goto Start;
                } 
            
            try
                {
                    Convert.ToDateTime(socialSecurityNumbersSplit[0]);
                    Convert.ToInt32(socialSecurityNumbersSplit[1]);
                }
            catch
                {
                    Console.WriteLine("\n ::::::::::::::: Exception :::::::::::::");
                    Console.WriteLine("\n   Invalid socialSecurityNumber. Pleas try again!  \n");
                    Console.WriteLine(" :::::::::::::::::::::::::::::::::::::::\n");
                    goto Start;
                }
                Console.WriteLine(" ===============================================================");

                VerifySSN.verifySSN(socialSecurityNumber,out verify);
                if (verify == false) { socialSecurityNumber = null; Main(null); }
            //End of Verification

                
            //Calculat of Birthday  
             DateTime birthDay = Convert.ToDateTime(socialSecurityNumbersSplit[0]);
           
            int age = int.Parse((DateTime.Today - birthDay).TotalDays.ToString());

            int ageYear = age / 365;
            int ageMonth = 0;
            int ageDay = 0;

            if (age % 365 > 30)
                {
                    ageMonth = (int)(age % 365.25)/30;
                    ageDay = (int)(age % 365.25 % 30);
                }
            else
                {                
                    ageDay = (int)(age % 365.25 % 30);
                }
            //End of Calculat of Birthday 

                Console.WriteLine("\n ===============================================================");
                Console.WriteLine("\n Information about prson with Social Scurity Number (SWEDEN)");
                Console.WriteLine(" ===============================================================");
                Console.WriteLine("\n  Social Scurity Number : {0}", socialSecurityNumber);
                Console.WriteLine("\n  Birth Date            : {0} ", birthDay.ToString("dd MMM yyyy"));

                string gender = Convert.ToInt32(socialSecurityNumbersSplit[1][2].ToString()) % 2 == 0 ? "Female" : "Male";
                Console.WriteLine("\n  Gender                : {0}", gender);

                Console.WriteLine("\n  Age                   : {0} years {1} Months {2} Days", ageYear,ageMonth,ageDay);

                Console.WriteLine(" ===============================================================");
            UserAnswer:
                Console.Write("\n Do you want to rerun this program? Yes/No :");
               string answerRerun =  Console.ReadLine();
                switch (answerRerun.ToUpper())
                {
                    case "YES":
                    case "Y":
                        Console.Clear();
                        goto Start;
                    case "NO":
                    case "N":
                        break;
                    default:
                        Console.WriteLine("Your answer is invalid. Please try again...");
                        goto UserAnswer;
                }
        
            } while (false);
        
        }

        
    }   
}
