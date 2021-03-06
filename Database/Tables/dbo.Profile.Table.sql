USE [Volunteer]
GO
/****** Object:  Table [dbo].[Profile]    Script Date: 24/04/2017 03:45:42 ******/
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
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_User]
GO
