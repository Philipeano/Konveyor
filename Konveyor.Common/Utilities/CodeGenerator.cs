using System;

namespace Konveyor.Common.Utilities
{
    public static class CodeGenerator
    {
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();


        private static int GetRandomNumber(int min, int max)
        {
            lock (syncLock)
            {
                return random.Next(min, max);
            }
        }


        public static string GenerateCode(string context) 
        {
            string uniqueCode;
            int randomNo = GetRandomNumber(1, int.MaxValue);
            string paddedNo = randomNo.ToString().PadLeft(10, '0');

            switch (context)
            {
                case "Customer":
                    uniqueCode = $"C{paddedNo}";
                    break;
                case "Employee":
                    uniqueCode = $"E{paddedNo}";
                    break;
                case "Order":
                    uniqueCode = $"{DateTime.Today.Year}{paddedNo}";
                    break;
                default:
                    uniqueCode = string.Empty;
                    break;
            }
            return uniqueCode;
        }
    }
}
