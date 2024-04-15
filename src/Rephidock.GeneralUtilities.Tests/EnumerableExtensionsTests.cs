using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Rephidock.GeneralUtilities;


namespace Rephidock.GeneralUtilities.Tests;


public sealed class EnumerableExtensionsTests {

	#region //// Yield

	[Fact]
	public void Yield_UseOnReferenceTypes_ReturnSingleLengthEnumerableWithEqualRefernce() {

		// Arrange
		object someObject = new();

		// Act
		IEnumerable<object> enumerable = someObject.Yield();

		// Assert
		Assert.Single(enumerable);
		Assert.Same(someObject, enumerable.First());

	}

	[Fact]
	public void Yield_UseOnValueTypes_ReturnSingleLengthEnumerableWithEqualValue() {

		// Arrange
		int someValue = 64;

		// Act
		IEnumerable<int> enumerable = someValue.Yield();

		// Assert
		Assert.Single(enumerable);
		Assert.Equal(someValue, enumerable.First());

	}

	#endregion

	#region //// SplitIntoSegments

	readonly Dictionary<string, byte[][]> expectedSegments = new() {
		{
			"123 4 56 7",
			new byte[][] {
				new byte[] { 1, 2, 3 },
				new byte[] { 4 },
				new byte[] { 5, 6 },
				new byte[] { 7 }
			}
		},
		{
			"1289",
			new byte[][] {
				new byte[] { 1, 2, 8, 9 }
			}
		},
		{
			"1empty",
			new byte[][] {
				new byte[] { }
			}
		},
		{
			"2empty",
			new byte[][] {
				new byte[] { },
				new byte[] { }
			}
		},
		{
			"3empty",
			new byte[][] {
				new byte[] { },
				new byte[] { },
				new byte[] { }
			}
		},
		{
			"12 empty 56 empty empty",
			new byte[][] {
				new byte[] { 1, 2 },
				new byte[] { },
				new byte[] { 5, 6 },
				new byte[] { },
				new byte[] { }
			}
		},
	};

	[Theory]
	[InlineData(new byte[] { 1, 2, 3, 0, 4, 0, 5, 6, 0, 7 }, new byte[] { 0 }, "123 4 56 7")]
	[InlineData(new byte[] { 1, 2, 3, 9, 4, 0, 5, 6, 9, 7 }, new byte[] { 0, 9 }, "123 4 56 7")]
	[InlineData(new byte[] { 1, 2, 3, 9, 4, 0, 5, 6, 9, 7 }, new byte[] { 9, 0 }, "123 4 56 7")]
	[InlineData(new byte[] { 1, 2, 3, 9, 4, 0, 5, 6, 9, 7 }, new byte[] { 0, 0, 9 }, "123 4 56 7")]
	[InlineData(new byte[] { 1, 2, 8, 9 }, new byte[] { 0 }, "1289")]
	[InlineData(new byte[] { 1, 2, 8, 9 }, new byte[] { 5 }, "1289")]
	[InlineData(new byte[] { 1, 2, 8, 9 }, new byte[] { 3, 4, 5, 6, 7, 0 }, "1289")]
	[InlineData(new byte[] { 1, 2, 8, 9 }, new byte[] { }, "1289")]
	[InlineData(new byte[] { }, new byte[] { }, "1empty")]
	[InlineData(new byte[] { }, new byte[] { 0, 255 }, "1empty")]
	[InlineData(new byte[] { 0 }, new byte[] { 0 }, "2empty")]
	[InlineData(new byte[] { 0, 0 }, new byte[] { 0, 255 }, "3empty")]
	[InlineData(new byte[] { 0, 255 }, new byte[] { 0, 255 }, "3empty")]
	[InlineData(new byte[] { 255, 0 }, new byte[] { 0, 255 }, "3empty")]
	[InlineData(new byte[] { 1, 2, 0, 0, 5, 6, 0, 0 }, new byte[] { 0 }, "12 empty 56 empty empty")]
	[InlineData(new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 }, new byte[] { 3, 4, 7, 8 }, "12 empty 56 empty empty")]
	public void SplitIntoSegments_SimpleUseOnBytes_CorrectReturn(byte[] source, byte[] separators, string expectedSegmentsKey) {

		// Arrange
		byte[][] expected = expectedSegments[expectedSegmentsKey];

		// Act
		IEnumerable<ArraySegment<byte>> segments = source.SplitIntoSegments(separators);
		byte[][] arraysOfSegements = segments.Select(seg => seg.ToArray()).ToArray();

		// Assert
		Assert.Equal(arraysOfSegements, expected);

	}

	#endregion

}
