<%@ Page Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="SearchByKeyword.aspx.cs" Inherits="User_SearchByKeyword" Title="E-Library : Search by Keyword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบค้นหาหนังสือโดยใช้ Keyword</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript" language="javascript">
        function CheckEnter(event)
        {
            var keyCode = event.keyCode ? event.keyCode : event.which ? event.which : event.charCode;
            if (keyCode == 13)
            {
                return false;
            }
            else 
            {
                return true;
            }
        }
    </script>
    
    <style type="text/css">
        .productItem
        {
            width: 150px;
            float: left;
            padding: 5px;
            margin: 5px;
            text-align: center;
        }
        .productName
        {
            font-size:x-small;
            display:block ;
            width:150px;
            height:30px;
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
        <table width="784px">
            <tr>
                <td width="150px" align="right" class="text" >Search By Keyword</td>
                <td width="100px">
                    <asp:DropDownList ID="ddlSearchBy" runat="server" ForeColor="Red" >
                        <asp:ListItem Value="0">SearchBy</asp:ListItem>
                        <asp:ListItem Value="1">ISBN</asp:ListItem>
                        <asp:ListItem Value="2">Barcode</asp:ListItem>
                        <asp:ListItem Value="3">ชื่อหนังสือ</asp:ListItem>
                        <asp:ListItem Value="4">ชื่อผู้แต่ง</asp:ListItem>
                        <asp:ListItem Value="5">ชื่อสำนักพิมพ์</asp:ListItem>
                        <asp:ListItem Value="6">คำอธิบาย</asp:ListItem>
                        <asp:ListItem Value="7">หมายเหตุ</asp:ListItem>
                    </asp:DropDownList>
               </td>
               <td width="70px" class="text" align="right">KeyWord</td>
               <td width="200px"><asp:TextBox ID="txtSearch" runat="server" Width = "230px"></asp:TextBox></td>
               <td width="70px" class="text" align="right">Sort By</td>
               <td width="80px">
                   <asp:DropDownList ID="ddlSortBy" runat="server" ForeColor="Red">
                       <%--<asp:ListItem Value="0">---------</asp:ListItem>--%>
                       <asp:ListItem Value="1">Newest</asp:ListItem>
                       <asp:ListItem Value="2">Oldest</asp:ListItem>
                   </asp:DropDownList>
               </td>
               <td><asp:Button ID="btnSearch" runat="server" Text="ค้นหา" 
                       onclick="btnSearch_Click" /></td>
            </tr>
            <tr><td colspan="7"></td></tr>
            <tr>
                <td colspan="7" align="center">
                    <asp:ListView ID="ListView1" runat="server" GroupItemCount="4">
                     <EmptyItemTemplate>
                            <td id="Td1" runat="server" />
                            </EmptyItemTemplate>
                     <ItemTemplate>
                                <td id="Td2" runat="server" style="background-color:#DCDCDC;color: #000000;">
                                    <div class="productItem">
                                        <a href = '<%# "ShowPages2.aspx?bookid="+Eval("bookid") %>' target ="_blank">
                                            <img title='<%# Eval("name") %>'  alt='' src='<%# "../E-library/" + Eval("imgpath") %>' 
                                            height="150" width="150"  />
                                        </a>
                                        <div class="productName"><%# Eval("name") %></div>
                                    </div>
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="Table1" runat="server" 
                                    style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;" align="center">
                                    <tr>
                                        <td align="center">No data was returned.</td>
                                    </tr>
                                </table>
                            </EmptyDataTemplate>
                            <LayoutTemplate>
                                <table id="Table2" runat="server">
                                    <tr id="Tr1" runat="server">
                                        <td id="Td3" runat="server">
                                            <table ID="groupPlaceholderContainer" runat="server" border="1" 
                                                style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;font-family: Verdana, Arial, Helvetica, sans-serif;">
                                                <tr ID="groupPlaceholder" runat="server">
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="Tr2" runat="server">
                                        <td id="Td4" runat="server" 
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
                     <SelectedItemTemplate></SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="SearchKeyword" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connection %>" 
                        SelectCommand="sp_GetBookListByKeyword" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="ddlSearchBy" Name="SearchBy" 
                                PropertyName="SelectedValue" Type="Int32" DefaultValue="3" />
                            <asp:ControlParameter ControlID="txtSearch" Name="keyword" PropertyName="Text" 
                                Type="String" DefaultValue="" />
                            <asp:ControlParameter ControlID="ddlSortBy" Name="SortBy" 
                                PropertyName="SelectedValue" Type="Int32"  DefaultValue="1"/>
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

