using System;
using System.Collections.Generic;


namespace Rephidock.GeneralUtilities.Collections {

	/// <summary>
	/// <para>
	/// Represent a pair of values.
	/// Equivalent to ValueTuple in future versions of .NET
	/// </para>
	/// <para>
	/// Unlike <see cref="KeyValuePair{TKey, TValue}"/> there
	/// is no implied relation between the items of the pair.
	/// </para>
	/// </summary>
	public struct Pair<T1, T2> : IEquatable<Pair<T1, T2>> {

		/// <summary>The first item of this pair.</summary>
		public T1 First { get; set; }

		/// <summary>The second item of this pair.</summary>
		public T2 Second { get; set; }

		/// <summary>Creates a generic pair of two items.</summary>
		public Pair(T1 first, T2 second) {
			First = first;
			Second = second;
		}

		/// <summary>
		/// Implicitly converts a <see cref="KeyValuePair{TKey, TValue}"/>
		/// into a <see cref="Pair{T1, T2}"/>. The key becomes the first item.
		/// </summary>
		public static implicit operator Pair<T1, T2>(KeyValuePair<T1, T2> kvpair) {
			return new Pair<T1, T2>(kvpair.Key, kvpair.Value);
		}

		/// <summary>Returns <see langword="true"/> if the provided object is equal to this pair.</summary>
		public override bool Equals(object obj) {
			if (obj is Pair<T1, T2>) return Equals((Pair<T1, T2>)obj);
			return false;
		}


		/// <summary>
		/// Returns <see langword="true"/> if the provided pair is equal to this pair.
		/// Two pairs are equal if their corresponding elements are equal.
		/// </summary>
		public bool Equals(Pair<T1, T2> other) {
			return First.Equals(other.First) && Second.Equals(other.Second);
		}

		/// <inheritdoc/>
		public override int GetHashCode() => base.GetHashCode();

		/// <inheritdoc/>
		public override string ToString() => $"Pair[{First}, {Second}]";

	}

}
