using ImageStoreWeb.Infrastructure;
using ImageStoreWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ImageStoreWeb.Repositories
{
    public class ClientRepository : DBRepository
    {

        public async Task<Client> FindAsync(string userName, string password)
        {
            var hashPwd = AppHelper.GetHash(password);
            var user = await DB.Clients.FirstOrDefaultAsync(x => x.UserName == userName
           && x.Password == hashPwd);
            return user;
        }

        public async Task AddAsync(Client newUser)
        {
            DB.Clients.Add(newUser);

            await DB.SaveChangesAsync();
        }

        public async Task<Client> FindAsync(string userName)
        {
            var user = await DB.Clients.FirstOrDefaultAsync(x => x.UserName == userName);

            return user;

        }
    }
}