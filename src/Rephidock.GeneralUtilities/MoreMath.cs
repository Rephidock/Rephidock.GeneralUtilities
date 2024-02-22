using System;
using System.Numerics;


namespace Rephidock.GeneralUtilities;


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

	/// <inheritdoc cref="Lerp(float, float, float)"/>
	/// <remarks>
	/// Beware of preceision loss, as this method converts between
	/// <see cref="BigInteger"/> and <see cref="double"/>
	/// </remarks>
	public static BigInteger Lerp(BigInteger start, BigInteger end, double amount) {
		return (BigInteger)Math.Round(Lerp((double)start, (double)end, amount));
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

	#region //// TrueMod

	/// <summary>
	/// <para>
	/// Returns the value mod modulo.
	/// Keep in mind that % is the remainder operation.
	/// The result of mod is never negative, as opposed to remainder.
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
	public static int TrueMod(this int value, int modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		int remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="TrueMod(int, int)"/>
	public static float TrueMod(this float value, float modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		float remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="TrueMod(int, int)"/>
	public static double TrueMod(this double value, double modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		double remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="TrueMod(int, int)"/>
	public static long TrueMod(this long value, long modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		long remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	/// <inheritdoc cref="TrueMod(int, int)"/>
	public static BigInteger TrueMod(this BigInteger value, BigInteger modulo) {

		if (modulo == 0) throw new ArgumentException("x mod 0 is undefined", nameof(modulo));
		if (modulo < 0) throw new NotSupportedException("Negative modulo is not supported.");

		BigInteger remainder = value % modulo;
		return remainder < 0 ? remainder + modulo : remainder;
	}

	#endregion

	#region //// Wrap

	/// <summary>
	/// <para>
	/// Wraps value into given range:
	/// values below the range are wraped into it from the end and
	/// values above are wraped into it from the start
	/// </para>
	/// <para>
	/// Can be thought of as a generilized version of <c>TrueMod</c>, as
	/// <c>x.Wrap(0, y)</c> returns the same result as <c>x.TrueMod(y)</c>
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

		return (value - min).TrueMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static float Wrap(this float value, float min, float max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).TrueMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static double Wrap(this double value, double min, double max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).TrueMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static long Wrap(this long value, long min, long max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).TrueMod(max - min) + min;
	}

	/// <inheritdoc cref="Wrap(int, int, int)"/>
	public static BigInteger Wrap(this BigInteger value, BigInteger min, BigInteger max) {

		// Range of 0 -- easy return
		if (min == max) return min;

		// Swap min and max so that min < max
		if (min > max) (max, min) = (min, max);

		return (value - min).TrueMod(max - min) + min;
	}

	#endregion

	#region //// Digital Root

	/// <summary>
	/// Calculates the digital root of a number.
	/// Digital root is calculated by taking the sum of all of
	/// the number's digits, and then that sum, until the result is a single digit.
	/// </summary>
	public static int DigitalRoot(this int value, int rootBase = 10) {

		// Guards
		if (value < 0) {
			throw new ArgumentException("Digital root of a negative value is undefined", nameof(value));
		}

		if (rootBase < 2) {
			throw new ArgumentException("Integer base must be at least 2", nameof(rootBase));
		}

		// Digital root
		if (value == 0) return 0;
		return 1 + ((value - 1) % (rootBase - 1));
	}

	/// <inheritdoc cref="DigitalRoot(int, int)"/>
	public static long DigitalRoot(this long value, long rootBase = 10) {

		// Guards
		if (value < 0) {
			throw new ArgumentException("Digital root of a negative value is undefined", nameof(value));
		}

		if (rootBase < 2) {
			throw new ArgumentException("Integer base must be at least 2", nameof(rootBase));
		}

		// Digital root
		if (value == 0) return 0;
		return 1 + ((value - 1) % (rootBase - 1));
	}

	/// <inheritdoc cref="DigitalRoot(int, int)"/>
	public static BigInteger DigitalRoot(this BigInteger value, BigInteger rootBase) {

		// Guards
		if (value < 0) {
			throw new ArgumentException("Digital root of a negative value is undefined", nameof(value));
		}

		if (rootBase < 2) {
			throw new ArgumentException("Integer base must be at least 2", nameof(rootBase));
		}

		// Digital root
		if (value == 0) return 0;
		return 1 + ((value - 1) % (rootBase - 1));
	}

	/// <inheritdoc cref="DigitalRoot(int, int)"/>
	/// <remarks>Calculated digital root using default base of 10</remarks>
	public static BigInteger DigitalRoot(this BigInteger value) => value.DigitalRoot(10);

	#endregion

	#region //// Deg <-> Rad

	/// <summary>
	/// Converts angle measured in degress to angle measured in radians
	/// </summary>
	/// <param name="angleDegrees">Angle measued in degrees</param>
	/// <returns>Angle measured in radians</returns>
	public static float DegToRad(this float angleDegrees) => angleDegrees / 180f * MathF.PI;

	/// <summary>
	/// Converts angle measured in radians to angle measured in degrees
	/// </summary>
	/// <param name="angleRadians">Angle measued in radians</param>
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
