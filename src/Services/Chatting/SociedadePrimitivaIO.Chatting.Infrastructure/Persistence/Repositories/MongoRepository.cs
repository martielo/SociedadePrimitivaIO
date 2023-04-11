using MongoDB.Driver;
using NetDevPack.Domain;

namespace SociedadePrimitivaIO.Chatting.Infrastructure.Persistence.Repositories
{
    public class MongoRepository<TAggregateRoot> where TAggregateRoot : Entity, IAggregateRoot
    {
        protected readonly MongoContext _context;
        protected IMongoCollection<TAggregateRoot> _dbSet;

        public MongoRepository(MongoContext context)
        {
            _context = context;
            _dbSet = context.GetCollection<TAggregateRoot>(typeof(TAggregateRoot).Name);
        }

        protected virtual void Add(TAggregateRoot obj)
        {
            _context.AddCommand(() => _dbSet.InsertOneAsync(_context.Session, obj));
        }

        protected virtual async Task<TAggregateRoot> GetById(Guid id)
        {
            var data = await _dbSet.FindAsync(Builders<TAggregateRoot>.Filter.Eq("_id", id));
            return data.SingleOrDefault();
        }
        protected virtual async Task<IEnumerable<TAggregateRoot>> GetAll()
        {
            var all = await _dbSet.FindAsync(Builders<TAggregateRoot>.Filter.Empty);
            return all.ToList();
        }

        protected virtual void Update(TAggregateRoot obj)
        {
            _context.AddCommand(() => _dbSet.ReplaceOneAsync(Builders<TAggregateRoot>.Filter.Eq("_id", obj.Id), obj));
        }

        protected virtual void Remove(Guid id)
        {
            _context.AddCommand(() => _dbSet.DeleteOneAsync(Builders<TAggregateRoot>.Filter.Eq("_id", id)));
        }
    }
}
