using System;
using Xunit;
using Rephidock.GeneralUtilities;


namespace Rephidock.GeneralUtilities.Tests;


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

	#region //// TrueMod

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
	public void TrueMod_SimpleUse_CorrectReturn(int value, int modulo, int expectedResult) {

		// Arrange

		// Act
		int actualResult = value.TrueMod(modulo);

		// Assert
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(-1)]
	[InlineData(127)]
	public void TrueMod_ModuloZero_ThrowsArgument(int value) {

		// Arrange

		// Act
		void ThrowingCode() => value.TrueMod(0);

		// Assert
		Assert.Throws<ArgumentException>(ThrowingCode);
	}

	#endregion

	#region //// ReverseLerp

	[Theory]
	[InlineData(0, 1, 0.5)]
	[InlineData(0, 12.5, 0.2)]
	[InlineData(1, 0, 0.8)]
	[InlineData(99.5, 25, 0.8)]
	public void ReverseLerp_SimpleUse_InverseOfLerp(float start, float end, float amount) {

		// Arrange

		// Act
		float lerpResult = MoreMath.Lerp(start, end, amount);
		float amountFromReverse = MoreMath.ReverseLerp(start, end, lerpResult);

		// Assert
		Assert.Equal(amount, amountFromReverse);
	}

	#endregion

}
