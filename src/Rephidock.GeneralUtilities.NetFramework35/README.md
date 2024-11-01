# GeneralUtilities+NetFramework35

[![GitHub License Badge](https://img.shields.io/github/license/Rephidock/Rephidock.GeneralUtilities)](https://github.com/Rephidock/Rephidock.GeneralUtilities/blob/main/LICENSE) 

This is a source clone of GeneralUtilities downgraded to NET Framework 3.5. Due to drastic differences this needs to be a separate package.

The version of this package mimics the version of the original package.

Following features were removed:
- `IDictionary<TKey, TValue>.AsReadOnly()`
- `Type.IsSubclassOrSelfOf`: support for enums
- Anything relating to `BigInteger`
- Anything relating to `Span`
- Forcing of readonly in interfaces

Following features were added from future .NET versions:
- `IEnumerable<T>`.Append
- `IEnumerable<T>`.Prepend
