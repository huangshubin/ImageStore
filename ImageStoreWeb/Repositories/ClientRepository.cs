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

        public async Task<Client> FindAsync(string userName, string password, bool isActive = true)
        {
            var hashPwd = AppHelper.GetHash(password);
            var user = await DB.Clients.FirstOrDefaultAsync(x => x.UserName == userName
           && x.Password == hashPwd && x.Active == isActive);
            return user;
        }

    }
}