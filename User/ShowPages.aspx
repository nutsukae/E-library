<%@ Page Language="C#" MasterPageFile="~/User/User.master" AutoEventWireup="true" CodeFile="ShowPages.aspx.cs" Inherits="User_ShowPages" Title="E-Library : Show Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบแสดงหน้าในหนังสือ</div>
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
            font-weight:bold;
        }
        
        .titleBar
        {
            font-size:small;
            font-weight:bold;
            color:Red;
        }
        
        .selectAll
        {
        	font-size:small;
            font-weight:bold;
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
        <table width="100%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td  align="right" width="460px" class="titleBar">
                                กดที่รูปถ้าต้องการดูรูปขนาดใหญ่
                            </td>
                            <td align="right">
                                <asp:CheckBox ID="cbxSelectAll" runat="server" ForeColor="White"
                                    Text="Selected All This Pages" AutoPostBack="True" 
                                    oncheckedchanged="cbxSelectAll_CheckedChanged" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:ListView ID="ListView1" runat="server" DataKeyNames="Bookid" 
                        DataSourceID="GetPages" GroupItemCount="4" 
                        onpagepropertieschanging="ListView1_PagePropertiesChanging" 
                        onprerender="ListView1_PreRender">
                        <EmptyItemTemplate>
                            <td id="Td1" runat="server" />
                            </EmptyItemTemplate>
                                <ItemTemplate>
                                <td id="Td2" runat="server" style="background-color:#DCDCDC;color: #000000;">
                                    <div class="productItem">
                                        <a href ='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %>' 
                                            class="MagicThumb" rel="group: default-speed; slideshow-effect: expand;">
                                        <img src='<%# "../E-library/" + Eval("bookid") + "/pages/" + Eval("filename") %>' 
                                            alt=''
                                            height="150" width="150"  />
                                        </a>    
                                        <asp:CheckBox ID="cbxSelectFile" runat="server" Text="Select"
                                            Key='<%# Eval("filename") %>' CssClass="productName"/>
                                        
                                            &nbsp;&nbsp;&nbsp;
                                        <a href = '<%# "showpages.aspx?command=0&bookid=" + Eval("bookid") + "&filename=" + Eval("filename") %>' 
                                            target ='_blank' >
                                            <img src="../Image/Icon/Save.jpg" alt='' /></a>
                                        <asp:Label ID="Label1" runat="server" Text="Save" CssClass="productName"></asp:Label>
                                        
                                    </div>
                                </td>
                            </ItemTemplate>
                        <EmptyDataTemplate>
                                <table id="Table1" runat="server" 
                                    style="background-color: #FFFFFF;border-collapse: collapse;border-color: #999999;border-style:none;border-width:1px;">
                                    <tr>
                                        <td align="center">no pages found for this book.</td>
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
                                        <asp:DataPager ID="DataPager1" runat="server" PageSize="16">
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
                        <asp:SqlDataSource ID="GetPages" runat="server" 
                            ConnectionString="<%$ ConnectionStrings:connection %>" 
                            SelectCommand="sp_GetPages" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="bookid" QueryStringField="bookid" 
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                </td>
            </tr>
            <tr>
                <td valign="middle">
                    <asp:Button ID="BtnDownloadFile" runat="server" Text="Get Download Link" 
                        onclick="BtnDownloadFile_Click" />
                        &nbsp; &nbsp;
                    <asp:HyperLink ID="DownloadLnk" runat="server" Target="_blank" Visible="False">Download Link</asp:HyperLink>
                </td>
            </tr>
        </table>
      </div>
</asp:Content>

