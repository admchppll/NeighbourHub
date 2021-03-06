USE [Volunteer]
GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 24/04/2017 03:45:42 ******/
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
