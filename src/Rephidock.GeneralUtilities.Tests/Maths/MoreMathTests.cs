using System;
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

	#region //// Digital Root

	[Theory]
	[InlineData(0, 0)]
	[InlineData(1, 1)]
	[InlineData(2, 2)]
	[InlineData(5, 5)]
	[InlineData(8, 8)]
	[InlineData(9, 9)]
	[InlineData(10, 1)]
	[InlineData(100, 1)]
	[InlineData(1000, 1)]
	[InlineData(11, 2)]
	[InlineData(18, 9)]
	[InlineData(19, 1)]
	[InlineData(331, 7)]
	[InlineData(28585, 1)]
	[InlineData(28584, 9)]
	public void DigitalRoot_UseInDefaultBase_CorrectReturn(int value, int expectedResult) {

		// Arrange

		// Act
		int actualResult = value.DigitalRoot();

		// Assert
		Assert.Equal(expectedResult, actualResult);
	}

	[Theory]
	[InlineData(0, 2, 0)]
	[InlineData(0b1001, 2, 1)]
	[InlineData(0b11010001, 2, 1)]
	[InlineData(0b10000000, 2, 1)]
	[InlineData(0, 3, 0)]
	[InlineData(4, 3, 2)]
	[InlineData(5, 3, 1)]
	[InlineData(0, 4, 0)]
	[InlineData(10, 11, 10)]
	[InlineData(11, 12, 11)]
	[InlineData(11, 11, 1)]
	[InlineData(0x00, 16, 0)]
	[InlineData(0xa8, 16, 0x3)]
	[InlineData(0xb8, 16, 0x4)]
	[InlineData(0xff, 16, 0xf)]
	public void DigitalRoot_UseInADifferentBase_CorrectReturn(int value, int rootBase, int expectedResult) {

		// Arrange

		// Act
		int actualResult = value.DigitalRoot(rootBase);

		// Assert
		Assert.Equal(expectedResult, actualResult);
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

}
