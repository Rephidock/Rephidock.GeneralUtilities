using System;
using System.Collections.Generic;
using System.Numerics;


namespace Rephidock.GeneralUtilities.Maths;


/// <summary>
/// Provides static methods for some
/// arithmetic functions.
/// </summary>
public static class MoreMath {

	#region //// Lerp

	/// <summary>
	/// Linearly interpolates between 2 points.
	/// The result is not clamped.
	/// </summary>
	/// <param name="start">The start of interpolation</param>
	/// <param name="end">The end of interpolation</param>
	/// <param name="amount">
	/// <para>
	/// Interpolation amount.
	/// </para>
	/// <para>
	/// Value of <c>0</c> results in <paramref name="start"/>.
	/// Value of <c>1</c> results in <paramref name="end"/>.
	/// </para>
	/// </param>
	public static float Lerp(float start, float end, float amount) {
		return amount * (end - start) + start;
	}

	/// <inheritdoc cref="Lerp(float, float, float)"/>
	public static double Lerp(double start, double end, double amount) {
		return amount * (end - start) + start;
	}

	/// <inheritdoc cref="Lerp(float, float, float)"/>
	public static int Lerp(int start, int end, float amount) {
		return (int)MathF.Round(Lerp((float)start, end, amount));
	}

	/// <inheritdoc cref="Lerp(float, float, float)"/>
	public static long Lerp(long start, long end, double amount) {
		return (long)Math.Round(Lerp((double)start, end, amount));
	}

	/// <inheritdoc cref="Lerp(float, float, float)"/>
	public static byte Lerp(byte start, byte end, float amount) {
		return (byte)Lerp((int)start, end, amount);
	}

	/// <inheritdoc cref="MoreMath.Lerp(float, float, float)"/>
	/// <remarks>
	/// Possible precision loss if the bounds differ a lot,
	/// as this method converts between
	/// <see cref="BigInteger"/> and <see cref="double"/>.
	/// </remarks>
	public static BigInteger Lerp(BigInteger start, BigInteger end, double amount) {
		double differenceDouble = (double)(end - start);
		BigInteger offset = (BigInteger)Math.Round(amount * differenceDouble);
		return offset + start;
	}

	#endregion

	#region //// InverseLerp

	/// <summary>
	/// <para>
	/// An operation inverse to <see cref="Lerp(float, float, float)"/>.
	/// </para>
	/// <para>
	/// If <c>r = Lerp(a, b, x)</c> then <c>x = InverseLerp(a, b, r)</c>.
	/// </para>
	/// </summary>
	/// <param name="start">The start of interpolation</param>
	/// <param name="end">The end of interpolation</param>
	/// <param name="value">The result of linear interpolation</param>
	/// <returns>The amount of interpolation</returns>
	public static float InverseLerp(float start, float end, float value) {
		return (value - start) / (end - start);
	}

	/// <inheritdoc cref="InverseLerp(float, float, float)"/>
	public static double InverseLerp(double start, double end, double value) {
		return (value - start) / (end - start);
	}

	#endregion

	#region //// TabShift

	/// <summary>
	/// For a given 0-based column position of a tab character (<c>'\t'</c>)
	/// returns a column position for the next character.
	/// </summary>
	public static int TabShift(int tabColumn, int tabSize = 4) {
		return ((tabColumn / tabSize) + 1) * tabSize;
	}

	#endregion

	#region //// PosMod

	/// <summary>
	/// <para>
	/// Returns (the positive) <c>value mod modulo</c>.
	/// Keep in mind that % is the remainder operation.
	/// The result of this function is always positive or zero, as opposed to remainder.
	/// </para>
	/// <para>
	/// Example differences:
	/// <list>
	/// <item><c>-1 % 6 = -1</c> -but- <c>-1 mod 6 = 5</c></item>
	/// <item><c>-5 % 3 = -2</c> -but- <c>-5 mod 3 = 1</c></item>
	/// </list>
	/// </para>
	/// </summary>
	/// <exception cref="ArgumentException"><paramref name="modulo"/> is 0</exception>
	/// <exception cref="NotSupportedException"><paramref name="modulo"/> is negative</exception>
	public static int PosMod(this int value, int modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		int remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="PosMod(int, int)"/>
	public static float PosMod(this float value, float modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		float remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="PosMod(int, int)"/>
	public static double PosMod(this double value, double modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		double remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="PosMod(int, int)"/>
	public static long PosMod(this long value, long modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		long remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="PosMod(int, int)"/>
	public static BigInteger PosMod(this BigInteger value, BigInteger modulo) {

		if (modulo == BigInteger.Zero) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		BigInteger remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	#endregion

	#region //// Wrap

	/// <summary>
	/// <para>
	/// Wraps value into given range:
	/// values below the range are wrapped into it from the end and
	/// values above are wrapped into it from the start
	/// </para>
	/// <para>
	/// Can be thought of as a generilized version of <c>PosMod</c>, as
	/// <c>x.Wrap(0, y)</c> returns the same result as <c>x.PosMod(y)</c>
	/// </para>
	/// </summary>
	/// <param name="value">The value to wrap</param>
	/// <param name="min">The start of the range, inclusive</param>
	/// <param name="max">The end of the range, exclusive</param>
	/// <remarks>
	/// If <paramref name="min"/> is greater than <paramref name="max"/>, the values are swapped.
	/// If <paramref name="min"/> and <paramref name="max"/> are equal, <paramref name="min"/> is returned.
	/// </remarks>
	/// <returns>The value wrapped to be inside the range</returns>
	public static int Wrap(this int value, int min, int max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).PosMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static float Wrap(this float value, float min, float max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).PosMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static double Wrap(this double value, double min, double max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).PosMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static long Wrap(this long value, long min, long max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).PosMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static BigInteger Wrap(this BigInteger value, BigInteger min, BigInteger max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).PosMod(max - min) + min;
	}

	#endregion

	#region //// Factors

	/// <summary>
	/// <para>
	/// Returns all prime factors of an integer in ascending order
	/// (Performs prime factorization).
	/// </para>
	/// <para>
	/// This function was extended to work on all integers.
	/// Special cases apply if <paramref name="n"/> is below 2.
	/// </para>
	/// <para>
	/// If <paramref name="n"/> is <c>0</c>, <c>1</c> or <c>-1</c>, then <paramref name="n"/> is returned;
	/// If <paramref name="n"/> is below <c>-1</c>, then <c>-1</c> is returned,
	/// followed by prime factors of <c>abs(<paramref name="n"/>)</c>.
	/// </para>
	/// </summary>
	/// <param name="n">The value to return factors of</param>
	public static IEnumerable<int> GetFactors(this int n) {

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
		int factor = 2;
		while (factor <= n) {

			while (n % factor == 0) {
				yield return factor;
				n /= factor;
			}

			factor++;
		}

	}

	/// <inheritdoc cref="GetFactors(int)"/>
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

	#region //// BigInt Sqrt

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

	#endregion

}
