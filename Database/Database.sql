USE [master]
GO
/****** Object:  Database [Volunteer]    Script Date: 11/12/2016 14:33:42 ******/
CREATE DATABASE [Volunteer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-WebApplication5-20161122110922.mdf', FILENAME = N'F:\aspnet-WebApplication5-20161122110922.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'aspnet-WebApplication5-20161122110922_log.ldf', FILENAME = N'F:\aspnet-WebApplication5-20161122110922_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Volunteer] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Volunteer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Volunteer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Volunteer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Volunteer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Volunteer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Volunteer] SET ARITHABORT OFF 
GO
ALTER DATABASE [Volunteer] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Volunteer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Volunteer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Volunteer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Volunteer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Volunteer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Volunteer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Volunteer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Volunteer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Volunteer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Volunteer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Volunteer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Volunteer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Volunteer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Volunteer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Volunteer] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [Volunteer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Volunteer] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Volunteer] SET  MULTI_USER 
GO
ALTER DATABASE [Volunteer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Volunteer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Volunteer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Volunteer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Volunteer] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Volunteer]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[Name] [nchar](10) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[County] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Postcode] [nvarchar](15) NULL,
	[Notes] [nvarchar](max) NULL,
	[Long] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[GeoLocation] [geography] NULL,
	[Default] [bit] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Audit]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[AuditMessage] [nvarchar](max) NOT NULL,
	[AdminOnly] [bit] NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bookmark]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmark](
	[UserID] [nvarchar](128) NOT NULL,
	[EventID] [int] NOT NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[HostID] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ShortDescription] [nvarchar](200) NULL,
	[LongDescription] [nvarchar](max) NOT NULL,
	[AddressID] [int] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Published] [bit] NOT NULL,
	[Edited] [smalldatetime] NULL,
	[Repeated] [smallint] NULL,
	[RepeatIncrement] [tinyint] NULL,
	[Date] [smalldatetime] NOT NULL,
	[Length] [tinyint] NOT NULL,
	[AM1] [bit] NULL,
	[AM2] [bit] NULL,
	[AM3] [bit] NULL,
	[AM4] [bit] NULL,
	[AM5] [bit] NULL,
	[AM6] [bit] NULL,
	[AM7] [bit] NULL,
	[PM1] [bit] NULL,
	[PM2] [bit] NULL,
	[PM3] [bit] NULL,
	[PM4] [bit] NULL,
	[PM5] [bit] NULL,
	[PM6] [bit] NULL,
	[PM7] [bit] NULL,
	[DateInfo] [nvarchar](200) NULL,
	[Suspended] [bit] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventTag]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTag](
	[ID] [int] NOT NULL,
	[EventID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_EventTag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Information]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Information](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[Edited] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Information] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Interest]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Interest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](30) NOT NULL,
 CONSTRAINT [PK_Interest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Message]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [nvarchar](128) NOT NULL,
	[RecipientID] [nvarchar](128) NULL,
	[Title] [nvarchar](50) NULL,
	[Body] [nvarchar](max) NULL,
	[Sent] [smalldatetime] NULL,
	[Read] [bit] NULL,
	[Saved] [bit] NULL,
	[Notified] [bit] NULL,
	[Admin] [nchar](10) NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notification]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](20) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Link] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [smallint] NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[County] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](15) NOT NULL,
	[Facebook] [nvarchar](200) NULL,
	[Twitter] [nvarchar](200) NULL,
	[Google] [nvarchar](200) NULL,
	[Youtube] [nvarchar](200) NULL,
	[CharityNumber] [nvarchar](50) NOT NULL,
	[Approved] [bit] NULL,
 CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[OrganisationID] [smallint] NULL,
	[Balance] [smallint] NOT NULL,
	[Title] [nvarchar](5) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Gender] [bit] NULL,
	[BirthDate] [smalldatetime] NULL,
	[Phone] [nvarchar](20) NULL,
	[Biography] [nvarchar](max) NULL,
	[PictureURL] [nvarchar](max) NULL,
	[Active] [bit] NULL,
	[Suspended] [bit] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Report](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[ReportedEvent] [int] NULL,
	[ReportedID] [nvarchar](128) NULL,
	[Description] [nvarchar](max) NULL,
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Skill]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tag]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL,
	[Created] [smalldatetime] NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[SenderID] [nvarchar](128) NOT NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[Gift] [bit] NULL,
	[Amount] [smallint] NOT NULL,
	[Complete] [bit] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](max) NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserInterest]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInterest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[InterestID] [int] NOT NULL,
 CONSTRAINT [PK_UserInterest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserLogin]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogin](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.UserLogin] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserSkill]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSkill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Skill] [int] NOT NULL,
 CONSTRAINT [PK_UserSkill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 11/12/2016 14:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volunteer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[VolunteerID] [nvarchar](128) NOT NULL,
	[Confirmed] [bit] NOT NULL,
	[Rejected] [bit] NOT NULL,
	[Withdrawn] [bit] NOT NULL,
 CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 11/12/2016 14:33:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Role]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 11/12/2016 14:33:42 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[User]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 11/12/2016 14:33:42 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserClaim]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 11/12/2016 14:33:42 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserLogin]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 11/12/2016 14:33:42 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserRole]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 11/12/2016 14:33:42 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Audit] ADD  CONSTRAINT [DF_Audit_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Suspended]  DEFAULT ((0)) FOR [Suspended]
