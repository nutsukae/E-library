 <%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true" CodeFile="editBook.aspx.cs" Inherits="Admin_editBook" Title="E-Library : Edit Book" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContentTitle" Runat="Server">
     <div id="MainContentTitle">ระบบแก้ไขหนังสือ</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="mainContentItem">
        <table>
            <tr>
                <td valign="top">
                    <table>
                        <tr>
                            <td colspan ="2" align="center"><asp:Image ID="CoverImage" runat="server" 
                                Height="150px" Width="150px"  />
                            </td>
                        </tr>
                    </table>                
                </td>
                <td>
            <table style="text-align:center;">
            <tr>
                <td class="title">Bar Code :</td>
                <td class="input"><asp:TextBox ID="txtBarCode" runat="server" Width="200px" 
                        Enabled="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">ISBN :</td>
                <td class="input"><asp:TextBox ID="txtISBN" runat="server" Width="200px" 
                        Enabled="False"></asp:TextBox></td>
            </tr>
            
            <tr>
                <td class="title">Category :</td>
                <td class="input">
                      <asp:DropDownList ID="ddlCategory" runat="server" Width="200px" 
                          onselectedindexchanged="ddlCategory_SelectedIndexChanged" 
                          AutoPostBack="True">
                    </asp:DropDownList>
                     &nbsp;&nbsp;
                    <asp:LinkButton ID="btnAddCategory" runat="server"
                        OnClientClick="return false;">เพิ่ม</asp:LinkButton>
                    <cc1:PopupControlExtender ID="PopupControlExtender1" runat="server" 
                        PopupControlID="PanelCategory" TargetControlID="btnAddCategory">
                    </cc1:PopupControlExtender>
                 </td>
            </tr>
            <tr>
                <td class="title">Sub Category :</td>
                <td class="input">
                    <asp:DropDownList ID="ddlSubCategory" runat="server" Width="200px">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnSubCategory" runat="server"
                        OnClientClick="return false;">เพิ่ม</asp:LinkButton>
                    <cc1:PopupControlExtender ID="PopupControlExtender2" runat="server" 
                        PopupControlID="PanelSubCategory" TargetControlID="btnSubCategory">
                    </cc1:PopupControlExtender>
                 </td>
            </tr>
            <tr>
                <td class="title">ชื่อหนังสือ :</td>
                <td class="input"><asp:TextBox ID="txtName" runat="server" Width="300px" Rows="2" 
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="title">รายละเอียด</td>
                <td class="input"><asp:TextBox ID="txtDescription" Rows="5" 
                        TextMode="MultiLine" Width="300px" runat="server"></asp:TextBox></td>
                    
            </tr>    
            <tr>
                <td class="title">ชื่อผู้แต่ง :</td>
                <td class="input">
                    <asp:TextBox ID="txtAuthor" runat="server" Width="300px"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="AuthorAutoComplete"   
                        TargetControlID="txtAuthor"  runat="server" ServiceMethod="GetAuthorList" 
                        UseContextKey="True" EnableCaching ="true" MinimumPrefixLength ="1" CompletionInterval = "1000" />
               
                    &nbsp;&nbsp;
                    <asp:LinkButton ID="btnAddAuthor" runat="server"
                        OnClientClick="return false;">เพิ่ม</asp:LinkButton>
                    <cc1:PopupControlExtender ID="PopupControlExtender3" runat="server" 
                        PopupControlID="PanelAuthor" TargetControlID="btnAddAuthor">
                    </cc1:PopupControlExtender>
                    
                </td>
            </tr>
            <tr>
                <td class="title">ชื่อสำนักพิมพ์ :</td>
                <td class="input">
                    <asp:TextBox ID="txtPublisher" runat="server" Width="300px"></asp:TextBox>
                     <cc1:AutoCompleteExtender ID="PublisherAutoComplete"   
                        TargetControlID="txtPublisher"  runat="server" ServiceMethod="GetPublisherList" 
                        UseContextKey="True" EnableCaching ="true" MinimumPrefixLength ="1" CompletionInterval = "1000" />
                     &nbsp;&nbsp;
                    <asp:LinkButton ID="btnAddPublisher" runat="server"
                        OnClientClick="return false;">เพิ่ม</asp:LinkButton>
                    <cc1:PopupControlExtender ID="PopupControlExtender4" runat="server" 
                        PopupControlID="PanelPublisher" TargetControlID="btnAddPublisher">
                    </cc1:PopupControlExtender>
                </td>                
            </tr>
             <tr>
               <td class="title">เดือนที่พิมพ์ :</td>
               <td class="input">
                   <asp:DropDownList ID="ddlPrintMonth" runat="server" Width="100px" DataTextField="MonthName"
                        DataValueField = "MonthValue" >
                   </asp:DropDownList>
               </td>
            </tr>
             <tr>
               <td class="title">ปีที่พิมพ์ :</td>
               <td class="input">
                   <asp:DropDownList ID="ddlPrintYear" runat="server" Width="100px" DataTextField = "YearName"
                        DataValueField = "YearValue">
                   </asp:DropDownList>
               </td>
            </tr>
             <tr>
                <td class="title">รูปปกหนังสือ :</td>
                <td class="input">
                    <asp:FileUpload ID="PictureUploader" runat="server"  ForeColor="Red"/>
                    <asp:HiddenField ID="hidImgPath" runat="server" />
                 </td>                 
            </tr>
            <tr>
                <td class="title">Zip File :</td>
                <td class="input">
                    <asp:FileUpload ID="ZipUploader" runat="server" ForeColor="Red" />
                    <asp:HiddenField ID="hidZipPath" runat="server" />
                </td> 
            </tr>
            <tr>
                <td class="title">Pdf File :</td>
                <td class="input">
                    <asp:FileUpload ID="pdfUploader" runat="server" ForeColor="Red" />
                    <asp:HiddenField ID="hidPdfPath" runat="server" />
                </td> 
            </tr>
             <tr>
                <td class="title">หมายเหตุ :</td>
                <td class="input"><asp:TextBox ID="txtRemark" runat="server" Rows="3" 
                        TextMode="MultiLine" Width="250px"></asp:TextBox></td>
            </tr>
            <tr><td colspan="2">
                <asp:HiddenField ID="hidBookId" runat="server" />
            </td></tr>
            <tr>
                <td colspan="2" >
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        ValidationGroup="MyValidator" 
                        
                        OnClientClick = "return confirm('Are you sure you want to update this record?');" 
                        onclick="btnSubmit_Click1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnReset" runat="server" Text="Reset" onclick="btnReset_Click" 
                        style="height: 26px" />
                </td>
            </tr>
        </table>               
                </td>
            </tr>
        </table>
    </div>
    
    
    <asp:Panel ID="PanelCategory" runat="server">
        <table class="PanelCategory">
            <tr><td align="center" colspan="2">เพิ่ม Category</td></tr>
            <tr><td></td></tr>
            <tr>
                <td>ชื่อ Category : </td>
                <td><asp:TextBox ID="txtCategory" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
             <tr>
                <td align="right">หมายเหตุ</td>
                <td>  <asp:TextBox ID="txtCategoryRemark" runat="server" Width="200px" 
                        TextMode="MultiLine" Rows="3"></asp:TextBox> </td>    
          </tr>
            <tr><td></td></tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmitCategory" runat="server" Text="Submit" 
                        
                        OnClientClick="return confirm('Are you sure you want to Add this Category?');" 
                        onclick="btnSubmitCategory_Click1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancelCategory" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    <asp:Panel ID="PanelSubCategory" runat="server" onload="PanelSubCategory_Load">
        <table class="PanelCategory">
            <tr><td align="center" colspan="2">เพิ่ม SubCategory</td></tr>
            <tr><td></td></tr>
            <tr>
                <td align="right">ชื่อ Category : </td>
                <td><asp:Label ID="lblCategory" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td align="right">ชื่อ SubCategory :</td>
                <td><asp:TextBox ID="txtSubCategory" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            
             <tr>
                 <td align="right">หมายเหตุ</td>
                <td>  <asp:TextBox ID="txtSubCategoryRemark" runat="server" Width="200px" 
                        TextMode="MultiLine" Rows="3"></asp:TextBox> </td>    
            </tr>
            <tr><td></td></tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmitSubCategory" runat="server" Text="Submit" 
                        
                        OnClientClick="return confirm('Are you sure you want to Add this SubCategory?');" 
                        onclick="btnSubmitSubCategory_Click1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="brnCancelSubCategory" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    
     <asp:Panel ID="PanelAuthor" runat="server">
        <table class="PanelCategory">
            <tr><td align="center" colspan="2">เพิ่ม ผู้แต่ง</td></tr>
            <tr><td></td></tr>
            <tr>
                <td>ชื่อ ผู้แต่ง :</td>
                <td> <asp:TextBox ID="txtAddAuthor" runat="server" Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                 <td align="right">หมายเหตุ</td>
                <td>  <asp:TextBox ID="txtAuthorRemark" runat="server" Width="200px" 
                        TextMode="MultiLine" Rows="3"></asp:TextBox> </td>    
            </tr>
            <tr><td></td></tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmitAuthor" runat="server" Text="Submit" 
                        
                        OnClientClick="return confirm('Are you sure you want to Add this Author?');" 
                        onclick="btnSubmitAuthor_Click1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancerAuthor" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    
     <asp:Panel ID="PanelPublisher" runat="server">
        <table class="PanelCategory">
            <tr><td align="center" colspan="2">เพิ่ม สำนักพิมพ์</td></tr>
            <tr><td></td></tr>
            <tr>
                <td>ชื่อ สำนักพิมพ์ : </td>
                <td><asp:TextBox ID="txtAddPublisher" runat="server" Width="200px"></asp:TextBox> </td>
            </tr>
             <tr>
                 <td align="right">หมายเหตุ</td>
                <td>  <asp:TextBox ID="txtPublisherRemark" runat="server" Width="200px" 
                        TextMode="MultiLine" Rows="3"></asp:TextBox> </td>    
            </tr>
            <tr><td></td></tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Button ID="btnSubmitPublisher" runat="server" Text="Submit" 
                        
                        OnClientClick="return confirm('Are you sure you want to Add this Publisher?');" 
                        onclick="btnSubmitPublisher_Click1" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancerPublisher" runat="server" Text="Cancel" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    
    </div>
</asp:Content>

