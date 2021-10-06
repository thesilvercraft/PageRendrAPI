using System;
using System.Drawing;
using System.Threading.Tasks;

namespace SDBrowser
{
    public interface IBrowser
    {
        Task<Bitmap> RenderUrlAsync(string url, bool waitfor10s = false);

        Task<Bitmap> RenderUrlAsync(Uri url, bool waitfor10s=false)
        {
            return RenderUrlAsync(url.ToString(), waitfor10s);
        }

        Task<Bitmap> RenderHtmlAsync(string html, bool waitfor10s = false);
    }
}