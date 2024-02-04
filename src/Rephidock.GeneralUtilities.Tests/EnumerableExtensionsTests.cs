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

}
