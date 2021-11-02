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

        public static string GetRndEmail()
        {
            var chars = "abcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[4];

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[Randomchik.Next(chars.Length)];
            }

            string date = DateTime.Now.ToString("yyyyMMddHHmmss");

            var email = new String(stringChars) + date + "@gmail.com";

            return email;
        }

        public static (string cardNumber, string cardData, string cardCvv) GetRndCreditCard()
        {
            Random rnd = new Random();
            List<string> cards = new List<string>() { "4605396609356244", "4485954940959186", "4038797119537982", "4532151451188015", "4556840461075441", "4532226166467670", "4916348528865577" };
            int cardNumber = rnd.Next(0, cards.Count()-1);
            int cvv = rnd.Next(100, 999);
            int expMonth = rnd.Next(1, 12);
            int expYear = rnd.Next(22, 27);

            return (cards[cardNumber], expMonth.ToString() + expYear.ToString(), cvv.ToString());
        }
    }
}
