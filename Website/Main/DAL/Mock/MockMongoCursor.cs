
using MongoDB.Driver;

namespace Main.DAL.Mock
{
    public class MockMongoCursor<T> : IAsyncCursor<T>
    {
        private List<T> _backing;
        private bool _hasMore = true;

        public MockMongoCursor(FilterDefinition<T> filter, List<T> list)
        {
            _backing = list;
        }

        public IEnumerable<T> Current => _backing;

        public void Dispose() {}

        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            bool result = _hasMore;
            _hasMore = false;
            return result;
        }

        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            return new Task<bool>(() => MoveNext(cancellationToken));
        }

    }

}