GO
ALTER TABLE [dbo].[Information] ADD  CONSTRAINT [DF_Information_Edited]  DEFAULT (getdate()) FOR [Edited]
GO
ALTER TABLE [dbo].[Profile] ADD  CONSTRAINT [DF_Profile_Balance]  DEFAULT ((0)) FOR [Balance]
GO
ALTER TABLE [dbo].[Tag] ADD  CONSTRAINT [DF_Tag_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_Gift]  DEFAULT ((0)) FOR [Gift]
GO
ALTER TABLE [dbo].[Volunteer] ADD  CONSTRAINT [DF_Volunteer_Confirmed]  DEFAULT ((0)) FOR [Confirmed]
GO
ALTER TABLE [dbo].[Volunteer] ADD  CONSTRAINT [DF_Volunteer_Rejected]  DEFAULT ((0)) FOR [Rejected]
GO
ALTER TABLE [dbo].[Volunteer] ADD  CONSTRAINT [DF_Volunteer_Withdrawn]  DEFAULT ((0)) FOR [Withdrawn]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_User]
GO
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_Audit_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_Audit_Event]
GO
ALTER TABLE [dbo].[Bookmark]  WITH CHECK ADD  CONSTRAINT [FK_Bookmark_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Bookmark] CHECK CONSTRAINT [FK_Bookmark_Event]
GO
ALTER TABLE [dbo].[Bookmark]  WITH CHECK ADD  CONSTRAINT [FK_Bookmark_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Bookmark] CHECK CONSTRAINT [FK_Bookmark_User]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Address]
GO
ALTER TABLE [dbo].[EventTag]  WITH CHECK ADD  CONSTRAINT [FK_EventTag_Event1] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[EventTag] CHECK CONSTRAINT [FK_EventTag_Event1]
GO
ALTER TABLE [dbo].[EventTag]  WITH CHECK ADD  CONSTRAINT [FK_EventTag_Tag] FOREIGN KEY([TagID])
REFERENCES [dbo].[Tag] ([ID])
GO
ALTER TABLE [dbo].[EventTag] CHECK CONSTRAINT [FK_EventTag_Tag]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User_Recipient] FOREIGN KEY([RecipientID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User_Recipient]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User_Sender] FOREIGN KEY([SenderID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User_Sender]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [FK_Notification_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [FK_Notification_User]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Organisation]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_Event] FOREIGN KEY([ReportedEvent])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_Event]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_User_Reported] FOREIGN KEY([ReportedID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_User_Reported]
GO
ALTER TABLE [dbo].[Report]  WITH CHECK ADD  CONSTRAINT [FK_Report_User_Reporter] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Report] CHECK CONSTRAINT [FK_Report_User_Reporter]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Event]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserClaim_dbo.User_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_dbo.UserClaim_dbo.User_IdentityUser_Id]
GO
ALTER TABLE [dbo].[UserInterest]  WITH CHECK ADD  CONSTRAINT [FK_UserInterest_Interest] FOREIGN KEY([InterestID])
REFERENCES [dbo].[Interest] ([ID])
GO
ALTER TABLE [dbo].[UserInterest] CHECK CONSTRAINT [FK_UserInterest_Interest]
GO
ALTER TABLE [dbo].[UserInterest]  WITH CHECK ADD  CONSTRAINT [FK_UserInterest_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserInterest] CHECK CONSTRAINT [FK_UserInterest_User]
GO
ALTER TABLE [dbo].[UserLogin]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserLogin_dbo.User_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserLogin] CHECK CONSTRAINT [FK_dbo.UserLogin_dbo.User_IdentityUser_Id]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.User_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.User_IdentityUser_Id]
GO
ALTER TABLE [dbo].[UserSkill]  WITH CHECK ADD  CONSTRAINT [FK_UserSkill_Skill] FOREIGN KEY([Skill])
REFERENCES [dbo].[Skill] ([ID])
GO
ALTER TABLE [dbo].[UserSkill] CHECK CONSTRAINT [FK_UserSkill_Skill]
GO
ALTER TABLE [dbo].[UserSkill]  WITH CHECK ADD  CONSTRAINT [FK_UserSkill_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserSkill] CHECK CONSTRAINT [FK_UserSkill_User]
GO
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_Event]
GO
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_User] FOREIGN KEY([VolunteerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_User]
GO
USE [master]
GO
ALTER DATABASE [Volunteer] SET  READ_WRITE 
GO
