#pragma checksum "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2aa2c42398b1e947f0b607e456c8b8911ccd09fe"
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
#nullable restore
#line 4 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
using StocksApp.Session;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2aa2c42398b1e947f0b607e456c8b8911ccd09fe", @"/Views/Stocks/Games.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a421b6ee63e8df0399751ec01a62a64ca849ec31", @"/Views/_ViewImports.cshtml")]
    public class Views_Stocks_Games : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<StocksApp.ViewModels.LearboardViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("    <div class=\"header\">\r\n        <h4><b>  Nifty 50 Mega Contest ");
#nullable restore
#line 16 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                  Write(ViewBag.ContestDate);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 16 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                                       Write(ViewBag.ContestMonth);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </b></h4>\r\n    </div>\r\n");
            WriteLiteral("    <div class=\"form-row table-class\">\r\n        <table id=\"customers\">\r\n            <tr>\r\n                <th>User</th>\r\n                <th>Profit</th>\r\n                <th>Rank</th>\r\n            </tr>\r\n");
#nullable restore
#line 26 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
              

                foreach (var item in Model.Leaderboard.leaderboard)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr");
            BeginWriteAttribute("class", " class=\"", 903, "\"", 966, 1);
#nullable restore
#line 30 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
WriteAttributeValue("", 911, item.member==sesssionData.UserID ? "current-user":"", 911, 55, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <td><a");
            BeginWriteAttribute("href", " href=\"", 1000, "\"", 1074, 1);
#nullable restore
#line 31 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
WriteAttributeValue("", 1007, Url.Action("Performance", "Stocks", new { teamname = item.member}), 1007, 67, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 31 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                                                                                                     Write(item.member);

#line default
#line hidden
#nullable disable
            WriteLiteral("</a></td>\r\n                        <td>");
#nullable restore
#line 32 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                       Write(item.score);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 33 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
                       Write(item.rank);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    </tr>\r\n");
#nullable restore
#line 35 "C:\Hackathon-Onmobile\StocksApp\Views\Stocks\Games.cshtml"
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
                background-color: #0069d9;
                color: white;
            }

        .table-class {
            margin-top: 20px;
            padding:5px;
        }

        .card-contest {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2);
            transition: 0.3s;
            border-radius: 5px;
            background-color: #ffd800;
            ma");
            WriteLiteral(@"rgin: auto;
            width: 95%;
            margin-bottom: 5px;
            color: black;
            cursor: no-drop;
            text-align: center;
        }

        .container {
            padding: 2px 16px;
        }

        .header {
            padding: 5px;
            text-align: center;
            background: #1abc9c;
            color: white;
            font-size: 30px;
        }

        .current-user {
            background-color: #fab7b7 !important;
        }
    </style>
");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SessionData sesssionData { get; private set; }
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
