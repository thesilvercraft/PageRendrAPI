using System;
using System.Drawing;
using System.Threading.Tasks;

namespace SDBrowser
{
    public interface IBrowser
    {
        Task<Bitmap> RenderUrlAsync(string url, byte waittime = 0);

        Task<Bitmap> RenderUrlAsync(Uri url, byte waittime = 0)
        {
            return RenderUrlAsync(url.ToString(), waittime);
        }

        Task<Bitmap> RenderHtmlAsync(string html, byte waittime = 0);
    }
}