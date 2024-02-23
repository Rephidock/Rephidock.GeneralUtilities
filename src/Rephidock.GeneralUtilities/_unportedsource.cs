using System;
using System.Numerics;


namespace Rephidock.GeneralUtilities;


#if false
internal class UnportedSource {

	// From https://stackoverflow.com/questions/15743192/check-if-number-is-prime-number
	public static bool IsPrime(BigInteger number) {

		if (number <= 1) return false;
		if (number == 2) return true;
		if (number % 2 == 0) return false;

		var boundary = (BigInteger)Math.Floor(number.Sqrt());

		for (BigInteger i = 3; i <= boundary; i += 2)
			if (number % i == 0)
				return false;

		return true;
	}

	public static BigInteger GreatestCommonDenominator(BigInteger a, BigInteger b) {

		// Guards
		if (a <= 0) throw new ArgumentException("a must be positive", nameof(a));
		if (b <= 0) throw new ArgumentException("b must be positive", nameof(b));

		//Euclid's algorithm
		while (a != BigInteger.Zero && b != BigInteger.Zero) {
			if (a > b) a %= b;
			else b %= a;
		}

		return a | b;
	}

}
#endif
