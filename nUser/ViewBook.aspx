<%@ Page Language="C#" MasterPageFile="~/nUser/nUser.master" AutoEventWireup="true" CodeFile="ViewBook.aspx.cs" Inherits="nUser_ViewBook" Title="E-Library : Book View" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Navi_Bar" Runat="Server">
     <div class="NaviBar">
        <a href="home.aspx">HOME</a>
        &nbsp;&nbsp;>&nbsp;&nbsp;
        <asp:HyperLink ID="lblCat" runat="server">HyperLink</asp:HyperLink>
        &nbsp;&nbsp;>&nbsp;&nbsp;
        <asp:HyperLink ID="lblSubCat" runat="server">HyperLink</asp:HyperLink>
        &nbsp;&nbsp;>&nbsp;&nbsp;
        <asp:HyperLink ID="lblBookName" runat="server">HyperLink</asp:HyperLink>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Main_Panel" Runat="Server">
    <style type="text/css">
        .productItem
        {
            width: 120px;
            float: left;
            padding: 7px;
            text-align: center;
            vertical-align:top;
        }
        
        .productContainer
        {   
            width:134px;
            height:190px;
            background-color:Black;
        }
            
        .itemSeparator
        {
            height: 170px;
            width: 10px;
        }
    </style>
    
     <!-- Fancy Box -->    
    <script src="../Script/jquery-1.4.3.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
    <script src="../fancybox/jquery.fancybox-1.3.4.js" type="text/javascript"></script>
    <script src="../fancybox/jquery.mousewheel-3.0.4.pack.js" type="text/javascript"></script>
    <link href="../fancybox/jquery.fancybox-1.3.4.css" rel="stylesheet" type="text/css" /> 
    <script type="text/javascript">
    $(document).ready(function() 
    {
	    $("a.grouped_elements").fancybox({
		    'transitionIn'	:	'elastic',
		    'transitionOut'	:	'elastic',
		    'speedIn'		:	600, 
		    'speedOut'		:	200, 
		    'overlayShow'	:	false
	    });
    });
    </script>
    
    <div>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="GetPages"  
            DataKeyNames="Bookid" GroupItemCount="5" onprerender="ListView1_PreRender" 
            onselectedindexchanged="ListView1_SelectedIndexChanged" 
            onlayoutcreated="ListView1_LayoutCreated">
            <EmptyItemTemplate>
                <td id="Td1" runat="server" />
            </EmptyItemTemplate>
            <ItemTemplate>
                <td id="Td2" runat="server" style="background-color:#DCDCDC;color: #000000;">
                    <div class="productContainer">
                        <div class="productItem">
                            <%--<a href ='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %>' 
                                class="MagicThumb" rel="group: default-speed; slideshow-effect: expand;">
                            <img src='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %>' 
                                alt=''
                                height="148" width="120"  />
                            </a> --%>
                            <a class='grouped_elements' rel='group1' title='<%# Eval("no") %>' href ='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %> '>
                                <img src='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %>' 
                                   alt='<%# Eval("no") %>'
                                   height="148" width="120"  />
                            </a> 
                            
                            <table width="120px">
                                <tr>
                                    <td align="left">
                                        <asp:CheckBox ID="cbxSelectFile" runat="server" Text="" Key='<%# Eval("filename")%>'/>
                                    </td>
                                    <td align="center">
                                        <div style="font-family: Calibri; font-size: small; color: #FFFFFF"  ><%# Eval("no") %></div> 
                                    </td>
                                   <%-- <td align="right">
                                        <a href = '<%# "ViewBook.aspx?command=0&bookid=" + Eval("bookid") + "&filename=" + Eval("filename") %>' 
                                            target ='_self' >
                                        <img src="../Image/Button/download.png" alt='' /></a>
                                    </td>--%>
                                    <td align="right">
                                        <a href = '<%# "ViewBook.aspx?command=0&bookid=" + Eval("bookid") + "&filename=" + Eval("imageurl") %>' 
                                            target ='_self' >
                                        <img src="../Image/Button/download.png" alt='' /></a>
                                    </td>
                                </tr>
                            </table>    
                            
                        </div>
                    </div>
                </td>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table id="Table1" runat="server" 
                    style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                    <tr>
                        <td>No data was returned.</td>
                    </tr>
                </table>
            </EmptyDataTemplate>
            <LayoutTemplate>
                <table id="Table2" runat="server">
                    <tr id="Tr1" runat="server">
                        <td id="Td3" runat="server">
                            <table ID="groupPlaceholderContainer" runat="server">
                                <tr ID="groupPlaceholder" runat="server">
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="Tr2" runat="server">
                        <td id="Td4" runat="server" class="TablePager">
                            <table width="100%">
                            <tr>
                                <td align="left">
                                    <asp:LinkButton ID="btnDownloadSelected"  runat="server" PostBackUrl="ViewBook.aspx?command=1">Download Selected</asp:LinkButton>
                                    &nbsp;
                                    <asp:LinkButton ID="btnDownloadAll"  runat="server" PostBackUrl="ViewBook.aspx?command=2">Download All</asp:LinkButton>
                                </td>
                                <td align="right">
                                     <asp:DataPager ID="DataPager1" runat="server" PageSize="50">
                                        <Fields>
                                            <asp:NumericPagerField 
                                                NumericButtonCssClass ="NumericButtonCSS"
                                                CurrentPageLabelCssClass="CurrentPageLabelCSS"
                                                NextPreviousButtonCssClass="NextPreviousButtonCSS"
                                            />
                                        </Fields>
                                    </asp:DataPager>
                                </td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>
            </LayoutTemplate>
            <GroupTemplate>
                <tr ID="itemPlaceholderContainer" runat="server">
                    <td ID="itemPlaceholder" runat="server">
                    </td>
                </tr>
            </GroupTemplate>
            <SelectedItemTemplate>
            </SelectedItemTemplate>
        </asp:ListView>
        <asp:SqlDataSource ID="GetPages" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_GetPages" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:QueryStringParameter Name="bookid" QueryStringField="bookid" 
                    Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SideBar_Top_Panel" Runat="Server">
    <table class="SideBar_Top_Panel">
        <tr><td class="SideBar_Top_BottomText">Relative Book</td></tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SideBar_Bottom_Panel" Runat="Server">
    <div id="SideBar_Bottom_Panel">
        <asp:Panel ID="BookPanel" runat="server" ></asp:Panel>
    </div>
</asp:Content>

