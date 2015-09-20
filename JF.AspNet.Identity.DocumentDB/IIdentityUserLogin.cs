namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user login
	/// </summary>
	public interface IIdentityUserLogin {

		/// <summary>
		///     The provider name
		/// </summary>
		string LoginProvider { get; set; }

		/// <summary>
		///     The provider key
		/// </summary>
		string ProviderKey { get; set; }

	}

}
