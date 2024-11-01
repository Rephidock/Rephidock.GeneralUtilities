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

}

}