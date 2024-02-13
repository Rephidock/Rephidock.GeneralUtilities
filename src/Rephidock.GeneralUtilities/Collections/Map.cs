using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;


namespace Rephidock.GeneralUtilities.Collections;


/// <summary>
/// <para>
/// A collecion of bijective pairs of items (a bidirectional dictionary).
/// </para>
/// <para>
/// Use <see cref="Forward"/> and <see cref="Reverse"/> indexers
/// to access the map in forward or backward direction.
/// </para>
/// </summary>
public class Map<T1, T2> : IReadOnlyCollection<KeyValuePair<T1, T2>> 
	where T1 : notnull
	where T2 : notnull
{

	/// <summary>The forward internal dictionary of the map.</summary>
	readonly protected Dictionary<T1, T2> forward;

	/// <summary>The backward internal dictionary of the map.</summary>
	readonly protected Dictionary<T2, T1> reverse;

	#region //// Indexers

	/// <summary>
	/// A way to access the <see cref="Map{T1, T2}"/> using 1st parameter (<typeparamref name="T1"/>)
	/// as keys and 2nd paramater (<typeparamref name="T2"/>) as values
	/// </summary>
	public MapIndexer<T1, T2> Forward { get; private set; }

	/// <summary>
	/// A way to access the <see cref="Map{T1, T2}"/> using 1st parameter (<typeparamref name="T1"/>)
	/// as values and 2nd paramater (<typeparamref name="T2"/>) as keys
	/// </summary>
	public MapIndexer<T2, T1> Reverse { get; private set; }

	/// <summary>
	/// A way to access the <see cref="Map{T1, T2}"/> in either
	/// forward or backward direction
	/// </summary>
	public sealed class MapIndexer<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
		where TKey : notnull
		where TValue : notnull
	{
	
		readonly private Dictionary<TKey, TValue> dictionary;
	
		internal MapIndexer(Dictionary<TKey, TValue> dictionary) {
			this.dictionary = dictionary;
		}

		/// <summary>
		/// Gets the value under specified key
		/// in current indexing direction
		/// </summary>
		public TValue this[TKey key] => dictionary[key];

		/// <summary>Gets the value associated specified key</summary>
		public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value) => dictionary.TryGetValue(key, out value);

		/// <summary>
		/// Gets a collection of all the keys
		/// in current indexing direction
		/// </summary>
		public IEnumerable<TKey> Keys => dictionary.Keys;

		/// <summary>
		/// Gets a collection of all the values
		/// in current indexing direction
		/// </summary>
		public IEnumerable<TValue> Values => dictionary.Values;

		/// <summary>
		/// Gets the number of pairs in the <see cref="Map{T1, T2}"/>
		/// </summary>
		public int Count => dictionary.Count;

		/// <summary>
		/// Determines if the <see cref="Map{T1, T2}"/> contains a key
		/// in current indexing direction
		/// </summary>
		public bool ContainsKey(TKey key) => dictionary.ContainsKey(key);

		/// <summary>
		/// Retuns a collection of all key-value pairs
		/// in current indexing direction
		/// </summary>
		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => dictionary.GetEnumerator();
		
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

	}

	#endregion

	// TODO

	/// <summary>
	/// Creates an empty map
	/// </summary>
	public Map() {
		forward = new Dictionary<T1, T2>();
		reverse = new Dictionary<T2, T1>();
		Forward = new MapIndexer<T1, T2>(forward);
		Reverse = new MapIndexer<T2, T1>(reverse);
	}

	public void Add(T1 t1, T2 t2) {
		forward.Add(t1, t2);
		reverse.Add(t2, t1);
	}

	/// <summary>
	/// Gets the number of pairs in the <see cref="Map{T1, T2}"/>
	/// </summary>
	public int Count => Forward.Count;

	/// <summary>
	/// Retuns a collection of all key-value pairs in forward indexing direction
	/// </summary>
	/// <remarks>
	/// Prefer using <see cref="Forward"/> indexer to be explicit
	/// </remarks>
	public IEnumerator<KeyValuePair<T1, T2>> GetEnumerator() => Forward.GetEnumerator();

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
