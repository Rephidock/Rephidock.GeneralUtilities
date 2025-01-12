using System;
using System.Collections.Generic;
using System.Linq;


namespace Rephidock.GeneralUtilities.Collections {


/// <summary>
/// Provides extensions for <see cref="IEnumerable{T}"/>
/// which were added in future versions of .NET
/// </summary>
public static class FutureEnumerableExtensions {

	/// <summary>Appends a value to the end of the sequence.</summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source">Sequence of values to append to</param>
	/// <param name="item">The value to append</param>
	/// <returns>A new sequence that ends with the appended element</returns>
	public static IEnumerable<T> Append<T>(this IEnumerable<T> source, T item) {
		foreach (T sourceElement in source) yield return sourceElement;
		yield return item;
	}

	/// <summary>Prepends a value to the start of the sequence.</summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="source">Sequence of values to prepend to</param>
	/// <param name="item">The value to prened</param>
	/// <returns>A new sequence that starts with the prepended element</returns>
	public static IEnumerable<T> Prepend<T>(this IEnumerable<T> source, T item) {
		yield return item;
		foreach (T sourceElement in source) yield return sourceElement;
	}

	/// <summary>
	/// Produces a sequence of pairs with elements from the two specified sequences.
	/// </summary>
	/// <typeparam name="TFirst">The type of the elements of the first input sequence</typeparam>
	/// <typeparam name="TSecond">The type of the elements of the second input sequence.</typeparam>
	/// <param name="first">The first sequence to merge.</param>
	/// <param name="second">The second sequence to merge.</param>
	/// <returns>
	/// An <see cref="IEnumerable{T}"/> that contains merged elements of two input sequences
	/// </returns>
	public static IEnumerable<Pair<TFirst, TSecond>> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) {

		// Guards
		if (null == first) throw new ArgumentNullException("first");
		if (null == second) throw new ArgumentNullException("second");

		// Enumerate
		using (var enumFirst = first.GetEnumerator()) {
			using (var enumSecond = second.GetEnumerator()) {

				while (enumFirst.MoveNext() && enumSecond.MoveNext()) {
					yield return new Pair<TFirst, TSecond>(enumFirst.Current, enumSecond.Current);
				}

			}
		}

	}

}

}