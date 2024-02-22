using System;
using System.Linq.Expressions;


namespace Rephidock.GeneralUtilities {


/// <summary>
/// <para>
/// A generic converter between an enum and an integer type.
/// Created for use with enums as generics.
/// </para>
/// <para>
/// Checks integers to be defined enum enteries.
/// </para>
/// </summary>
/// <remarks>
/// Taken from <see href="https://github.com/dotnet/csharplang/discussions/1993"/>
/// </remarks>
/// <typeparam name="TEnum">Enum to convert</typeparam>
/// <typeparam name="TInt">Base integer type of the enum</typeparam>
public static class EnumConverter<TEnum, TInt>
	where TEnum : struct 
	where TInt : struct
{

	private static readonly Func<TEnum, TInt> _convertFromEnum = GenerateConverter<TEnum, TInt>();
	private static readonly Func<TInt, TEnum> _convertToEnum = GenerateConverter<TInt, TEnum>();

	private static Func<TInput, TOutput> GenerateConverter<TInput, TOutput>() {
		var parameter = Expression.Parameter(typeof(TInput), typeof(TInput).Name);
		var dynamicMethod = Expression.Lambda<Func<TInput, TOutput>>
		(
			// Make sure the type is not silently converted to a wrong value (overflow)
			Expression.ConvertChecked(parameter, typeof(TOutput)),
			parameter
		);
		return dynamicMethod.Compile();
	}

	/// <summary>Converts a generic Enum to the base integer type</summary>
	public static TInt ToInt(TEnum input) => _convertFromEnum(input);

	/// <summary>
	/// Converts from an integer to generic Enum type.
	/// Throws on values not defined in the enum.
	/// </summary>
	/// <exception cref="InvalidCastException">Value is not defined in the enum</exception>
	public static TEnum ToEnum(TInt input) {

		var result = _convertToEnum(input);

		if (!Enum.IsDefined(typeof(TEnum), result)) {
			throw new InvalidCastException("Value is not defined in the enum.");
		}

		return result;
	}

}

}