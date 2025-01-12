﻿using System;
using System.Collections;
using System.Linq;
using System.Reflection;


namespace Rephidock.GeneralUtilities.Reflection;


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
	/// Also supports generic type definitions and enums.
	/// Does not support interfaces.
	/// </para>
	/// </summary>
	/// <remarks>
	/// For array types return true only on type equality.
	/// </remarks>
	/// <exception cref="ArgumentNullException"><paramref name="baseType"/> is null</exception>
	/// <exception cref="NotSupportedException"><paramref name="baseType"/> is an interface</exception>
	public static bool IsSubclassOrSelfOf(this Type? derivedType, Type baseType) {

		// Guards
		ArgumentNullException.ThrowIfNull(baseType, nameof(baseType));

		if (baseType.IsInterface) {
			throw new NotSupportedException($"{nameof(baseType)} cannot be an interface");
		}

		// Check loop
		while (derivedType is not null) {

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
				derivedType = derivedType.GetEnumUnderlyingType();
				continue;
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
		ArgumentNullException.ThrowIfNull(methodInfo);

		// Check
		return methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType;
	}

	/// <summary>
	/// Casts the elements of an <see cref="IEnumerable"/> to the specified type.
	/// </summary>
	/// <remarks>
	/// Has the same quirks with boxed values as the original 
	/// <see cref="Enumerable.Cast{TResult}(IEnumerable)"/>
	/// </remarks>
	public static IEnumerable Cast(this IEnumerable source, Type targetType) {

		// Guards
		ArgumentNullException.ThrowIfNull(source, nameof(source));
		ArgumentNullException.ThrowIfNull(targetType, nameof(targetType));

		// Find the cast method
		MethodInfo? castMethod =
			typeof(Enumerable)
			.GetMethod(nameof(Enumerable.Cast), BindingFlags.Static | BindingFlags.Public)
			?.MakeGenericMethod(new Type[] { targetType });

		if (castMethod is null) throw new InvalidOperationException("Could not find Enumerable.Cast method.");

		// Perform the cast
		return (castMethod.Invoke(null, new object[] { source }) as IEnumerable)!;
	}

}