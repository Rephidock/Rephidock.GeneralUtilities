using System;
using System.Collections.Generic;
using Xunit;
using Rephidock.GeneralUtilities;


namespace Rephidock.GeneralUtilities.Tests;


public sealed class ReflectionTests {

	#region //// Inheritance tree setup

	// Simple first tree
	class Base { }
	class FromBase : Base { }
	class FromFromBase : FromBase { }
	class Unrelated { }

	// Generics single
	class GenericFromBase<T> : Base { }
	class IntGenericFromBase : GenericFromBase<int> { }
	class FromGenericFromBase<T> : GenericFromBase<T> { }

	// Generic Multi
	class MultiGenericFromBase<T1, T2> : Base { }
	class IntFirstMultiGenericFromBase<T2> : MultiGenericFromBase<int, T2> { }
	class IntSecondMultiGenericFromBase<T1> : MultiGenericFromBase<T1, int> { } 
	
	// Enum
	enum IntEnum : int { }

	enum UIntEnum : uint { }

	#endregion

	#region //// IsSubcalssOrSelfOf

	[Theory]
	[InlineData(typeof(Base),			typeof(Base),			true )]
	[InlineData(typeof(FromBase),		typeof(Base),			true )]
	[InlineData(typeof(FromFromBase),	typeof(Base),			true )]
	[InlineData(typeof(Unrelated),		typeof(Base),			false)]
	[InlineData(typeof(Base),			typeof(FromBase),		false)]
	[InlineData(typeof(FromBase),		typeof(FromBase),		true )]
	[InlineData(typeof(FromFromBase),	typeof(FromBase),		true )]
	[InlineData(typeof(Unrelated),		typeof(FromBase),		false)]
	[InlineData(typeof(Base),			typeof(FromFromBase),	false)]
	[InlineData(typeof(FromBase),		typeof(FromFromBase),	false)]
	[InlineData(typeof(Unrelated),		typeof(FromFromBase),	false)]
	[InlineData(typeof(Base),			typeof(Unrelated),		false)]
	[InlineData(typeof(FromBase),		typeof(Unrelated),		false)]
	[InlineData(typeof(FromFromBase),	typeof(Unrelated),		false)]
	public void IsSubcalssOrSelfOf_NonGeneric_CorrectReturn(Type derivedType, Type baseType, bool expected) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(baseType);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(GenericFromBase<>),			typeof(Base),					true)]
	[InlineData(typeof(GenericFromBase<int>),		typeof(Base),					true)]
	[InlineData(typeof(GenericFromBase<string>),	typeof(Base),					true)]
	[InlineData(typeof(GenericFromBase<>),			typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(GenericFromBase<int>),		typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(GenericFromBase<string>),	typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(IntGenericFromBase),			typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(IntGenericFromBase),			typeof(GenericFromBase<int>),	true)]
	[InlineData(typeof(FromGenericFromBase<>),		typeof(Base),					true)]
	[InlineData(typeof(FromGenericFromBase<string>),typeof(Base),					true)]
	[InlineData(typeof(FromGenericFromBase<>),		typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(FromGenericFromBase<string>),typeof(GenericFromBase<>),		true)]
	[InlineData(typeof(FromGenericFromBase<string>),typeof(GenericFromBase<string>),true)]
	[InlineData(typeof(Base),					typeof(GenericFromBase<>),			false)]
	[InlineData(typeof(Base),					typeof(GenericFromBase<int>),		false)]
	[InlineData(typeof(Base),					typeof(GenericFromBase<string>),	false)]
	[InlineData(typeof(GenericFromBase<>),		typeof(GenericFromBase<int>),		false)]
	[InlineData(typeof(GenericFromBase<>),		typeof(GenericFromBase<string>),	false)]
	[InlineData(typeof(GenericFromBase<>),		typeof(IntGenericFromBase),			false)]
	[InlineData(typeof(GenericFromBase<int>),	typeof(IntGenericFromBase),			false)]
	[InlineData(typeof(GenericFromBase<>),		typeof(FromGenericFromBase<>),		false)]
	[InlineData(typeof(GenericFromBase<>),		typeof(FromGenericFromBase<string>),false)]
	[InlineData(typeof(GenericFromBase<string>),typeof(FromGenericFromBase<string>),false)]
	[InlineData(typeof(GenericFromBase<int>),		typeof(GenericFromBase<string>),false)]
	[InlineData(typeof(FromGenericFromBase<int>),	typeof(GenericFromBase<string>),false)]
	public void IsSubcalssOrSelfOf_GenericSingle_CorrectReturn(Type derivedType, Type baseType, bool expected) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(baseType);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(MultiGenericFromBase<,>), typeof(Base), true)]
	[InlineData(typeof(MultiGenericFromBase<int, int>), typeof(Base), true)]
	[InlineData(typeof(MultiGenericFromBase<,>), typeof(MultiGenericFromBase<,>), true)]
	[InlineData(typeof(IntFirstMultiGenericFromBase<string>), typeof(MultiGenericFromBase<int,string>), true)]
	[InlineData(typeof(IntSecondMultiGenericFromBase<string>), typeof(MultiGenericFromBase<string,int>), true)]
	[InlineData(typeof(IntFirstMultiGenericFromBase<string>), typeof(MultiGenericFromBase<,>), true)]
	[InlineData(typeof(IntSecondMultiGenericFromBase<string>), typeof(MultiGenericFromBase<,>), true)]
	[InlineData(typeof(IntFirstMultiGenericFromBase<string>), typeof(MultiGenericFromBase<string, int>), false)]
	[InlineData(typeof(IntSecondMultiGenericFromBase<string>), typeof(MultiGenericFromBase<int, string>), false)]
	public void IsSubcalssOrSelfOf_GenericMulti_CorrectReturn(Type derivedType, Type baseType, bool expected) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(baseType);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(Base[]),			typeof(Base[]),	true)]
	[InlineData(typeof(FromBase[]),		typeof(Base[]), false)]
	[InlineData(typeof(FromFromBase[]), typeof(Base[]), false)]
	[InlineData(typeof(Unrelated[]),	typeof(Base[]), false)]
	[InlineData(typeof(Base[]),			typeof(Base),	false)]
	[InlineData(typeof(FromBase[]),		typeof(Base),	false)]
	[InlineData(typeof(Unrelated[]),	typeof(Base),	false)]
	public void IsSubcalssOrSelfOf_Arrays_CorrectReturn(Type derivedType, Type baseType, bool expected) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(baseType);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(IntEnum), typeof(IntEnum), true)]
	[InlineData(typeof(IntEnum), typeof(int), true)]
	[InlineData(typeof(UIntEnum), typeof(uint), true)]
	[InlineData(typeof(int), typeof(IntEnum), false)]
	[InlineData(typeof(uint), typeof(UIntEnum), false)]
	[InlineData(typeof(UIntEnum), typeof(int), false)]
	[InlineData(typeof(IntEnum), typeof(uint), false)]
	[InlineData(typeof(IntEnum), typeof(UIntEnum), false)]
	[InlineData(typeof(UIntEnum), typeof(IntEnum), false)]
	public void IsSubcalssOrSelfOf_Enum_CorrectReturn(Type derivedType, Type baseType, bool expected) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(baseType);

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(Base))]
	[InlineData(typeof(FromBase))]
	[InlineData(typeof(object))]
	[InlineData(typeof(int))]
	[InlineData(typeof(IntEnum))]
	[InlineData(typeof(object[]))]
	[InlineData(typeof(Base[]))]
	[InlineData(typeof(List<>))]
	[InlineData(typeof(List<int>))]
	[InlineData(typeof(Dictionary<,>))]
	[InlineData(typeof(Dictionary<int, string>))]
	public void IsSubcalssOrSelfOf_EverythingIsObject_CorrectReturn(Type derivedType) {

		// Arrange

		// Act
		bool result = derivedType.IsSubclassOrSelfOf(typeof(object));

		// Assert
		Assert.True(result);
	}

	#endregion

}
