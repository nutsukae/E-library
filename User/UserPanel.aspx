<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserPanel.aspx.cs" Inherits="User_UserPanel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
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
                    <tr style="height:10%">
                        <td>
                            <table style="vertical-align:middle; width: 1024px;" align="center" >
                                <tr>
                                    <td>
                                        <table width="100%">
                                            <tr>
                                                <td width="224px"  style="height: 109px;" align="center">
                                                     <asp:Image ID="Image3" runat="server" ImageUrl="~/Image/logo.png" 
                                                Height="109px" Width="124px" />
                                                </td>
                                                <td align="right"style="height: 109px;">
                                                     <asp:Image ID="Image4" runat="server" ImageUrl="~/Image/e-library_logo.jpg" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr style="height:82%">
                        <td style="width:1024px; height:100%" align="center">
                            <asp:Button ID="btnPresentation" runat="server" Text="Presentation" 
                                onclick="btnPresentation_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnDownloadAllPages" runat="server" Text="Download All" 
                                onclick="btnDownloadAllPages_Click"/>
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
