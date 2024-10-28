using System;
using System.Collections.Generic;


namespace Rephidock.GeneralUtilities.Maths;


/// <summary>
/// Provides static methods for some
/// arithmetic functions.
/// </summary>
public static class MoreMath {

	#region //// Lerp, ReverseLerp

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

	/// <summary>
	/// <para>
	/// An operation inverse to <see cref="Lerp(float, float, float)"/>.
	/// </para>
	/// <para>
	/// If <c>r = Lerp(a, b, x)</c> then <c>x = ReverseLerp(a, b, r)</c>.
	/// </para>
	/// </summary>
	/// <param name="start">The start of interpolation</param>
	/// <param name="end">The end of interpolation</param>
	/// <param name="value">The result of linear interpolation</param>
	/// <returns>The amount of interpolation</returns>
	public static float ReverseLerp(float start, float end, float value) {
		return (value - start) / (end - start);
	}

	/// <inheritdoc cref="ReverseLerp(float, float, float)"/>
	public static double ReverseLerp(double start, double end, double value) {
		return (value - start) / (end - start);
	}

	#endregion

	/// <summary>
	/// For a given 0-based column position of a tab character (<c>'\t'</c>)
	/// returns a column position for the next character.
	/// </summary>
	public static int TabShift(int tabColumn, int tabSize = 4) {
		return ((tabColumn / tabSize) + 1) * tabSize;
	}

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

	#endregion

	#region //// Digital Root

	/// <summary>
	/// Calculates the digital root of a number.
	/// Digital root is calculated by taking the sum of all of
	/// the number's digits, and then that sum, until the result is a single digit.
	/// </summary>
	/// <exception cref="ArgumentException">
	/// <paramref name="value"/> is negative or
	/// <paramref name="radix"/> is smaller than 2
	/// </exception>
	public static int DigitalRoot(this int value, int radix = 10) {

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

	/// <inheritdoc cref="DigitalRoot(int, int)"/>
	public static long DigitalRoot(this long value, long radix = 10) {

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

	#endregion

	#region //// Factors

	/// <summary>
	/// <para>
	/// Returns all integer factors of the number in ascending order.
	/// </para>
	/// <para>
	/// If <paramref name="n"/> is <c>0</c>, <c>0</c> is returned.
	/// If <paramref name="n"/> is <c>1</c>, <c>1</c> is returned.
	/// If <paramref name="n"/> is negative, <c>-1</c> is returned, followed by factors of <c>abs(n)</c>
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

	#endregion

	#region //// Deg <-> Rad

	/// <summary>
	/// Converts angle measured in degrees to angle measured in radians
	/// </summary>
	/// <param name="angleDegrees">Angle measured in degrees</param>
	/// <returns>Angle measured in radians</returns>
	public static float DegToRad(this float angleDegrees) => angleDegrees / 180f * MathF.PI;

	/// <summary>
	/// Converts angle measured in radians to angle measured in degrees
	/// </summary>
	/// <param name="angleRadians">Angle measured in radians</param>
	/// <returns>Angle measured in degrees</returns>
	public static float RadToDeg(this float angleRadians) => angleRadians / MathF.PI * 180f;

	/// <inheritdoc cref="DegToRad(float)"/>
	public static double DegToRad(this double angleDegrees) => angleDegrees / 180d * Math.PI;

	/// <inheritdoc cref="RadToDeg(float)"/>
	public static double RadToDeg(this double angleRadians) => angleRadians / Math.PI * 180d;

	#endregion

	#region //// AngleDifference

	/// <summary>
	/// Given two angles calculates the shortest distance,
	/// taking the circle over/underflow into account.
	/// </summary>
	/// <param name="angleSourceRadians">First angle, measured in radians</param>
	/// <param name="angleDestinationRadians">Second angle, measured in radians</param>
	/// <remarks>
	/// Returned distance is in range of <c>[-<see cref="MathF.PI"/>, <see cref="MathF.PI"/>)</c>.
	/// </remarks>
	/// <returns>Shortest distance between two angles.</returns>
	public static float AngleDifference(float angleSourceRadians, float angleDestinationRadians) {
		return (angleDestinationRadians - angleSourceRadians).Wrap(-MathF.PI, MathF.PI);
	}

	/// <inheritdoc cref="AngleDifference(float, float)"/>
	/// <remarks>
	/// Returned distance is in range of <c>[-<see cref="Math.PI"/>, <see cref="Math.PI"/>)</c>.
	/// </remarks>
	public static double AngleDifference(double angleSourceRadians, double angleDestinationRadians) {
		return (angleDestinationRadians - angleSourceRadians).Wrap(-Math.PI, Math.PI);
	}

	#endregion

}
