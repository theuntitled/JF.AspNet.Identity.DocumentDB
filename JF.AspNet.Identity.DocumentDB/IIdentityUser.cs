using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user
	/// </summary>
	public interface IIdentityUser : IUser<string> {

		/// <summary>
		///     Unique user email
		/// </summary>
		string Email { get; set; }

		/// <summary>
		///     Email is confirmed
		/// </summary>
		bool EmailConfirmed { get; set; }

		/// <summary>
		///     Hashed password
		/// </summary>
		string PasswordHash { get; set; }

		/// <summary>
		///     Security stamp
		/// </summary>
		string SecurityStamp { get; set; }

		/// <summary>
		///     Phone number
		/// </summary>
		string PhoneNumber { get; set; }

		/// <summary>
		///     Phone number is confirmed
		/// </summary>
		bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		///     Two factor authentication is enabled
		/// </summary>
		bool TwoFactorEnabled { get; set; }

		/// <summary>
		///     End date of the user lockout if applicable
		/// </summary>
		DateTime? LockoutEndDateUtc { get; set; }

		/// <summary>
		///     Lockout is enabled
		/// </summary>
		bool LockoutEnabled { get; set; }

		/// <summary>
		///     Number of failed access attempts
		/// </summary>
		int AccessFailedCount { get; set; }

		/// <summary>
		///     List of roles for the user
		/// </summary>
		IList<string> Roles { get; set; }

		/// <summary>
		///     List of claims for the user
		/// </summary>
		IList<IdentityUserClaim> Claims { get; set; }

		/// <summary>
		///     List of logins for the user
		/// </summary>
		IList<IdentityUserLogin> Logins { get; set; }

	}

}
