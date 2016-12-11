USE [community]
GO
/****** Object:  Table [dbo].[Volunteer]    Script Date: 11/11/2016 15:41:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Volunteer](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[VolunteerID] [nvarchar](126) NOT NULL,
	[Confirmed] [bit] NOT NULL,
	[Rejected] [bit] NOT NULL,
 CONSTRAINT [PK_Volunteer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Volunteer] ADD  CONSTRAINT [DF_Volunteer_Confirmed]  DEFAULT ((0)) FOR [Confirmed]
GO
ALTER TABLE [dbo].[Volunteer] ADD  CONSTRAINT [DF_Volunteer_Rejected]  DEFAULT ((0)) FOR [Rejected]
GO
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_Event] FOREIGN KEY([VolunteerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_Event]
GO
ALTER TABLE [dbo].[Volunteer]  WITH CHECK ADD  CONSTRAINT [FK_Volunteer_User] FOREIGN KEY([VolunteerID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Volunteer] CHECK CONSTRAINT [FK_Volunteer_User]
GO
