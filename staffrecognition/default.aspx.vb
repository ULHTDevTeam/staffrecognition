Imports System.Net.Mail
Imports System.IO
Imports System.DirectoryServices
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.Data.Entity.Core.Objects
Imports System.Data.Entity.Core
Imports System.Threading

Public Class _default1
    Inherits System.Web.UI.Page

    Private DomainController1 As String = ConfigurationSettings.AppSettings("DomainController1")
    Private DomainController2 As String = ConfigurationSettings.AppSettings("DomainController2")

    Dim blnInserted As Boolean = False

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If IsPostBack = False Then
            Session("username") = ""
            subGetSponsorDetails()
        End If

        If Session("messageoutput") <> "" Then
            MessageOutput.InnerHtml = Session("messageoutput")
            Session("messageoutput") = ""
            pnlForm.Visible = False
        End If

        If Session("messageoutput2") <> "" Then
            MessageOutput2.InnerHtml = Session("messageoutput2")
            Session("messageoutput2") = ""
            pnlForm.Visible = False
        End If

    End Sub

    Public Structure Data

        Public Sub New(ByVal intValue As Integer, ByVal strValue As String)
            IntegerData = intValue
            StringData = strValue
        End Sub

        Public Property IntegerData As Integer

        Public Property StringData As String
    End Structure

    Protected Sub btnCreateCertificate_Click(sender As Object, e As EventArgs) Handles btnCreateCertificate.Click
        If Page.IsValid Then
            Dim strCertificateType As String
            Dim strSubject As String
            Dim strLoggedOnUser As String
            Dim bolEmailSent As Boolean
            Dim bolExecutiveTeamConsideration As Boolean
            Dim intError As Integer
            Dim ImageFilePath
            strCertificateType = ""
            strSubject = ""
            strLoggedOnUser = Session("username")
            bolEmailSent = False
            ImageFilePath = ""
            intError = 0

            If chkExecutiveTeamConsideration.Checked Then
                bolExecutiveTeamConsideration = True
            Else
                bolExecutiveTeamConsideration = False
            End If

            If cmbTemplates.SelectedIndex <> -1 Then
                Select Case cmbTemplates.SelectedItem.Value
                    Case "Template1"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template1.png"
                        strSubject = "Template1 Subject"
                    Case "Template2"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template2.png"
                        strSubject = "Template2 Subject"
                    Case "Template3"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template3.png"
                        strSubject = "Template3 Subject"
                    Case "Template4"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template4.png"
                        strSubject = "Template4 Subject"
                    Case "Template5"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template5.png"
                        strSubject = "Template5 Subject"
                    Case "Template6"
                        ImageFilePath = Server.MapPath(".\") & ConfigurationManager.AppSettings("templatelocation") & "template6.png"
                        strSubject = "Template6 Subject"
                    Case Else
                        ImageFilePath = ""
                        strSubject = ""
                End Select
                strCertificateType = cmbTemplates.SelectedItem.Value
            End If

            If ImageFilePath = "" Then intError = 1
            If ImageFilePath Is Nothing Then intError = 1
            If strSubject = "" Then intError = 1

            If intError = 0 Then

                'create pdf on the fly
                Dim jpg As Image
                Dim doc As New Document(PageSize.A4.Rotate(), 0.0F, 0.0F, 0.0F, 0.0F)
                Dim memoryStream As New MemoryStream

                'write to pdf
                Dim writer As PdfWriter = PdfWriter.GetInstance(doc, memoryStream)
                Dim cb As PdfContentByte
                Dim normalFont As Font = New Font(Font.FontFamily.HELVETICA, 12)
                Dim boldFont As Font = New Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)

                jpg = Image.GetInstance(ImageFilePath)
                jpg.ScaleToFit(842, 595)
                jpg.Alignment = Image.UNDERLYING

                doc.Open()
                doc.NewPage()
                doc.Add(jpg)

                'add metadata to pdf
                'doc.AddHeader("Header", "Header Text")
                doc.AddAuthor(lblSenderName.Text)
                doc.AddTitle("Staff Recognition Certificate")
                doc.AddSubject(strSubject)
                doc.AddCreator("Creator")
                doc.AddKeywords("staff, recognition, certificate, template 1, template 2, template 3, template 4, template 5, template 6")

                'ct2.AddElement(rect)
                'Dim content As PdfContentByte = writer.DirectContent
                'content.SetColorStroke(New BaseColor(0, 0, 0))
                'content.Rectangle(0, 0, 50, 50)
                'content.Stroke()

                'Dim content As PdfContentByte = writer.DirectContent
                'Dim rect As Rectangle = New Rectangle(30, 660, 280, 80)
                'rect.setBorder(Rectangle.BOX)
                'rect.setborderwidth(20)
                'content.Rectangle(30, 660, 280, 80)
                'Dim bColor As BaseColor = New BaseColor(0, 0, 0)
                'content.SetColorFill(bColor)
                'content.Stroke()

                cb = writer.DirectContent

                Dim ct As ColumnText = New ColumnText(cb)
                Dim ct2 As ColumnText = New ColumnText(cb)
                Dim myText As Phrase = New Phrase()
                Dim myText2 As Phrase = New Phrase()
                Dim myText3 As Phrase = New Phrase()
                Dim myText4 As Phrase = New Phrase()

                Dim strDate As String
                strDate = deDate.Value
                strDate = CDate(strDate).ToString("dd-MMM-yyyy")

                pnlForm.Visible = False

                myText.Add(New Chunk(txtRecipientName.Text, normalFont))
                myText.Add(New Chunk(Environment.NewLine))
                myText.Add(New Chunk(Environment.NewLine))

                myText2.Add(New Chunk(txtMessage.Text, normalFont))
                myText2.Add(New Chunk(Environment.NewLine))
                myText2.Add(New Chunk(Environment.NewLine))

                myText3.Add(New Chunk(lblSenderName.Text, normalFont))
                myText3.Add(New Chunk(Environment.NewLine))
                myText3.Add(New Chunk(Environment.NewLine))

                myText4.Add(New Chunk(strDate, normalFont))
                myText4.Add(New Chunk(Environment.NewLine))
                myText4.Add(New Chunk(Environment.NewLine))

                ct.SetSimpleColumn(750, 450, 460, 220)  'set the x y width height (750,480,460,220)
                ct2.SetSimpleColumn(750, 220, 460, 120)
                ct.Alignment = Element.ALIGN_LEFT

                'myText4.Add(New BorderStyle())

                ct.AddText(myText)
                ct.AddText(myText2)
                ct2.AddText(myText3)
                ct2.AddText(myText4)

                'Dim rect As New Rectangle(200, 200, 100, 100)
                'rect.BorderWidth = 5
                'rect.BorderColor = New BaseColor(0, 0, 0)

                ct.Go()
                ct2.Go()

                writer.CloseStream = False
                doc.Close()
                memoryStream.Position = 0

                'send email
                Dim strMailTo As String
                Dim strMailFrom As String
                Dim strMailCC As String
                Dim sbBodyText As New StringBuilder
                Dim strIllegalCharacters As String = "[,/;\\]" '/;\\

                Try
                    'attach pdf to email and send email
                    sbBodyText.AppendFormat("<p>You have been sent a staff recognition certificate by {0}.</p>", lblSenderName.Text.Replace("<", "&#60;"))

                    strMailTo = txtRecipientEmailAddress.Text
                    strMailTo = Regex.Replace(strMailTo, strIllegalCharacters, "")
                    strMailFrom = lblSenderEmailAddress.Text
                    strMailFrom = Regex.Replace(strMailFrom, strIllegalCharacters, "")
                    strMailCC = txtRecipientManagerEmailAddress.Text
                    strMailCC = Regex.Replace(strMailCC, strIllegalCharacters, "")

                    Dim objEmail As New MailMessage()
                    objEmail.From = New MailAddress(strMailFrom)
                    objEmail.To.Add(strMailTo)
                    If strMailCC <> "" Then
                        objEmail.CC.Add(strMailCC)
                    End If

                    objEmail.Subject = strSubject
                    objEmail.Body = "<html><head>"
                    objEmail.Body += "<style type='text/css'> .displayheader { display: block; } .printheader { display: none; } .printed {color: #EEEEEE; font-size: 8pt; padding: 2px;}</style>"
                    objEmail.Body += "<style type='text/css' media='print'> .printheader { display: block; } body { margin: 0cm; color: #000000; }"
                    objEmail.Body += " thead.patientheader { display: table-header-group; } tfoot td.footernote { display: table-row-group; } </style></head><body>"
                    objEmail.Body += sbBodyText.ToString
                    objEmail.Body += "</body></html>"
                    objEmail.IsBodyHtml = True

                    Dim todayDate As Date = Date.Today()

                    objEmail.Attachments.Add(New Attachment(memoryStream, "certificate for " & txtRecipientName.Text.Replace("<", "&#60;") & " " & todayDate.ToString("dd-MMM-yyyy") & ".pdf"))

                    Dim SendReferralEmail As New SendMail : SendReferralEmail.SendEmail(objEmail) ' send email only if referral logged ok
                    Session("messageoutput") = "<p style='color: green'>Email sent successfully.</p>"
                    Session("messageoutput2") = "<p style='color: green'>Thank you for submitting the form. The certificate has been created and sent to " & txtRecipientName.Text & ".</p>"
                    bolEmailSent = True
                Catch ex As Exception
                    'btnCreateCertificate.Enabled = True
                    Session("messageoutput") = "<p style='color: red'>An Email error occurred. Please try again.</p><p><a href='default.aspx'>Create a certificate</a></p>" ' Problem sending email
                    Session("messageoutput2") = ""
                    'Response.Write(ex.ToString)
                    bolEmailSent = False
                End Try

                memoryStream.Position = 0   'have to reset position to use the pdf again

                If bolExecutiveTeamConsideration = True Then

                    Try
                        strMailTo = ConfigurationManager.AppSettings("ExecutiveTeamEmail")      'set in web config - review version goes to me. Live version goes to Jane Ablewhite
                        strMailFrom = lblSenderEmailAddress.Text
                        strMailFrom = Regex.Replace(strMailFrom, strIllegalCharacters, "")

                        Dim objEmail As New MailMessage()
                        objEmail.From = New MailAddress(strMailFrom)
                        objEmail.To.Add(strMailTo)

                        objEmail.Subject = "Staff recognition certificate for consideration by the executive team"
                        objEmail.Body = ""
                        objEmail.IsBodyHtml = True

                        Dim todayDate As Date = Date.Today()

                        objEmail.Attachments.Add(New Attachment(memoryStream, "certificate for " & txtRecipientName.Text.Replace("<", "&#60;") & " " & todayDate.ToString("dd-MMM-yyyy") & ".pdf"))

                        Dim SendReferralEmail As New SendMail : SendReferralEmail.SendEmail(objEmail) ' send email only if referral logged ok
                        Session("messageoutput2") += "<p style='color: green'>Your certificate has been forwarded to the executive team for their consideration.</p>"
                    Catch ex As Exception
                        Session("messageoutput2") += "<p style='color: red'>An email error occured. It was not possible to forward your certificate to the executive team for their consideration.</p>"
                    End Try

                End If

                If bolEmailSent = True Then
                    Session("messageoutput2") += "<p><a href='default.aspx'>Create another certificate</a></p>"
                End If

                memoryStream.Close()

                'log to database
                Using certificateentities = New StaffRecognition_Entities
                    Try
                        Dim certificate As New Certificate_Logging
                        certificate.RecipientName = txtRecipientName.Text
                        certificate.RecipientEmailAddress = txtRecipientEmailAddress.Text
                        certificate.RecipientManagerEmailAddress = txtRecipientManagerEmailAddress.Text
                        certificate.SenderName = lblSenderName.Text
                        certificate.SenderEmailAddress = lblSenderEmailAddress.Text
                        certificate.CertificateMessage = txtMessage.Text
                        certificate.CertificateDate = deDate.Value
                        certificate.CertificateType = strCertificateType
                        certificate.EmailSubject = strSubject
                        certificate.EmailBody = sbBodyText.ToString
                        certificate.EmailSent = bolEmailSent
                        certificate.ExecutiveTeamConsideration = bolExecutiveTeamConsideration
                        'certificate.EmailBody = 
                        certificate.LoggedOnUser = strLoggedOnUser

                        Dim result As Objects.ObjectParameter
                        result = New Objects.ObjectParameter("result", GetType(String))
                        Dim identity As Objects.ObjectParameter
                        identity = New Objects.ObjectParameter("new_identity", GetType(Integer))

                        identity.Value = certificateentities.InsertCertificateLog(certificate.RecipientName, certificate.RecipientEmailAddress, certificate.RecipientManagerEmailAddress, _
                                                                 certificate.SenderName, certificate.SenderEmailAddress, certificate.CertificateMessage, _
                                                                 certificate.CertificateDate, certificate.CertificateType, certificate.EmailSubject, _
                                                                 certificate.EmailBody, certificate.EmailSent, certificate.ExecutiveTeamConsideration, certificate.LoggedOnUser, result, identity).First
                        'If identity.Value IsNot Nothing Then
                        '    Response.Write("new identity value = " & identity.Value.ToString)
                        'End If

                        'If result.Value IsNot Nothing Then
                        '    Response.Write("result = " & result.Value.ToString)
                        'End If

                        'Response.Write("<p style='color: green'>record is saved</p>")
                    Catch ex As Exception
                        'btnCreateCertificate.Enabled = True
                        'Response.Write("<p style='color: red'>record is not saved</p>")
                        'Response.Write(ex.ToString)
                    End Try
                End Using

            End If
            'btnCreateCertificate.Enabled = True
            Response.Redirect("default.aspx")
        End If

    End Sub

    Sub subGetSponsorDetails()

        Dim strUserDomain, strUsername, strUsername1 As String

        strUsername = ""
        strUserDomain = ""
        strUsername1 = Request.ServerVariables("REMOTE_USER")
        '-------------------------------------------------------------------------
        ' Perform User Lookup
        '-------------------------------------------------------------------------
        Try
            If InStr(strUsername1, "\") > 0 Then
                strUserDomain = Left(strUsername1, InStr(strUsername1, "\") - 1)
                strUsername = Right(strUsername1, Len(strUsername1) - InStrRev(strUsername1, "\"))
            End If

            Dim strDomain As String
            Dim objRoot As System.DirectoryServices.DirectoryEntry
            Dim objSearcher As System.DirectoryServices.DirectorySearcher
            Dim objResult As SearchResult
            Dim User As System.DirectoryServices.DirectoryEntry

            Select Case (strUserDomain.ToUpper())
                Case "1" : strDomain = DomainController1
                Case Else : strDomain = DomainController2
            End Select

            objRoot = New System.DirectoryServices.DirectoryEntry(strDomain)
            objSearcher = New System.DirectoryServices.DirectorySearcher(objRoot)
            objSearcher.Filter = "(&(objectCategory=user)(sAMAccountName=" + strUsername + "))"
            objResult = objSearcher.FindOne()

            User = New System.DirectoryServices.DirectoryEntry(objResult.Path)

            lblSenderName.Text = User.Properties("givenName").Value + " " + User.Properties("sn").Value
            Session("username") = strUsername1
            lblSenderEmailAddress.Text = User.Properties("mail").Value
        Catch
            ' error collecting user details
            ' user will have to enter the sponsor details manually
        End Try
        '-------------------------------------------------------------------------

    End Sub

    Protected Sub DateCheck(ByVal sender As Object, ByVal args As ServerValidateEventArgs)
        If IsDate(CType(aspnetForm.FindControl(CType(sender, CustomValidator).ControlToValidate), TextBox).Text) Then
            args.IsValid = True
        Else
            args.IsValid = False
        End If
    End Sub

End Class