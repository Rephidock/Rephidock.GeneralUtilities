using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;


namespace Rephidock.GeneralUtilities.Tests;


public sealed class BasesMathTests {

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
		IEnumerable<ushort[]> actual = BasesMath.CountAllAccending(@base, places);

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
		IEnumerable<ushort[]> actual = BasesMath.CountAllAccending(@base, 1);

		// Assert
		Assert.Equal(expected, actual);
	}

	#endregion

}
