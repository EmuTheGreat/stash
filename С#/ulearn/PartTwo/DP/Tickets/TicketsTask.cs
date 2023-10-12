using System.Numerics;

namespace Tickets
{
    public class TicketsTask
    {
        private const int MaxSum = 2000;
        private const int MaxLen = 100;

        public static BigInteger Solve(int halfLen, int totalSum)
        {
            if (totalSum % 2 != 0) return BigInteger.Zero;

            var tickets = new BigInteger[MaxLen + 1, MaxSum + 1];

            for (var i = 0; i <= MaxLen; i++)
                for (var j = 0; j <= MaxSum; j++)
                    tickets[i, j] = -1;

            var halfResult = CountHappyTickets(tickets, halfLen, totalSum / 2);
            return BigInteger.Pow(halfResult, 2);
        }

        public static BigInteger CountHappyTickets(BigInteger[,] happyTickets, int len, int sum)
        {
            if (happyTickets[len, sum] >= 0) return happyTickets[len, sum];

            if (sum == 0) return BigInteger.One;
            if (len == 0) return BigInteger.Zero;

            happyTickets[len, sum] = BigInteger.Zero;

            for (var i = 0; i < 10; i++)
            {
                if (sum - i >= 0) 
                    happyTickets[len, sum] += CountHappyTickets(happyTickets, len - 1, sum - i);
            }

            return happyTickets[len, sum];
        }
    }
}

