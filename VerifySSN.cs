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
            int[] ssNumberControlPart = new int[13];
            int SumssNumberControlPart = 0;
            for (int i = 2; i < 12; i++)
            {
                if (i != 8) 
                { 
                    ssNumberControlPart[i] = Convert.ToInt32(SSN[i].ToString());
                    if (i < 8 && i % 2 == 0)
                    {
                        ssNumberControlPart[i] *= 2;
                        if (ssNumberControlPart[i] > 9)
                        {
                            ssNumberControlPart[i] = 1 + (ssNumberControlPart[i] - 10);
                        }
                    }
                    if (i > 8 && i % 2 == 1)
                    {
                        ssNumberControlPart[i] *= 2;
                        if (ssNumberControlPart[i] > 9)
                        {
                            ssNumberControlPart[i] = 1 + (ssNumberControlPart[i] - 10);
                        }
                    }
                }
                SumssNumberControlPart += ssNumberControlPart[i];
            }
            int helpNumber = SumssNumberControlPart / 10;
            ssNumberControlPart[12] = 10 - (SumssNumberControlPart - (helpNumber * 10));

            if(ssNumberControlPart[12] == 10) { ssNumberControlPart[12] = 0; }

            if (ssNumberControlPart[12] == Convert.ToInt32(SSN[12].ToString()))
            {
                Console.WriteLine("\n This Social securty Number is valid! \n The last digit is {0}", ssNumberControlPart[12]);
                Verify = true;
            }
            else
            {
                Console.WriteLine("\n This Social securty Number is Invalid!  \n The last digitss must be {0}", ssNumberControlPart[12]);
                Console.WriteLine("==============================================================\n");
                Verify = false;
            }

            //This codes it will do the same function for verfication of the SSNumber

            //int y1 = Convert.ToInt32(SSN[2].ToString()); y1 = y1 * 2; if (y1 > 9) { y1 = 1 + (y1 - 10); }
            //int y2 = Convert.ToInt32(SSN[3].ToString()); /*y2 = y2 * 1;*/
            //int m1 = Convert.ToInt32(SSN[4].ToString()); m1 = m1 * 2;
            //int m2 = Convert.ToInt32(SSN[5].ToString()); /*m2 = m2 * 1;*/
            //int d1 = Convert.ToInt32(SSN[6].ToString()); d1 = d1 * 2;
            //int d2 = Convert.ToInt32(SSN[7].ToString()); /*d2 = d2 * 1;*/
            //int x1 = Convert.ToInt32(SSN[9].ToString()); x1 = x1 * 2; if (x1 > 9) { x1 = 1 + (x1 - 10); }
            //int x2 = Convert.ToInt32(SSN[10].ToString()); /*x2 = x2 * 1;*/
            //int x3 = Convert.ToInt32(SSN[11].ToString()); x3 = x3 * 2; if (x3 > 9) { x3 = 1 + (x3 - 10); }

            //int x4 = y1 + y2 + m1 + m2 + d1 + d2 + x1 + x2 + x3;
            
            //int xx4 = x4 / 10;
            //x4 = 10 - (x4 - (xx4 * 10));
            //if(x4 == 10) { x4 = 0;}

            //if (x4 == Convert.ToInt32(SSN[12].ToString()))
            //{
            //    Console.WriteLine("\n This Social securty Number is valid! \n The last digit is {0}",x4);
            //    Verify = true;
            //}
            //else
            //{
            //    Console.WriteLine("\n This Social securty Number is Invalid!  \n The last digitss must be {0}", x4);
            //    Console.WriteLine("==============================================================\n");
            //    Verify = false;
            //}
        }
        
    }
}
