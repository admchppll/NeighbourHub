USE [Volunteer]
GO
/****** Object:  Table [dbo].[Bookmark]    Script Date: 11/12/2016 14:31:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmark](
	[UserID] [nvarchar](128) NOT NULL,
	[EventID] [int] NOT NULL
) ON [PRIMARY]

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
