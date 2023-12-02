using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ChatrBox.Models
{
    public class ElementTree : HtmlElement
    {
        public List<HtmlElement> InnerElements { get; private set; } = new List<HtmlElement>();

        public static ElementTree Parse(string html)
        {
            var dom = new ElementTree();

            
            
            return dom;
        }
    }
}
