using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Security
{
    public  class RandomNumberGenerator
    {
        public static int SecretCode()
        {
            int min = 1000;
            int max = 9999;
            Random rand = new Random();
            int value = rand.Next(min, max);
            return value;
        }
      
    }
}
