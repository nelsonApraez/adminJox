namespace Infrastructure.Repositories
{
    using System.Threading.Tasks;    
    using Domain.AggregateModels;
    using Domain.Common;
    using Package.Utilities.Net;
    using Package.Utilities.Net.Utilities;

    /// <summary>
    /// Class represents data access for Entity (User)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class UserRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.User>,
        Domain.Repositories.Interfaces.IUserRepository
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (User)
        /// </summary>
        /// <param name="context">Database context instance</param>
        public UserRepository(IMainContext context) : base(context) { }

        /// <summary>
        /// Async Creation of Entity Business Objects <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Specified entity <typeparamref name="T"/></param>
        /// <returns>Returns if Id Inserted or, failing that, number of records altered</returns>
        public new async Task<int?> CreateAsync(User objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                objCreate.PasswordHash = SecurityToken.HashPassword(objCreate.PasswordHash, out var salt);
                objCreate.Salt = salt;
                await GetInstanceRepository.Set<User>().AddAsync(objCreate);
                returnCreate = await RepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnCreate;
        }

        public async Task<User> LoginAsync(User user)
        {
            User userDatabase = await SearchAsync(x => x.Username.Equals(user.Username) );
            bool passwordValid = SecurityToken.VerifyHashedPassword(user.PasswordHash, userDatabase.Salt, userDatabase.PasswordHash);

            if(passwordValid)
            {
                // TODO: return Successful response
                return userDatabase;
            } else
            {
                // TODO: return error
                throw new System.Exception();
            }
        }
    }
}
