using System;
using System.Collections.Generic;
using System.Linq;


namespace Rephidock.GeneralUtilities {


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

	/// <summary>
	/// Fulently performs <see cref="string.Join{T}(string?, IEnumerable{T})"/>
	/// </summary>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, string separator) {
		return string.Join(separator, items);
	}

	/// <summary>
	/// Fulently performs <see cref="string.Join{T}(char, IEnumerable{T})"/>
	/// </summary>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, char separator) {
		return string.Join(separator, items);
	}

}

}