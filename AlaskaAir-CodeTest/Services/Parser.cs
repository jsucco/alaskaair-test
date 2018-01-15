using System;

namespace AlaskaAir_CodeTest.Services
{
    public class Parser
    {
        public static string ParseErrorMessage = "";
        public static bool ParseErrorFlag = false;

        public static int ToInt32(string str)
        {
            int number;

            if (Int32.TryParse(str, out number))
                return number;

            ParseErrorFlag = true;
            ParseErrorMessage = "Failed to Convert " + str + " to number";

            return 0; 
        }

        public static DateTime ToDateTime(string str)
        {
            DateTime dt;

            string d_s = DateTime.Now.Date.ToShortDateString() + " " + str;

            if (DateTime.TryParse(d_s, out dt))
                return dt;

            ParseErrorFlag = true;
            ParseErrorMessage = "Failed to Convert " + str + " to datetime";

            return new DateTime(1900, 1, 1);
        }

        public static Decimal ToDecimal(string str)
        {
            decimal dc;

            if (Decimal.TryParse(str, out dc))
                return dc;

            ParseErrorFlag = true; 
            ParseErrorMessage = "Failed to Convert " + str + " to decimal";

            return 0; 
        }
    }
}