#pragma checksum "C:\Users\ttane\OneDrive\Documents\Fresh\NBD-TractionFive\Views\Shared\_PageSizeModal.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ea830a7d5641fbff14b79c55caf5efb060333904"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__PageSizeModal), @"mvc.1.0.view", @"/Views/Shared/_PageSizeModal.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\ttane\OneDrive\Documents\Fresh\NBD-TractionFive\Views\_ViewImports.cshtml"
using NBD_TractionFive;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\ttane\OneDrive\Documents\Fresh\NBD-TractionFive\Views\_ViewImports.cshtml"
using NBD_TractionFive.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea830a7d5641fbff14b79c55caf5efb060333904", @"/Views/Shared/_PageSizeModal.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"38fe0db2d0a1ca75109588fbc654324c134c3499", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__PageSizeModal : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<!-- Modal-->
<div class=""modal fade"" id=""pageSizeModal"" tabindex=""-1"" role=""dialog"" aria-labelledby=""pageSizeModalLabel"">
    <div class=""modal-dialog"" role=""document"">
        <div class=""modal-content"">
            <div class=""modal-header"">
                <h4 class=""modal-title"" id=""pageSizeModalLabel"">Set Page Size</h4>
                <button type=""button"" class=""close"" data-dismiss=""modal"" aria-label=""Close""><span aria-hidden=""true"">&times;</span></button>
            </div>
            <div class=""modal-body"">
                <div class=""form-row"">
                    <div class=""form-group col-md-12"">
                        <div class=""input-group"">
                            <div class=""input-group-prepend"">
                                <span class=""input-group-text"">Page Size: </span>
                            </div>
                            ");
#nullable restore
#line 16 "C:\Users\ttane\OneDrive\Documents\Fresh\NBD-TractionFive\Views\Shared\_PageSizeModal.cshtml"
                       Write(Html.DropDownList("pageSizeID", null, htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            <div class=""input-group-append"">
                                <input type=""submit"" class=""btn btn-primary"" value=""Set Page Size"" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
