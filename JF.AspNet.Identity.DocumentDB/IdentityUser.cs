using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Documents;
using Newtonsoft.Json;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user resource
	/// </summary>
	public class IdentityUser : Resource , IIdentityUser , IUser {

		/// <summary>
		///     Constructor generates a unique user id (Guid)
		/// </summary>
		public IdentityUser() {
			Id = Guid.NewGuid().ToString();

			Roles = new List<string>();
			Claims = new List<IdentityUserClaim>();
			Logins = new List<IdentityUserLogin>();
		}

		/// <summary>
		///     List of roles for the user
		/// </summary>
		public IList<string> Roles { get; set; }

		/// <summary>
		///     List of claims for the user
		/// </summary>
		public IList<IdentityUserClaim> Claims { get; set; }

		/// <summary>
		///     List of logins for the user
		/// </summary>
		public IList<IdentityUserLogin> Logins { get; set; }

		/// <summary>
		///     Gets or sets the Id of the resource.
		/// </summary>
		/// <value>
		///     The Id associated with the resource.
		/// </value>
		/// <remarks>
		///     <para>
		///         Every resource within a DocumentDB database account needs to have a unique identifier. This id is set by the
		///         user.
		///     </para>
		/// </remarks>
		[JsonProperty( PropertyName = "id" )]
		public override string Id { get; set; }

		/// <summary>
		///     Unique username
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		///     Unique user email
		/// </summary>
		public string Email { get; set; }

		/// <summary>
		///     Email is confirmed
		/// </summary>
		public bool EmailConfirmed { get; set; }

		/// <summary>
		///     Hashed password
		/// </summary>
		public string PasswordHash { get; set; }

		/// <summary>
		///     Security stamp
		/// </summary>
		public string SecurityStamp { get; set; }

		/// <summary>
		///     Phone number
		/// </summary>
		public string PhoneNumber { get; set; }

		/// <summary>
		///     Phone number is confirmed
		/// </summary>
		public bool PhoneNumberConfirmed { get; set; }

		/// <summary>
		///     Two factor authentication is enabled
		/// </summary>
		public bool TwoFactorEnabled { get; set; }

		/// <summary>
		///     End date of the user lockout if applicable
		/// </summary>
		public DateTime? LockoutEndDateUtc { get; set; }

		/// <summary>
		///     Lockout is enabled
		/// </summary>
		public bool LockoutEnabled { get; set; }

		/// <summary>
		///     Number of failed access attempts
		/// </summary>
		public int AccessFailedCount { get; set; }

	}

}
