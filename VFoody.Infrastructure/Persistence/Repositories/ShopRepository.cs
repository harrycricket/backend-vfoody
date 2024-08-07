﻿using Microsoft.EntityFrameworkCore;
using VFoody.Application.Common.Repositories;
using VFoody.Domain.Entities;
using VFoody.Domain.Enums;

namespace VFoody.Infrastructure.Persistence.Repositories;

public class ShopRepository : BaseRepository<Shop>, IShopRepository
{
    public ShopRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }

    public bool IsActiveShop(int shopId)
    {
        return DbSet.Any(s => s.Id == shopId && s.Status == (int)ShopStatus.Active);
    }

    public int CountAll()
    {
        return DbSet.Count(s => s.Status != (int)ShopStatus.Delete);
    }

    public List<Shop> GetAllShopIncludeAddressAccount(int pageNum, int pageSize)
    {
        return DbSet.Include(s => s.Building)
            .Include(s => s.Account)
            .Where(s => s.Status != (int)ShopStatus.Delete)
            .OrderBy(s => s.Id)
            .Skip((pageNum - 1) * pageSize)
            .Take(pageSize)
            .ToList();
    }

    public Account GetAccountByShopId(int shopId)
    {
        return this.DbSet.Include(x => x.Account).SingleOrDefault(x => x.Id == shopId).Account;
    }

    public Shop? GetInfoByShopIdAndStatusIn(int shopId, int[] statusList)
    {
        return DbSet.Include(s => s.Building).SingleOrDefault(s => s.Id == shopId && statusList.Contains(s.Status));
    }

    public async Task<Shop> GetShopByAccountId(int id)
    {
        return await DbSet.Where(shop => shop.AccountId == id).SingleAsync();
    }

    public async Task<Shop> GetShopProfileByAccountIdAsync(int id)
    {
        return await this.DbSet.Include(sh => sh.Building)
            .SingleOrDefaultAsync(shop => shop.AccountId == id).ConfigureAwait(false);
    }
}