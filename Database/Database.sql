USE [master]
GO
/****** Object:  Database [Volunteer]    Script Date: 24/04/2017 03:47:12 ******/
CREATE DATABASE [Volunteer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'aspnet-WebApplication5-20161122110922.mdf', FILENAME = N'F:\aspnet-WebApplication5-20161122110922.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[Address]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[County] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](15) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[Long] [float] NULL,
	[Lat] [float] NULL,
	[Default] [bit] NOT NULL CONSTRAINT [DF_Address_Default]  DEFAULT ((0)),
	[GeoLocation] [geography] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Audit]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Audit](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL CONSTRAINT [DF_Audit_Date]  DEFAULT (getdate()),
	[UserID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[AuditMessage] [nvarchar](max) NOT NULL,
	[ReportID] [int] NULL,
 CONSTRAINT [PK_Audit] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bookmark]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmark](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Bookmarked] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL CONSTRAINT [DF_Contact_Date]  DEFAULT (getdate()),
	[Replied] [bit] NOT NULL CONSTRAINT [DF_Contact_Replied]  DEFAULT ((0)),
	[LinkedEmail] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 24/04/2017 03:47:12 ******/
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
	[Published] [bit] NOT NULL CONSTRAINT [DF_Event_Published]  DEFAULT ((1)),
	[Edited] [smalldatetime] NULL,
	[Date] [date] NOT NULL,
	[Time] [time](7) NOT NULL,
	[Repeated] [smallint] NULL,
	[RepeatIncrement] [tinyint] NULL,
	[Length] [tinyint] NOT NULL,
	[AM1] [bit] NOT NULL CONSTRAINT [DF_Event_AM1]  DEFAULT ((0)),
	[AM2] [bit] NOT NULL CONSTRAINT [DF_Event_AM2]  DEFAULT ((0)),
	[AM3] [bit] NOT NULL CONSTRAINT [DF_Event_AM3]  DEFAULT ((0)),
	[AM4] [bit] NOT NULL CONSTRAINT [DF_Event_AM4]  DEFAULT ((0)),
	[AM5] [bit] NOT NULL CONSTRAINT [DF_Event_AM5]  DEFAULT ((0)),
	[AM6] [bit] NOT NULL CONSTRAINT [DF_Event_AM6]  DEFAULT ((0)),
	[AM7] [bit] NOT NULL CONSTRAINT [DF_Event_AM7]  DEFAULT ((0)),
	[PM1] [bit] NOT NULL CONSTRAINT [DF_Event_PM1]  DEFAULT ((0)),
	[PM2] [bit] NOT NULL CONSTRAINT [DF_Event_PM2]  DEFAULT ((0)),
	[PM3] [bit] NOT NULL CONSTRAINT [DF_Event_PM3]  DEFAULT ((0)),
	[PM4] [bit] NOT NULL CONSTRAINT [DF_Event_PM4]  DEFAULT ((0)),
	[PM5] [bit] NOT NULL CONSTRAINT [DF_Event_PM5]  DEFAULT ((0)),
	[PM6] [bit] NOT NULL CONSTRAINT [DF_Event_PM6]  DEFAULT ((0)),
	[PM7] [bit] NOT NULL CONSTRAINT [DF_Event_PM7]  DEFAULT ((0)),
	[DateInfo] [nvarchar](200) NULL,
	[Suspended] [bit] NOT NULL CONSTRAINT [DF_Event_Suspended]  DEFAULT ((0)),
	[VolunteerQuantity] [smallint] NOT NULL,
	[Points] [smallint] NOT NULL,
	[PictureURL] [nvarchar](max) NULL,
	[Cancelled] [bit] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventTag]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventTag](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[TagID] [int] NOT NULL,
 CONSTRAINT [PK_EventTag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Information]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[Interest]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[Message]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [nvarchar](128) NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Body] [nvarchar](max) NULL,
	[Sent] [smalldatetime] NULL,
	[Read] [bit] NOT NULL CONSTRAINT [DF_Message_Read]  DEFAULT ((0)),
	[Saved] [bit] NOT NULL CONSTRAINT [DF_Message_Saved]  DEFAULT ((0)),
	[Admin] [bit] NULL,
	[ParentMessage] [int] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Notification]    Script Date: 24/04/2017 03:47:12 ******/
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
	[Viewed] [bit] NOT NULL CONSTRAINT [DF_Notification_Viewed]  DEFAULT ((0)),
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Balance] [smallint] NOT NULL CONSTRAINT [DF_Organisation_Balance]  DEFAULT ((0)),
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
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
	[Approved] [bit] NOT NULL CONSTRAINT [DF_Organisation_Approved]  DEFAULT ((0)),
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[Balance] [smallint] NOT NULL CONSTRAINT [DF_Profile_Balance]  DEFAULT ((0)),
	[Title] [nvarchar](5) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Gender] [bit] NOT NULL,
	[BirthDate] [smalldatetime] NOT NULL,
	[Phone] [nvarchar](20) NULL,
	[Biography] [nvarchar](max) NULL,
	[PictureURL] [nvarchar](max) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Profile_Active]  DEFAULT ((1)),
	[Suspended] [bit] NOT NULL CONSTRAINT [DF_Profile_Suspended]  DEFAULT ((0)),
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegularPoints]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegularPoints](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[OrganisationID] [smallint] NULL,
	[DayOfMonth] [tinyint] NOT NULL,
	[Expiry] [date] NOT NULL,
	[Reason] [nchar](10) NULL,
	[Submitted] [smalldatetime] NOT NULL,
	[Approved] [bit] NOT NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Report]    Script Date: 24/04/2017 03:47:12 ******/
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
	[Sent] [datetime] NOT NULL CONSTRAINT [DF_Report_Sent]  DEFAULT (getdate()),
	[ResolvedDate] [datetime] NULL CONSTRAINT [DF_Report_Resolved]  DEFAULT ((0)),
 CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Review]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Review](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[VolunteerID] [varchar](128) NULL,
	[EventID] [int] NOT NULL,
	[Rating] [tinyint] NULL,
	[Review] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Review] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[Skill]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Skill_Active]  DEFAULT ((1)),
	[Created] [smalldatetime] NOT NULL CONSTRAINT [DF_Skill_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tag]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Tag_Active]  DEFAULT ((1)),
	[Created] [smalldatetime] NULL CONSTRAINT [DF_Tag_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL CONSTRAINT [DF_Transaction_Date]  DEFAULT (getdate()),
	[SenderID] [nvarchar](128) NOT NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[Gift] [bit] NOT NULL CONSTRAINT [DF_Transaction_Gift]  DEFAULT ((0)),
	[Amount] [smallint] NOT NULL,
	[Cancelled] [bit] NOT NULL CONSTRAINT [DF_Transaction_Cancelled]  DEFAULT ((0)),
	[Complete] [bit] NOT NULL CONSTRAINT [DF_Transaction_Complete]  DEFAULT ((0)),
	[ParentTransaction] [int] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 24/04/2017 03:47:12 ******/
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
	[Created] [smalldatetime] NOT NULL CONSTRAINT [DF_User_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserClaim]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[UserInterest]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[UserLogin]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[UserOrganisation]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrganisation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[OrganisationID] [smallint] NOT NULL,
	[Approved] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Approved]  DEFAULT ((0)),
	[Denied] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Denied]  DEFAULT ((0)),
	[Admin] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Admin]  DEFAULT ((0)),
	[Created] [smalldatetime] NOT NULL CONSTRAINT [DF_UserOrganisation_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_UserOrganisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[UserSkill]    Script Date: 24/04/2017 03:47:12 ******/
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
/****** Object:  Table [dbo].[Volunteer]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volunteer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[VolunteerID] [nvarchar](128) NOT NULL,
	[Accepted] [bit] NOT NULL,
	[Confirmed] [bit] NOT NULL CONSTRAINT [DF_Volunteer_Confirmed]  DEFAULT ((0)),
	[Rejected] [bit] NOT NULL CONSTRAINT [DF_Volunteer_Rejected]  DEFAULT ((0)),
	[Withdrawn] [bit] NOT NULL CONSTRAINT [DF_Volunteer_Withdrawn]  DEFAULT ((0)),
 CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[EventSearch]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EventSearch]
AS
SELECT        E.ID, E.Title, E.ShortDescription AS Description, E.Date, E.Time, A.GeoLocation AS Location, E.VolunteerQuantity, ISNULL(vc.Volunteers, 0) AS Volunteers, E.Length, E.Published, E.Suspended
FROM            dbo.Event AS E LEFT OUTER JOIN
                             (SELECT        EventID, COUNT(*) AS Volunteers
                               FROM            dbo.Volunteer AS v
                               WHERE        (Accepted = 1) AND (Rejected = 0) AND (Withdrawn = 0)
                               GROUP BY EventID) AS vc ON vc.EventID = E.ID INNER JOIN
                         dbo.Address AS A ON A.ID = E.AddressID

GO
/****** Object:  View [dbo].[EventStatistics]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[EventStatistics]
AS
SELECT        Created.Month, Months.MonthName, Created.Year, ISNULL(Created.Count, 0) AS Total, ISNULL(Created.Unpublished, 0) AS UnpublishedCount, ISNULL(Created.Cancelled, 0) AS CancelledCount, 
                         ISNULL(Created.Suspended, 0) AS SuspendedCount
FROM            (SELECT        MONTH(Created) AS Month, YEAR(Created) AS Year, COUNT(*) AS Count, SUM(CASE WHEN e.Cancelled = 1 THEN 1 ELSE 0 END) AS Cancelled, 
                                                    SUM(CASE WHEN E.Suspended = 1 THEN 1 ELSE 0 END) AS Suspended, SUM(CASE WHEN E.Published = 0 THEN 1 ELSE 0 END) AS Unpublished
                          FROM            dbo.Event AS e
                          GROUP BY YEAR(Created), MONTH(Created)) AS Created LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Created.Month

GO
/****** Object:  View [dbo].[IndividualUserStatistics]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[IndividualUserStatistics]
AS
SELECT        Created.Month, Months.MonthName, Created.Year, Created.UserID, Created.Count AS HostedCount, ISNULL(Volunteer.Count, 0) AS VolunteerCount, ISNULL(Volunteer.WithdrawnCount, 0) AS WithdrawnCount, 
                         ISNULL(Volunteer.RejectedCount, 0) AS RejectedCount, ISNULL(Volunteer.ConfirmedCount, 0) AS ConfirmedCount, ISNULL(Volunteer.PendingConfirmationCount, 0) AS PendingConfirmationCount
FROM            (SELECT        MONTH(Created) AS Month, YEAR(Created) AS Year, COUNT(*) AS Count, HostID AS UserID
                          FROM            dbo.Event AS e
                          GROUP BY YEAR(Created), MONTH(Created), HostID) AS Created LEFT OUTER JOIN
                             (SELECT        MONTH(e.Date) AS Month, YEAR(e.Date) AS Year, COUNT(*) AS Count, SUM(CASE WHEN v.Withdrawn = 1 THEN 1 ELSE 0 END) AS WithdrawnCount, 
                                                         SUM(CASE WHEN v.Rejected = 1 THEN 1 ELSE 0 END) AS RejectedCount, SUM(CASE WHEN v.Confirmed = 1 THEN 1 ELSE 0 END) AS ConfirmedCount, SUM(CASE WHEN v.Accepted = 1 AND 
                                                         v.Rejected <> 1 AND v.Withdrawn <> 1 AND v.Confirmed <> 1 THEN 1 ELSE 0 END) AS PendingConfirmationCount, v.VolunteerID AS UserID
                               FROM            dbo.Event AS e INNER JOIN
                                                         dbo.Volunteer AS v ON v.EventID = e.ID
                               GROUP BY YEAR(e.Date), MONTH(e.Date), v.VolunteerID) AS Volunteer ON Volunteer.Month = Created.Month AND Volunteer.Year = Created.Year AND Volunteer.UserID = Created.UserID LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Created.Month

GO
/****** Object:  View [dbo].[ReportStatistics]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ReportStatistics]
AS
SELECT        TOP (100) PERCENT Reported.Month, Months.MonthName, Reported.Year, Reported.Count AS ReportedCount, ISNULL(Resolved.Count, 0) AS ResolvedCount, ISNULL
                             ((SELECT        COUNT(*) AS Expr1
                                 FROM            dbo.Report
                                 WHERE        (Sent < CAST(CAST(Reported.Year AS VARCHAR(4)) + RIGHT('0' + CAST(Reported.Month + 1 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(1 AS VARCHAR(2)), 2) AS DATETIME)) AND 
                                                          (ResolvedDate >= CAST(CAST(Reported.Year AS VARCHAR(4)) + RIGHT('0' + CAST(Reported.Month + 1 AS VARCHAR(2)), 2) + RIGHT('0' + CAST(1 AS VARCHAR(2)), 2) AS DATETIME) OR
                                                          ResolvedDate IS NULL)), 0) AS UresolvedCount
FROM            (SELECT        MONTH(Sent) AS Month, YEAR(Sent) AS Year, ISNULL(COUNT(*), 0) AS Count
                          FROM            dbo.Report AS r
                          GROUP BY YEAR(Sent), MONTH(Sent)) AS Reported LEFT OUTER JOIN
                             (SELECT        MONTH(ResolvedDate) AS Month, YEAR(ResolvedDate) AS Year, COUNT(*) AS Count
                               FROM            dbo.Report AS r
                               GROUP BY YEAR(ResolvedDate), MONTH(ResolvedDate)) AS Resolved ON Reported.Month = Resolved.Month AND Reported.Year = Resolved.Year LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Reported.Month

GO
/****** Object:  View [dbo].[VolunteerStatistics]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[VolunteerStatistics]
AS
SELECT        Created.Month, Months.MonthName, Created.Year, ISNULL(Created.Count, 0) AS Potential, ISNULL(Volunteer.Count, 0) AS VolunteerCount, ISNULL(Volunteer.WithdrawnCount, 0) AS WithdrawnCount, 
                         ISNULL(Volunteer.RejectedCount, 0) AS RejectedCount, ISNULL(Volunteer.ConfirmedCount, 0) AS ConfirmedCount, ISNULL(Volunteer.PendingConfirmationCount, 0) AS PendingConfirmationCount
FROM            (SELECT        MONTH(Created) AS Month, YEAR(Created) AS Year, SUM(VolunteerQuantity) AS Count
                          FROM            dbo.Event AS e
                          GROUP BY YEAR(Created), MONTH(Created)) AS Created LEFT OUTER JOIN
                             (SELECT        MONTH(e.Date) AS Month, YEAR(e.Date) AS Year, COUNT(*) AS Count, SUM(CASE WHEN v.Withdrawn = 1 THEN 1 ELSE 0 END) AS WithdrawnCount, 
                                                         SUM(CASE WHEN v.Rejected = 1 THEN 1 ELSE 0 END) AS RejectedCount, SUM(CASE WHEN v.Confirmed = 1 THEN 1 ELSE 0 END) AS ConfirmedCount, SUM(CASE WHEN v.Accepted = 1 AND 
                                                         v.Rejected <> 1 AND v.Withdrawn <> 1 AND v.Confirmed <> 1 THEN 1 ELSE 0 END) AS PendingConfirmationCount
                               FROM            dbo.Event AS e INNER JOIN
                                                         dbo.Volunteer AS v ON v.EventID = e.ID
                               GROUP BY YEAR(e.Date), MONTH(e.Date), v.VolunteerID) AS Volunteer ON Volunteer.Month = Created.Month AND Volunteer.Year = Created.Year LEFT OUTER JOIN
                             (SELECT        number + 1 AS MonthNumber, DATENAME(mm, DATEADD(mm, number, 0)) AS MonthName
                               FROM            master.dbo.spt_values
                               WHERE        (type = 'P') AND (number < 12)) AS Months ON Months.MonthNumber = Created.Month

GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [RoleNameIndex]    Script Date: 24/04/2017 03:47:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[Role]
(
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [UserNameIndex]    Script Date: 24/04/2017 03:47:12 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[User]
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 24/04/2017 03:47:12 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserClaim]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 24/04/2017 03:47:12 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserLogin]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_IdentityUser_Id]    Script Date: 24/04/2017 03:47:12 ******/
CREATE NONCLUSTERED INDEX [IX_IdentityUser_Id] ON [dbo].[UserRole]
(
	[IdentityUser_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_RoleId]    Script Date: 24/04/2017 03:47:12 ******/
CREATE NONCLUSTERED INDEX [IX_RoleId] ON [dbo].[UserRole]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Information] ADD  CONSTRAINT [DF_Information_Edited]  DEFAULT (getdate()) FOR [Edited]
GO
ALTER TABLE [dbo].[RegularPoints] ADD  CONSTRAINT [DF_Points_DayOfMonth]  DEFAULT ((1)) FOR [DayOfMonth]
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
ALTER TABLE [dbo].[Audit]  WITH CHECK ADD  CONSTRAINT [FK_Audit_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Audit] CHECK CONSTRAINT [FK_Audit_User]
GO
ALTER TABLE [dbo].[Bookmark]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarked_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Bookmark] CHECK CONSTRAINT [FK_Bookmarked_Event]
GO
ALTER TABLE [dbo].[Bookmark]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarked_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Bookmark] CHECK CONSTRAINT [FK_Bookmarked_User]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Address]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_User] FOREIGN KEY([HostID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_User]
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
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
ALTER TABLE [dbo].[RegularPoints]  WITH CHECK ADD  CONSTRAINT [FK_Points_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[RegularPoints] CHECK CONSTRAINT [FK_Points_Organisation]
GO
ALTER TABLE [dbo].[RegularPoints]  WITH CHECK ADD  CONSTRAINT [FK_Points_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[RegularPoints] CHECK CONSTRAINT [FK_Points_User]
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
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_Event]
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD  CONSTRAINT [FK_Review_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Review] CHECK CONSTRAINT [FK_Review_User]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Event]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_RecipientUser] FOREIGN KEY([RecipientID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_RecipientUser]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([SenderID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_User]
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
ALTER TABLE [dbo].[UserOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisation_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[UserOrganisation] CHECK CONSTRAINT [FK_UserOrganisation_Organisation]
GO
ALTER TABLE [dbo].[UserOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserOrganisation] CHECK CONSTRAINT [FK_UserOrganisation_User]
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
/****** Object:  StoredProcedure [dbo].[clearNotifications]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[clearNotifications]
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM	[Notification] 
	WHERE	Viewed = 1
END

GO
/****** Object:  StoredProcedure [dbo].[confirmVolunteer]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[confirmVolunteer] (@VolunteerID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Volunteer NVARCHAR(128),
		@TransactionID INT;

	SELECT	@EventID = v.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Volunteer = V.VolunteerID
	FROM	Volunteer v
		INNER JOIN [Transaction] t ON v.VolunteerID = t.RecipientID 
			AND t.EventID = v.EventID
	WHERE	v.ID = @VolunteerID

	UPDATE	[Transaction]
	SET	Complete = 1
	WHERE	ID = @TransactionID

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Volunteer 
END

GO
/****** Object:  StoredProcedure [dbo].[createGeoLocationAddress]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[createGeoLocationAddress] (@AddressID INT)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	UPDATE	Address
	SET	GeoLocation = GEOGRAPHY::Point(Lat, Long, 4326)
	WHERE	ID = @AddressID
END

GO
/****** Object:  StoredProcedure [dbo].[reduceSenderBalance]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[reduceSenderBalance] (@TransactionID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@Points SMALLINT,
		@Sender NVARCHAR(128);

	SELECT	@Points = t.Amount,
		@Sender = t.SenderID
	FROM	[Transaction] t 
	WHERE	t.ID = @TransactionID

	UPDATE	Profile
	SET	Balance = Balance - @Points
	WHERE	UserID = @Sender
END
GO
/****** Object:  StoredProcedure [dbo].[reverseTransaction]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[reverseTransaction] (@TransactionID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Sender NVARCHAR(128),
		@Recipient NVARCHAR(128),
		@Complete BIT;

	SELECT	@EventID = t.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Recipient = t.RecipientID,
		@Sender = t.SenderID,
		@Complete = t.Complete
	FROM	[Transaction] t 
	WHERE	t.ID = @TransactionID

	UPDATE	[Transaction]
	SET	Cancelled = 1
	WHERE	ID = @TransactionID

	INSERT	[Transaction] (	SenderID, 
				RecipientID,
				Amount,
				EventID,
				Complete, 
				ParentTransaction)
	VALUES	(	@Recipient,
			@Sender,
			@Points,
			@EventID,
			1,
			@TransactionID
		)

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Sender 

	IF @Complete = 1 
	BEGIN
		UPDATE	Profile
		SET	Balance = Balance - @Points
		WHERE	UserID = @Recipient
	END

	INSERT INTO	Audit (	UserID, 
				EventID, 
				AuditMessage)
	VALUES	(	@Recipient, 
			@EventID, 
			'Transaction Reversed: #' + @TransactionID + ' (Recipient)'),
		(	@Sender, 
			@EventID, 
			'Transaction Reversed: #' + @TransactionID + ' (Sender)')
END

GO
/****** Object:  StoredProcedure [dbo].[spUsersWithoutProfile]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsersWithoutProfile]
AS 
BEGIN

DECLARE	@Count INT

SELECT	@Count = ISNULL(COUNT(*),0) 
FROM	[User] u 
	LEFT JOIN Profile p on p.UserID = u.ID 
WHERE	p.ID IS NULL

SELECT TOP 1 @Count [Count]

END

GO
/****** Object:  StoredProcedure [dbo].[withdrawVolunteer]    Script Date: 24/04/2017 03:47:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[withdrawVolunteer] (@VolunteerID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Volunteer NVARCHAR(128),
		@Host NVARCHAR(128),
		@TransactionID INT;

	SELECT	@EventID = v.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Volunteer = V.VolunteerID,
		@Host = t.SenderID
	FROM	Volunteer v
		INNER JOIN [Transaction] t ON v.VolunteerID = t.RecipientID 
			AND t.EventID = v.EventID
	WHERE	v.ID = @VolunteerID

	UPDATE	[Transaction]
	SET	Cancelled = 1
	WHERE	ID = @TransactionID

	INSERT	[Transaction] (	SenderID, 
				RecipientID,
				Amount,
				EventID,
				Complete, 
				ParentTransaction)
	VALUES	(	@Volunteer,
			@Host,
			@Points,
			@EventID,
			1,
			@TransactionID
		)

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Host 
END

GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "E"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 224
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A"
            Begin Extent = 
               Top = 6
               Left = 470
               Bottom = 136
               Right = 640
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "vc"
            Begin Extent = 
               Top = 6
               Left = 262
               Bottom = 102
               Right = 432
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1965
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EventSearch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EventSearch'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Created"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 102
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EventStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'EventStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Created"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Volunteer"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 483
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 6
               Left = 521
               Bottom = 102
               Right = 691
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IndividualUserStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'IndividualUserStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Reported"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Resolved"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 102
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1905
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ReportStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "Created"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 119
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Volunteer"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 136
               Right = 483
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "Months"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 216
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 10
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 975
         Width = 780
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 1935
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerStatistics'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'VolunteerStatistics'
GO
USE [master]
GO
ALTER DATABASE [Volunteer] SET  READ_WRITE 
GO
