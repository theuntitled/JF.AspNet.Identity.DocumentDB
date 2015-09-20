using Microsoft.AspNet.Identity;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user store
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	public interface IDocumentDBUserStore<TUser> : IUserStore<TUser>
		where TUser : class , IIdentityUser , IUser<string> , new() {

	}

}
