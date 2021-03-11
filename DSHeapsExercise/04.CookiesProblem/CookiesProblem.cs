using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var bag = new OrderedBag<int>();
            foreach (var cookie in cookies)
            {
                bag.Add(cookie);
            }
            int currentCookie = bag.GetFirst();
            int count = 0;
            while (currentCookie<k
                && bag.Count>1)
            {
                int firstCookie = bag.RemoveFirst();
                int secondCookie = bag.RemoveFirst();
                int combined = firstCookie + 2 * secondCookie;
                bag.Add(combined);
                currentCookie = bag.GetFirst();
                count++;
            }
            return currentCookie < k ? -1 : count;
        }
    }
}
