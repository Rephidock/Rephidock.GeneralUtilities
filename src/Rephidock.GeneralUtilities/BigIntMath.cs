using System;
using System.Numerics;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides static methods for some
/// arithmetic functions.
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

	#region //// From MoreMath

	/// <inheritdoc cref="MoreMath.Lerp(float, float, float)"/>
	/// <remarks>
	/// Beware of preceision loss, as this method converts between
	/// <see cref="BigInteger"/> and <see cref="double"/>
	/// </remarks>
	public static BigInteger Lerp(BigInteger start, BigInteger end, double amount) {
		return (BigInteger)Math.Round(MoreMath.Lerp((double)start, (double)end, amount));
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

	#endregion

}
