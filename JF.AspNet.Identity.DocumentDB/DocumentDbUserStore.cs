using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Azure.Documents;

namespace JF.AspNet.Identity.DocumentDB {

	/// <summary>
	///     Represents an identity user store
	/// </summary>
	/// <typeparam name="TUser"></typeparam>
	public class DocumentDBUserStore<TUser> : IDocumentDBUserStore<TUser> ,
											  // ReSharper disable once RedundantExtendsListEntry
											  IUserStore<TUser> ,
											  IUserRoleStore<TUser , string> ,
											  IUserClaimStore<TUser , string> ,
											  IUserPasswordStore<TUser , string> ,
											  IUserSecurityStampStore<TUser , string> ,
											  IUserEmailStore<TUser , string> ,
											  IUserPhoneNumberStore<TUser , string> ,
											  IUserLoginStore<TUser , string> ,
											  IUserTwoFactorStore<TUser , string> ,
											  IUserLockoutStore<TUser , string>
		where TUser : Resource , IIdentityUser , IUser<string> , new() {

		private readonly IIdentityCollectionManager<TUser> _collectionManager;

		private readonly IIdentityRoleStore _roleStore;

		/// <summary>
		///     Constructor
		/// </summary>
		/// <param name="collectionManager"></param>
		/// <param name="roleStore"></param>
		public DocumentDBUserStore( IIdentityCollectionManager<TUser> collectionManager , IIdentityRoleStore roleStore ) {
			_collectionManager = collectionManager;
			_roleStore = roleStore;
		}

		/// <summary>
		///     Dispose
		/// </summary>
		/// <filterpriority>2</filterpriority>
		public void Dispose() {
		}

		#region ICloudTableUserStore

		/// <summary>
		///     Insert a new user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public async Task CreateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			await _collectionManager.Users.AddOrUpdateAsync( user );
		}

