using ImageWebAPIs.Infrastructure;
using ImageWebAPIs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace ImageWebAPIs.Repositories
{
    public class TokenRepository : DBRepository
    {
      
        public async Task<Token> FindAsync(string token)
        {
            var entity = await DB.Tokens.Where(x => x.AuthToken == token).FirstOrDefaultAsync();

            return entity;
        }

        public async Task RemoveAsync(string token)
        {
            var entity = await FindAsync(token);
            if (entity != null)
            {
                DB.Tokens.Remove(entity);
                await DB.SaveChangesAsync();
            }

        }

        public async Task RemoveUserTokenAsync(int userId,string token)
        {
            var entities = await DB.Tokens.Where(x => x.UserId == userId&&x.AuthToken==token).ToListAsync();
            if (entities.Count > 0)
            {
                DB.Tokens.RemoveRange(entities);
                await DB.SaveChangesAsync();
            }
        }

        public async Task SaveAsync(string token, DateTime? expiresOn, DateTime? issuedOn, int userId)
        {
            var tokenEntity = new Token();
            tokenEntity.AuthToken = token;
            tokenEntity.ExpiresOn = expiresOn;
            tokenEntity.IssuedOn = issuedOn;
            tokenEntity.UserId = userId;

            DB.Tokens.Add(tokenEntity);
            await DB.SaveChangesAsync();
        }
    }
}