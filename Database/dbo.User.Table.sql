USE [community]
GO
/****** Object:  Table [dbo].[User]    Script Date: 11/11/2016 15:41:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [nvarchar](126) NOT NULL,
	[OrganisationID] [smallint] NULL,
	[Email] [nvarchar](256) NOT NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[AccessFailedCount] [smallint] NOT NULL,
	[LockoutEndDate] [datetime] NULL,
	[LockoutEnabled] [nchar](10) NOT NULL,
	[Created] [smalldatetime] NOT NULL,
	[Title] [nvarchar](5) NULL,
	[FirstName] [nvarchar](50) NULL,
	[Surname] [nvarchar](50) NULL,
	[PictureURL] [nvarchar](max) NULL,
	[Biography] [nvarchar](max) NULL,
	[Tokens] [smallint] NULL,
	[Admin] [bit] NULL,
	[Gender] [nvarchar](6) NULL,
	[FacebookToken] [nvarchar](max) NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Created]  DEFAULT (getdate()) FOR [Created]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Organisation]
GO
