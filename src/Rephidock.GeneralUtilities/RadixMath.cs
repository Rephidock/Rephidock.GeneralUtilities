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
	/// An integer resperentation in a base (radix).
	/// </summary>
	public class RadixInteger {

		sbyte _sign = 1;

		/// <summary>
		/// The sign of the number, <c>1</c> for positive and <c>-1</c> for negative.
		/// </summary>
		/// <remarks>
		/// Allows for existance of negative zero.
		/// </remarks>
		/// <exception cref="ArgumentException">Value of <c>0</c> is set</exception>
		public int Sign {
			get => _sign;
			set {
				if (value > 0) _sign = 1;
				else if (value < 0) _sign = -1;
				else throw new ArgumentException("Cannot set sign of an integer to 0");
			}
		}

		/// <summary>The radix/base of the integer</summary>
		public ushort Radix { get; private init; }
		
		/// <summary>The number of allocated digits in the number</summary>
		public int Places => Digits.Length;

		/// <summary>
		/// <para>
		/// The individual digits of the integer, units place last.
		/// </para>
		/// <para>
		/// Not immutable and never empty.
		/// </para>
		/// </summary>
		public ushort[] Digits { get; private init; }

		/// <summary>
		/// Allocates a radix number in a certain base
		/// with a certain number of digits.
		/// </summary>
		/// <param name="radix">The base of the number.</param>
		/// <param name="places">The number of digits the number has.</param>
		/// <param name="sign">The sign of the number: <c>1</c> for positive and <c>-1</c> for negative</param>
		public RadixInteger(ushort radix, int places = 1, int sign = 1) {

			// Guards
			if (places < 1) {
				throw new ArgumentException("There must be at least one place in the counter", nameof(places));
			}

			if (radix < 2) {
				throw new ArgumentException("Base must be at least 2", nameof(radix));
			}

			// Create
			Radix = radix;
			Digits = new ushort[places];
			Sign = sign;
		}

		/// <summary>
		/// Allocates a radix number in a certain base
		/// with initial digits.
		/// </summary>
		/// <param name="radix">The base of the number.</param>
		/// <param name="digits">The digits to copy.</param>
		/// <param name="sign">The sign of the number: <c>1</c> for positive and <c>-1</c> for negative</param>

		public RadixInteger(ushort radix, ushort[] digits, int sign = 1) {

			// Guards
			if (radix < 2) {
				throw new ArgumentException("Base must be at least 2", nameof(radix));
			}

			// Create
			int places = digits.Length;
			if (places < 1) places = 1;

			Radix = radix;
			Digits = new ushort[places];
			Sign = sign;

			// Copy digits
			for (int i = 0; i < digits.Length; i++) {

				if (digits[i] < 0) {
					throw new ArgumentException("A digit cannot be negative", nameof(digits));
				}

				if (digits[i] >= radix) {
					throw new ArgumentException("A digit cannot have a value of radix or higher", nameof(digits));
				}

				Digits[i] = digits[i];
			}

		}

	}

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
	public static IEnumerable<RadixInteger> CountAllAccending(ushort radix, int places) {

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
			yield return new RadixInteger(radix, currentCount);

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

