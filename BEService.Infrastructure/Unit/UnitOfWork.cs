using BEService.Core.CustomEntities;
using BEService.Core.Entities;
using BEService.Core.Interfaces.Repositories;
using BEService.Core.Interfaces.Unit;
using BEService.Infrastructure.Data;
using BEService.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BEService.Infrastructure.Unit
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BEDBContext _ctx;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IBaseRepository<MenuApp> _menuAppRepository;
        private readonly IBaseRepository<MenuAccess> _menuAccessRepository;

        public UnitOfWork(BEDBContext ctx, IHttpContextAccessor httpContextAccessor)
        {
            _ctx = ctx;
            _httpContextAccessor = httpContextAccessor;
        }

        public IUserRepository UserRepository => _userRepository ?? new UserRepository(_ctx);
        public IRoleRepository RoleRepository => _roleRepository ?? new RoleRepository(_ctx);
        public IBaseRepository<MenuApp> MenuAppRepository => _menuAppRepository ?? new BaseRepository<MenuApp>(_ctx);
        public IBaseRepository<MenuAccess> MenuAccessRepository => _menuAccessRepository ?? new BaseRepository<MenuAccess>(_ctx);

        public void Dispose()
        {
            if(_ctx != null)
            {
                _ctx.Dispose();
            }
        }

        public void SaveChanges()
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                if (((BaseEntity)entityEntry.Entity).IsDelete == true)
                {
                    ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = true;
                }
                else
                {
                    ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }
            }

            _ctx.SaveChanges();
        }

        public async Task SaveChangesAsync(bool delete = false)
        {
            var entries = _ctx.ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                        e.State == EntityState.Added
                        || e.State == EntityState.Modified));

            var username = "";
            if (_httpContextAccessor.HttpContext != null && _httpContextAccessor.HttpContext.User != null)
            {
                ClaimsIdentity claimsIdentity = _httpContextAccessor.HttpContext.User.Identity as ClaimsIdentity;
                var claims = claimsIdentity.Claims.Select(x => new { type = x.Type, value = x.Value });
                if (claims.Any())
                {
                    username = claims.SingleOrDefault(x => x.type == "Email").value;
                }
                else
                {
                    username = "System";
                }
            }

            foreach (var entityEntry in entries)
            {
                if (delete == true)
                {
                    ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).DeletedBy = username;
                    ((BaseEntity)entityEntry.Entity).IsDelete = true;
                }
                else
                {
                    if (((BaseEntity)entityEntry.Entity).IsDelete == true)
                    {
                        ((BaseEntity)entityEntry.Entity).DeletedAt = DateTime.UtcNow;
                        ((BaseEntity)entityEntry.Entity).DeletedBy = username;
                        ((BaseEntity)entityEntry.Entity).IsDelete = true;
                    }
                    else
                    {
                        ((BaseEntity)entityEntry.Entity).UpdatedAt = DateTime.UtcNow;
                        ((BaseEntity)entityEntry.Entity).UpdatedBy = username;
                        ((BaseEntity)entityEntry.Entity).IsDelete = false;
                    }
                }
                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedAt = DateTime.UtcNow;
                    ((BaseEntity)entityEntry.Entity).CreatedBy = username;
                    ((BaseEntity)entityEntry.Entity).IsDelete = false;
                }
            }

            await _ctx.SaveChangesAsync();
        }
    }
}
