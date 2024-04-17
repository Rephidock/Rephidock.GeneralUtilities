using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Rephidock.GeneralUtilities {


/// <summary>
/// Provides extensions added in .net7 to fluently
/// create readonly collections to .net6
/// </summary>
public static class ReadOnlyExtensions {

	/// <summary>
	/// Returns a readonly <see cref="ReadOnlyCollection{T}"/>
	/// wrapper for the given list.
	/// (Fluent way to call ReadOnlyCollection's constructor)
	/// </summary>
#if NET7_0_OR_GREATER
	public static ReadOnlyCollection<T> AsReadOnly<T>(IList<T> list) {
#else
	public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list) {
#endif
		return new ReadOnlyCollection<T>(list);
	}

/*
	/// <summary>
	/// Returns a readonly <see cref="ReadOnlyDictionary{TKey, TValue}"/>
	/// wrapper for the given dictionary.
	/// (Fluent way to call ReadOnlyDictionary's constructor)
	/// </summary>
#if NET7_0_OR_GREATER
	public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
#else
	public static ReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
#endif
	where TKey : notnull {
		return new ReadOnlyDictionary<TKey, TValue>(dictionary);
	}
*/

}

}