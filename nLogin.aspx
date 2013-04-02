<%@ Page Language="C#" AutoEventWireup="true" CodeFile="nLogin.aspx.cs" Inherits="nLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Library : Login</title>
    <link rel ="Stylesheet" type="text/css" href="Css/Login.css" />
</head>
<body style="margin: 0px; height: 100%; width: 100%;">
    <form id="form1" runat="server" style="height: 100%; width: 100%;">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table style="width:100%;height:100%;">
            <tr>
                <td>
                    <table id="Web_bg" style="width:100%;height:100%; ">
                    <tr>
                        <td style="background-position: center center; background-image: url('Image/Graphic/login_BG.png'); background-repeat:no-repeat; background-attachment: scroll;">
                            <table id="BG" style="width:385px;height:220px; vertical-align:middle; /*background-image: url('Image/Graphic/login_panel_bg.png'); background-repeat: no-repeat;*/" 
                                align="center">
                                <tr>
                                <td id="Left">
                                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Image/Logo/LogInLogo.jpg" /></td>
                                <td id="Right">
                                    <table style="width:100%;height:100%;text-align:left;">
                                        <tr>
                                            <td>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                                                    TargetControlID="txtUserName" WatermarkText="User Name" 
                                                    WatermarkCssClass="Watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <asp:TextBox ID="txtUserName" runat="server" BorderStyle="None"></asp:TextBox>
                                             </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                                                    TargetControlID="txtPassword" WatermarkText="Password" 
                                                    WatermarkCssClass="Watermark">
                                                </cc1:TextBoxWatermarkExtender>
                                                <asp:TextBox ID="txtPassword" runat="server" BorderStyle="None" 
                                                    onclick="this.value='';this.type='password';" onfocus="this.value='';this.type='password';"></asp:TextBox>
                                             </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="btnLogin" runat="server" 
                                                    ImageUrl="~/Image/Button/login.png" onclick="btnLogin_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkRememberMe" runat="server" Font-Bold="False" 
                                                    Font-Names="Calibri" Font-Size="Medium" Font-Strikeout="False" 
                                                    Text="Remember Me" />
                                            </td>
                                        </tr>
                                         <tr>
                                            <td>
                                                <asp:Label ID="lblWarning" runat="server" Text=" ** Password Invalid **"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            </table>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
