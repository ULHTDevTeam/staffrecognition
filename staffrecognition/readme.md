Staff Recognition Certificate Application

1. System Requirements

What you will need to run this application:
- Microsoft Visual Studio 2013 or later version
- DevExpress v15.2
- iTextSharp 5.5.10
- SQL Server 2012 or later version
- Internet Explorer 11 or similar browser

2. Changes to make before running the application:

In web config file add:
smtphost value
ExecutiveTeamEmail key value
Connection strings

In default.aspx.vb add:
domain controllers

In SendMail.vb add:
smtphost value

Add references for:
DevExpress
EntityFramework
iTextSharp

3. Running the application

Create the SQL server database on a local or remote server. Open the application in Visual Studio. Make sure the application connects to the database (check connection strings and open edmx file). Create a publish profile as appropriate. Publish the application to your chosen destination. The server needs to be capable of running v4.0 of the .NET framework and will need to have the DevExpress tools installed.

4. Summary of application

The user fills in the form, selects a certificate template and clicks the create button. The certificate is created, an email is sent to the recipient (manager cc'd in if appropriate) with the certificate as an attachment. A copy of the email is sent to the executive team (if appropriate). The details of the created certificate are logged in a database.

5. Further details on application

a. iTextSharp

A new pdf document is created. A background image is added to the pdf document based on the template selection. Meta data is added to the pdf document such as author, title, subject, creator and keywords. The form data is added to the pdf document as text columns and positioned appropriately. The form fields displayed on the certificate are recipient name, message, sender name and date. The text area size and position are the same for all the certificates. The document is then closed and the memorystream position is set to 0 so that it is at the beginning of the pdf. The document is then attached to the emails. The document is held in memory and not saved anywhere. When the memory stream is closed the document no longer exists.

b. Database

The database consists of one table for logging the certificate information and a stored procedure for inserting the information into the table. The application connects to the database through the entity framework.

c. Active Directory

The name, username and email address is taken from active directory for the logged in user and displayed as labels on the form. The certificate is shown to come from the logged in user.

d. DevExpress

All the form fields are DevExpress controls. Skins are used to style the controls. The built in validation for the controls are used. Some examples of the controls used on the form are Combo Boxes, Checkboxes, Textboxes.

6. Possible future developments

The information for the certificate templates could be held in a new table within the database. The information held on the templates could include name, location, size, type, text area position, text area size. The template names in the database could be used to populate the template drop down list on the form. The code behind would retrieve the information on the selected template from the database and use this to determine how to create the pdf document.

There could be an employee look up functionality on the form for selecting the recipient and auto populating their name and email address. This would reduce the risk of the user typing in the information incorrectly.

The user could be able to preview the certificate (as a link or embedded into the web page) and given the option to edit the information they have entered before choosing to send the certificate.

7. Known issues/bugs

If the user types in an invalid email address for the recipient then they will receive an undeliverable email message.

If the user types in the incorrect email address for the recipient then it will be sent to the wrong person (assuming the email address exists).

The web page doesn't check if the user details were successfully captured from active directory. When submitting the form without the user details it will produce an error.

The error message is very generic and doesn't help to identify what the problem is.



