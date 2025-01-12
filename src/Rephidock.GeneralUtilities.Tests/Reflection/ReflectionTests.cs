using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using Rephidock.GeneralUtilities.Reflection;
using System.Linq;


namespace Rephidock.GeneralUtilities.Tests.Reflection;


public sealed class ReflectionTests {

	#region //// Inheritance tree setup for IsSubcalssOrSelfOf

	// Simple first tree
	class Base { }
	abstract class FromBase : Base { }
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

	#region //// Inheritance setup for IsOverride

	abstract class AbstractSource {

		public abstract int AbstractPropertySet { set; }
		public abstract int AbstractPropertyGet { get; }
		public abstract int AbstractPropertyGetSet { get; set; }

		public abstract void AbstractMethod();

		public virtual int VirtualPropertySet { set { } }
		public virtual int VirtualPropertyGet { get; }
		public virtual int VirtualPropertyGetSet { get; set; }

		public virtual void VirtualMethod() { }

		public virtual void MethodToBeHidden() { }

	}

	class Destination : AbstractSource {

		public override int AbstractPropertySet { set { } }
		public override int AbstractPropertyGet { get; }
		public override int AbstractPropertyGetSet { get; set; }

		public override void AbstractMethod() { }

		public override int VirtualPropertySet { set { } }
		public override int VirtualPropertyGet { get; }
		public override int VirtualPropertyGetSet { get; set; }

		public override void VirtualMethod() { }

		public void NonVirtualMethod() { }

		public override void MethodToBeHidden() { }
	}

	class DestinationSubclass : Destination {

		public override void VirtualMethod() { }

		public new void NonVirtualMethod() { }

		public new void MethodToBeHidden() { }

	}

	#endregion

	#region //// IsOverride

	[Theory]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.AbstractMethod), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.VirtualMethod), false)]
	[InlineData(typeof(Destination), nameof(AbstractSource.AbstractMethod), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.VirtualMethod), true)]
	[InlineData(typeof(Destination), nameof(Destination.NonVirtualMethod), false)]
	[InlineData(typeof(DestinationSubclass), nameof(AbstractSource.VirtualMethod), true)]
	public void IsOverride_AbstractBase_ReturnsTrueIfOverride(Type type, string methodName, bool expected) {

		// Arrange
		MethodInfo methodInfo = type.GetMethod(methodName) ?? throw new ArgumentException("Invalid test: method not found");

		// Act
		bool result = methodInfo.IsOverride();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(DestinationSubclass), nameof(AbstractSource.MethodToBeHidden), false)]
	[InlineData(typeof(DestinationSubclass), nameof(Destination.NonVirtualMethod), false)]
	[InlineData(typeof(Destination), nameof(AbstractSource.MethodToBeHidden), true)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.MethodToBeHidden), false)]
	public void IsOverride_NewMethodToHideOld_ReturnsFalseIfNew(Type type, string methodName, bool expected) {

		// Arrange
		MethodInfo methodInfo = type.GetMethod(methodName) ?? throw new ArgumentException("Invalid test: method not found");

		// Act
		bool result = methodInfo.IsOverride();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.AbstractPropertyGet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.AbstractPropertyGetSet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.VirtualPropertyGet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.VirtualPropertyGetSet), false)]
	[InlineData(typeof(Destination), nameof(AbstractSource.AbstractPropertyGet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.AbstractPropertyGetSet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.VirtualPropertyGet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.VirtualPropertyGetSet), true)]
	public void IsOverride_PropertyGetter_ReturnsTrueIfOverride(Type type, string methodName, bool expected) {

		// Arrange
		MethodInfo? methodInfo = type.GetProperty(methodName)?.GetGetMethod();
		if (methodInfo is null) throw new ArgumentException("Invalid test: method or property not found");

		// Act
		bool result = methodInfo.IsOverride();

		// Assert
		Assert.Equal(expected, result);
	}

	[Theory]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.AbstractPropertySet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.AbstractPropertyGetSet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.VirtualPropertySet), false)]
	[InlineData(typeof(AbstractSource), nameof(AbstractSource.VirtualPropertyGetSet), false)]
	[InlineData(typeof(Destination), nameof(AbstractSource.AbstractPropertySet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.AbstractPropertyGetSet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.VirtualPropertySet), true)]
	[InlineData(typeof(Destination), nameof(AbstractSource.VirtualPropertyGetSet), true)]
	public void IsOverride_PropertySetter_ReturnsTrueIfOverride(Type type, string methodName, bool expected) {

		// Arrange
		MethodInfo? methodInfo = type.GetProperty(methodName)?.GetSetMethod();
		if (methodInfo is null) throw new ArgumentException("Invalid test: method or property not found");

		// Act
		bool result = methodInfo.IsOverride();

		// Assert
		Assert.Equal(expected, result);
	}

	#endregion

	#region //// Cast

	[Fact]
	public void Cast_ValidCast_CompilesAndRuns() {

		// Arrange
		IEnumerable baseSequence = Enumerable.Range(0, 100).Select(_ => new FromFromBase());

		// Act
		IEnumerable castSequence1 = baseSequence.Cast(typeof(Base));
		IEnumerable castSequence2 = baseSequence.Cast(typeof(object));
		IEnumerable castSequenceBack = castSequence2.Cast(typeof(FromFromBase));

		// Assert
		foreach (object? _ in castSequence1) { }
		foreach (object? _ in castSequence2) { }
		foreach (object? _ in castSequenceBack) { }

	}

	[Fact]
	public void Cast_InvalidCast_Throws() {

		// Arrange
		IEnumerable baseSequence = Enumerable.Range(0, 100).Select(_ => new Base());

		// Act
		IEnumerable castSequenceInvalid = baseSequence.Cast(typeof(FromFromBase));

		// Assert
		Assert.Throws<InvalidCastException>(() => {
			foreach (object? _ in castSequenceInvalid) { }
		});
	
	}

	#endregion

}
