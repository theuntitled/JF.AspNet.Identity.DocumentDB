namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity claim
	/// </summary>
	public class IdentityUserClaim : IIdentityUserClaim {

		/// <summary>
		///     The claim type
		/// </summary>
		public string ClaimType { get; set; }

		/// <summary>
		///     The claim value
		/// </summary>
		public string ClaimValue { get; set; }

	}

}
