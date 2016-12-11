USE [Volunteer]
GO
/****** Object:  Table [dbo].[Event]    Script Date: 11/12/2016 14:31:52 ******/
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
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Published]  DEFAULT ((0)) FOR [Published]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Suspended]  DEFAULT ((0)) FOR [Suspended]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_Address]
GO
