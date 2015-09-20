using System.Collections.Generic;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity role store
	/// </summary>
	public interface IIdentityRoleStore {

		/// <summary>
		///     List of all roles
		/// </summary>
		List<string> Roles { get; set; }

	}

}
