namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity claim
	/// </summary>
	public interface IIdentityUserClaim {

		/// <summary>
		///     The claim type
		/// </summary>
		string ClaimType { get; set; }

		/// <summary>
		///     The claim value
		/// </summary>
		string ClaimValue { get; set; }

	}

}
