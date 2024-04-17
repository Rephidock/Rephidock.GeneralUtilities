using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;


namespace Rephidock.GeneralUtilities {


/// <summary>
/// Provides methods to operate on numbers
/// in arbitrary bases
/// </summary>
public static class RadixMath {

	/// <summary>
	/// Converts a value to an arbitrary base,
	/// returning arrays of digits in that base,
	/// units place last.
	/// </summary>
	/// <param name="value">The value to convert. If negative, absolute is used.</param>
	/// <param name="radix">The base of the returned number</param>
	/// <param name="padToPlaces">
	/// <para>The number of places to pre-pad the number to with zeros.</para>
	/// <para>The resulting array can be larger if there are not enough digits.</para>
	/// </param>
	/// <exception cref="ArgumentException"><paramref name="radix"/> is below 2</exception>
	public static ushort[] ToDigits(this long value, ushort radix, int padToPlaces = -1) {

		// Guards
		if (radix < 2) {
			throw new ArgumentException("Base must be at least 2", nameof(radix));
		}

		// Take absolute
		if (value < 0) value = -value;

		// Get digits
		List<ushort> digitsStartingFromUnits = new List<ushort>(padToPlaces > 0 ? padToPlaces : 4);

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

	/// <inheritdoc cref="ToDigits(long, ushort, int)"/>
	public static ushort[] ToDigits(this int value, ushort radix, int padToPlaces = -1) {
		return ToDigits((long)value, radix, padToPlaces);
	}

	/// <summary>
	/// Converts an array of digits in a given base,
	/// units place last, to an integer value.
	/// </summary>
	/// <param name="digits">The digits to convert, units place last</param>
	/// <param name="radix">The base of the returned number</param>
	/// <exception cref="ArgumentException"><paramref name="radix"/> is below 2</exception>
	/// <exception cref="OverflowException">Resulting value is too big for a <see cref="long"/></exception>
	public static long FromDigits(this ushort[] digits, ushort radix) {

		// Guards
		if (radix < 2) {
			throw new ArgumentException("Base must be at least 2", nameof(radix));
		}

		// Add up all the digits with respective powers of radix
		long result = 0;
		checked {
			long currentMultiplier = 1;

			for (int i = digits.Length - 1; i >= 0; i--) {

				// Add value to the result
				result += digits[i] * currentMultiplier;

				// Updte multiplier
				currentMultiplier *= radix;
			}
		}

		return result;
	}

	/// <summary>
	/// <para>
	/// Enumerates all positive numbers in an arbitrary base
	/// up to a given digit length,
	/// returning arrays of digits in that base,
	/// units place last.
	/// </para>
	/// <para>
	/// Starts with array of zeros.
	/// Digit at the end of the array is incremented first.
	/// </para>
	/// </summary>
	/// <param name="radix">The base of the counter (exclusive maxim value of individual digits)</param>
	/// <param name="places">The number of the digits in the counter</param>
	/// <exception cref="ArgumentException">
	/// <paramref name="radix"/> is smaller than 2 or
	/// <paramref name="places"/> is smaller than 1
	/// </exception>
	public static IEnumerable<ushort[]> CountAllAscending(ushort radix, int places) {

		// Guards
		if (places < 1) {
			throw new ArgumentException("There must be at least one place in the counter", nameof(places));
		}

		if (radix < 2) {
			throw new ArgumentException("Base must be at least 2", nameof(radix));
		}

		// Create current with all 0 
		ushort[] currentCount = new ushort[places];

		// Main loop
		while (true) {

			// Return a copy
			yield return currentCount.ToArray();

			// Add one
			bool carry = true;
			for (int i = places - 1; i >= 0; i--) {

				// Carry the carry to the next place
				if (currentCount[i] == radix - 1) {
					currentCount[i] = 0;
					continue;
				}

				// Consume the carry
				currentCount[i] += 1;
				carry = false;
				break;

			}

			// Overflowed to 0 -- counting ended
			if (carry) break;

		}

	}

	/// <inheritdoc cref="CountAllAscending(ushort, int)"/>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Method is obsolete due to a spelling error. Use CountAllAscending instead.")]
	public static IEnumerable<ushort[]> CountAllAccending(ushort radix, int places) {
		return CountAllAscending(radix, places);
	}

}

}