# GeneralUtilities

[![GitHub Licence Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE)

 A package with general utilities that may be useful.

## Contents



### Root namespace

The root namespace contains most general of the utilities

| Class                                | Summary                              |
| ------------------------------------ | ------------------------------------ |
| (static) `EnumConverter<TEnum,TInt>` | A generic enum <-> integer converter |

| Method                    | Summary                                            |
| ------------------------- | -------------------------------------------------- |
| (extension) `int.TrueMod` | Performs a modulo operation (`%` is remainder)     |
| `MoreMath.Lerp`           | Linearly interpolates between 2 values             |
| `MoreMath.ReverseLerp`    | Inverse of `Lerp` (returns lerp amount form value) |
| `MoreMath.TabShift`       | Returns column position of a character after tab   |



### `.Color` namespace

The `.Color` namespace relates to `System.Drawing.Color`

| Method                          | Summary                                          |
| ------------------------------- | ------------------------------------------------ |
| (extension) `Color.WithAlpha`   | Returns source `Color` with given alpha          |
| (extension) `Color.Transparent` | Returns source `Color` with alpha of 0           |
| `ColorMath.LerpColor`           | Linearly interpolates between 2 colors           |
| `ColorMath.AlphaBlend`          | Blend 2 colors with alpha-1-minus-alpha blending |



### `.Easing` namespace

The `.Easing` namespace relates to easing functions/curves (tweening maths). See [easings.net](https://easings.net) for information on easing.

| Class or Delegate        | Summary                                             |
| ------------------------ | --------------------------------------------------- |
| (static class) `Easing`  | Provides easing functions in form of static methods |
| (delegate) `EasingCurve` | Delegate matching methods in the `Easing` class     |