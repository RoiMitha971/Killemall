using System;

namespace Killemall.Tools
{
    public static class IdGenerator
    {
        private static int lastSeed = 0;

        public static string GenerateUniqueID()
        {
            string generatedID = "";

            DateTime epochStart = new System.DateTime(1970, 1, 1, 8, 0, 0, System.DateTimeKind.Utc);
            double timestamp = (System.DateTime.UtcNow - epochStart).TotalSeconds;
            string timestampStr = timestamp.ToString();
            int seed = (timestampStr.Length > 4) ? (int)(Convert.ToInt32(timestampStr[timestampStr.Length - 1]) * Convert.ToInt32(timestampStr[timestampStr.Length - 2]) * Convert.ToInt32(timestampStr[timestampStr.Length - 3])) : lastSeed;
            if (seed == lastSeed)
                seed--;
            lastSeed = seed;
            var random = new System.Random(seed);
            generatedID += String.Format("{0:X}", Convert.ToInt32(timestamp)) // Timestamp
                     + "-" + String.Format("{0:X}", random.Next(10000000)); // Random

            return generatedID;
        }
    }
}