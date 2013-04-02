<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageSubCategory.aspx.cs" Inherits="Admin_E_Library_ManageSubCategory" Title="E-Library : Manage Subcategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบจัดการ Sub Category</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="mainContentItem" align="center">
        <table width="400px">
            <tr>
                <td class="CategoryManTitle">เลือก Category</td>
                <td align ="left">
                    <asp:DropDownList ID="ddlCategory" runat="server" AutoPostBack="True" 
                        Width="250px" DataSourceID="GetAllCategory" DataTextField="name" 
                        DataValueField="CategoryId" >
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="GetAllCategory" runat="server" 
                        ConnectionString="<%$ ConnectionStrings:connection %>" 
                        SelectCommand="SELECT [CategoryId], [name] FROM [mst_Category]">
                    </asp:SqlDataSource>
                </td>
            </tr>
            <tr><td colspan = "2">&nbsp;</td></tr>
        </table>
        <table width="400px" style="text-align: center">
            <tr>
                <td class="CategoryManTitle">ชื่อ Sub Category</td>
                <td align="left"><asp:TextBox ID="txtSubCategoryName" runat="server" Width="230px"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CategoryManTitle">หมายเหตุ</td>
                <td><asp:TextBox ID="txtSubCategoryRemark" runat="server" Width="250px" Rows="3" 
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="CategoryManTitle">การใช้งาน</td>
                <td align="left">
                    <asp:RadioButton ID="radUsed" runat="server" Text="ใช้" GroupName="UseCheck" ForeColor="White" />
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
         <asp:GridView ID="GridView1" runat="server" BackColor="White" 
            BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            AutoGenerateColumns="False" AllowPaging="True" 
            DataKeyNames="subcategoryid" DataSourceID="GetSubCategoryByCategoryId" 
            onpageindexchanging="GridView1_PageIndexChanging" 
            onselectedindexchanged="GridView1_SelectedIndexChanged" PageSize="8">
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
                    <ItemStyle Width="330px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:BoundField DataField="remark" HeaderText="Remark">
                    <ItemStyle Width="400px" HorizontalAlign="Left" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Use">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" ImageUrl='<%#Eval("imgLink") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ButtonType="Image" 
                    SelectImageUrl="~/Image/Button/btn_update.gif" ShowSelectButton="True" />
            </Columns>
         </asp:GridView>
        <asp:SqlDataSource ID="GetSubCategoryByCategoryId" runat="server" 
            ConnectionString="<%$ ConnectionStrings:connection %>" 
            SelectCommand="sp_GetSubCategoryList" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="ddlCategory" Name="categoryid" 
                    PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>        
</asp:Content>

