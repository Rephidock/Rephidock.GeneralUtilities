using System;
using System.Collections.Generic;
using System.Linq;
using Rephidock.GeneralUtilities.Maths;
using Xunit;


namespace Rephidock.GeneralUtilities.Tests.Maths;


public sealed class RadixMathTests {

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

	#region //// CountAllAccending

	readonly Dictionary<string, ushort[][]> expectedCounterResults = new() {
		{
			"bin3places", new ushort[][] {
				new ushort[] { 0, 0, 0 },
				new ushort[] { 0, 0, 1 },
				new ushort[] { 0, 1, 0 },
				new ushort[] { 0, 1, 1 },
				new ushort[] { 1, 0, 0 },
				new ushort[] { 1, 0, 1 },
				new ushort[] { 1, 1, 0 },
				new ushort[] { 1, 1, 1 },
			}
		},
		{
			"quad2places",  new ushort[][] {
				new ushort[] { 0, 0 },
				new ushort[] { 0, 1 },
				new ushort[] { 0, 2 },
				new ushort[] { 0, 3 },
				new ushort[] { 1, 0 },
				new ushort[] { 1, 1 },
				new ushort[] { 1, 2 },
				new ushort[] { 1, 3 },
				new ushort[] { 2, 0 },
				new ushort[] { 2, 1 },
				new ushort[] { 2, 2 },
				new ushort[] { 2, 3 },
				new ushort[] { 3, 0 },
				new ushort[] { 3, 1 },
				new ushort[] { 3, 2 },
				new ushort[] { 3, 3 },
			}
		}
	};

	[Theory]
	[InlineData(2, 3, "bin3places")]
	[InlineData(4, 2, "quad2places")]
	public void CountAllAccending_SimpleUse_CorrectReturn(ushort @base, int places, string expectedKey) {

		// Arrange
		IEnumerable<ushort[]> expected = expectedCounterResults[expectedKey];

		// Act
		IEnumerable<ushort[]> actual = RadixMath.CountAllAscending(@base, places);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(4)]
	[InlineData(16)]
	[InlineData(89)]
	[InlineData(99)]
	[InlineData(ushort.MaxValue)]
	public void CountAllAccending_SinglePlace_ReturnsNumbesUpToNotIncludingBase(ushort @base) {

		// Arrange
		IEnumerable<ushort[]> expected = Enumerable
											.Range(0, @base)
											.Select(n => new ushort[] { (ushort)n });

		// Act
		IEnumerable<ushort[]> actual = RadixMath.CountAllAscending(@base, 1);

		// Assert
		Assert.Equal(expected, actual);
	}

	#endregion

	#region //// ToDigits, FromDigits

	[Theory]
	[InlineData(4627, 10, new ushort[] { 4, 6, 2, 7 })]
	[InlineData(0x73f8da, 16, new ushort[] { 7, 3, 0xf, 8, 0xd, 0xa})]
	[InlineData(32, 2, new ushort[] { 1, 0, 0, 0, 0, 0 })]
	[InlineData(5, 3, new ushort[] { 1, 2 })]
	[InlineData(11, 3, new ushort[] { 1, 0, 2 })]
	[InlineData(6537, 6537, new ushort[] { 1, 0 })]
	[InlineData(6536, 6537, new ushort[] { 6536 })]
	public void ToDigitsFromDigits_ConvertPositive_CorrectReturn(int value, ushort radix, ushort[] digits) {

		// Arrange

		// Act
		ushort[] actualDigits = RadixMath.ToDigits(value, radix);
		long actualValue = RadixMath.FromDigits(digits, radix);

		// Assert
		Assert.Equal(digits, actualDigits);
		Assert.Equal(value, actualValue);
	}

	[Theory]
	[InlineData(-4627, 10)]
	[InlineData(-0x73f8da, 16)]
	[InlineData(-32, 2)]
	[InlineData(-5, 3)]
	[InlineData(-11, 3)]
	[InlineData(-6537, 6537)]
	[InlineData(-6536, 6537)]
	public void ToDigits_ConvertNegative_ReturnsSameAsPositive(int value, ushort radix) {

		// Arrange
		ushort[] expected = RadixMath.ToDigits(-value, radix);

		// Act
		ushort[] actual = RadixMath.ToDigits(value, radix);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(4)]
	[InlineData(16)]
	[InlineData(89)]
	[InlineData(99)]
	[InlineData(ushort.MaxValue)]
	public void ToDigits_ConvertZero_ReturnsArrayOfSingleZero(ushort radix) {

		// Arrange
		ushort[] expected = new ushort[] { 0 };

		// Act
		ushort[] actual = RadixMath.ToDigits(0, radix);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(4627, 10, 0, new ushort[] { 4, 6, 2, 7 })]
	[InlineData(0x73f8da, 16, 8, new ushort[] { 0, 0, 7, 3, 0xf, 8, 0xd, 0xa })]
	[InlineData(32, 2, 3, new ushort[] { 1, 0, 0, 0, 0, 0 })]
	[InlineData(32, 2, 8, new ushort[] { 0, 0, 1, 0, 0, 0, 0, 0 })]
	[InlineData(5, 3, 3, new ushort[] { 0, 1, 2 })]
	[InlineData(11, 3, 3, new ushort[] { 1, 0, 2 })]
	[InlineData(6536, 6537, 0, new ushort[] { 6536 })]
	[InlineData(6536, 6537, 1, new ushort[] { 6536 })]
	[InlineData(6536, 6537, 2, new ushort[] { 0, 6536 })]
	[InlineData(6536, 6537, 7, new ushort[] { 0, 0, 0, 0, 0, 0, 6536 })]
	public void ToDigits_ConvertPositiveWithPadding_CorrectReturn(int value, ushort radix, int length, ushort[] expected) {

		// Arrange

		// Act
		ushort[] actual = RadixMath.ToDigits(value, radix, length);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(2, 5)]
	[InlineData(4, 3)]
	[InlineData(16, 1)]
	[InlineData(65, 31)]
	[InlineData(ushort.MaxValue, 99)]
	public void ToDigits_ConvertZeroWithPadding_ReturnsArrayOfZerosOfCorrectLength(ushort radix, int length) {

		// Arrange
		int value = 0;
		ushort[] expected = Enumerable.Repeat((ushort)0, length).ToArray();

		// Act
		ushort[] actual = RadixMath.ToDigits(value, radix, length);

		// Assert
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(new ushort[] { 0 }, 10, 0)]
	[InlineData(new ushort[] { 0, 0 }, 17, 0)]
	[InlineData(new ushort[] { 0, 0, 0, 0, 0, 0, 0, 0 }, 2, 0)]
	[InlineData(new ushort[] { 0, 0, 4, 6, 2, 7 }, 10, 4627)]
	[InlineData(new ushort[] { 0, 0, 0, 7, 3, 0xf, 8, 0xd, 0xa }, 16, 0x73f8da)]
	[InlineData(new ushort[] { 0, 1, 2 }, 3, 5)]
	[InlineData(new ushort[] { 0, 1, 0 }, 6537, 6537)]
	[InlineData(new ushort[] { 0, 0, 6536 }, 6537, 6536)]
	public void FromDigits_ConvertZeroAndPadded_CorrectReturn(ushort[] digits, ushort radix, long expected) {

		// Arrange

		// Act
		long actualValue = RadixMath.FromDigits(digits, radix);

		// Assert
		Assert.Equal(expected, actualValue);
	}

	#endregion

}
