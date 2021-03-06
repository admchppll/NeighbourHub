USE [Volunteer]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 24/04/2017 03:45:42 ******/
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
