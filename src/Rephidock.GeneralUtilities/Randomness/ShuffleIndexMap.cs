using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace Rephidock.GeneralUtilities.Randomness {


/// <summary>
/// A collection of index pairs, where
/// key is the old index and value is the new index.
/// Can be used for tracking where the items ended up.
/// </summary>
/// <remarks>
/// Is an abstraction over a shuffled array of indexes.
/// </remarks>
public class ShuffleIndexMap : IEnumerable<KeyValuePair<int, int>> {

	#region //// Storage
	
	/// <summary>An array which translates old indexes to new indexes</summary>
	readonly protected int[] oldToNew;

	/// <inheritdoc/>
	public int Count => oldToNew.Length;

	#endregion

	#region //// Creation

	/// <summary>
	/// Generates an identity index mapping.
	/// (each index is mapped to itself)
	/// </summary>
	/// <param name="size">Number of indexes in the mapping</param>
	/// <exception cref="ArgumentException"><paramref name="size"/> is negative</exception>
	protected ShuffleIndexMap(int size) {

		// Safe guards
		if (size < 0) throw new ArgumentException("Size for a shuffle map cannot be negative.", nameof(size));

		// 0-length
		if (size == 0) {
			oldToNew = new int[0];
			return;
		}

		// Indexes with no shuffle
		oldToNew = new int[size];
		for (int i = 0; i < size; i++) {
			oldToNew[i] = i;
		}

	}

	/// <summary>Generates a random mapping that can be used for shuffling.</summary>
	/// <param name="size">Number of indexes in the shuffle</param>
	/// <param name="rng">The random number generator</param>
	/// <exception cref="ArgumentException"><paramref name="size"/> is negative</exception>
	public ShuffleIndexMap(int size, Random rng) : this(size) {
		oldToNew.Shuffle(rng);
	}

	#endregion

	/// <summary>
	/// Applies mapping to the list.
	/// Mutates the list.
	/// </summary>
	/// <param name="values">List to shuffle</param>
	public void ApplyTo<T>(IList<T> values) {

		// Safe guards
		if (values == null) throw new ArgumentNullException(nameof(values));
		
		// Apply mapping
		T[] oldList = values.ToArray();
		for (int i = 0; i < oldToNew.Length; i++) {
			values[oldToNew[i]] = oldList[i];
		}

	}

	/// <summary>
	/// Returns array of new positions at indexes of old positions.
	/// </summary>
	/// <remarks>
	/// Essentially copies the internal index array without the abstraction.
	/// </remarks>
	public int[] ToArrayOfIndexes() {
		return oldToNew.ToArray();
	}

	/// <summary>
	/// Returns the new position, corresponding to an old position.
	/// </summary>
	/// <param name="oldIndex">The index to use as a key (old index).</param>
	/// <returns>New index, corresponding to the old index.</returns>
	public int this[int oldIndex] => oldToNew[oldIndex];

	#region //// GetEnumerator

	/// <inheritdoc/>
	public IEnumerator<KeyValuePair<int, int>> GetEnumerator() {
		return oldToNew.Select((value, at) => new KeyValuePair<int, int>(at, value)).GetEnumerator();
	}

	/// <inheritdoc/>
	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	#endregion

}

}