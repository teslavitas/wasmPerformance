namespace CSharpPayload;

public class CSharpDirectSolution
{
    // Leetcode #878
    public static int NthMagicalNumber(int n, int a, int b)
    {
        long ab = GetAb(a, b);
        int modulo = 1000_000_007;
        long result = 0;
        long countStep = (ab / a) + (ab / b) - 1;
        long multStep = 1;
        long currentCount = 0;
        while (true)
        {
            long newCurrentCount = currentCount + countStep * multStep;
            long newResult = (result + ((ab * multStep) % modulo)) % modulo;


            if (newCurrentCount == n)
            {
                return (int)newResult;
            }
            if (newCurrentCount > n)
            {
                if (multStep > 1)
                {
                    multStep = 1;
                    continue;
                }
                else
                {
                    break;
                }
            }
            multStep *= 2;
            currentCount = newCurrentCount;
            result = newResult;
        }

        //ab doesn't fit in anymore
        long pathA = 0;
        long pathB = 0;
        long resultA = result;
        long resultB = result;
        bool lastWasA = false;
        while (currentCount < n)
        {
            if (pathA + a < pathB + b)
            {
                resultA = (resultA + a) % modulo;
                pathA += a;
                lastWasA = true;
            }
            else
            {
                resultB = (resultB + b) % modulo;
                pathB += b;
                lastWasA = false;
            }

            currentCount++;
        }

        if (lastWasA)
        {
            return (int)resultA;
        }
        else
        {
            return (int)resultB;
        }
    }

    private static long GetAb(int a, int b)
    {
        var primeNumbers = GetPrimeNumbers();
        var aComponents = GetPrimeComponents(a, primeNumbers);
        var bComponents = GetPrimeComponents(b, primeNumbers);
        long ab = 1;
        foreach (var prime in primeNumbers)
        {
            int maxCount = 0;
            if (aComponents.TryGetValue(prime, out int aCount))
            {
                maxCount = aCount;
            }
            if (bComponents.TryGetValue(prime, out int bCount))
            {
                maxCount = Math.Max(maxCount, bCount);
            }

            while (maxCount > 0)
            {
                ab *= prime;
                maxCount--;
            }
        }

        return ab;
    }

    private static int[] GetPrimeNumbers()
    {
        int n = 40_001;
        var allNumbers = new int[n];
        for (int i = 2; i < n; ++i)
        {
            allNumbers[i] = 1;
        }
        for (int i = 2; i < n; ++i)
        {
            if (allNumbers[i] == 1)
            {
                for (int j = i * 2; j < n; j += i)
                {
                    allNumbers[j] = 0;
                }
            }
        }
        var result = allNumbers.Select((num, index) => new { num, index })
            .Where(x => x.num == 1).Select(x => x.index).ToArray();
        return result;
    }

    private static Dictionary<int, int> GetPrimeComponents(int num, int[] primes)
    {
        var result = new Dictionary<int, int>();
        foreach (var prime in primes)
        {
            int count = 0;
            while ((num % prime) == 0)
            {
                count++;
                num /= prime;
            }
            if (count > 0)
            {
                result.Add(prime, count);
            }
        }
        return result;
    }
}