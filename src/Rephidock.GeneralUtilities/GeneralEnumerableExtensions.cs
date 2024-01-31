using System;
using System.Collections.Generic;
using System.Linq;


namespace Rephidock.GeneralUtilities;


/// <summary>
/// Provides extensions for <see cref="IEnumerable{T}"/>
/// </summary>
public static class GeneralEnumerableExtensions {

	/// <summary>
	/// Returns an IEnumerable with a single given item
	/// (yields given item)
	/// </summary>
	/// <typeparam name="T">The type of the item</typeparam>
	/// <param name="item">The item to yield</param>
	public static IEnumerable<T> Yield<T>(this T item) {
		yield return item;
	}

}
