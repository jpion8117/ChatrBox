using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Runtime.InteropServices;

namespace ChatrBox.Models
{
    public class HtmlElement
    {
        private Dictionary<string, string> _styles = new Dictionary<string, string>();
        private Dictionary<string, string> _additionalAttributes = new Dictionary<string, string>();
        private List<string> _booleanAttributes = new List<string>();   
        
        /// <summary>
        /// Used to make a tag that self closes ex. <img />, <br />, etc.
        /// </summary>
        public bool SelfClosing { get; private set; } = false;

        /// <summary>
        /// HTML id attribute
        /// </summary>
        public string Id { get; private set; } = string.Empty;

        /// <summary>
        /// Content of the html element
        /// </summary>
        public string InnerHTML { get; private set; } = string.Empty;
        
        /// <summary>
        /// HTML tag type
        /// </summary>
        public string Tag { get; private set; } = string.Empty;
        
        /// <summary>
        /// CSS classes to apply
        /// </summary>
        public string Class { get; private set; } = string.Empty;

        /// <summary>
        /// Formats all stored styles into an inline style string
        /// </summary>
        public string Style
        {
            get
            {
                string styles = "";

                foreach (var style in _styles.Keys)
                {
                    _styles[style] = _styles[style].Replace(";", "");
                    styles += $"{style}: {_styles[style]}; ";
                }

                return styles;
            }
            private set
            {
                //removes any spaces then splits the supplied string at the ';' character used for inline styles
                var styles = value.Replace(" ", "").Split(";");

                //itterates through the style strings
                foreach (var style in styles)
                {
                    //validates against invalid inputs
                    if (!style.IsNullOrEmpty() && style.Contains(':'))
                    {
                        //splits into key/value pairs
                        var styleKeyPair = style.Split(':');
                        if (styleKeyPair.Length == 2)
                        {
                            //adds the style to the styles dictionary
                            _styles[styleKeyPair[0]] = styleKeyPair[1];
                        }
                    }
                }
            }
        }
        public string AdditionalAttributes
        {
            get
            {
                string attrib = "";

                foreach (var attr in _additionalAttributes.Keys)
                {
                    attrib += $" {attr}=\"{_additionalAttributes[attr]}\"";
                }
                foreach (var attr in _booleanAttributes)
                {
                    attrib += $" {attr}";
                }

                return attrib;
            }
        }

        /// <summary>
        /// Creates a new HTMLElement with the provided tag type
        /// </summary>
        /// <param name="tag">HTML tag type</param>
        /// <returns>Empty HTML element of a given type</returns>
        public static HtmlElement Create(string tag, 
                                         string innerHTML = "", 
                                         string cssClass = "",
                                         string style = "")
        {
            var element = new HtmlElement
            {
                Tag = tag,
                InnerHTML = innerHTML,
                Class = cssClass,
                Style = style
            };

            return element;
        }

        /// <summary>
        /// Enables self closing tag style
        /// </summary>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement EnableSelfClose()
        {
            SelfClosing = true;
            return this;
        }

        /// <summary>
        /// Disables self closing tag style
        /// </summary>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement DisableSelfClose()
        {
            SelfClosing = false;
            return this;
        }

        /// <summary>
        /// Adds a CSS class (or classes) to the element
        /// </summary>
        /// <param name="cssClass">Css class(s)</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement AddClass(string cssClass)
        {
            Class += " " + cssClass;
            return this;
        }

        /// <summary>
        /// adds an html id attribute to the element
        /// </summary>
        /// <param name="id">id value</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement SetID (string id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// Add a CSS inline style key value pair to the element
        /// </summary>
        /// <param name="style">style key</param>
        /// <param name="value">style value</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement AddStyle(string style, string value)
        {
            _styles.Add(style, value);
            return this;
        }

        /// <summary>
        /// Adds one or more inline styles to the element using the 
        /// inline style parser ex "style1Option: style1Value; 
        /// style2Option: style2Value"
        /// </summary>
        /// <param name="style">Inline styles to add</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement AddStyle(string style)
        {
            Style = style;
            return this;
        }

        /// <summary>
        /// Adds any additional html attribute to the element
        /// </summary>
        /// <param name="attribute">name of the attribute</param>
        /// <param name="value">attribute value</param>
        /// <returns>Itself for the purpose fo chaining</returns>
        public HtmlElement AddAttribute(string attribute, string value)
        {
            _additionalAttributes.Add(attribute, value);
            return this;
        }

        /// <summary>
        /// Adds any additional html boolean attribute to the element
        /// </summary>
        /// <param name="attribute">name of the boolean attribute</param>
        /// <returns>Itself for the purpose fo chaining</returns>
        public HtmlElement AddAttribute(string attribute)
        {
            _booleanAttributes.Add(attribute);
            return this;
        }

        /// <summary>
        /// Set the content of the current html element
        /// </summary>
        /// <param name="htmlContent">Content to be placed inside the element tags</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement SetContent (string htmlContent)
        {
            InnerHTML = htmlContent;
            return this;
        }

        /// <summary>
        /// Embed one or more html elements in another html element.
        /// </summary>
        /// <param name="innerElements">Element(s) to embed</param>
        /// <returns>Itself for the purpose of chaining</returns>
        public HtmlElement AddContent (params HtmlElement[] innerElements)
        {
            foreach (var innerElement in innerElements)
            {
                InnerHTML += innerElement;
            }
            return this;
        }

        public HtmlElement AddContent(string htmlContent, bool toBeginning = false)
        {
            if (toBeginning)
                InnerHTML = $"{htmlContent} {InnerHTML}";
            else
                InnerHTML += " " + htmlContent;

            return this;
        }
        public HtmlElement AddContent(HtmlElement htmlElement, bool toBeginning = false)
        {
            if (toBeginning)
                InnerHTML = $"{htmlElement} {InnerHTML}";
            else
                InnerHTML += " " + htmlElement;

            return this;
        }

        public HtmlElement ChangeTag(string tag)
        {
            Tag = tag;
            return this;
        }

        /// <summary>
        /// Formats the given settings into an HTML string that can be sent to view.
        /// </summary>
        /// <returns>String containing a complete html element</returns>
        public override string ToString()
        {
            string html = "";

            if (SelfClosing)
                html = $"<{Tag} id=\"{Id}\" class=\"{Class}\" style=\"{Style}\" {AdditionalAttributes} />";
            else
                html = $"<{Tag} id=\"{Id}\" class=\"{Class}\" style=\"{Style}\" {AdditionalAttributes}>{InnerHTML}</{Tag}>";

            return html;
        }
    }
}
