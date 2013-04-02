<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageCategory.aspx.cs" Inherits="Admin_E_Library_CategoryManagement" Title="E-Library : Manage Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบจัดการ Category</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="mainContentItem" align="center">
        <table width="400px" style="text-align: center">
            <tr>
                <td class="CategoryManTitle">ชื่อ Category</td>
                <td align="left"><asp:TextBox ID="txtCategoryName" runat="server" Width="230px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CategoryManTitle">หมายเหตุ</td>
                <td><asp:TextBox ID="txtCategoryRemark" runat="server" Width="250px" Rows="3" 
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CategoryManTitle">การใช้งาน</td>
                <td align="left">
                    <asp:RadioButton ID="radUsed" runat="server" Text="ใช้" GroupName="UseCheck" 
                        ForeColor="White" />
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="radUnUsed" runat="server" Text="ไม่ใช้" ForeColor="White" 
                        GroupName="UseCheck" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="บันทึก" 
                        CssClass="SaveCancelButton" onclick="btnSave_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text="ยกเลิก" 
                        CssClass="SaveCancelButton" onclick="btnCancel_Click" />
                </td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" AllowPaging="True" PageSize="9" 
            DataSourceID="GetCategoryList" DataKeyNames="categoryid" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" 
            onpageindexchanging="GridView1_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#003399" />
            <Columns>
                <asp:BoundField DataField="no" HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="Name">
                    <ItemStyle Width="330px" />
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="Remark">
                    <ItemStyle Width="400px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Use">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("imgLink") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Image" 
                    SelectImageUrl="~/Image/Button/btn_update.gif" ShowSelectButton="True" />
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        </asp:GridView>
        <asp:SqlDataSource ID="GetCategoryList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_GetCategoryList" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
    </div>
</asp:Content>

