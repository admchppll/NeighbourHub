USE [Volunteer]
GO
/****** Object:  Table [dbo].[Bookmarked]    Script Date: 16/04/2017 05:10:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmarked](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[EventID] [int] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_Bookmarked] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Bookmarked]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarked_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Bookmarked] CHECK CONSTRAINT [FK_Bookmarked_Event]
GO
ALTER TABLE [dbo].[Bookmarked]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarked_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Bookmarked] CHECK CONSTRAINT [FK_Bookmarked_User]
GO
