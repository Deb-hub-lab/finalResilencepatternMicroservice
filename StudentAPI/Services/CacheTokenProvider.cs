using Microsoft.Extensions.Primitives;

namespace StudentAPI.Services
{
    public class CacheTokenProvider
    {
        private CancellationTokenSource _cts = new();

        public IChangeToken GetToken()
            => new CancellationChangeToken(_cts.Token);

        // Call this whenever data is updated
        public void Reset()
        {
            var oldToken = _cts;
            _cts = new CancellationTokenSource();
            oldToken.Cancel();
        }
    }
}
