<%@ Page Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="SearchByCategory.aspx.cs" Inherits="User_SearchByCategory" Title="E-Library : Search by Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบค้นหาหนังสือโดยใช้ Category</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <style type="text/css">
        .productItem
        {
            width: 150px;
            float: left;
            padding: 5px;
            margin: 5px;
            text-align: center;
            vertical-align:top;
        }
        
        .productName
        {
            font-size:x-small;
            display:block ;
            width:150px;
            height:30px;
        }
        
        .productDescription
        {
        	font-size:x-small;
        }
        
        .groupSeparator
        {
            border-top: 1px dotted Gray;
            height: 1px;
            clear: both;
        }
        .itemSeparator
        {
            height: 170px;
            width: 1px;
            border-left: 1px dotted Gray;
            margin-top: 5px;
            margin-bottom: 5px;
            float: left;
        }
    </style>
    
    <div id="mainContentItem">
        <table align="center">
            <tr >
                <td width ="150px" align="left" class="text">Search By Category</td>
                <td width ="225px">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="225px" ForeColor="Red"
                        AutoPostBack="True" onselectedindexchanged="ddlCategory_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td width="100px" align="right" class="text">Sub Category</td>
                <td width="225px">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" Width="225px"
                        AutoPostBack="true" ForeColor="Red">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr><td colspan="4"></td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Bookid" 
                        DataSourceID="SearchByCategory" GroupItemCount="4">
                        <EmptyItemTemplate>
                            <td runat="server" />
                            </EmptyItemTemplate>
                        <ItemTemplate>
                                <td runat="server" style="background-color:#DCDCDC;color: #000000;">
                                    <div class="productItem">
                                        <a href = '<%# "ShowPages2.aspx?bookid="+Eval("bookid") %>' target ="_blank">
                                        <img class="preview"
                                            title='<%# Eval("name") %>'  alt=''
                                            src='<%# "../E-library/" + Eval("imgpath") %>' 
                                            height="150" width="150"  />
                                        </a>
                                        <div class="productName"><%# Eval("name") %></div>
                                       <%-- <div class="productDescription"><%# Eval("description") %></div>--%>
                                    </div>
                                </td>
                            </ItemTemplate>
                        <EmptyDataTemplate>
                                <table runat="server" 
                                    style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                    <tr>
                                        <td>
                                            No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                        <LayoutTemplate>
                            <table runat="server">
                                <tr runat="server">
                                    <td runat="server">
                                        <table ID="groupPlaceholderContainer" runat="server" border="1" 
                                            style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                            <tr ID="groupPlaceholder" runat="server">
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr runat="server">
                                    <td runat="server" 
                                        style="text-align: center;background-color: #CCCCCC;font-family: Verdana, Arial, Helvetica, sans-serif;color: #000000;">
                                        <asp:DataPager ID="DataPager1" runat="server" PageSize="12">
                                            <Fields>
                                                <asp:NumericPagerField />
                                            </Fields>
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
                </td>
            </tr>
        </table>
        
        <asp:SqlDataSource ID="SearchByCategory" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_GetBookListByCategory" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlSubCategory" Name="SubCategoryId" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>

    </div>
</asp:Content>

