using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.Caching.Distributed;
using Npgsql.Replication.PgOutput.Messages;
using SSRUnited.Shared.Data;
using SSRUnited.Shared.Dtos;
using SSRUnited.Shared.Entity;
using SSRUnited.Shared.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSRUnited.Shared.Respository
{

    public class Respository : IIRespository
    {
        private readonly ApplicatonDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _cacheOptions;

        public Respository(ApplicatonDbContext context, IDistributedCache cache)
        {
            _context = context;
            _cache = cache;

            _cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            };
        }


        public async Task CreateAsync(HumanDto request, CancellationToken cancellationToken = default!)
        {
            var check = await _context.humans.AnyAsync(s => s.name == request.content);
            if (check) return;

            var entity = new Human
            {
                name = request.name,
                content = request.content,
                created_at = DateTime.UtcNow
            };
            _context.humans.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync("humans_list", cancellationToken);
        }

        public async Task<List<HumanDto>> Listing(CancellationToken cancellationToken = default!)
        {
            string cacheKey = "humans_list";

            var cached_data = await _cache.GetStringAsync(cacheKey, cancellationToken);

            if(cached_data is not null)
            {
                return JsonSerializer.Deserialize<List<HumanDto>>(cached_data)!;
            }

            var data = await _context.humans
                .Select(s => new HumanDto
                {
                    name = s.name,
                    content = s.content,
                    Id = s.Id,
                    created_at = s.created_at,

                }).ToListAsync(cancellationToken);

            var SerializeData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(cacheKey, SerializeData, _cacheOptions, cancellationToken);
            return data;
        }

        public async Task<HumanDto?> Get(int Id, CancellationToken cancellationToken = default!)
        {
            string cacheKey = $"human_{Id}";

            var CachedData = await _cache.GetStringAsync($"{Id}", cancellationToken);

            if(CachedData is not null)
            {
                return JsonSerializer.Deserialize<HumanDto>(CachedData)!;
            }

            var query = await _context.humans.FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);
            if (query is null) return null;


            var data = new HumanDto
            {
                content = query.content,
                name = query.name,
                created_at = query.created_at,
            };

            var SerializeData = JsonSerializer.Serialize(data);
            await _cache.SetStringAsync(cacheKey,SerializeData, _cacheOptions, cancellationToken);
            return data;
        }
        public async Task<bool> Delete(int Id, CancellationToken cancellationToken = default!)
        {
            var query = await _context.humans.FirstOrDefaultAsync(s => s.Id == Id, cancellationToken);
            if (query is null) return false;

            _context.humans.Remove(query);
            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync($"human_{Id}", cancellationToken);
            await _cache.RemoveAsync("humans_list", cancellationToken);
            return true;

        }
        public async Task<bool> Update(HumanDto request, CancellationToken cancellationToken = default!)
        {
            var query = await _context.humans.FirstOrDefaultAsync(s => s.Id == request.Id);
            if (query is null) return false;

            query.content = request.content;
            query.name = request.name;
            _context.humans.Update(query);
            await _context.SaveChangesAsync(cancellationToken);

            await _cache.RemoveAsync($"human_{request.Id}", cancellationToken);
            await _cache.RemoveAsync("humans_list", cancellationToken);
            return true;

        }
    }
}
