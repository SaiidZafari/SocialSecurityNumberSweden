using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;


namespace SocialSecurityNumberSweden
{
    class VerifySSN
    {
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
                Console.WriteLine("\n This Social securty Number is valid! \n The last digit is {0}", socialSecurityNumberControlPart[12]);
                Verify = true;
            }
            else
            {
                Console.WriteLine("\n This Social securty Number is Invalid!  \n The last digitss must be {0}", socialSecurityNumberControlPart[12]);
                Console.WriteLine("==============================================================\n");
                Verify = false;
            }
        }
        
    }
}
