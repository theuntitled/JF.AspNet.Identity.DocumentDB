using JF.Azure.DocumentDB;
using Microsoft.Azure.Documents.Client;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents the identity DocumentCollection as an ICollectionManager
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	public class IdentityCollectionManager<TUser> : CollectionManager , IIdentityCollectionManager<TUser>
		where TUser : IdentityUser , new() {

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="documentClient"></param>
		/// <param name="databaseId"></param>
		/// <param name="createDatabaseIfNonexistent"></param>
		public IdentityCollectionManager( DocumentClient documentClient ,
										  string databaseId ,
										  bool createDatabaseIfNonexistent = false )
			: base( documentClient , databaseId , createDatabaseIfNonexistent ) {
		}

		/// <summary>
		///     DocumentCollection of all users as an ICollection
		/// </summary>
		public Collection<TUser> Users { get; set; }

	}

}
