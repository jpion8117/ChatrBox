using Markdig.Extensions.MediaLinks;
using Markdig.Renderers;
using Markdig.Syntax;
using System.Diagnostics.CodeAnalysis;

namespace ChatrBox.CoreComponents
{
    public class Youtube : IHostProvider
    {
        public bool AllowFullScreen { get; set; }

        string? IHostProvider.Class { get; }

        public bool TryHandle(Uri mediaUri, bool isSchemaRelative, [NotNullWhen(true)] out string? iframeUrl)
        {
            throw new NotImplementedException();
        }
    }
}
