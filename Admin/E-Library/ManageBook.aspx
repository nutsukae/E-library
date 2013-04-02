<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="ManageBook.aspx.cs" Inherits="Admin_Book_Management" Title="E-Library : Manage Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
    <div id="MainContentTitle">ระบบจัดการหนังสือ</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class ="mainContentItem">
        <table class="tbFullWidth">
            <%--<tr>
                <td width="12%" 
                    style="font-family: Arial, Helvetica, sans-serif; font-size: small; color: #FFFFFF" >Print Month</td>
                <td width="15%">
                    <asp:DropDownList ID="ddlPrintMonth" runat="server" Width="100px" DataTextField="MonthName"
                        DataValueField = "MonthValue" >
                   </asp:DropDownList>
                </td>
                <td width="10%" style="font-family: Arial, Helvetica, sans-serif; font-size: small; color: #FFFFFF" >Print Year</td>
                <td width="15%">
                   <asp:DropDownList ID="ddlPrintYear" runat="server" Width="100px" DataTextField = "YearName"
                        DataValueField = "YearValue">
                   </asp:DropDownList>
               </td>
               <td width="8%"  style="font-family: Arial, Helvetica, sans-serif; font-size: small; color: #FFFFFF" >Sort By</td>
               <td width="10%">
                   <asp:DropDownList ID="ddlSearchSort" runat="server" DataTextField ="SortBy"
                        DataValueField ="Value">
                   </asp:DropDownList>
               </td>
               <td style="font-family: Arial, Helvetica, sans-serif; font-size: small; color: #FFFFFF" >Other</td>
               <td>
                   <asp:DropDownList ID="ddlOther" runat="server" DataTextField ="SearchOtherOption"
                        DataValueField="Value">
                   </asp:DropDownList>
               </td>
               <td><asp:Button ID="btnSearch" runat="server" Text="Search" 
                       onclick="btnSearch_Click" /></td>
            </tr>--%>
            <tr>
                <td>
                    <table>
                         <tr>
                            <td class="WhiteText">
                                <asp:RadioButton ID="radSearchByKeyword" runat="server" Text="ค้นหาด้วยชื่อหนังสือ" 
                                        Width="150px" GroupName="Search"/>
                            </td>
                            <td colspan="3"><asp:TextBox ID="txtKeyword" runat="server" Width="400px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="WhiteText">
                                <asp:RadioButton ID="radSearchByCategory" runat="server" Text="ค้นหาด้วย Category" 
                                        Width="150px" GroupName="Search" Checked="true" />
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCategory" runat="server" Width="250px" 
                                    AutoPostBack="True" 
                                    onselectedindexchanged="ddlCategory_SelectedIndexChanged" ></asp:DropDownList>
                            </td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlSubCategory" runat="server" Width="250px" ></asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </td>
                <td align="center">
                    <asp:Button ID="btnSearch" runat="server" Text="ค้นหา" Font-Size="Medium" 
                        onclick="btnSearch_Click" />
                </td>
            </tr>
            <tr style="height:20px;"><td colspan="5"><asp:Label ID="lblSQL" runat="server" Text="" Visible ="false"></asp:Label></td></tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
            BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" 
            CellPadding="4" onrowdeleting="GridView1_RowDeleting" 
            onselectedindexchanging="GridView1_SelectedIndexChanging" 
            AllowPaging="True" PageSize="10" EmptyDataText="ไม่พบข้อมูลที่กำลังค้นหา" 
            DataKeyNames="bookid" onpageindexchanging="GridView1_PageIndexChanging">
            <RowStyle BackColor="White" ForeColor="#003399" CssClass="GridViewItem" />
            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <Columns>
                <asp:BoundField DataField="no" HeaderText="No.">
                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                </asp:BoundField>
                <asp:BoundField DataField="isbn" HeaderText="isbn"  >
                    <ItemStyle Width="100px" />
                </asp:BoundField>
                <asp:BoundField DataField="book_name" HeaderText="Book Name" >
                    <ItemStyle Width="600px" HorizontalAlign="Left"  />
                </asp:BoundField>
                <asp:ImageField DataImageUrlField="image" HeaderText="Cover">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:ImageField>
                <asp:ImageField DataImageUrlField="zip" HeaderText="ZIP"></asp:ImageField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton2" runat="server"
                            ImageUrl="~/Image/Button/btn_update.gif" CommandName="Select"
                            CommandArgument='<%# Eval("isbn") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" 
                            ImageUrl="~/Image/Button/btn_del.gif" CommandName="Delete" 
                            CommandArgument='<%# Eval("isbn") %>' OnClientClick="return confirm('Are you sure you want to delete this record?');" />
                    </ItemTemplate>
                </asp:TemplateField>
                
               
                
            </Columns>
            <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
            <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Right" 
                VerticalAlign="Top" />
            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" 
                CssClass="GridViewHeader" />
        </asp:GridView>
    </div>
   
</asp:Content>

