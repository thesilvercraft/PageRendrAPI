using System;
using System.Drawing;
using System.Threading.Tasks;

namespace SDBrowser
{
    public interface IBrowser
    {
        Task<Bitmap> RenderUrlAsync(string url);

        Task<Bitmap> RenderUrlAsync(Uri url);

        Task<Bitmap> RenderHtmlAsync(string html);
    }
}