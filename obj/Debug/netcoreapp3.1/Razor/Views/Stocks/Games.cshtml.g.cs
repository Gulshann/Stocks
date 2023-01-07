#pragma checksum "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "707af7db954e6f0298c6f0f37032647d4cdaee85"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Stocks_Games), @"mvc.1.0.view", @"/Views/Stocks/Games.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"707af7db954e6f0298c6f0f37032647d4cdaee85", @"/Views/Stocks/Games.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a421b6ee63e8df0399751ec01a62a64ca849ec31", @"/Views/_ViewImports.cshtml")]
    public class Views_Stocks_Games : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StocksApp.ViewModels.LearboardViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div class=\"card-contest\">\r\n        <div class=\"container\">\r\n            <h5><b>  Nifty 50 Mega Contest ");
#nullable restore
#line 9 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                      Write(ViewBag.ContestDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 9 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                                           Write(ViewBag.ContestMonth);

#line default
#line hidden
#nullable disable
            WriteLiteral(@" </b></h5>
        </div>
    </div>
    <div class=""form-row"">
        <div class=""col"">
            <div class=""form-control btn btn-primary div-btn"">Leaderboard</div>
        </div>
        <div class=""col"">
            <div class=""form-control btn btn-success div-btn"">Stocks Stats</div>
        </div>
    </div>
");
            WriteLiteral("    <div class=\"form-row table-class\">\r\n        <table id=\"customers\">\r\n            <tr>\r\n                <th>User</th>\r\n                <th>Profit</th>\r\n                <th>Rank</th>\r\n            </tr>\r\n");
#nullable restore
#line 28 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
              

                foreach (var item in Model.Leaderboard.leaderboard)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td><a");
            BeginWriteAttribute("href", " href=\"", 1025, "\"", 1099, 1);
#nullable restore
#line 33 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
WriteAttributeValue("", 1032, Url.Action("Performance", "Stocks", new { teamname = item.member}), 1032, 67, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 33 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                                                                                     Write(item.member);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></td>\r\n                        <td>");
#nullable restore
#line 34 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                       Write(item.score);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 35 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                       Write(item.rank);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 37 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </table>\r\n    </div>\r\n");
            WriteLiteral(@"    <style>
        #customers {
            font-family: Arial, Helvetica, sans-serif;
            border-collapse: collapse;
            width: 100%;
        }

            #customers td, #customers th {
                border: 1px solid #ddd;
                padding: 8px;
            }

            #customers tr:nth-child(even) {
                background-color: #f2f2f2;
            }

            #customers tr:hover {
                background-color: #ddd;
            }

            #customers th {
                padding-top: 12px;
                padding-bottom: 12px;
                text-align: left;
                background-color: #D65E2E;
                color: white;
            }

        .table-class {
            margin-top: 20px;
        }

        .card-contest {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            border-radius: 5px;
            background-color: #ffd800;
            margin: auto;
            w");
            WriteLiteral("idth: 95%;\r\n            margin-bottom: 5px;\r\n            color: black;\r\n            cursor: no-drop;\r\n            text-align: center;\r\n        }\r\n\r\n        .container {\r\n            padding: 2px 16px;\r\n        }\r\n    </style>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<StocksApp.ViewModels.LearboardViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
