# GeneralUtilities

[![GitHub Licence Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE)

 A package with general utilities that may be useful.

## Contents



| Class                                | Summary                              |
| ------------------------------------ | ------------------------------------ |
| (static) `EnumConverter<TEnum,TInt>` | A generic enum <-> integer converter |

| Method                                  | Summary                                            |
| --------------------------------------- | -------------------------------------------------- |
| (extension) `T.Yield<T>`                | Wraps anything in a `IEnumerable<T>`               |
| (extension) `IEnumerable<T>.JoinString` | A fluent way to call `string.Join`                 |
| (extension) `int.TrueMod`[^1]           | Performs a modulo operation (`%` is remainder)     |
| (extension) `int.Wrap`[^1]              | Wraps value into given range                       |
| (extension) `Random.NextUInt31`         | Returns a random int in range of [0, int.MaxValue] |
| (extension) `Type.IsSubcalssOrSelfOf`   | Checks if a type is base type or subclass of it    |
| `MoreMath.Lerp`                         | Linearly interpolates between 2 values             |
| `MoreMath.ReverseLerp`                  | Inverse of `Lerp` (returns lerp amount form value) |
| `MoreMath.TabShift`                     | Returns column position of a character after tab   |

[^1]: Also exits for `float` and `double`



### `.Color` namespace

The `.Color` namespace relates to `System.Drawing.Color`

| Method                          | Summary                                          |
| ------------------------------- | ------------------------------------------------ |
| (extension) `Color.WithAlpha`   | Returns source `Color` with given alpha          |
| (extension) `Color.Transparent` | Returns source `Color` with alpha of 0           |
| `ColorMath.LerpColor`           | Linearly interpolates between 2 colors           |
| `ColorMath.AlphaBlend`          | Blend 2 colors with alpha-1-minus-alpha blending |
