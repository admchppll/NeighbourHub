USE [Volunteer]
GO
/****** Object:  Table [dbo].[Points]    Script Date: 16/04/2017 05:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Points](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[OrganisationID] [smallint] NULL,
	[DayOfMonth] [tinyint] NOT NULL,
	[Expiry] [date] NOT NULL,
 CONSTRAINT [PK_Points] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Points] ADD  CONSTRAINT [DF_Points_DayOfMonth]  DEFAULT ((1)) FOR [DayOfMonth]
GO
ALTER TABLE [dbo].[Points]  WITH CHECK ADD  CONSTRAINT [FK_Points_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[Points] CHECK CONSTRAINT [FK_Points_Organisation]
GO
ALTER TABLE [dbo].[Points]  WITH CHECK ADD  CONSTRAINT [FK_Points_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Points] CHECK CONSTRAINT [FK_Points_User]
GO
