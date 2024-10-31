using System;
using System.Collections.Generic;


namespace Rephidock.GeneralUtilities.Maths;


/// <summary>
/// Provides static methods to perfom some
/// angle related maths.
/// </summary>
public static class AngleMath {

	#region //// Deg <-> Rad

	/// <summary>
	/// Converts angle measured in degrees to angle measured in radians
	/// </summary>
	/// <param name="angleDegrees">Angle measured in degrees</param>
	/// <returns>Angle measured in radians</returns>
	public static float DegToRad(this float angleDegrees) => angleDegrees / 180f * MathF.PI;

	/// <summary>
	/// Converts angle measured in radians to angle measured in degrees
	/// </summary>
	/// <param name="angleRadians">Angle measured in radians</param>
	/// <returns>Angle measured in degrees</returns>
	public static float RadToDeg(this float angleRadians) => angleRadians / MathF.PI * 180f;

	/// <inheritdoc cref="DegToRad(float)"/>
	public static double DegToRad(this double angleDegrees) => angleDegrees / 180d * Math.PI;

	/// <inheritdoc cref="RadToDeg(float)"/>
	public static double RadToDeg(this double angleRadians) => angleRadians / Math.PI * 180d;

	#endregion

	#region //// AngleDifference

	/// <summary>
	/// Given two angles calculates the shortest distance,
	/// taking the circle over/underflow into account.
	/// </summary>
	/// <param name="angleSourceRadians">First angle, measured in radians</param>
	/// <param name="angleDestinationRadians">Second angle, measured in radians</param>
	/// <remarks>
	/// Returned distance is in range of <c>[-<see cref="MathF.PI"/>, <see cref="MathF.PI"/>)</c>.
	/// </remarks>
	/// <returns>Shortest distance between two angles.</returns>
	public static float AngleDifference(float angleSourceRadians, float angleDestinationRadians) {
		return (angleDestinationRadians - angleSourceRadians).Wrap(-MathF.PI, MathF.PI);
	}

	/// <inheritdoc cref="AngleDifference(float, float)"/>
	/// <remarks>
	/// Returned distance is in range of <c>[-<see cref="Math.PI"/>, <see cref="Math.PI"/>)</c>.
	/// </remarks>
	public static double AngleDifference(double angleSourceRadians, double angleDestinationRadians) {
		return (angleDestinationRadians - angleSourceRadians).Wrap(-Math.PI, Math.PI);
	}

	#endregion

}
