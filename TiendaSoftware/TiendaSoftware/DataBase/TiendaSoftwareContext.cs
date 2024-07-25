﻿using TiendaSoftware.DataBase.Entities;
using TiendaSoftware.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TiendaSoftware.DataBase
{
    public class TiendaSoftwareContext : DbContext
    {
        private readonly IAuthService _authService;

        public TiendaSoftwareContext(
            DbContextOptions options,
            IAuthService authService
            ) : base(options)
        {
            this._authService = authService;
        }

        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (
                    e.State == EntityState.Added ||
                    e.State == EntityState.Modified
                ));

            foreach (var entry in entries)
            {
                var entity = entry.Entity as BaseEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = _authService.GetUserId();
                        entity.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        entity.UpdatedBy = _authService.GetUserId();
                        entity.UpdatedDate = DateTime.Now;
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TagEntity> Tags { get; set; }
        public DbSet<ListEntity> Lists { get; set; }
        public DbSet<SoftwareTagsEntity> SoftwareTags { get; set; }
        public DbSet<ReviewEntity> Reviews { get; set; }
        public DbSet<SoftwareEntity> Software { get; set; }
        public DbSet<ListSoftwareEntity> SoftwareLists { get; set; }
        public DbSet<PublisherEntity> Publishers { get; set; }
        public DbSet<UserDownloadsEntity> UserDownloads { get; set; }
    }
}

