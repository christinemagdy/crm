using Bebrand.Domain.Core;
using Bebrand.Domain.Enums;
using Bebrand.Domain.Interfaces;
using Bebrand.Domain.Models;
using Bebrand.Infra.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Bebrand.Infra.Data.Repository
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        protected readonly ITeamLeaderRepository _teamLeaderRepository;
        protected readonly ITeamMemberRepository _teamMemberRepository;

        Dictionary<string, Expression<Func<Client, object>>> columnsMap = new Dictionary<string, Expression<Func<Client, object>>>()
        {
            ["email"] = c => c.Email,
            ["name_of_business"] = c => c.Name_of_business,
            ["modifiedon"] = c => c.ModifiedOn,
            ["position"] = c => c.Position,
            ["completeaddress"] = c => c.Completeaddress,
            ["field"] = c => c.Field,
            ["facebooklink"] = c => c.Facebooklink,
            ["instagramlink"] = c => c.Instagramlink,
            ["lastfeedback"] = c => c.Lastfeedback,
        };

        public ClientRepository(BebrandContext context, IUser user, ITeamLeaderRepository teamLeaderRepository, ITeamMemberRepository teamMemberRepository) : base(context, user)
        {
            _teamLeaderRepository = teamLeaderRepository;
            _teamMemberRepository = teamMemberRepository;
        }
        public void AddBulk(List<Client> clients)
        {
            DbSet.AddRange(clients);
            Db.SaveChanges();
        }
        public QueryResult<Client> GetAll(bool User, OwnerParameters ownerParameters, string key = null)
        {
            var result = new QueryResult<Client>();
            if (!User)
            {
                var personalData = DbSet
                    .Include(x => x.Area)
                    .Include(x => x.ServiceProviders)
                    .ThenInclude(x => x.Service).Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)

                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))
                     .ApplyOrdering(ownerParameters, columnsMap);

                result.data = ownerParameters.PageSize != 0 ? personalData.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                    : personalData.Skip(ownerParameters.PageNumber);

                result.Total = personalData.Count();
                result.PageNumber = ownerParameters.PageNumber;
                result.PageSize = ownerParameters.PageSize;
                result.success = true;
                return result;
            }

            var Data = DbSet.Include(x => x.Area).
                Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                .Where(x => x.AccountManager.ToString() == _user.GetParentUserId())
                .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))
                    .ApplyOrdering(ownerParameters, columnsMap);

            result.data = ownerParameters.PageSize != 0 ? Data.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                    : Data.Skip(ownerParameters.PageNumber);
            result.Total = Data.Count();
            result.PageNumber = ownerParameters.PageNumber;
            result.PageSize = ownerParameters.PageSize;
            result.success = true;
            return result;

        }
        public QueryResult<Client> GetAllActive(bool User, OwnerParameters ownerParameters, string key = null)
        {

            var result = new QueryResult<Client>();
            if (!User)
            {
                var personalData = DbSet

                    .Where(x => x.Status != Domain.Core.UserStatus.Deactivate)
                    .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))

                    .Include(x => x.Area).Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                    .ApplyOrdering(ownerParameters, columnsMap);

                result.data = ownerParameters.PageSize != 0 ? personalData.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                      : personalData.Skip(ownerParameters.PageNumber);
                result.Total = personalData.Count();
                result.PageNumber = ownerParameters.PageNumber;
                result.PageSize = ownerParameters.PageSize;
                return result;
            }
            var Data = DbSet.Include(x => x.Area).Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                .Where(x => x.Status != Domain.Core.UserStatus.Deactivate && x.AccountManager.ToString() == _user.GetParentUserId())
                .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))
                    .ApplyOrdering(ownerParameters, columnsMap);
            result.data = ownerParameters.PageSize != 0 ? Data.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                     : Data.Skip(ownerParameters.PageNumber);
            result.Total = Data.Count();
            result.PageNumber = ownerParameters.PageNumber;
            result.PageSize = ownerParameters.PageSize;
            return result;
        }
        public QueryResult<Client> GetAllDeleted(bool User, OwnerParameters ownerParameters, string key = null)
        {
            var result = new QueryResult<Client>();

            if (!User)
            {
                var personalData = DbSet

                    .Where(x => x.Status == Domain.Core.UserStatus.Deactivate)
                    .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key)).Include(x => x.Area)
                    .Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                    .ApplyOrdering(ownerParameters, columnsMap);

                result.data = ownerParameters.PageSize != 0 ? personalData.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                    : personalData.Skip(ownerParameters.PageNumber);
                result.Total = personalData.Count();
                result.PageNumber = ownerParameters.PageNumber;
                result.PageSize = ownerParameters.PageSize;
                return result;
            }

            var Data = DbSet

                .Where(x => x.AccountManager.ToString() == _user.GetParentUserId() && x.Status == Domain.Core.UserStatus.Deactivate)
                 .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key)).Include(x => x.Area)

                .Include(x => x.ServiceProviders).ThenInclude(x => x.Service).ApplyOrdering(ownerParameters, columnsMap);
            result.data = ownerParameters.PageSize != 0 ? Data.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                   : Data.Skip(ownerParameters.PageNumber);
            result.Total = Data.Count();
            result.PageNumber = ownerParameters.PageNumber;
            result.PageSize = ownerParameters.PageSize;
            return result;
        }
        public QueryResult<Client> GetAvtiveUpdated(bool User, OwnerParameters ownerParameters, string key = null)
        {


            var result = new QueryResult<Client>();
            if (!User)
            {
                var personalData = DbSet
                    .Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                    .Where(x => x.Status == Domain.Core.UserStatus.Updated).
                    Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))

                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key)).Include(x => x.Area)
                    .ApplyOrdering(ownerParameters, columnsMap);

                result.data = ownerParameters.PageSize != 0 ? personalData.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                   : personalData.Skip(ownerParameters.PageNumber);
                result.Total = personalData.Count();
                return result;
            }

            var Data = DbSet
                 .Include(x => x.Area)
                .Include(x => x.ServiceProviders).ThenInclude(x => x.Service).
                Where(x => x.AccountManager.ToString() == _user.GetParentUserId() && x.Status == Domain.Core.UserStatus.Updated).
                Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))
                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))
                    .ApplyOrdering(ownerParameters, columnsMap);

            result.data = ownerParameters.PageSize != 0 ? Data.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                    : Data.Skip(ownerParameters.PageNumber);
            result.Total = Data.Count();
            result.PageNumber = ownerParameters.PageNumber;
            result.PageSize = ownerParameters.PageSize;
            return result;
        }
        public async Task<bool> IfkeyExistence(string key, bool user)
        {
            if (user)
                return await DbSet.Where(x => x.AccountManager != Guid.Parse(_user.GetParentUserId())).AnyAsync(x => x.Email.Equals(key) || x.Number.Equals(key) || x.Name_of_business.Equals(key));
            return await DbSet.AnyAsync(x => x.Email.Equals(key) || x.Number.Equals(key) || x.Name_of_business.Equals(key));
        }
        public async Task<Client> GetById(Guid id)
        {
            return await DbSet.Include(c => c.Area)

                  .Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<Client> GetByEmail(string email)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<QueryMultipleResult<IEnumerable<Client>>> ClientsPerTeam(OwnerParameters ownerParameters, string key = null)
        {
            var result = new QueryMultipleResult<IEnumerable<Client>>();
            try
            {
                var Team = await ClientTeam(Guid.Parse(_user.GetParentUserId()));
                var data = DbSet.Include(x => x.Area).Include(x => x.ServiceProviders).ThenInclude(x => x.Service)
                    .Where(x => Team.Contains(x.AccountManager))
                    .Where(x =>
                   (ownerParameters.AcccountManager == Guid.Empty || x.AccountManager == ownerParameters.AcccountManager)
                   && (ownerParameters.AriaId == Guid.Empty || x.AriaId == ownerParameters.AriaId)
                   && (ownerParameters.From == null || x.ModifiedOn >= ownerParameters.From)
                   && (ownerParameters.To == null || x.ModifiedOn >= ownerParameters.To))

                    .Where(x => x.Nameofcontact.Contains(key) || key == null || x.Name_of_business.Contains(key))
                        .ApplyOrdering(ownerParameters, columnsMap);

                result.Data = ownerParameters.PageSize != 0 ? data.Skip(ownerParameters.PageNumber).Take(ownerParameters.PageSize)
                    : data.Skip(ownerParameters.PageNumber);
                result.IsSucceeded = true;
                result.Total = data.Count();
                result.PageNumber = ownerParameters.PageNumber;
                result.PageSize = ownerParameters.PageSize;
                return result;
            }
            catch (Exception ex)
            {
                result.Errors.Add(ex.Message);
                result.IsSucceeded = false;
                return result;
            }
        }

        public async Task<IEnumerable<Guid>> ClientTeam(Guid ParentId)
        {
            // parent id == SalesDirectorId
            #region Sales Director
            var Teamleaders = await Db.TeamLeaders.Where(x => x.SalesDirectorId == ParentId).ToListAsync(); //Add it to list
            if (Teamleaders.Count != 0)
            {
                var teamLeadersId = Teamleaders.Select(x => x.Id);
                var salesdirectorteamMembers = await Db.TeamMembers.Where(x => teamLeadersId.Contains(x.TeamLeaderId)).ToListAsync(); //Add it to list
                var salesDir = salesdirectorteamMembers.Select(x => x.Id).Concat(Teamleaders.Select(x => x.Id));
                return salesDir;
            }

            #endregion

            #region Team Leader
            //ParentId == TeamLeaderId
            var Teammembers = await Db.TeamMembers.Where(x => x.TeamLeaderId == ParentId).ToListAsync();
            #endregion
            return Teammembers.Select(x => x.Id);
        }
    }
}
