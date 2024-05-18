using System;
using System.ComponentModel;
using System.Reflection;


namespace Rephidock.GeneralUtilities;


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
	/// is not in its declaring type (because the method is overridden).
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

	/// <inheritdoc cref="IsSubclassOrSelfOf(Type?, Type)"/>
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Method is obsolete due to a spelling error. Use IsSubclassOrSelfOf instead.")]
	public static bool IsSubcalssOrSelfOf(this Type? derivedType, Type baseType) {
		return IsSubclassOrSelfOf(derivedType, baseType);
	}

}