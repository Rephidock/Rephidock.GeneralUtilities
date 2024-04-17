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
	/// Joins elements into a single delimited string.
	/// (Fulently performs (netframework35) <see cref="string.Join(string, string[])"/>)
	/// </summary>
	public static string JoinString(this string[] items, string separator) {
		return string.Join(separator, items);
	}

	/// <inheritdoc cref="JoinString{TSource}(IEnumerable{TSource}, string)"/>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, string separator) {
		return items.Select(item => item.ToString()).ToArray().JoinString(separator);
	}

	/// <inheritdoc cref="JoinString{TSource}(IEnumerable{TSource}, string)"/>
	public static string JoinString<TSource>(this IEnumerable<TSource> items, char separator) {
		return items.JoinString(separator.ToString());
	}

	/// <summary>
	/// Joins a sequence of characters into a string.
	/// </summary>
	public static string JoinString(this IEnumerable<char> characters) {
		return new string(characters.ToArray());
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

	/// <summary>
	/// <para>
	/// Creates a sequence of <see cref="ArraySegment{T}"/>s based on
	/// a given array and delimiters.
	/// </para>
	/// <para>
	/// Note that a segment is only a view on the source array.
	/// </para>
	/// </summary>
	/// <param name="array">The array to split.</param>
	/// <param name="separators">The delimiters to split by.</param>
	public static IEnumerable<ArraySegment<T>> SplitIntoSegments<T>(this T[] array, params T[] separators) {

		int lastSeparatorIndex = -1;
		for (int i = 0; i < array.Length; i++) {

			// See if current character is a separator 
			bool foundSeparator = false;
			for (int sepI = 0; sepI < separators.Length; sepI++) {

				if (Array.IndexOf(separators, array[i]) >= 0) {
					foundSeparator = true;
					break;
				}

			}

			if (!foundSeparator) continue;
			
			// If it is - create new segment
			int start = lastSeparatorIndex + 1;
			int count = i - start;
			lastSeparatorIndex = i;
			yield return new ArraySegment<T>(array, start, count);

		}

		// Create final segment till the end of the array
		{
			int start = lastSeparatorIndex + 1;
			int count = array.Length - start;
			yield return new ArraySegment<T>(array, start, count);
		}

	}

}

}