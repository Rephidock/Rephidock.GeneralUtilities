using System;
using System.Drawing;


namespace Rephidock.GeneralUtilities.Colors {


/// <summary>
/// Provides some extension methods for <see cref="Color"/>.
/// </summary>
public static class ColorExtensions {

	/// <summary>
	/// Returns the given color with a changed alpha component.
	/// </summary>
	/// <param name="color">Color to chnage alpha of</param>
	/// <param name="alpha">The alpha value to set</param>
	public static Color WithAlpha(this Color color, byte alpha) {
		return Color.FromArgb(alpha, color.R, color.G, color.B);
	}

	/// <summary>Returns the given color with 0 alpha component.</summary>
	/// <param name="color">Color to make transparent</param>
	public static Color Transparent(this Color color) => color.WithAlpha(0);

}

}