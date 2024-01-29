using System;
using Xunit;
using Rephidock.GeneralUtilities;


namespace Rephidock.GeneralUtilities.Tests;


public sealed class EnumConverterTests {

	#region //// int base

	public enum IntEnum : int {
		Zero = 0,
		One = 1,
		Ninety = 90,
		NegativeOne = -1,
		NegativeFive = -5
	}

	[Theory]
	[InlineData(IntEnum.Zero)]
	[InlineData(IntEnum.One)]
	[InlineData(IntEnum.Ninety)]
	[InlineData(IntEnum.NegativeOne)]
	[InlineData(IntEnum.NegativeFive)]
	public void EnumConverterInt_ConvertToInt_ReturnsSameAsCast(IntEnum enumValue) {

		// Arrange

		// Act
		int convertedValue = EnumConverter<IntEnum, int>.ToInt(enumValue);
		int castValue = (int)enumValue;

		// Assert
		Assert.Equal(convertedValue, castValue);
	}

	[Theory]
	[InlineData(0, IntEnum.Zero)]
	[InlineData(1, IntEnum.One)]
	[InlineData(90, IntEnum.Ninety)]
	[InlineData(-1, IntEnum.NegativeOne)]
	[InlineData(-5, IntEnum.NegativeFive)]
	public void EnumConverterInt_ConvertValidToEnum_ReturnsValid(int intValue, IntEnum expectedValue) {

		// Arrange

		// Act
		IntEnum returnValue = EnumConverter<IntEnum, int>.ToEnum(intValue);

		// Assert
		Assert.Equal(expectedValue, returnValue);
	}

	[Theory]
	[InlineData(-2)]
	[InlineData(-255)]
	[InlineData(17)]
	[InlineData(30)]
	public void EnumConverterInt_ConvertInvalidToEnum_ThrowsInvalidCast(int intValue) {

		// Arrange

		// Act
		void ThowingCode() => EnumConverter<IntEnum, int>.ToEnum(intValue);

		// Assert
		Assert.Throws<InvalidCastException>(ThowingCode);
	}

	#endregion

	#region //// uint base

	public enum UIntEnum : uint {
		Zero = 0,
		One = 1,
		Ninety = 90
	}

	[Theory]
	[InlineData(UIntEnum.Zero)]
	[InlineData(UIntEnum.One)]
	[InlineData(UIntEnum.Ninety)]
	public void EnumConverterUInt_ConvertToUInt_ReturnsSameAsCast(UIntEnum enumValue) {

		// Arrange

		// Act
		uint convertedValue = EnumConverter<UIntEnum, uint>.ToInt(enumValue);
		uint castValue = (uint)enumValue;

		// Assert
		Assert.Equal(convertedValue, castValue);
	}

	[Theory]
	[InlineData(0, UIntEnum.Zero)]
	[InlineData(1, UIntEnum.One)]
	[InlineData(90, UIntEnum.Ninety)]
	public void EnumConverterUInt_ConvertValidToEnum_ReturnsValid(uint intValue, UIntEnum expectedValue) {

		// Arrange

		// Act
		UIntEnum returnValue = EnumConverter<UIntEnum, uint>.ToEnum(intValue);

		// Assert
		Assert.Equal(expectedValue, returnValue);
	}

	[Theory]
	[InlineData(17)]
	[InlineData(30)]
	public void EnumConverterUInt_ConvertInvalidToEnum_ThrowsInvalidCast(uint intValue) {

		// Arrange

		// Act
		void ThowingCode() => EnumConverter<UIntEnum, uint>.ToEnum(intValue);

		// Assert
		Assert.Throws<InvalidCastException>(ThowingCode);
	}

	#endregion

	#region //// byte base

	public enum ByteEnum : byte {
		Zero = 0,
		One = 1,
		Ninety = 90,
		Max = 255
	}

	[Theory]
	[InlineData(ByteEnum.Zero)]
	[InlineData(ByteEnum.One)]
	[InlineData(ByteEnum.Ninety)]
	[InlineData(ByteEnum.Max)]
	public void EnumConverterByte_ConvertToByte_ReturnsSameAsCast(ByteEnum enumValue) {

		// Arrange

		// Act
		byte convertedValue = EnumConverter<ByteEnum, byte>.ToInt(enumValue);
		byte castValue = (byte)enumValue;

		// Assert
		Assert.Equal(convertedValue, castValue);
	}

	[Theory]
	[InlineData(0, ByteEnum.Zero)]
	[InlineData(1, ByteEnum.One)]
	[InlineData(90, ByteEnum.Ninety)]
	[InlineData(255, ByteEnum.Max)]
	public void EnumConverterByte_ConvertValidToEnum_ReturnsValid(byte intValue, ByteEnum expectedValue) {

		// Arrange

		// Act
		ByteEnum returnValue = EnumConverter<ByteEnum, byte>.ToEnum(intValue);

		// Assert
		Assert.Equal(expectedValue, returnValue);
	}

	[Theory]
	[InlineData(17)]
	[InlineData(30)]
	[InlineData(127)]
	public void EnumConverterByte_ConvertInvalidToEnum_ThrowsInvalidCast(byte intValue) {

		// Arrange

		// Act
		void ThowingCode() => EnumConverter<ByteEnum, byte>.ToEnum(intValue);

		// Assert
		Assert.Throws<InvalidCastException>(ThowingCode);
	}

	#endregion

	#region //// Mismatched base, out of bounds

	[Theory]
	[InlineData(IntEnum.NegativeOne)]
	[InlineData(IntEnum.NegativeFive)]
	public void EnumConverter_ConvertToInvalidBaseOutOfBounds_ThrowsOverflow(IntEnum negativeValue) {

		// Arrange

		// Act
		void ThowingCode() => EnumConverter<IntEnum, uint>.ToInt(negativeValue);

		// Assert
		Assert.Throws<OverflowException>(ThowingCode);
	}

	[Theory]
	[InlineData(-9)]
	[InlineData(-64)]
	public void EnumConverter_ConvertFromInvalidBaseOutOfBounds_ThrowsOverflow(int negativeValue) {

		// Arrange

		// Act
		void ThowingCode() => EnumConverter<UIntEnum, int>.ToEnum(negativeValue);

		// Assert
		Assert.Throws<OverflowException>(ThowingCode);
	}

	#endregion

}
