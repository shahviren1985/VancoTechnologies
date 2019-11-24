using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections;


namespace Util
{
    public class GeneratePassword
    {
        public static string CreateRandomPassword(int charLength, int specialCL, int numberCL)
        {
            try
            {
                Hashtable args = new Hashtable();
                args.Add("CharLength", charLength);
                args.Add("SpecialCL", specialCL);
                args.Add("NumberCL", numberCL);
                
                string allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789";
                string allowedSpecialChars = "!@$?_-";
                string allowedNumbers = "0123456789";

                char[] chars = new char[charLength];
                char[] sChars = new char[specialCL];
                char[] nChars = new char[numberCL];

                Random rd = new Random();
                //for charactores
                for (int i = 0; i < charLength; i++)
                {
                    chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
                }
                //for special charactors
                for (int j = 0; j < specialCL; j++)
                {
                    sChars[j] = allowedSpecialChars[rd.Next(0, allowedSpecialChars.Length)];
                }
                //for numbers
                for (int k = 0; k < numberCL; k++)
                {
                    nChars[k] = allowedNumbers[rd.Next(0, allowedNumbers.Length)];
                }

                string cPass = new string(chars);// +sChars.ToString() + nChars.ToString();
                string sPass = new string(sChars);
                string nPass = new string(nChars);
                
                return cPass + sPass + nPass;//new string(chars.ToString() + sChars.ToString() + nChars.ToString());

            }
            catch (Exception ex)
            {
                
                
            }
            return "This@123";
        }
    }
}
