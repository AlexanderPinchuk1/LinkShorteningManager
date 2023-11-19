using LinkShorteningManager.Foundation.Interfaces;

namespace LinkShorteningManager.Foundation
{
    public class RandomGenerator : IRandomGenerator
    {
        private readonly Random _random;



        public RandomGenerator() 
        {
            _random = new Random();
        }



        public string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            
            return new string(Enumerable.Repeat(chars, length)
                .Select(str => str[_random.Next(str.Length)]).ToArray());
        }
    }
}
