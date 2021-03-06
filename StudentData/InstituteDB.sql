USE [master]
GO
/****** Object:  Database [Institute]    Script Date: 2018/08/09 1:02:20 PM ******/
CREATE DATABASE [Institute]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Institute', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Institute.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Institute_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\Institute_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Institute] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Institute].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Institute] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Institute] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Institute] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Institute] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Institute] SET ARITHABORT OFF 
GO
ALTER DATABASE [Institute] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Institute] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Institute] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Institute] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Institute] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Institute] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Institute] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Institute] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Institute] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Institute] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Institute] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Institute] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Institute] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Institute] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Institute] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Institute] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Institute] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Institute] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Institute] SET RECOVERY FULL 
GO
ALTER DATABASE [Institute] SET  MULTI_USER 
GO
ALTER DATABASE [Institute] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Institute] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Institute] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Institute] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Institute', N'ON'
GO
USE [Institute]
GO
/****** Object:  StoredProcedure [dbo].[sp_test_print_out]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[sp_test_print_out]
@printMessages varchar(max) output
as
begin
set @printMessages='Test'
Print 'Test';

set @printMessages= @printMessages + CHAR(10)
set @printMessages= @printMessages + 'Test 1'
print 'Test 1';
end

GO
/****** Object:  StoredProcedure [dbo].[sp_test_print_out_to_select]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create procedure [dbo].[sp_test_print_out_to_select]
as 
begin
declare @printOut varchar(max)
exec sp_test_print_out @printOut output -- can be achieved using output parameter ?
select @printOut
end


GO
/****** Object:  StoredProcedure [dbo].[test_NewGroupMembers]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[test_NewGroupMembers]-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [PersonNo]
      ,[PersonTypeNo]
      ,[IdTypeNo]
      ,[IdNo]
      ,[Name]
      ,[Surname]
      ,[GenderNo]
      ,[DOB]
      ,[SystemDate]
  FROM [Institute].[dbo].[Person]

SELECT g.GroupDesc,p.PersonNo,gm.GroupMemberPersonNo,* 
	FROM dbo.GroupMember gm
	INNER JOIN dbo.[Group] g ON g.GroupNo = gm.GroupNo
	LEFT JOIN dbo.Person p ON p.PersonNo = gm.GroupMemberPersonNo
END

GO
/****** Object:  Table [dbo].[Gender]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderNo] [int] NOT NULL,
	[GenderDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Group]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[GroupNo] [int] IDENTITY(1,1) NOT NULL,
	[GroupTypeNo] [int] NOT NULL,
	[GroupPersonNo] [int] NOT NULL,
	[GroupDesc] [nvarchar](100) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[SystemDate] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[GroupNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupMember]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMember](
	[GroupMemberNo] [int] IDENTITY(1,1) NOT NULL,
	[GroupMemberPersonNo] [int] NOT NULL,
	[GroupNo] [int] NOT NULL,
	[SystemDate] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_GroupMember] PRIMARY KEY CLUSTERED 
(
	[GroupMemberNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupPersonType]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPersonType](
	[GroupPersonTypeNo] [int] NOT NULL,
	[GroupPersonTypeDesc] [nvarchar](50) NULL,
	[SystemDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupType]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupType](
	[GroupTypeNo] [int] NOT NULL,
	[GroupTypeDesc] [nvarchar](50) NULL,
	[SystemDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_GroupType] PRIMARY KEY CLUSTERED 
(
	[GroupTypeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[IDType]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IDType](
	[IDTypeNo] [int] NOT NULL,
	[IDTypeDesc] [nvarchar](50) NULL,
 CONSTRAINT [PK_IDType] PRIMARY KEY CLUSTERED 
(
	[IDTypeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Person]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[PersonNo] [int] IDENTITY(1,1) NOT NULL,
	[PersonTypeNo] [int] NOT NULL,
	[IdTypeNo] [int] NOT NULL,
	[IdNo] [nvarchar](20) NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[GenderNo] [int] NOT NULL,
	[DOB] [datetime] NOT NULL,
	[SystemDate] [datetime] NULL DEFAULT (getdate()),
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[PersonNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PersonType]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PersonType](
	[PersonTypeNo] [int] NOT NULL,
	[PersonTypeDesc] [nvarchar](50) NULL,
	[SystemDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_PersonType] PRIMARY KEY CLUSTERED 
(
	[PersonTypeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserNo] [nchar](10) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserPassword] [nvarchar](50) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Work]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Work](
	[WorkNo] [int] NOT NULL,
	[WorkDesc] [nvarchar](50) NULL,
	[WorkTypeNo] [int] NOT NULL,
	[GroupNo] [int] NOT NULL,
	[PersonNo] [int] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[SystemDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Work] PRIMARY KEY CLUSTERED 
(
	[WorkNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WorkType]    Script Date: 2018/08/09 1:02:21 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkType](
	[WorkTypeNo] [int] NOT NULL,
	[WorkTypeDesc] [nvarchar](50) NULL,
	[SystemDate] [datetime] NULL,
 CONSTRAINT [PK_WorkType] PRIMARY KEY CLUSTERED 
(
	[WorkTypeNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Work] ADD  DEFAULT (getdate()) FOR [SystemDate]
GO
ALTER TABLE [dbo].[WorkType] ADD  DEFAULT (getdate()) FOR [SystemDate]
GO
USE [master]
GO
ALTER DATABASE [Institute] SET  READ_WRITE 
GO
