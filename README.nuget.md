# GeneralUtilities

[![GitHub Licence Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE)

 A package with general utilities that may be useful.

## Contents



### Arithmetic

| Method                           | Summary                                            |
| -------------------------------- | -------------------------------------------------- |
| (extension) `int.TrueMod`[1]     | Performs a modulo operation (`%` is remainder)     |
| (extension) `int.Wrap`[1]        | Wraps value into given range                       |
| (extension) `int.DigitalRoot`[1] | Calculates digital root (repeated digit sum)       |
| (extension) `int.GetFactors`     | Returns all factors of an integer                  |
| (extension) `float.DegToRad`[1]  | Converts angle in degrees to radians               |
| (extension) `float.RadToDeg`[1]  | Converts angle in radians to degrees               |
| `MoreMath.Lerp`                  | Linearly interpolates between 2 values             |
| `MoreMath.ReverseLerp`           | Inverse of `Lerp` (returns lerp amount form value) |
| `MoreMath.TabShift`              | Returns column position of a character after tab   |
| `MoreMath.AngleDifference`       | Calculates the shortest distance between 2 angles  |



Some methods also exist for `BigInteger`:

| Method                               | Summary                               |
| ------------------------------------ | ------------------------------------- |
| (extension) `BigInteger.Sqrt`        | Returns a square root of `BigInteger` |
| (extension) `BigInteger.TrueMod `    | *Same as above*                       |
| (extension) `BigInteger.Wrap`        | *Same as above*                       |
| (extension) `BigInteger.DigitalRoot` | *Same as above*                       |
| (extension) `BigInteger.GetFactors`  | *Same as above*                       |
| (extension) `BigInteger.ToDigits`    | *Same as below*                       |
| `BigIntMath.FromDigits`              | *Same as below*                       |
| `BigIntMath.Lerp`                    | *Same as above*                       |



### Arbitrary Base Representation

Use `RadixMath` to perform operations with digits with arbitrary base, represented as arrays of digit values:

| Method                        | Summary                                          |
| ----------------------------- | ------------------------------------------------ |
| (extension) `int.ToDigits`[1] | Converts a value to an array of digits           |
| `RadixMath.FromDigits`        | Converts an array of digits to a value           |
| `RadixMath.CountAllAscending` | Enumerates all numbers with a given places count |



### Other

| Class                                | Summary                              |
| ------------------------------------ | ------------------------------------ |
| (static) `EnumConverter<TEnum,TInt>` | A generic enum <-> integer converter |

| Method                                  | Summary                                         |
| --------------------------------------- | ----------------------------------------------- |
| (extension) `T.Yield<T>`                | Wraps anything in a `IEnumerable<T>`            |
| (extension) `IEnumerable<T>.JoinString` | A fluent way to call `string.Join`              |
| (extension) `char[].JoinString`         | A fluent way to call string constructor         |
| (extension) `T[].SplitIntoSegments`     | "Splits" array into `ArraySegment<T>`s          |
| (extension) `Type.IsSubclassOrSelfOf`   | Checks if a type is base type or subclass of it |



This package also implements some methods that were added in .NET7 as extensions for .NET6

| Extension Method for .NET6             | Summary                              |
| -------------------------------------- | ------------------------------------ |
| `IList<T>.AsReadOnly`                  | Constructs a `ReadOnlyCollection<T>` |
| `IDictionary<TKey, TValue>.AsReadOnly` | Constructs a `ReadOnlyDictionary<T>` |



### `.Randomness` namespace

The `.Randomness` namespace relates to `System.Random`

| Class             | Summary                                                    |
| ----------------- | ---------------------------------------------------------- |
| `ShuffleIndexMap` | The index map of a shuffle (to track where items ended up) |

| Extension method           | Summary                                                   |
| -------------------------- | --------------------------------------------------------- |
| `Random.NextUInt31`        | Returns a random int in range of [0, int.MaxValue]        |
| `Random.Chance`            | Returns `true` with %-chance                              |
| `Random.GetItem`           | Returns a random item from a list or span                 |
| `Random.GetDifferentItems` | Returns multiple different random items from a collection |
| `Random.Shuffle`           | Shuffles given items in-place                             |
| `Random.ShuffleRemap`      | Shuffles given items in-place and returns and index map   |

The following methods also exist and are extensions on collection interfaces to allow fluent syntax:

- `IReadOnlyList<T>.PickRandom` is equivalent to `Random.GetItem`
- `IReadOnlyCollection<T>.PickMultipleDifferent` is equivalent to `Random.GetDifferentItems`
- `IList<T>.Shuffle` is equivalent to `Random.Shuffle`
- `IList<T>.ShuffleRemap` is equivalent to `Random.ShuffleRemap`



### `.Color` namespace

The `.Color` namespace relates to `System.Drawing.Color`

| Method                          | Summary                                          |
| ------------------------------- | ------------------------------------------------ |
| (extension) `Color.WithAlpha`   | Returns source `Color` with given alpha          |
| (extension) `Color.Transparent` | Returns source `Color` with alpha of 0           |
| `ColorMath.LerpColor`           | Linearly interpolates between 2 colors           |
| `ColorMath.AlphaBlend`          | Blend 2 colors with alpha-1-minus-alpha blending |



[1]: Extension also exists for other numeric types.

*\* - Extension methods are static methods and can be used as such.*
