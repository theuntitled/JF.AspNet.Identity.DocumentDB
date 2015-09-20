namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user login
	/// </summary>
	public class IdentityUserLogin : IIdentityUserLogin {

		/// <summary>
		///     The provider name
		/// </summary>
		public string LoginProvider { get; set; }

		/// <summary>
		///     The provider key
		/// </summary>
		public string ProviderKey { get; set; }

	}

}
