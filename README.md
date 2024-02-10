# GeneralUtilities

[![GitHub Licence Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE) [![Nuget Version Badge](https://img.shields.io/nuget/v/Rephidock.GeneralUtilities?logo=nuget)](https://www.nuget.org/packages/Rephidock.GeneralUtilities)

 A package with general utilities that may be useful.

## Contents



| Class                                | Summary                              |
| ------------------------------------ | ------------------------------------ |
| (static) `EnumConverter<TEnum,TInt>` | A generic enum <-> integer converter |

| Method                                  | Summary                                            |
| --------------------------------------- | -------------------------------------------------- |
| (extension) `T.Yield<T>`                | Wraps anything in a `IEnumerable<T>`               |
| (extension) `IEnumerable<T>.JoinString` | A fluent way to call `string.Join`                 |
| (extension) `int.TrueMod`               | Performs a modulo operation (`%` is remainder)     |
| (extension) `Random.NextUInt31`         | Returns a random int in range of [0, int.MaxValue] |
| `MoreMath.Lerp`                         | Linearly interpolates between 2 values             |
| `MoreMath.ReverseLerp`                  | Inverse of `Lerp` (returns lerp amount form value) |
| `MoreMath.TabShift`                     | Returns column position of a character after tab   |



### `.Color` namespace

The `.Color` namespace relates to `System.Drawing.Color`

| Method                          | Summary                                          |
| ------------------------------- | ------------------------------------------------ |
| (extension) `Color.WithAlpha`   | Returns source `Color` with given alpha          |
| (extension) `Color.Transparent` | Returns source `Color` with alpha of 0           |
| `ColorMath.LerpColor`           | Linearly interpolates between 2 colors           |
| `ColorMath.AlphaBlend`          | Blend 2 colors with alpha-1-minus-alpha blending |
