#pragma checksum "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\stocks.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9e48f57645deb5c75dfb1512544f2883215513e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Stocks_stocks), @"mvc.1.0.view", @"/Views/Stocks/stocks.cshtml")]
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
#line 1 "C:\Hackathon-Onmobile\StocksApp\Views\_ViewImports.cshtml"
using StocksApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Hackathon-Onmobile\StocksApp\Views\_ViewImports.cshtml"
using StocksApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9e48f57645deb5c75dfb1512544f2883215513e9", @"/Views/Stocks/stocks.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a421b6ee63e8df0399751ec01a62a64ca849ec31", @"/Views/_ViewImports.cshtml")]
    public class Views_Stocks_stocks : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "9e48f57645deb5c75dfb1512544f2883215513e93158", async() => {
                WriteLiteral(@"
        <div class=""form-row"">
            <div class=""col"">
                <div class=""form-control btn btn-primary div-btn"">Upcoming</div>
            </div>
            <div class=""col"">
                <div class=""form-control btn btn-success div-btn"">My Games</div>
            </div>
        </div>

        <div class=""card-section"">
            <div class=""toggle-div"" id=""home-div"">
                <div class=""card"">
                    <div class=""container navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target="".card-collapse"" aria-controls=""navbarSupportedContent""
                         aria-expanded=""false"" aria-label=""Toggle navigation"">
                        <h5><b>Nifty 50</b></h5>
                        <p class=""para"">Top 50 Stocks</p>
                    </div>
                </div>
                <div class=""card-collapse collapse d-sm-inline-flex flex-sm-row-reverse card-context-parent"">
                    <div class=""card-contest"">
                  ");
                WriteLiteral(@"      <div class=""container"">
                            <h5><b>Mega Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1L</p>
                                <p class=""card-expando"">6100/100000</p>
                            </div>
                        </div>
                    </div>
                    <div class=""card-contest"">
                        <div class=""container"">
                            <h5><b>Pro Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1L</p>
                                <p class=""card-expando"">6100/100000</p>
                            </div>

                        </div>
                    </div>
                    <div class=""card-contest"">
                        <div class=""container"">
                            <h5><b>Pro Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1");
                WriteLiteral(@"L</p>
                                <p class=""card-expando"">6100/100000</p>
                            </div>
                        </div>
                    </div>
                </div>
                <div class=""card"">
                    <div  class=""container navbar-toggler"" type=""button"" data-toggle=""collapse"" data-target="".card-collapse1"" aria-controls=""navbarSupportedContent""
                         aria-expanded=""false"" aria-label=""Toggle navigation"">
                        <h5><b>Bank Nifty</b></h5>
                        <p class=""para"">Bank Stocks</p>
                    </div>
                </div>
                <div class=""card-collapse1 collapse d-sm-inline-flex flex-sm-row-reverse card-context-parent"">
                    <div id=""mega_nifty_50"" class=""card-contest"">
                        <div class=""container"">
                            <h5><b>Mega Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1L</p");
                WriteLiteral(@">
                                <p class=""card-expando"">6100/100000</p>
                            </div>
                        </div>
                    </div>
                    <div class=""card-contest"">
                        <div class=""container"">
                            <h5><b>Pro Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1L</p>
                                <p class=""card-expando"">6100/100000</p>
                            </div>

                        </div>
                    </div>
                    <div class=""card-contest"">
                        <div class=""container"">
                            <h5><b>Pro Contest</b></h5>
                            <div class=""row-div"">
                                <p>Win 1L</p>
                                <p class=""card-expando"">6100/100000</p>
                            </div>
                        </div>
                    </div>
        ");
                WriteLiteral(@"        </div>
                <div class=""card"">
                    <div class=""container"">
                        <h5><b>Nifty Auto</b></h5>
                        <p>Auto Stocks</p>
                    </div>
                </div>
                <div class=""card"">
                    <div class=""container"">
                        <h5><b>Nifty Oil</b></h5>
                        <p>Oil Stocks</p>
                    </div>
                </div>
            </div>
        </div>

    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    <script>\r\n        var playUrl = \'");
#nullable restore
#line 108 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\stocks.cshtml"
                  Write(Url.Action("Play", "Stocks"));

#line default
#line hidden
#nullable disable
            WriteLiteral("\'\r\n    </script>\r\n");
            WriteLiteral(@"    <style>
       /* .para{
            font-size:13px;
        }*/

        p{
            font-size:13px;
        }
        .card-expando:hover {
            /*height: 300px;*/
        }

        .card-expando {
            margin-left: auto;
            /*cursor:pointer;*/
        }
        .row-div {
            display: flex;
            flex-wrap: wrap;
        }
        .div-btn {
            cursor: pointer;
        }
        .card-section {
            margin-top: 30px;
        }
        .card {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            width: 100%;
            border-radius: 5px;
            margin-bottom: 10px;
            /*transition: height 5s;*/
            /*background-color:lightcoral;*/
        }

        .card-contest {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            border-radius: 5px;
            background-color: #354832;
            margin: auto;");
            WriteLiteral(@"
            width: 95%;
            margin-bottom: 5px;
            color: white;
            cursor:pointer;
        }

            .card-contest:hover {
                box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
            }

        .card:hover {
            box-shadow: 0 8px 16px 0 rgba(0,0,0,0.2);
        }

        .container {
            padding: 2px 16px;
        }
        .card-context-parent {
            background: rgb(129, 129, 129);
            border-radius: 5px;
            padding-top: 4%;
            padding-bottom: 4%;
            margin-bottom:10%;
        }

    </style>
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
