using System;


namespace Rephidock.GeneralUtilities {


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
	/// (netframework3.5) Does not support enums.
	/// </para>
	/// </summary>
	/// <remarks>
	/// For array types return true only on type equality.
	/// </remarks>
	/// <exception cref="ArgumentNullException"><paramref name="baseType"/> is null</exception>
	/// <exception cref="NotSupportedException"><paramref name="baseType"/> is an interface or (netframework35) <paramref name="derivedType"/> is an enum</exception>
	public static bool IsSubcalssOrSelfOf(this Type derivedType, Type baseType) {

		// Guards
		if (baseType == null) {
			throw new ArgumentNullException(nameof(baseType));
		}

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
				throw new NotSupportedException("Enums are not supported in netframework35 branch of this method");
			}

			// Find base
			derivedType = derivedType.BaseType;
		}

		return false;

	}

}

}