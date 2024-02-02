using System;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides extensions for <see cref="Random"/>
/// </summary>
public static class RandomnessExtensions {

	/// <summary>
	/// Returns a non-negative random <see cref="int"/>.
	/// Unlike <see cref="Random.Next()"/>, includes
	/// <see cref="int.MaxValue"/> as a possible value.
	/// </summary>
	public static int NextUInt31(this Random rng) {

		// Generate random bytes
		Span<byte> seedBytes = stackalloc byte[4];
		rng.NextBytes(seedBytes);

		// Strip the int32 sign bit
		seedBytes[3] &= 0b0111_1111;

		// Convert and return
		return BitConverter.ToInt32(seedBytes);
	}

}
