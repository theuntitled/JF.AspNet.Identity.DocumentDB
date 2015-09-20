using System.Collections.Generic;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity role store
	/// </summary>
	public class IdentityRoleStore : IIdentityRoleStore {

		/// <summary>
		///     List of all roles
		/// </summary>
		public List<string> Roles { get; set; }

	}

}
