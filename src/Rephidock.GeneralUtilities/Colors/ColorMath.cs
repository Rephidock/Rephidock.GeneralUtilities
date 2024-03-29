﻿using System;
using System.Drawing;


namespace Rephidock.GeneralUtilities.Colors;


/// <summary>
/// Provides static methods for some color math.
/// </summary>
public static class ColorMath {

	/// <summary>Blend 2 colors with alpha-1-minus-alpha blending.</summary>
	/// <param name="oldColor">The old color (existing on the canvas)</param>
	/// <param name="newColor">The new color (drawn on top)</param>
	/// <returns>A new blended color.</returns>
	public static Color AlphaBlend(Color oldColor, Color newColor) {

		// Because multiplication from 0 to 1
		float oldAFloat = oldColor.A / 255f;
		float newAFloat = newColor.A / 255f;
		float reverseNewAFloat = Math.Clamp(1 - newAFloat, 0, 1);

		float resultingAFloat = newAFloat + oldAFloat * reverseNewAFloat;
		int resultingA = (int)MathF.Round(resultingAFloat * 255);
		int resultingR = (int)MathF.Round(newAFloat * newColor.R + oldColor.R * oldAFloat * reverseNewAFloat);
		int resultingG = (int)MathF.Round(newAFloat * newColor.G + oldColor.G * oldAFloat * reverseNewAFloat);
		int resultingB = (int)MathF.Round(newAFloat * newColor.B + oldColor.B * oldAFloat * reverseNewAFloat);

		return Color.FromArgb(resultingA, resultingR, resultingG, resultingB);
	}

	/// <summary>
	/// Linearly interpolates between 2 colors.
	/// Result <u>is</u> clamped.
	/// </summary>
	/// <inheritdoc cref="MoreMath.Lerp(float, float, float)"/>
	public static Color LerpColor(Color start, Color end, float amount) {

		amount = Math.Clamp(amount, 0f, 1f);

		return Color.FromArgb(
			MoreMath.Lerp(start.A, end.A, amount),
			MoreMath.Lerp(start.R, end.R, amount),
			MoreMath.Lerp(start.G, end.G, amount),
			MoreMath.Lerp(start.B, end.G, amount)
		);
	}

}
