﻿using System;
using System.Reflection;


namespace Rephidock.GeneralUtilities.Reflection {


/// <summary>
/// Provides static extension methods for reflection.
/// </summary>
public static class ReflectionExtensions {

	/// <summary>
	/// <para>
	/// Returns true if the given <paramref name="derivedType"/> is
	/// <paramref name="baseType"/> or a subclass of it.
	/// </para>
	/// <para>
	/// Also supports generic type definitions.
	/// Does not support interfaces.
	/// </para>
	/// <para>
	/// (net framework 3.5) Does not support enums.
	/// </para>
	/// </summary>
	/// <remarks>
	/// For array types return true only on type equality.
	/// </remarks>
	/// <exception cref="ArgumentNullException"><paramref name="baseType"/> is null</exception>
	/// <exception cref="NotSupportedException"><paramref name="baseType"/> is an interface</exception>
	public static bool IsSubclassOrSelfOf(this Type derivedType, Type baseType) {

		// Guards
		if (baseType == null) throw new ArgumentNullException(nameof(baseType));

		if (baseType.IsInterface) {
			throw new NotSupportedException($"{nameof(baseType)} cannot be an interface");
		}

		// Check loop
		while (derivedType != null) {

			// Check type equality
			if (derivedType == baseType) {
				return true;
			}

			// Check generic definition equality
			if (derivedType.IsGenericType && derivedType.GetGenericTypeDefinition() == baseType) {
				return true;
			}

			// Find enum base
			if (derivedType.IsEnum) {
				throw new NotSupportedException();
			}

			// Find base
			derivedType = derivedType.BaseType;
		}

		return false;

	}

	/// <summary>
	/// Returns true if given <see cref="MethodInfo"/>'s base definition
	/// is not in its declaring type (because the method is an override).
	/// </summary>
	/// <remarks>
	/// Note that implementations of interface methods are not overrides,
	/// but implementation of abstract methods are.
	/// </remarks>
	/// <exception cref="ArgumentNullException"><paramref name="methodInfo"/> is null</exception>
	public static bool IsOverride(this MethodInfo methodInfo) {

		// Guards
		if (methodInfo == null) throw new ArgumentNullException(nameof(methodInfo));

		// Check
		return methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType;
	}

}

}