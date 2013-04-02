<%@ Page Language="C#" MasterPageFile="~/nUser/nUser.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="nUser_home" Title="E-Library : Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Navi_Bar" Runat="Server">
    <a href="home.aspx">HOME</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Main_Panel" Runat="Server">
    <style type="text/css">
        .productItem
        {
            width: 148px;
            float: left;
            padding: 7px;
            text-align: center;
            vertical-align:top;
        }
        
        .productContainer
        {   
            width:162px;
            height:227px;
            background-color:Black;
        }
        
        .productName
        {
            font-size:x-small;
            display:block ;
            width:150px;
            height:30px;
            color:White;
            font-family:Calibri;
            font-size:small;
        }
                
        .productDescription
        {
        	font-size:x-small;
        }
        
        
        .itemSeparator
        {
            height: 170px;
            width: 10px;
        }
    </style>
    
    <div>
        <asp:ListView ID="ListView1" runat="server" DataSourceID="GetLastest8"  DataKeyNames="Bookid" GroupItemCount="4">
            <EmptyItemTemplate>
                <td id="Td1" runat="server" />
            </EmptyItemTemplate>
            <ItemTemplate>
                <td id="Td2" runat="server" style="background-color:#DCDCDC;color: #000000;">
                    <div class="productContainer">
                        <div class="productItem">
                            <a href = '<%# "ViewBook.aspx?bookid="+Eval("bookid") %>' target ="_blank">
                            <img
                                title='<%# Eval("name") %>'  alt=''
                                src='<%# "../E-library/" + Eval("imgpath") %>' 
                                height="183" width="148"  />
                            </a>
                            <div class="productName"><%# Eval("name") %></div>
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
                        <td id="Td4" runat="server" 
                            style="text-align: center;background-color: #CCCCCC;
                            font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                             <asp:DataPager ID="DataPager1" runat="server" PageSize="8">
                                
                            </asp:DataPager>
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
        <asp:SqlDataSource ID="GetLastest8" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_Book_GetLastestMagazine" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="SideBar_Top_Panel" Runat="Server">
    <table class="SideBar_Top_Panel">
        <tr><td class="WelcomeText">Welcome</td></tr>
        <tr><td class="SideBar_Top_BottomText">E-library</td></tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="SideBar_Bottom_Panel" Runat="Server">
    <script type="text/javascript" src="../Script/jquery-1.4.2.min.js"></script>
        <script type="text/javascript" src="../Script/jquery.jcarousel.min.js"></script>
        <link rel="Stylesheet" type="text/css" href="../skins/tango/skin.css" />
      
       
      <script type ="text/javascript">
        jQuery(document).ready(function() {
            jQuery('#mycarousel').jcarousel({
                auto: 5,
                wrap: 'last',
                //initCallback: mycarousel_initCallback,
                vertical: true,
                scroll:5
            });
        });
      </script>

     <div id="SideBar_Bottom_Panel">
        <asp:Panel ID="BookPanel" runat="server" ></asp:Panel>
    </div>
</asp:Content>

