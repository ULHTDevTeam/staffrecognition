<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/sitemasterpage.master" CodeBehind="default.aspx.vb" Inherits="StaffRecognition._default1" Theme="Skin1" EnableTheming="true" %>

<%@ Register Assembly="DevExpress.Web.v15.2, Version=15.2.10.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Staff Recognition Certificate <% = ConfigurationManager.AppSettings("AppTitleSuffix")%></h1>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="aspnetForm" runat="server">
    <script src="scripts/validation.js" type="text/javascript"></script>

        <div id="TestMessage" runat="server"></div>
        <div id="MessageOutput" runat="server"></div>
        <div id="MessageOutput2" runat="server"></div>

        <asp:Panel ID="pnlForm" runat="server">

            <p style="clear: both;">
                <dx:ASPxComboBox ID="cmbTemplates" runat="server" ValueType="System.String" Caption="Certificate template:" SkinID="ComboBoxNormal">
                    <items>
                        <dx:ListEditItem Value="Template1" />
                        <dx:ListEditItem Value="Template2" />
                        <dx:ListEditItem Value="Template3" />
                        <dx:ListEditItem Value="Template4" />
                        <dx:ListEditItem Value="Template5" />
                        <dx:ListEditItem Value="Template6" />
                    </items>
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText="Please select a certificate template" />
                    </ValidationSettings>
                </dx:ASPxComboBox>
            </p>

            <p style="clear: both;">
                <dx:ASPxCheckBox ID="chkExecutiveTeamConsideration" runat="server" Text="For executive team consideration" SkinID="CheckBoxNormal" />
            </p>

            <p style="clear: both;">
                <dx:ASPxTextBox ID="txtRecipientName" runat="server" MaxLength="44" Caption="Recipient name:" SkinID="TextBoxNormal">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText="Please tell us the recipient name" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </p>

            <p style="clear: both;">
                <dx:ASPxTextBox ID="txtRecipientEmailAddress" runat="server" MaxLength="100" Caption="Recipient email address:" SkinID="TextBoxNormal">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText="Please tell us the recipient email address" />
                        <RegularExpression ValidationExpression=".+\@.+\..+" ErrorText="Please input a valid email address" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </p>

            <p style="clear: both;">
                <dx:ASPxTextBox ID="txtRecipientManagerEmailAddress" runat="server" MaxLength="100" Caption="Recipient manager email address (optional):" SkinID="TextBoxNormal">
                    <ValidationSettings>
                        <RegularExpression ValidationExpression=".+\@.+\..+" ErrorText="Please input a valid email address" />
                    </ValidationSettings>
                </dx:ASPxTextBox>
            </p>

            <p style="clear: both;">
                <dx:ASPxMemo ID="txtMessage" runat="server" Caption="Message:" MaxLength="500" ClientInstanceName="memo" SkinID="MemoNormal">
                    <ClientSideEvents KeyUp="function(s, e) { return maxlength(s); }" />
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText="Please tell us the message you want to add to the certificate" />
                    </ValidationSettings>
                </dx:ASPxMemo>
            </p>

            <p style="clear: both;">
                <dx:ASPxTextBox ID="txtCounter" runat="server" Caption="&nbsp;" MaxLength="4" ReadOnly="true" ClientIDMode="Static" 
                    ClientInstanceName="txtCounter" Text="500" SkinID="TextBoxSmall"></dx:ASPxTextBox>
            </p>

            <p style="clear: both; padding-top: 5px; padding-bottom: 5px;">
                <dx:ASPxLabel ID="lblSenderNameHeader" runat="server" Text="Your name:" SkinID="LabelNormalHeader"></dx:ASPxLabel>
                <dx:ASPxLabel ID="lblSenderName" runat="server" SkinID="LabelNormal"></dx:ASPxLabel>
            </p>

            <p style="clear: both; padding-top: 5px; padding-bottom: 5px;">
                <dx:ASPxLabel ID="lblSenderEmailAddressHeader" runat="server" Text="Your email address:" SkinID="LabelNormalHeader"></dx:ASPxLabel>
                <dx:ASPxLabel ID="lblSenderEmailAddress" runat="server" SkinID="LabelNormal"></dx:ASPxLabel>
            </p>

            <p style="clear: both;">
                <dx:ASPxDateEdit ID="deDate" runat="server" NullText="dd/MM/yyyy" EditFormatString="dd/MM/yyyy" EditFormat="custom" DisplayFormatString="dd/MM/yyyy" Caption="Date:" SkinID="DateEditNormal">
                    <ValidationSettings>
                        <RequiredField IsRequired="true" ErrorText="Please tell us the date you want to put on the certificate" />
                    </ValidationSettings>
                </dx:ASPxDateEdit>
            </p>

            <p style="clear: both;">
                <dx:ASPxButton ID="btnCreateCertificate" runat="server" Text="Create certificate" SkinID="ButtonNormal" DisabledStyle-CssClass="dxeDisabled_Glass" 
                    ClientIDMode="Static" ClientInstanceName="btnCreateCertificate">
                    <ClientSideEvents Click="OnClick" />
                </dx:ASPxButton>
            </p>

        </asp:Panel>
            
    </form>

</asp:Content>