		/// <summary>
		///     Update a user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public async Task UpdateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			await _collectionManager.Users.AddOrUpdateAsync( user );
		}

		/// <summary>
		///     Delete a user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public async Task DeleteAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			await _collectionManager.Users.RemoveAsync( user );
		}

		/// <summary>
		///     Finds a user
		/// </summary>
		/// <param name="userId" />
		/// <returns />
		public Task<TUser> FindByIdAsync( string userId ) {
			return _collectionManager.Users.FindAsync( userId );
		}

		/// <summary>
		///     Find a user by name
		/// </summary>
		/// <param name="userName" />
		/// <returns />
		public Task<TUser> FindByNameAsync( string userName ) {
			return Task.FromResult( _collectionManager.Users.AsQueryable().FirstOrDefault( user => user.UserName == userName ) );
		}

		#endregion

		#region IUserRoleStore

		/// <summary>
		///     Validates the role name
		/// </summary>
		/// <param name="roleName"></param>
		/// <exception cref="InvalidOperationException"></exception>
		public void ValidateRole( string roleName ) {
			if ( !_roleStore.Roles.Contains( roleName ) ) {
				throw new InvalidOperationException( $"'{roleName}' is not a valid role name." );
			}
		}

		/// <summary>
		///     Adds a user to a role
		/// </summary>
		/// <param name="user" />
		/// <param name="roleName" />
		/// <returns />
		public Task AddToRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			ValidateRole( roleName );

			user.Roles.Add( roleName );

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Removes the role for the user
		/// </summary>
		/// <param name="user" />
		/// <param name="roleName" />
		/// <returns />
		public async Task RemoveFromRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			ValidateRole( roleName );

			if ( await IsInRoleAsync( user , roleName ) ) {
				user.Roles.Remove( roleName );
			}
		}

		/// <summary>
		///     Returns the roles for this user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<IList<string>> GetRolesAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.Roles );
		}

		/// <summary>
		///     Returns true if a user is in the role
		/// </summary>
		/// <param name="user" />
		/// <param name="roleName" />
		/// <returns />
		public Task<bool> IsInRoleAsync( TUser user , string roleName ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			ValidateRole( roleName );

			return Task.FromResult( user.Roles.Contains( roleName ) );
		}

		#endregion

		#region IUserClaimStore

		/// <summary>
		///     Returns the claims for the user with the issuer set
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<IList<Claim>> GetClaimsAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return
				Task.FromResult(
					user.Claims.Select( item => new Claim( item.ClaimType , item.ClaimValue ) ).ToList() as IList<Claim> );
		}

		/// <summary>
		///     Add a new user claim
		/// </summary>
		/// <param name="user" />
		/// <param name="claim" />
		/// <returns />
		public Task AddClaimAsync( TUser user , Claim claim ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			if ( !user.Claims.Any(
				userClaim => userClaim.ClaimType == claim.Type && userClaim.ClaimValue == claim.Value ) ) {
				user.Claims.Add( new IdentityUserClaim {
					ClaimType = claim.Type ,
					ClaimValue = claim.Value
				} );
			}

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Remove a user claim
		/// </summary>
		/// <param name="user" />
		/// <param name="claim" />
		/// <returns />
		public Task RemoveClaimAsync( TUser user , Claim claim ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			if ( claim == null ) {
				throw new ArgumentNullException( nameof( claim ) );
			}

			var identityUserClaim =
				user.Claims.FirstOrDefault( userClaim => userClaim.ClaimType == claim.Type && userClaim.ClaimValue == claim.Value );

			if ( identityUserClaim != null ) {
				user.Claims.Remove( identityUserClaim );
			}

			return Task.FromResult( 0 );
		}

		#endregion

		#region IUserPasswordStore

		/// <summary>
		///     Set the user password hash
		/// </summary>
		/// <param name="user" />
		/// <param name="passwordHash" />
		/// <returns />
		public Task SetPasswordHashAsync( TUser user , string passwordHash ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.PasswordHash = passwordHash;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Get the user password hash
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<string> GetPasswordHashAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.PasswordHash );
		}

		/// <summary>
		///     Returns true if a user has a password set
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<bool> HasPasswordAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( !string.IsNullOrEmpty( user.PasswordHash ) );
		}

		#endregion

		#region IUserSecurityStampStore

		/// <summary>
		///     Set the security stamp for the user
		/// </summary>
		/// <param name="user" />
		/// <param name="stamp" />
		/// <returns />
		public Task SetSecurityStampAsync( TUser user , string stamp ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.SecurityStamp = stamp;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Get the user security stamp
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<string> GetSecurityStampAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.SecurityStamp );
		}

		#endregion

		#region IUserEmailStore

		/// <summary>
		///     Set the user email
		/// </summary>
		/// <param name="user" />
		/// <param name="email" />
		/// <returns />
		public Task SetEmailAsync( TUser user , string email ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.Email = email;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Get the user email
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<string> GetEmailAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.Email );
		}

		/// <summary>
		///     Returns true if the user email is confirmed
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<bool> GetEmailConfirmedAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.EmailConfirmed );
		}

		/// <summary>
		///     Sets whether the user email is confirmed
		/// </summary>
		/// <param name="user" />
		/// <param name="confirmed" />
		/// <returns />
		public Task SetEmailConfirmedAsync( TUser user , bool confirmed ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.EmailConfirmed = confirmed;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Returns the user associated with this email
		/// </summary>
		/// <param name="email" />
		/// <returns />
		public Task<TUser> FindByEmailAsync( string email ) {
			return Task.FromResult( _collectionManager.Users.AsQueryable().FirstOrDefault( user => user.Email == email ) );
		}

		#endregion

		#region IUserPhoneNumberStore

		/// <summary>
		///     Set the user's phone number
		/// </summary>
		/// <param name="user" />
		/// <param name="phoneNumber" />
		/// <returns />
		public Task SetPhoneNumberAsync( TUser user , string phoneNumber ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.PhoneNumber = phoneNumber;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Get the user phone number
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<string> GetPhoneNumberAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.PhoneNumber );
		}

		/// <summary>
		///     Returns true if the user phone number is confirmed
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<bool> GetPhoneNumberConfirmedAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.PhoneNumberConfirmed );
		}

		/// <summary>
		///     Sets whether the user phone number is confirmed
		/// </summary>
		/// <param name="user" />
		/// <param name="confirmed" />
		/// <returns />
		public Task SetPhoneNumberConfirmedAsync( TUser user , bool confirmed ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.PhoneNumberConfirmed = confirmed;

			return Task.FromResult( 0 );
		}

		#endregion

		#region IUserLoginStore

		/// <summary>
		///     Adds a user login with the specified provider and key
		/// </summary>
		/// <param name="user" />
		/// <param name="login" />
		/// <returns />
		public Task AddLoginAsync( TUser user , UserLoginInfo login ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			if ( login == null ) {
				throw new ArgumentNullException( nameof( login ) );
			}

			var userLogin =
				user.Logins.FirstOrDefault(
					item => item.LoginProvider == login.LoginProvider && item.ProviderKey == login.ProviderKey );

			if ( userLogin == null ) {
				user.Logins.Add( new IdentityUserLogin {
					ProviderKey = login.ProviderKey ,
					LoginProvider = login.LoginProvider
				} );
			}

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Removes the user login with the specified combination if it exists
		/// </summary>
		/// <param name="user" />
		/// <param name="login" />
		/// <returns />
		public Task RemoveLoginAsync( TUser user , UserLoginInfo login ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			if ( login == null ) {
				throw new ArgumentNullException( nameof( login ) );
			}

			var userLogin =
				user.Logins.FirstOrDefault(
					item => item.LoginProvider == login.LoginProvider && item.ProviderKey == login.ProviderKey );

			if ( userLogin != null ) {
				user.Logins.Remove( userLogin );
			}

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Returns the linked accounts for this user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<IList<UserLoginInfo>> GetLoginsAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return
				Task.FromResult(
					user.Logins.Select( item => new UserLoginInfo( item.LoginProvider , item.ProviderKey ) ).ToList() as
						IList<UserLoginInfo> );
		}

		/// <summary>
		///     Returns the user associated with this login
		/// </summary>
		/// <returns />
		public Task<TUser> FindAsync( UserLoginInfo login ) {
			if ( login == null ) {
				throw new ArgumentNullException( nameof( login ) );
			}

			var userFromDb =
				_collectionManager.Users.AsQueryable()
								  .FirstOrDefault(
									  user =>
										  user.Logins.Any(
											  userLogin => userLogin.LoginProvider == login.LoginProvider && userLogin.ProviderKey == login.ProviderKey ) );

			if ( userFromDb == null ) {
				return Task.FromResult<TUser>( null );
			}

			return Task.FromResult( userFromDb );
		}

		#endregion

		#region IUserTwoFactorStore

		/// <summary>
		///     Sets whether two factor authentication is enabled for the user
		/// </summary>
		/// <param name="user" />
		/// <param name="enabled" />
		/// <returns />
		public Task SetTwoFactorEnabledAsync( TUser user , bool enabled ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.TwoFactorEnabled = enabled;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Returns whether two factor authentication is enabled for the user
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<bool> GetTwoFactorEnabledAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.TwoFactorEnabled );
		}

		#endregion

		#region IUserLockoutStore

		/// <summary>
		///     Returns the DateTimeOffset that represents the end of a user's lockout, any time in the past should be considered
		///     not locked out.
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<DateTimeOffset> GetLockoutEndDateAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return
				Task.FromResult( user.LockoutEndDateUtc.HasValue
					? new DateTimeOffset( DateTime.SpecifyKind( user.LockoutEndDateUtc.Value , DateTimeKind.Utc ) )
					: new DateTimeOffset() );
		}

		/// <summary>
		///     Locks a user out until the specified end date (set to a past date, to unlock a user)
		/// </summary>
		/// <param name="user" />
		/// <param name="lockoutEnd" />
		/// <returns />
		public Task SetLockoutEndDateAsync( TUser user , DateTimeOffset lockoutEnd ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.LockoutEndDateUtc = lockoutEnd == DateTimeOffset.MinValue ? new DateTime?() : lockoutEnd.UtcDateTime;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Used to record when an attempt to access the user has failed
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<int> IncrementAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			++user.AccessFailedCount;

			return Task.FromResult( user.AccessFailedCount );
		}

		/// <summary>
		///     Used to reset the access failed count, typically after the account is successfully accessed
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task ResetAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.AccessFailedCount = 0;

			return Task.FromResult( 0 );
		}

		/// <summary>
		///     Returns the current number of failed access attempts.  This number usually will be reset whenever the password is
		///     verified or the account is locked out.
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<int> GetAccessFailedCountAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.AccessFailedCount );
		}

		/// <summary>
		///     Returns whether the user can be locked out.
		/// </summary>
		/// <param name="user" />
		/// <returns />
		public Task<bool> GetLockoutEnabledAsync( TUser user ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			return Task.FromResult( user.LockoutEnabled );
		}

		/// <summary>
		///     Sets whether the user can be locked out.
		/// </summary>
		/// <param name="user" />
		/// <param name="enabled" />
		/// <returns />
		public Task SetLockoutEnabledAsync( TUser user , bool enabled ) {
			if ( user == null ) {
				throw new ArgumentNullException( nameof( user ) );
			}

			user.LockoutEnabled = enabled;

			return Task.FromResult( 0 );
		}

		#endregion
	}

}
