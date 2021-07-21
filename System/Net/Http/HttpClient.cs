using System;
using System.IO;
using System.Threading.Tasks;

namespace Sandbox.System.Net.Http
{
	public class HttpClient : IDisposable
	{
        public void Dispose()
        {
        }

        public async Task<string> GetStringAsync(string uri)
			=> await GetStringAsync(new Uri(uri));

		public async Task<string> GetStringAsync(Uri uri)
		{
			var http = new Internal.Http(uri);

			return await http.GetStringAsync();
		}

		public async Task<Stream> GetStreamAsync(string uri)
			=> await GetStreamAsync(new Uri(uri));

		public async Task<Stream> GetStreamAsync(Uri uri)
		{
			var http = new Internal.Http(uri);

			return await http.GetStreamAsync();
		}

		public async Task<byte[]> GetByteArrayAsync(string uri)
			=> await GetByteArrayAsync(new Uri(uri));

		public async Task<byte[]> GetByteArrayAsync(Uri uri)
		{
			var http = new Internal.Http(uri);

			return await http.GetBytesAsync();
		}
	}
}
