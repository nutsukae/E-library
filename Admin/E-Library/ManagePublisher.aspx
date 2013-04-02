<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManagePublisher.aspx.cs" Inherits="Admin_E_Library_ManagePublisher" Title="E-Library : Manage Publisher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบจัดการ สำนักพิมพ์</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="mainContentItem" align="center">
        <table width="400px" style="text-align: center">
            <tr>
                <td class="CategoryManTitle">ชื่อ สำนักพิมพ์</td>
                <td align="left"><asp:TextBox ID="txtName" runat="server" Width="230px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CategoryManTitle">หมายเหตุ</td>
                <td><asp:TextBox ID="txtRemark" runat="server" Width="250px" Rows="3" 
                        TextMode="MultiLine"></asp:TextBox></td>
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
        <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
            AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" 
            BorderStyle="None" BorderWidth="1px" CellPadding="4" PageSize="10" 
            DataKeyNames="publisherid" DataSourceID="GetPublisherList" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged">
            <RowStyle BackColor="White" ForeColor="#003399" />
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Right" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
             <Columns>
                <asp:BoundField DataField="no" HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="name" HeaderText="Name">
                    <ItemStyle Width="340px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="Remark">
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Image" 
                    SelectImageUrl="~/Image/Button/btn_update.gif" ShowSelectButton="True" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="GetPublisherList" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_GetPublisherList" SelectCommandType="StoredProcedure">
        </asp:SqlDataSource>
    </div>
</asp:Content>

