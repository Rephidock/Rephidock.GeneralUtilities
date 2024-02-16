using System;
using System.Collections.Generic;


namespace Rephidock.GeneralUtilities.Randomness;


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

	/// <summary>
	/// Returns <see langword="true"/> with %-chance (0 to 1 both inclusive)
	/// </summary>
	/// <param name="rng">The random number generator</param>
	/// <param name="chance">The chance of <see langword="true"/> being returned</param>
	public static bool Chance(this Random rng, double chance) {
		return rng.NextDouble() < chance;
	}

	#region //// PickRandom

	/// <summary>Returns a random item form a list or span</summary>
	/// <typeparam name="T">The type of the item</typeparam>
	/// <param name="items">List of items to pick from</param>
	/// <param name="rng">Random number generator</param>
	/// <inheritdoc cref="PickRandom{T}(IReadOnlyList{T}, Random)"/>
	public static T GetItem<T>(this Random rng, IReadOnlyList<T> items) {
		if (items.Count == 0) throw new ArgumentException("Cannot pick items from empty list", nameof(items));
		return items[rng.Next(0, items.Count)];
	}

	/// <inheritdoc cref="GetItem{T}(Random, IReadOnlyList{T})"/>
	public static T GetItem<T>(this Random rng, ReadOnlySpan<T> items) {
		if (items.Length == 0) throw new ArgumentException("Cannot pick items from empty span", nameof(items));
		return items[rng.Next(0, items.Length)];
	}

	/// <inheritdoc cref="GetItem{T}(Random, IReadOnlyList{T})"/>
	public static T PickRandom<T>(this IReadOnlyList<T> items, Random rng) {
		return rng.GetItem(items);
	}

	#endregion

	#region //// PickMultipleDifferent

	/// <summary>
	/// Picks multiple different items from a collection.
	/// Retains order the items were in.
	/// </summary>
	/// <typeparam name="T">The type of items in the collection</typeparam>
	/// <param name="items">The collection to pick items from</param>
	/// <param name="count">The number of items to pick</param>
	/// <param name="rng">Random number generator</param>
	/// <returns>An array of picked items in the order they were in collection.</returns>
	public static T[] PickMultipleDifferent<T>(this IReadOnlyCollection<T> items, int count, Random rng) {

		// Guards
		ArgumentNullException.ThrowIfNull(items, nameof(items));

		if (count < 0) {
			throw new ArgumentException("Cannot pick a negative number of items from a collection", nameof(count));
		}

		if (items.Count < count) {
			throw new ArgumentException("Cannot pick more items than a collection contains", nameof(count));
		}

		// Pick
		T[] result = new T[count];

		if (count == 0) return result;

		if (count == 1 && items is IReadOnlyList<T> list) {
			result[0] = list.PickRandom(rng);
			return result;
		}

		// Selection sampling
		int leftToPick = count;
		int itemsLeft = items.Count;
		foreach (T item in items) {

			if (rng.Chance(leftToPick / itemsLeft)) {
				result[count - leftToPick] = item;
				leftToPick--;
				if (leftToPick < 1) break;
			}

			itemsLeft--;
		}

		return result;
	}

	/// <inheritdoc cref="PickMultipleDifferent{T}(IReadOnlyCollection{T}, int, Random)"/>
	public static T[] GetDifferentItems<T>(this Random rng, IReadOnlyCollection<T> collection, int count) {
		return collection.PickMultipleDifferent(count, rng);
	}

	#endregion

	#region //// Shuffle

	/// <summary>
	/// Shuffles a list in-place (mutates the array).
	/// </summary>
	/// <param name="rng">The random number generator</param>
	/// <param name="values">The values to shuffle</param>
	/// <remarks>Performes Durstenfeld's version of Fisher–Yates shuffle</remarks>
	public static void Shuffle<T>(this Random rng, IList<T> values) {

		// i from (length − 1) till 1
		for (int i = values.Count - 1; i >= 1; i--) {

			// Pick random j, such that 0 <= j <= i
			int j = rng.Next(0, i + 1);

			// Swap values at i and j
			(values[j], values[i]) = (values[i], values[j]);	
		}

	}

	/// <inheritdoc cref="Shuffle{T}(Random, IList{T})"/>
	/// <returns>The list operated on.</returns>
	public static IList<T> Shuffle<T>(this IList<T> values, Random rng) {
		rng.Shuffle(values);
		return values;
	}

	/// <summary>
	/// <para>
	/// Shuffles the given list and also
	/// gives the mapping of old indexes to new indexes.
	/// </para>
	/// <para>
	/// Suffles in-place (mutates the list).
	/// </para>
	/// </summary>
	/// <param name="values">List to shuffle</param>
	/// <param name="rng">The random number generator</param>
	public static ShuffleIndexMap ShuffleRemap<T>(this IList<T> values, Random rng) {

		// Create a shuffle
		ShuffleIndexMap oldToNew = new(values.Count, rng);

		// Apply the shuffle
		oldToNew.ApplyTo(values);

		// Return
		return oldToNew;
	}

	/// <inheritdoc cref="ShuffleRemap{T}(IList{T}, Random)"/>
	public static ShuffleIndexMap ShuffleRemap<T>(this Random rng, IList<T> values) {
		return ShuffleRemap(values, rng);
	}

	/// <summary>
	/// <para>
	/// Shuffles the given span and also
	/// gives the mapping of old indexes to new indexes.
	/// </para>
	/// <para>
	/// Mutates the span.
	/// </para>
	/// </summary>
	/// <param name="values">Span to shuffle</param>
	/// <param name="rng">The random number generator</param>
	public static ShuffleIndexMap ShuffleRemap<T>(this Random rng, Span<T> values) {

		// Create a shuffle
		ShuffleIndexMap oldToNew = new(values.Length, rng);

		// Apply the shuffle
		oldToNew.ApplyTo(values);

		// Return
		return oldToNew;
	}

#endregion

}
