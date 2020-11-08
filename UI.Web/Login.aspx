<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Login" MasterPageFile="~/AcademiaMaster.Master" %>

        <asp:Content runat="server" ContentPlaceHolderID="PageContent" ID="Login">
            <div>
                <table style="width:100%;">
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="Bienvenido al sistema!"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblUser" runat="server" Text="Usuario"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbUser" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblPass" runat="server" Text="Contraseña"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="tbPass" runat="server" TextMode="Password"></asp:TextBox>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Button ID="btnSubmit" runat="server" Text="Iniciar sesion" OnClick="btnSubmit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:LinkButton ID="lnkRecordarClave" runat="server" Text="Olvidé mi Clave" OnClick="lnkRecordarClave_Click"></asp:LinkButton>
                        </td>
                        <td class="auto-style1"></td>
                        <td class="auto-style1">&nbsp;</td>
                    </tr>
                </table>
            </div>
        </asp:Content>
