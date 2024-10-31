using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Rephidock.GeneralUtilities.Collections {


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
	public static ReadOnlyCollection<T> AsReadOnly<T>(this IList<T> list) {
		return new ReadOnlyCollection<T>(list);
	}

}

}
