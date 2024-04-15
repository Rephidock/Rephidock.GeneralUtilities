using System;
using System.Collections.Generic;
using System.Numerics;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides static methods for some
/// arithmetic functions for BigInteger.
/// </summary>
public static class BigIntMath {

	/// <summary>
	/// <para>
	/// Returns a square root of a given <see cref="BigInteger"/>.
	/// </para>
	/// <para>
	/// IMPORTANT: Has its precision and magnitude limits due to
	/// conversion to <see cref="double"/>.
	/// </para>
	/// </summary>
	public static double Sqrt(this BigInteger n) => Math.Pow(Math.E, BigInteger.Log(n) / 2);

	#region //// Source clone from MoreMath

	/// <inheritdoc cref="MoreMath.Lerp(float, float, float)"/>
	/// <remarks>
	/// Possible precision loss, as this method converts between
	/// <see cref="BigInteger"/> and <see cref="double"/>.
	/// </remarks>
	public static BigInteger Lerp(BigInteger start, BigInteger end, double amount) {
		double differenceDouble = (double)(end - start);
		BigInteger offset = (BigInteger)Math.Round(amount * differenceDouble);
		return offset + start;
	}



	/// <inheritdoc cref="MoreMath.TrueMod(int, int)"/>
	public static BigInteger TrueMod(this BigInteger value, BigInteger modulo) {

		if (modulo == BigInteger.Zero) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		BigInteger remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}



	/// <inheritdoc cref="MoreMath.Wrap(int, int, int)"/>
	public static BigInteger Wrap(this BigInteger value, BigInteger min, BigInteger max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).TrueMod(max - min) + min;
	}



	/// <inheritdoc cref="MoreMath.DigitalRoot(int, int)"/>
	public static BigInteger DigitalRoot(this BigInteger value, BigInteger radix) {

		// Guards
		if (value < 0) {
			throw new ArgumentException("Digital root of a negative value is undefined", nameof(value));
		}

		if (radix < 2) {
			throw new ArgumentException("Integer base must be at least 2", nameof(radix));
		}

		// Digital root
		if (value == 0) return 0;
		return 1 + ((value - 1) % (radix - 1));
	}

	/// <inheritdoc cref="MoreMath.DigitalRoot(int, int)"/>
	/// <remarks>Calculated digital root using default base of 10</remarks>
	public static BigInteger DigitalRoot(this BigInteger value) => value.DigitalRoot(10);


	/// <inheritdoc cref="MoreMath.GetFactors(int)"/>
	public static IEnumerable<BigInteger> GetFactors(this BigInteger n) {

		// Zero, one, negative one
		if (n >= -1 && n <= 1) {
			yield return n;
			yield break;
		}

		// Negative
		if (n < 0) {
			yield return -1;
			n = -n;
		}

		// Main loop
		BigInteger factor = 2;
		while (factor <= n) {

			while (n % factor == 0) {
				yield return factor;
				n /= factor;
			}

			factor++;
		}

	}

	#endregion

	#region //// Source clone from RadixMath

	/// <inheritdoc cref="RadixMath.ToDigits(int, ushort, int)"/>
	public static ushort[] ToDigits(this BigInteger value, ushort radix, int padToPlaces = -1) {

		// Guards
		if (radix < 2) {
			throw new ArgumentException("Base must be at least 2", nameof(radix));
		}

		// Take absolute
		if (value < 0) value = -value;

		// Get digits
		List<ushort> digitsStartingFromUnits = new(padToPlaces > 0 ? padToPlaces : 4);

		do {
			digitsStartingFromUnits.Add((ushort)(value % radix));
			value /= radix;
		} while (value > 0);

		// Pad digits
		int prepadCount = padToPlaces - digitsStartingFromUnits.Count;
		for (; prepadCount > 0; prepadCount--) {
			digitsStartingFromUnits.Add(0);
		}

		// Create array and return
		ushort[] result = new ushort[digitsStartingFromUnits.Count];
		int lastI = digitsStartingFromUnits.Count - 1;
		for (int i = 0; i <= lastI; i++) {
			result[i] = digitsStartingFromUnits[lastI - i];
		}

		return result;
	}

	/// <inheritdoc cref="RadixMath.FromDigits(ushort[], ushort)"/>
	/// <remarks>
	/// Use of <see cref="BigInteger"/> means that
	/// <see cref="OverflowException"/> is not thrown for larger numbers.
	/// </remarks>
	public static BigInteger FromDigits(this ushort[] digits, ushort radix) {

		// Guards
		if (radix < 2) {
			throw new ArgumentException("Base must be at least 2", nameof(radix));
		}

		// Add up all the digits with respective powers of radix
		BigInteger result = 0;
		BigInteger currentMultiplier = 1;

		for (int i = digits.Length - 1; i >= 0; i--) {

			// Add value to the result
			result += digits[i] * currentMultiplier;

			// Updte multiplier
			currentMultiplier *= radix;
		}

		return result;
	}

	#endregion

}
