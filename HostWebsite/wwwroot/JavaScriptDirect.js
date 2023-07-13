export function runJSPayload (n, a, b) {
    const ab = GetAb(a, b);
    const modulo = 1000_000_007;
    let result = 0;
    const countStep = (ab / a) + (ab / b) - 1;
    let multStep = 1;
    let currentCount = 0;
    while (true) {
        let newCurrentCount = currentCount + countStep * multStep;
        let newResult = (result + ((ab * multStep) % modulo)) % modulo;

        if (newCurrentCount == n) {
            return newResult;
        }
        if (newCurrentCount > n) {
            if (multStep > 1) {
                multStep = 1;
                continue;
            }
            else {
                break;
            }
        }
        multStep *= 2;
        currentCount = newCurrentCount;
        result = newResult;
    }

    //ab doesn't fit in anymore
    let pathA = 0;
    let pathB = 0;
    let resultA = result;
    let resultB = result;
    let lastWasA = false;
    while (currentCount < n) {
        if (pathA + a < pathB + b) {
            resultA = (resultA + a) % modulo;
            pathA += a;
            lastWasA = true;
        }
        else {
            resultB = (resultB + b) % modulo;
            pathB += b;
            lastWasA = false;
        }

        currentCount++;
    }

    if (lastWasA) {
        return resultA;
    }
    else {
        return resultB;
    }
};

function GetAb(a, b)
{
    let primeNumbers = GetPrimeNumbers();
    let aComponents = GetPrimeComponents(a, primeNumbers);
    let bComponents = GetPrimeComponents(b, primeNumbers);
    let ab = 1;
    //for(let prime in primeNumbers)
    primeNumbers.forEach(prime => {
        let maxCount = 0;
        let aCount = aComponents.get(prime);
        if (aCount !== undefined) {
            maxCount = aCount;
        }

        let bCount = bComponents.get(prime);
        if (bCount !== undefined) {
            maxCount = Math.max(maxCount, bCount);
        }

        while (maxCount > 0) {
            ab *= prime;
            maxCount--;
        }
    });

    return ab;
}

function GetPrimeNumbers() {
    const n = 40_001;
    let allNumbers = new Array(n);
    for (let i = 2; i < n; ++i)
    {
        allNumbers[i] = 1;
    }
    for (let i = 2; i < n; ++i)
    {
        if (allNumbers[i] == 1) {
            for (let j = i * 2; j < n; j += i)
            {
                allNumbers[j] = 0;
            }
        }
    }

    let result = [];
    for (let i = 1; i < n; ++i) {
        if (allNumbers[i] == 1) {
            result.push(i);
        }
    }

    return result;
}

function GetPrimeComponents(num, primes) {
    var result = new Map();
    primes.forEach(prime => {
        let count = 0;
        while ((num % prime) == 0) {
            count++;
            num /= prime;
        }
        if (count > 0) {
            result.set(prime, count);
        }
    });
    return result;
}
