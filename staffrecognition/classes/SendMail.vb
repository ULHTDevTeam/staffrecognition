Imports System.Net.Mail

Public Class SendMail

    Public Function SendEmail(ByVal ThisMailMessage As MailMessage) As Boolean
        'Dim sendresult As Boolean
        'Dim sendexception As Exception

        ' if no mail message supplied then return false
        If ThisMailMessage Is Nothing Then
            'SendResult = False
            'Return SendResult
            Throw New System.Exception("No MailMessage")    '20160822 Throw an exception for the parent page to deal with
        End If

        ' Otherwise, set up mail client and attempt to send email
        Dim smtphost As String = ConfigurationManager.AppSettings("smtphost")
        Dim mSmtpClient As New SmtpClient(smtphost)

        'mSmtpClient.Port = "25"
        mSmtpClient.Send(ThisMailMessage)                   '20160822 No TryCatch as errors are currently caught in parent page
        Return True                                         '20160822 Return Boolean - which is currently ignored by the parent page

        'Try
        '    mSmtpClient.Send(ThisMailMessage)
        '    SendResult = True

        'Catch ex As Exception

        '    SendResult = False
        '    SendException = ex
        'End Try

        'Return SendResult

    End Function

    Public Function SendEmail2(ByVal ThisMailMessage As MailMessage) As Boolean 'copy of above function BUT "smtphost" has been changed

        ' if no mail message supplied then return false
        If ThisMailMessage Is Nothing Then
            'SendResult = False
            'Return SendResult
            Throw New System.Exception("No MailMessage")
        End If

        ' Otherwise, set up mail client and attempt to send email
        Dim smtphost As String = "SMTPHOST GOES HERE"
        Dim mSmtpClient As New SmtpClient(smtphost)

        mSmtpClient.Send(ThisMailMessage)
        Return True

        'Try
        '    mSmtpClient.Send(ThisMailMessage)
        '    SendResult = True

        'Catch ex As Exception

        '    SendResult = False
        '    SendException = ex

        'End Try

        'Return SendResult

    End Function

End Class
