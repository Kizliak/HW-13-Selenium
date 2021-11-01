using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_13_Selenium
{
    public static class Generators
    {
        static public Random Randomchik = new Random();
        public static string GetRandName()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            //var randomchik = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Randomchik.Next(chars.Length)];
            }

            stringChars[0] = Char.ToUpper(stringChars[0]);

            var name = new String(stringChars);

            return name;
        }

        public static string GetRndPass()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var specialChars = "!@#$%^&*()";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            stringChars[0] = Char.ToUpper(stringChars[0]);

            var password = new String(stringChars) + random.Next(0,9) + specialChars[random.Next(0,specialChars.Count())];

            return password;
        }

        public static string GetRndPhone()
        {
            var chars = "123456789";
            var stringChars = new char[10];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Randomchik.Next(chars.Length)];
            }

            var phoneNumber = new String(stringChars);

            return phoneNumber;
        }
    }
}
