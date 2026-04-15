using System;
using System.Collections.Generic;


namespace Rephidock.GeneralUtilities.Collections {

/// <summary>
/// Provides some extensions for collections
/// which were added in future versions of .NET
/// </summary>
public static class FutureCollectionExtensions {

    /// <summary>
    /// Returns a value from the dictionaries if it exists,
    /// otherwise returns the default value.
    /// </summary>
    public static TValue GetValueOrDefault<TKey, TValue>(
        this IDictionary<TKey, TValue> dictionary, 
        TKey key,
        TValue defaultValue = default(TValue)
    ) {

        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        TValue value;
        return dictionary.TryGetValue(key, out value) ? value : defaultValue;
    }

    /// <summary>
    /// Adds a value to the dictionary only if the dictionary
    /// does not already have a value under that key.
    /// Returns <see langword="true"/> if the value was added. 
    /// </summary>
    public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value) {
        
        if (dictionary == null) throw new ArgumentNullException(nameof(dictionary));

        if (dictionary.ContainsKey(key)) return false;
        
        dictionary.Add(key, value);
        return true;
    }
	
}

}