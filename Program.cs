using System;
using System.IO;
using System.Text.RegularExpressions;
using PersonalIdentityNumberSweden;
using SocialSecurityNumberSweden;

namespace PersonalIdentityNumberSweden
{
    class Program
    {
        //private static object birthDay;

        static void Main(string[] args)
        {
            bool clientAnswer = false;
            string gender;
            int ageYear;
            int ageMonth;
            int ageDay;
            DateTime birthDay;
            int birthYear;
            string SSNumber;
            do
            {
                string firstName = args[0];
                string lastName = args[1];
                SSNumber = args[2];

                if (args.Length > 0)
                {
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
                                VerifySSN.askClient(out firstName, out lastName, out SSNumber);
                                break;
                            default:
                                Console.WriteLine("\n\t\t Your answer is invalid. Please try again...");
                                Console.Beep(300, 800);
                                clientAnswer = bla;
                                break;
                                //goto UserVerification;
                        }
                    } while (clientAnswer);
                }
                // In this stage try to verify the data insert to the system. 
                //It is important to manage the data from the beginning.
                //Split data to Birthday and Security Parts

                bool verify = true;
                string[] SSNumbersSplit = SSNumber.Split("-");
                if (SSNumber.Length != 13)
                {
                    Console.WriteLine("Invalid SSnumber. Pleas try again! ");
                    VerifySSN.askClient(out firstName, out lastName, out SSNumber);
                }

                if (SSNumbersSplit[0].Length == 8)
                {
                    SSNumbersSplit[0] = SSNumbersSplit[0].Insert(4, "-");
                    SSNumbersSplit[0] = SSNumbersSplit[0].Insert(7, "-");
                }
                else
                {
                    VerifySSN.askClient(out firstName, out lastName, out SSNumber);
                }

                try
                {
                    birthDay = Convert.ToDateTime(SSNumbersSplit[0]);
                    Convert.ToInt32(SSNumbersSplit[1]);
                }
                catch
                {
                    Console.WriteLine("\n ::::::::::::::: Exception :::::::::::::");
                    Console.WriteLine("\n   Invalid SSnumber. Pleas try again!  \n");
                    Console.WriteLine(" :::::::::::::::::::::::::::::::::::::::\n");
                    VerifySSN.askClient(out firstName, out lastName, out SSNumber);
                }
                Console.WriteLine("\t\t==============================================");

                VerifySSN.verifySSN(SSNumber, out verify);
                if (!verify) { VerifySSN.askClient(out firstName, out lastName, out SSNumber); }
                //End of Verification
                birthDay = Convert.ToDateTime(SSNumbersSplit[0]);
                VerifySSN.AgeCalculator(birthDay,SSNumbersSplit[1], out ageYear, out ageMonth, out ageDay, out gender);
                birthYear = birthDay.Year;
                Console.Clear();
                Console.WriteLine($@" 
                
                       Social Scurity Number (SWEDEN)
                       Information about {firstName} {lastName} {SSNumber} 
                =====================================================
                 Name                  : {firstName} {lastName}
                 Social Scurity Number : {SSNumber}
                 Birth Date            : { birthDay.ToString("yyyy MMM dd")}                
                 Gender                : {gender}
                 Age                   : {ageYear} years {ageMonth} Months {ageDay} Days
                 Generation            : {VerifySSN.Generation(birthYear)}
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
                            Console.Clear();
                            break;
                        case "N":
                            clientAnswer = false;
                            break;
                        default:
                            Console.WriteLine("Your answer is invalid. Please try again...");
                            userAnswer = true;
                            break;
                    }
                } while (userAnswer);
            } while (clientAnswer);
        }
    }    
}

