using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace Instagram.TagHelpers
{
    public class ContactsTagHelper : TagHelper
    {
        private const string address = "https://www.instagram.com/oliasadnes/";
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a"; 
            output.Attributes.SetAttribute("href", address);
            output.Attributes.SetAttribute("class", "icons-href");
            output.Content.SetContent("About us");
        }
    }
}
