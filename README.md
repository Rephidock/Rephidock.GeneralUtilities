# GeneralUtilities

[![GitHub License Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE) [![Nuget Version Badge](https://img.shields.io/nuget/v/Rephidock.GeneralUtilities?logo=nuget)](https://www.nuget.org/packages/Rephidock.GeneralUtilities)

A collection of general utilities useful for other projects.

## Contents

All utilities are split into the following sub-namespaces:

### `.Maths` namespace

The package provides utilities for arithmetic, including work with arbitrary bases.

All of the listed methods are static.

Note: extension methods also exit for other numeric types when applicable. Additionally overloads for `BigInteger` exist.

| Method (`MoreMath`)           | Summary                                           |
| ----------------------------- | ------------------------------------------------- |
| (extension) `int.PosMod`      | Calculates modulo (`%` but always positive)       |
| (extension) `int.Wrap`        | Wraps value into given range                      |
| (extension) `int.GetFactors`  | Performs integer factorization                    |
| `MoreMath.Lerp`               | Linearly interpolates between 2 values            |
| `MoreMath.InverseLerp`        | Inverse of `Lerp` (returns lerp %-age form value) |
| `MoreMath.TabShift`           | Returns column position of a character after tab  |
| `MoreMath.AngleDifference`    | Calculates the shortest distance between 2 angles |
| (extension) `float.DegToRad`  | Converts angle in degrees to radians              |
| (extension) `float.RadToDeg`  | Converts angle in radians to degrees              |
| (extension) `BigInteger.Sqrt` | Returns a square root of `BigInteger` as `double` |

| Method (`RadixMath`)             | Summary                                          |
| -------------------------------- | ------------------------------------------------ |
| (extension) `int.DigitalRoot`    | Calculates digital root (repeated digit sum)     |
| (extension) `int.ToDigits`       | Converts a value into an array of digits         |
| `RadixMath.FromDigits`           | Converts an array of digits into a value         |
| `RadixMath.BigIntegerFromDigits` | Same as `FromDigits` but returns a `BigInteger`  |
| `RadixMath.CountAllAscending`    | Enumerates all numbers with a given places count |



### `.Collections` namespace

The following methods for work with enumerables exist:

| Method (`GeneralEnumerableExtensions`)  | Summary                                 |
| --------------------------------------- | --------------------------------------- |
| (extension) `T.Yield<T>`                | Wraps anything in a `IEnumerable<T>`    |
| (extension) `IEnumerable<T>.JoinString` | A fluent way to call `string.Join`      |
| (extension) `char[].JoinString`         | A fluent way to call string constructor |
| (extension) `T[].SplitIntoSegments`     | "Splits" array into `ArraySegment<T>`s  |


This package also implements some methods that were added in .NET7 as extensions for .NET6

| .NET6 Extension Method (`ReadOnlyExtensions`) | Summary                              |
| --------------------------------------------- | ------------------------------------ |
| `IList<T>.AsReadOnly`                         | Constructs a `ReadOnlyCollection<T>` |
| `IDictionary<TKey, TValue>.AsReadOnly`        | Constructs a `ReadOnlyDictionary<T>` |



### `.Randomness` namespace

The `.Randomness` namespace relates to `System.Random`

| Class             | Summary                                                    |
| ----------------- | ---------------------------------------------------------- |
| `ShuffleIndexMap` | The index map of a shuffle (to track where items ended up) |

| Method (`RandomnessExtensions`)        | Summary                                             |
| -------------------------------------- | --------------------------------------------------- |
| (extension) `Random.NextUInt31`        | Returns a random int in range of [0, int.MaxValue]  |
| (extension) `Random.Chance`            | Returns `true` with %-chance                        |
| (extension) `Random.GetItem`           | Randomly picks an item from a list or span          |
| (extension) `Random.GetDifferentItems` | Randomly picks multiple different items             |
| (extension) `Random.Shuffle`           | Shuffles items in-place                             |
| (extension) `Random.ShuffleRemap`      | Shuffles items in-place & returns `ShuffleIndexMap` |

The following methods also exist and are extensions on collection interfaces to allow fluent syntax:

| Collection extension method                    | Above equivalent           |
| ---------------------------------------------- | -------------------------- |
| `IReadOnlyList<T>.PickRandom`                  | `Random.GetItem`           |
| `IReadOnlyCollection<T>.PickMultipleDifferent` | `Random.GetDifferentItems` |
| `IList<T>.Shuffle`                             | `Random.Shuffle`           |
| `IList<T>.ShuffleRemap`                        | `Random.ShuffleRemap`      |



### `.Reflection` namespace

The `.Reflection` namespace contains reflections extensions and a generic enum <-> integer converter.

| Method (`EnumConverter<TEnum,TInt>`) | Summary                                    |
| ------------------------------------ | ------------------------------------------ |
| `EnumConverter<TEnum,TInt>.ToInt`    | Converts an enum value to an integral type |
| `EnumConverter<TEnum,TInt>.ToEnum`   | Converts an integral value to an enum type |

| Method (`ReflectionExtensions`)       | Summary                                         |
| ------------------------------------- | ----------------------------------------------- |
| (extension) `Type.IsSubclassOrSelfOf` | Checks if a type is base type or subclass of it |
| (extension) `MethodInfo.IsOverride`   | Checks if a method is an override               |



*\* - Reminder that extension methods are static methods and can be used as such.*
