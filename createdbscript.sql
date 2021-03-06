USE [master]
GO
/****** Object:  Database [StaffRecognition]    Script Date: 01/05/2018 16:02:28 ******/
CREATE DATABASE [StaffRecognition] ON  PRIMARY 
( NAME = N'StaffRecognition', FILENAME = N'E:\SQL_Databases\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\StaffRecognition.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'StaffRecognition_log', FILENAME = N'E:\SQL_Databases\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\StaffRecognition_log.ldf' , SIZE = 76800KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [StaffRecognition] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [StaffRecognition].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [StaffRecognition] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [StaffRecognition] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [StaffRecognition] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [StaffRecognition] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [StaffRecognition] SET ARITHABORT OFF 
GO
ALTER DATABASE [StaffRecognition] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [StaffRecognition] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [StaffRecognition] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [StaffRecognition] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [StaffRecognition] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [StaffRecognition] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [StaffRecognition] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [StaffRecognition] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [StaffRecognition] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [StaffRecognition] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [StaffRecognition] SET  DISABLE_BROKER 
GO
ALTER DATABASE [StaffRecognition] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [StaffRecognition] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [StaffRecognition] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [StaffRecognition] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [StaffRecognition] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [StaffRecognition] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [StaffRecognition] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [StaffRecognition] SET RECOVERY FULL 
GO
ALTER DATABASE [StaffRecognition] SET  MULTI_USER 
GO
ALTER DATABASE [StaffRecognition] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [StaffRecognition] SET DB_CHAINING OFF 
GO
USE [StaffRecognition]
GO
/****** Object:  User [Web_User]    Script Date: 01/05/2018 16:02:28 ******/
CREATE USER [Web_User] FOR LOGIN [Web_User] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  StoredProcedure [dbo].[InsertCertificateLog]    Script Date: 01/05/2018 16:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: 7/2/18
-- Description: Logging certificate in database
-- =============================================
CREATE PROCEDURE [dbo].[InsertCertificateLog]
	@RecipientName varchar(100),
	@RecipientEmailAddress varchar(100),
	@RecipientManagerEmailAddress varchar(100),
	@SenderName varchar(100),
	@SenderEmailAddress varchar(100),
	@CertificateMessage varchar(500),
	@CertificateDate date,
	@CertificateType varchar(50),
	@EmailSubject varchar(100),
	@EmailBody text,
	@EmailSent bit,
	@ExecutiveTeamConsideration bit,
	@LoggedOnUser varchar(50),
	@result varchar(10) OUTPUT,
	@new_identity int OUTPUT
	AS
	BEGIN TRY
		INSERT INTO [StaffRecognition].[dbo].[Certificate_Logging]
			   ([RecipientName]
			   ,[RecipientEmailAddress]
			   ,[RecipientManagerEmailAddress]
			   ,[SenderName]
			   ,[SenderEmailAddress]
			   ,[CertificateMessage]
			   ,[CertificateDate]
			   ,[CertificateType]
			   ,[EmailSubject]
			   ,[EmailBody]
			   ,[EmailSent]
			   ,[ExecutiveTeamConsideration]
			   ,[DateCreated]
			   ,[LoggedOnUser])
		 VALUES
			   (@RecipientName
			   ,@RecipientEmailAddress
			   ,@RecipientManagerEmailAddress
			   ,@SenderName
			   ,@SenderEmailAddress
			   ,@CertificateMessage
			   ,@CertificateDate
			   ,@CertificateType
			   ,@EmailSubject
			   ,@EmailBody
			   ,@EmailSent
			   ,@ExecutiveTeamConsideration
			   ,GetDate()
			   ,@LoggedOnUser)
			   
		SET @new_identity = SCOPE_IDENTITY()
		SET @result = 'success'
	END TRY
	BEGIN CATCH
		 SET @result = 'failed'
		RETURN
	END CATCH
	--SET @new_identity = 1
	SELECT @new_identity, @result
 RETURN

GO
/****** Object:  Table [dbo].[Certificate_Logging]    Script Date: 01/05/2018 16:02:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Certificate_Logging](
	[Identifier] [int] IDENTITY(1,1) NOT NULL,
	[RecipientName] [varchar](100) NULL,
	[RecipientEmailAddress] [varchar](100) NULL,
	[RecipientManagerEmailAddress] [varchar](100) NULL,
	[SenderName] [varchar](100) NULL,
	[SenderEmailAddress] [varchar](100) NULL,
	[CertificateMessage] [varchar](500) NULL,
	[CertificateDate] [date] NULL,
	[CertificateType] [varchar](50) NULL,
	[EmailSubject] [varchar](100) NULL,
	[EmailBody] [text] NULL,
	[EmailSent] [bit] NULL,
	[ExecutiveTeamConsideration] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[LoggedOnUser] [varchar](50) NULL,
 CONSTRAINT [PK_Certificate_Logging] PRIMARY KEY CLUSTERED 
(
	[Identifier] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [StaffRecognition] SET  READ_WRITE 
GO
