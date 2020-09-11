using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


namespace SocialSecurityNumberSweden
{
    class VerifySSN
    {
        /// <summary>
        /// This method will calculate the last digit of the Social Security Number.
        /// </summary>
        /// <param name="SSN"></param>
        /// <param name="Verify"></param>
        public static void verifySSN(string SSN, out bool Verify)
        {
            int[] socialSecurityNumberControlPart = new int[13];
            int SumsocialSecurityNumberControlPart = 0;
            for (int i = 2; i < 12; i++)
            {
                if (i != 8) 
                { 
                    socialSecurityNumberControlPart[i] = Convert.ToInt32(SSN[i].ToString());
                    if (i < 8 && i % 2 == 0)
                    {
                        socialSecurityNumberControlPart[i] *= 2;
                        if (socialSecurityNumberControlPart[i] > 9)
                        {
                            socialSecurityNumberControlPart[i] = 1 + (socialSecurityNumberControlPart[i] - 10);
                        }
                    }
                    if (i > 8 && i % 2 == 1)
                    {
                        socialSecurityNumberControlPart[i] *= 2;
                        if (socialSecurityNumberControlPart[i] > 9)
                        {
                            socialSecurityNumberControlPart[i] = 1 + (socialSecurityNumberControlPart[i] - 10);
                        }
                    }
                }
                SumsocialSecurityNumberControlPart += socialSecurityNumberControlPart[i];
            }
            int helpNumber = SumsocialSecurityNumberControlPart / 10;
            socialSecurityNumberControlPart[12] = 10 - (SumsocialSecurityNumberControlPart - (helpNumber * 10));

            if(socialSecurityNumberControlPart[12] == 10) { socialSecurityNumberControlPart[12] = 0; }

            if (socialSecurityNumberControlPart[12] == Convert.ToInt32(SSN[12].ToString()))
            {
                Console.WriteLine("\n\t\t This Social securty Number is valid! \n\t\t The last digit is {0}", socialSecurityNumberControlPart[12]);
                Verify = true;
            }
            else
            {
                Console.WriteLine("\n\t\t This Social securty Number is Invalid!  \n\t\t The last digitss must be {0}", socialSecurityNumberControlPart[12]);
                Console.WriteLine("\t\t==============================================\n");
                Verify = false;
            }
        }
        /// <summary>
        /// With this method you will ask for the basic information from clint.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="SSNumber"></param>
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
        /// <summary>
        /// This Method wil calculate generation of the client.
        /// </summary>
        /// <param name="birthYear"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method will calculate ageYear, ageMonth, ageDay and gender, then reinject them to program.
        /// </summary>
        /// <param name="SSNumbersSplitDate"></param>
        /// <param name="SSNumbersSplitNum"></param>
        /// <param name="ageYear"></param>
        /// <param name="ageMonth"></param>
        /// <param name="ageDay"></param>
        /// <param name="gender"></param>
        public static void AgeCalculator(DateTime SSNumbersSplitDate, string SSNumbersSplitNum, out int ageYear, out int ageMonth, out int ageDay, out string gender)
        {
            DateTime birthDay = Convert.ToDateTime(SSNumbersSplitDate);
            int age = int.Parse((DateTime.Today - birthDay).TotalDays.ToString());
            ageYear = age / 365;
            ageMonth = 0;
            //ageDay = 0;

            if (age % 365 > 30)
            {
                ageMonth = (int)(age % 365.25) / 30;
                ageDay = (int)(age % 365.25 % 30);
            }
            else
            {
                ageDay = (int)(age % 365.25 % 30);
            }
            gender = Convert.ToInt32(SSNumbersSplitNum[2].ToString()) % 2 == 0 ? "Female" : "Male";
        }

    }
}
