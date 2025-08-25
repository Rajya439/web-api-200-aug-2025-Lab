

using Marten;

namespace Demos.Api.Home;

public class PersistentHitCounter(

    IServiceScopeFactory scopeFactory
    ) : ICountHits

{



    //private object _hitLock = new object();

    public async Task<int> GetHitCount(CancellationToken token)
    {
        // inside a singleton, but I need to use a "scoped" service - 
        using (var scope = scopeFactory.CreateScope())
        {
            using var session = scope.ServiceProvider.GetRequiredService<IDocumentSession>();
            var id = Guid.Parse("b37bd889-591a-40b0-b97b-00f007866607");
            var hitCount = await session.Query<HitCounterEntity>()
                .Where(h => h.Id == id)
                .SingleOrDefaultAsync();

            if (hitCount is null)
            {
                session.Store(new HitCounterEntity(id, 1));
                await session.SaveChangesAsync(token);
                return 1;
            }
            else
            {
                var updatedHitCount = hitCount with { Count = hitCount.Count + 1 };
                session.Store(updatedHitCount);
                await session.SaveChangesAsync(token);
                return updatedHitCount.Count;
            }


        } // this will "dispose" the scope. "Using" does that.
    }
}

public record HitCounterEntity(Guid Id, int Count);