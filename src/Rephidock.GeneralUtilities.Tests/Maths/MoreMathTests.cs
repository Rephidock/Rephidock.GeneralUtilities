﻿using System;
using System.Collections.Generic;
using Rephidock.GeneralUtilities.Maths;
using Xunit;


namespace Rephidock.GeneralUtilities.Tests.Maths;


public sealed class MoreMathTests {

	#region //// TabShift

	[Theory]
	[InlineData(0, 4)]
	[InlineData(1, 4)]
	[InlineData(2, 4)]
	[InlineData(3, 4)]
	[InlineData(4, 8)]
	[InlineData(5, 8)]
	[InlineData(7, 8)]
	[InlineData(8, 12)]
	[InlineData(9, 12)]
	public void TabShift_SimpleUse_CorrectReturn(int tabColumn, int expectedResult) {

		// Arrange

		// Act
		int givenResult = MoreMath.TabShift(tabColumn);

		// Assert
		Assert.Equal(expectedResult, givenResult);
	}

	[Theory]
	[InlineData(0, 4, 4)]
	[InlineData(1, 4, 4)]
	[InlineData(3, 4, 4)]
	[InlineData(4, 4, 8)]
	[InlineData(0, 5, 5)]
	[InlineData(1, 5, 5)]
	[InlineData(4, 5, 5)]
	[InlineData(5, 5, 10)]
	[InlineData(0, 2, 2)]
	[InlineData(1, 2, 2)]
	[InlineData(2, 2, 4)]
	[InlineData(3, 2, 4)]
	[InlineData(4, 2, 6)]
	public void TabShift_UseWithTabWidth_CorrectReturn(int tabColumn, int tabWidth, int expectedResult) {

		// Arrange

		// Act
		int givenResult = MoreMath.TabShift(tabColumn, tabWidth);

		// Assert
		Assert.Equal(expectedResult, givenResult);
	}

	#endregion

	#region //// PosMod

	[Theory]
	[InlineData(0, 2, 0)]
	[InlineData(5, 2, 1)]
	[InlineData(-1, 2, 1)]
	[InlineData(-1, 6, 5)]
	[InlineData(1, 6, 1)]
	[InlineData(-6, 6, 0)]
	[InlineData(5, 3, 2)]
	[InlineData(-5, 3, 1)]
	[InlineData(-3, 3, 0)]
	[InlineData(0, 1, 0)]
	[InlineData(1, 1, 0)]
	[InlineData(-1, 1, 0)]
	public void PosMod_SimpleUse_CorrectReturn(int value, int modulo, int expectedResult) {

		// Arrange

		// Act
		int actualResult = value.PosMod(modulo);

		// Assert
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(-1)]
	[InlineData(127)]
	public void PosMod_ModuloZero_ThrowsArgument(int value) {

		// Arrange

		// Act
		void ThrowingCode() => value.PosMod(0);

		// Assert
		Assert.Throws<ArgumentException>(ThrowingCode);
	}

	#endregion

	#region //// InverseLerp

	[Theory]
	[InlineData(0, 1, 0.5)]
	[InlineData(0, 12.5, 0.2)]
	[InlineData(1, 0, 0.8)]
	[InlineData(99.5, 25, 0.8)]
	public void InverseLerp_SimpleUse_InverseOfLerp(float start, float end, float amount) {

		// Arrange

		// Act
		float lerpResult = MoreMath.Lerp(start, end, amount);
		float amountFromReverse = MoreMath.InverseLerp(start, end, lerpResult);

		// Assert
		Assert.Equal(amount, amountFromReverse);
	}

	#endregion

	#region //// Wrap

	[Theory]
	[InlineData(-4, 1, 4, 2)]
	[InlineData(-3, 1, 4, 3)]
	[InlineData(-2, 1, 4, 1)]
	[InlineData(-1, 1, 4, 2)]
	[InlineData(0, 1, 4, 3)]
	[InlineData(1, 1, 4, 1)]
	[InlineData(2, 1, 4, 2)]
	[InlineData(3, 1, 4, 3)]
	[InlineData(4, 1, 4, 1)]
	[InlineData(-4, -3, 0, -1)]
	[InlineData(-3, -3, 0, -3)]
	[InlineData(-2, -3, 0, -2)]
	[InlineData(-1, -3, 0, -1)]
	[InlineData(0, -3, 0, -3)]
	[InlineData(1, -3, 0, -2)]
	[InlineData(2, -3, 0, -1)]
	public void Wrap_SimpleUse_CorrectReturn(int value, int min, int max, int expectedResult) {

		// Arrange

		// Act
		int actualResult = value.Wrap(min, max);

		// Assert
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(0, 1, 2)]
	[InlineData(0, -3, 2)]
	[InlineData(-98, 12, 24)]
	public void Wrap_SwappedRangeBounds_ReturnsTheSameValue(int value, int min, int max) {

		// Arrange

		// Act
		int expectedResult = value.Wrap(min, max);
		int resultWithSwapped = value.Wrap(max, min);

		// Assert
		Assert.Equal(expectedResult, resultWithSwapped);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(99, 0)]
	[InlineData(0, 1)]
	[InlineData(-6, 1)]
	[InlineData(999, -3)]
	[InlineData(-98, 12)]
	public void Wrap_ZeroLengthRange_ReturnsMin(int value, int min) {

		// Arrange

		// Act
		int result = value.Wrap(min, min);

		// Assert
		Assert.Equal(min, result);
	}

	#endregion

	#region //// Factors

	[Theory]
	[InlineData(0, new int[] { 0 })]
	[InlineData(1, new int[] { 1 })]
	[InlineData(-1, new int[] { -1 })]
	[InlineData(2, new int[] { 2 })]
	[InlineData(-2, new int[] { -1, 2 })]
	[InlineData(331, new int[] { 331 })]
	[InlineData(256, new int[] { 2, 2, 2, 2, 2, 2, 2, 2 })]
	[InlineData(9610, new int[] { 2, 5, 31, 31 })]
	[InlineData(134386, new int[] { 2, 7, 29, 331 })]
	public void GetFactors_SimpleUse_CorrectReturn(int value, int[] expectedResult) {

		// Arrange

		// Act
		IEnumerable<int> actualResult = value.GetFactors();

		// Assert
		Assert.Equal(expectedResult, actualResult);
	}

	#endregion

	#region //// Deg <-> Rad

	[Theory]
	[InlineData(0, 0)]
	[InlineData(180, MathF.PI)]
	[InlineData(-180, -MathF.PI)]
	[InlineData(90, MathF.PI/2)]
	[InlineData(-90, -MathF.PI/2)]
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
		float distanceRad = MoreMath.AngleDifference(angleSrcRad,angleDestRad);

		// Assert
		Assert.Equal(expectedRad, distanceRad, toleranceRad);
	}

	#endregion

}
