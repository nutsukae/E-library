<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E-Library</title>
    <style type="text/css">
        html { height:100%;width:100%}
    </style>
    
</head>
<body style="margin: 0px; height: 100%; width: 100%; background-color:Black; ">
    <form id="form1" runat="server" style="height: 100%; width: 100%;">
    <table  style="width:100%; height:100%; ">
        <tr>
            <td>
                <table style="width:100%;height:100% ">
                    <tr>
                        <td>
                            <table style="vertical-align:middle;" align="center">
                                <tr>
                                    <td>
                                     <table style="vertical-align: middle">
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td><asp:Image ID="Image1" runat="server" ImageUrl="~/Image/logo.png" /></td>
                                                <td><asp:Image ID="Image2" runat="server" ImageUrl="~/Image/e-library_logo.jpg" /></td>
                                            </tr>
                                        </table>  
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left">
                                        <asp:Login ID="Login1" runat="server" BackColor="#E3EAEB" BorderColor="#E6E2D8" 
                                            BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" 
                                            FailureText="Username หรือ password ไม่ถูกต้องครับ" Font-Names="Tahoma" 
                                            Font-Size="Small" ForeColor="#333333" OnAuthenticate="Login1_Authenticate" 
                                            TextLayout="TextOnTop" Width="306px" 
                                            RememberMeText="จำ Password ไว้ในครั้งต่อไป">
                                            <TitleTextStyle BackColor="#1C5E55" Font-Bold="True" Font-Size="0.9em" 
                                                ForeColor="White" />
                                            <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
                                            <TextBoxStyle Font-Size="0.8em" Width="150" />
                                            <LoginButtonStyle BackColor="White" BorderColor="#C5BBAF" BorderStyle="Solid" 
                                                BorderWidth="1px" Font-Names="Verdana" Font-Size="0.8em" ForeColor="#1C5E55" />
                                        </asp:Login>
                                    </td>
                                </tr>
                            </table>
                                    </td> 
                                </tr>
                            </table>        
                        </td>
                    </tr>
                    <tr>
                        <td style="height:8%; color: #FFFFFF; font-family: Arial, Helvetica, sans-serif; font-size: medium;" 
                            align="center">
                            Interior Visions Co., Ltd.<br />
                            1000/157,160,163,166 Liberty Plaza Bldg.3 th Sukhumvit 55 (Thonglor) North Klongton, Wattana, Bangkok 10110, Thailand
                        </td>
                   </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
