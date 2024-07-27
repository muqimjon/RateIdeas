using Microsoft.EntityFrameworkCore;
using RateIdeas.Application.Commons.Interfaces;
using RateIdeas.Domain.Common;
using RateIdeas.Infrastructure.Contexts;
using System.Linq.Expressions;

namespace RateIdeas.Infrastructure.Repositories;

public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : Auditable
{
    public DbSet<T> Table
    {
        get => dbContext.Set<T>();
    }

    public async Task InsertAsync(T entity)
    {
        entity.CreatedAt = DateTimeOffset.UtcNow;
        await Table.AddAsync(entity);
    }

    public void Update(T entity)
    {
        entity.UpdatedAt = DateTimeOffset.UtcNow;
        Table.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(T entity)
    {
        Table.Remove(entity);
    }

    public void Delete(Expression<Func<T, bool>> expression)
    {
        Table.RemoveRange(Table.Where(expression));
    }

    public async Task<T> SelectAsync(Expression<Func<T, bool>> expression, string[] includes = null!)
    {
        var query = Table.AsQueryable();

        if (includes is not null)
            foreach (string item in includes)
                query = query.Include(item);

        return (await query.FirstOrDefaultAsync(expression))!;
    }

    public IQueryable<T> SelectAll(Expression<Func<T, bool>> expression = null!, string[] includes = null!)
    {
        var query = Table.AsQueryable();

        if (includes is not null)
            foreach (string include in includes)
                query = query.Include(include);

        return expression is null ? query : query.Where(expression);
    }

    public Task<int> SaveAsync()
        => dbContext.SaveChangesAsync();
}