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

	/// <summary>
	/// Joins elements into a single delimited string.
	/// (Fluently performs <see cref="string.Join{T}(string?, IEnumerable{T})"/>)
	/// </summary>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, string separator) {
		return string.Join(separator, items);
	}

	/// <summary>
	/// Joins elements into a single delimited string.
	/// (Fluently performs <see cref="string.Join{T}(char, IEnumerable{T})"/>)
	/// </summary>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, char separator) {
		return string.Join(separator, items);
	}

	/// <summary>
	/// Joins a sequence of characters into a string.
	/// (Fluent way to call <see cref="string.Concat{T}(IEnumerable{T})"/>)
	/// </summary>
	public static string JoinString(this IEnumerable<char> characters) {
		return string.Concat(characters);
	}

	/// <summary>
	/// Joins an array of characters into a string.
	/// (Fluent way to call <see cref="string(char[])"/>)
	/// </summary>
	public static string JoinString(this char[] characters) {
		return new string(characters);
	}

	/// <summary>
	/// Joins part of an array of characters into a string.
	/// (Fluent way to call <see cref="string(char[], int, int)"/>)
	/// </summary>
	public static string JoinString(this char[] characters, int startIndex, int length) {
		return new string(characters, startIndex, length);
	}

}
