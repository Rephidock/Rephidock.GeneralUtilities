using System;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides static methods for some
/// arithmatic functions.
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
		return (int)MathF.Round(Lerp(start, end, amount));
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

}
