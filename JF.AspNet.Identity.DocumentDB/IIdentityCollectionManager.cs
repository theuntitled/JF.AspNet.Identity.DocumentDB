using JF.Azure.DocumentDB;
using Microsoft.Azure.Documents;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents the identity DocumentCollection as an ICollectionManager
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	public interface IIdentityCollectionManager<TUser> : ICollectionManager where TUser : Resource , IIdentityUser , new() {

		/// <summary>
		///     DocumentCollection of all users as an ICollection
		/// </summary>
		Collection<TUser> Users { get; set; }

	}

}
