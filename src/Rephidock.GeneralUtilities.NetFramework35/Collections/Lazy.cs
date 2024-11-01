using System;


namespace Rephidock.GeneralUtilities.Collections {


	/// <summary>
	/// Provides support for lazy initialization.
	/// </summary>
	/// <typeparam name="T">The type of element being lazily initialized.</typeparam>
	public class Lazy<T> {

		private bool isInitialized = false;
		private Func<T> factory = null;
		private T value;

		/// <summary>
		/// Creates a new instance of the <see cref="Lazy{T}"/>
		/// with a pre-initialized specified value.
		/// </summary>
		public Lazy(T value) {
			this.value = value;
			isInitialized = true;
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Lazy{T}"/>
		/// with a specified value initialization function.
		/// </summary>
		/// <param name="valueFactory">
		/// The <see cref="Func{T}"/> invoked to produce the lazily-initialized value when it is
		/// needed.
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="valueFactory"/> is <see langword="null"/>.</exception>
		public Lazy(Func<T> valueFactory) {

			if (valueFactory == null) throw new ArgumentException(nameof(valueFactory));

			factory = valueFactory;
		}

		/// <summary>
		/// <para>
		/// Gets the lazily initialized value of the current <see cref="Lazy{T}"/>.
		/// </para>
		/// <para>
		/// If <see cref="IsValueCreated"/> is <see langword="false"/>,
		/// accessing <see cref="Value"/> will force initialization.
		/// </para>
		/// </summary>
		public T Value {
			get {

				// Return value if exists
				if (isInitialized) return value;

				// Otherwise initialize it
				value = factory();
				isInitialized = true;
				factory = null;

				return value;
			}
		}

		/// <summary>
		/// <see langword="true"/> if the <see cref="Value"/> of this <see cref="Lazy{T}"/>
		/// has been initialized.
		/// </summary>
		public bool IsValueCreated => isInitialized;

	}

}
