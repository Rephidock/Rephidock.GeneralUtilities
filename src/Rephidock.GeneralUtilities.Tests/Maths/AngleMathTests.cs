using System;
using Rephidock.GeneralUtilities.Maths;
using Xunit;


namespace Rephidock.GeneralUtilities.Tests.Maths;


public sealed class AngleMathTests {

	#region //// Deg <-> Rad

	[Theory]
	[InlineData(0, 0)]
	[InlineData(180, MathF.PI)]
	[InlineData(-180, -MathF.PI)]
	[InlineData(90, MathF.PI / 2)]
	[InlineData(-90, -MathF.PI / 2)]
	public void DegRadConversion_SimpleUse_CorrectReturn(float angleDegress, float angleRadians) {

		// Arrange

		// Act
		float actualDegrees = angleRadians.RadToDeg();
		float actualRadians = angleDegress.DegToRad();

		// Assert
		Assert.Equal(angleDegress, actualDegrees);
		Assert.Equal(angleRadians, actualRadians);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(180)]
	[InlineData(-45)]
	[InlineData(Math.PI)]
	[InlineData(-Math.Tau)]
	public void DegRadConversion_UsingBoth_ReturnIntialValue(float angle) {

		// Arrange

		// Act
		float recived1 = angle.RadToDeg().DegToRad();
		float recived2 = angle.RadToDeg().DegToRad();

		// Assert
		Assert.Equal(angle, recived1);
		Assert.Equal(angle, recived2);
	}

	#endregion

	#region //// AngleDiference

	const float AngleDiffToleranceZero = 1E-44f;
	const float AngleDiffToleranceSmall = 1E-7f;
	const float AngleDiffToleranceBig = 1E-5f;

	[Theory]
	[InlineData(0, 0, 0, AngleDiffToleranceZero)]
	[InlineData(45, 45, 0, AngleDiffToleranceZero)]
	[InlineData(360, 720, 0, AngleDiffToleranceBig)]
	[InlineData(0, 10, 10, AngleDiffToleranceSmall)]
	[InlineData(10, 0, -10, AngleDiffToleranceSmall)]
	[InlineData(359, 1, 2, AngleDiffToleranceBig)]
	[InlineData(359, -2, -1, AngleDiffToleranceBig)]
	[InlineData(45, 315, -90, AngleDiffToleranceBig)]
	[InlineData(0, 170, 170, AngleDiffToleranceBig)]
	[InlineData(0, 190, -170, AngleDiffToleranceBig)]
	[InlineData(360, 190, -170, AngleDiffToleranceBig)]
	[InlineData(360, 170, 170, AngleDiffToleranceBig)]
	[InlineData(5360, 5190, -170, AngleDiffToleranceBig)]
	[InlineData(5360, 5170, 170, AngleDiffToleranceBig)]
	public void AngleDifference_ConvertThenUse_CorrectReturn(float angleSrcDeg, float angleDestDeg, float expectedDeg, float toleranceRad) {

		// Arrange
		float angleSrcRad = angleSrcDeg.DegToRad();
		float angleDestRad = angleDestDeg.DegToRad();
		float expectedRad = expectedDeg.DegToRad();

		// Act
		float distanceRad = AngleMath.AngleDifference(angleSrcRad, angleDestRad);

		// Assert
		Assert.Equal(expectedRad, distanceRad, toleranceRad);
	}

	#endregion

}
