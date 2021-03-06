USE [master]
GO
/****** Object:  Database [ExamMarkSoftware]    Script Date: 7/21/2021 1:58:15 PM ******/
CREATE DATABASE [ExamMarkSoftware] ON  PRIMARY 
( NAME = N'ExamMarkSoftware_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExamMarkSoftware.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ExamMarkSoftware_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\ExamMarkSoftware.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ExamMarkSoftware].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ExamMarkSoftware] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET ARITHABORT OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ExamMarkSoftware] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ExamMarkSoftware] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ExamMarkSoftware] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ExamMarkSoftware] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ExamMarkSoftware] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ExamMarkSoftware] SET  MULTI_USER 
GO
ALTER DATABASE [ExamMarkSoftware] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ExamMarkSoftware] SET DB_CHAINING OFF 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ExamMarkSoftware', N'ON'
GO
USE [ExamMarkSoftware]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/21/2021 1:58:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[accountID] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[accountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 7/21/2021 1:58:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[className] [nvarchar](50) NOT NULL,
	[accountID] [int] NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[className] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScoreStudent]    Script Date: 7/21/2021 1:58:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScoreStudent](
	[StudentID] [nvarchar](50) NOT NULL,
	[examCode] [nvarchar](2) NOT NULL,
	[className] [nvarchar](50) NOT NULL,
	[totalScore] [float] NOT NULL,
	[scoreDetail] [ntext] NULL,
	[studentName] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (1, N'nguyennhhe141584', N'5E1D0B3366C62535E5D7E6F0DA84BB1F')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (2, N'hieunxhe141585', N'E36A688E4838C0F47F7ECA369706D43B')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (3, N'thanhcvhe141466', N'893C3FD491F30B629FDE7ABE2BA1B516')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (4, N'tungcthe141587', N'0F043C901AC151F0E881BB1428B7D8AF')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (5, N'thuylthe141588', N'E5484520B896B2F8749D489E72EDE084')
INSERT [dbo].[Account] ([accountID], [username], [password]) VALUES (6, N'nganlmhe141589', N'FBF4F0ED9AC707753B9049F0626C0B5D')
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
ALTER TABLE [dbo].[Class]  WITH CHECK ADD  CONSTRAINT [FK_Class_Account] FOREIGN KEY([accountID])
REFERENCES [dbo].[Account] ([accountID])
GO
ALTER TABLE [dbo].[Class] CHECK CONSTRAINT [FK_Class_Account]
GO
ALTER TABLE [dbo].[ScoreStudent]  WITH CHECK ADD  CONSTRAINT [FK_ScoreStudent_Class] FOREIGN KEY([className])
REFERENCES [dbo].[Class] ([className])
GO
ALTER TABLE [dbo].[ScoreStudent] CHECK CONSTRAINT [FK_ScoreStudent_Class]
GO
USE [master]
GO
ALTER DATABASE [ExamMarkSoftware] SET  READ_WRITE 
GO
