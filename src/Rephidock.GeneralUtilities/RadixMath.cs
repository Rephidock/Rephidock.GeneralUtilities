using System;
using System.Collections.Generic;
using System.Linq;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides methods to operate on numbers
/// in arbitrary bases
/// </summary>
public static class RadixMath {

	/// <summary>
	/// <para>
	/// Enumerates all positive numbers in an arbitaray base
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
	public static IEnumerable<ushort[]> CountAllAccending(ushort radix, int places) {

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

}

