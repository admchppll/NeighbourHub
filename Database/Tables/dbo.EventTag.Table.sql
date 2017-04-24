USE [Volunteer]
GO
/****** Object:  Table [dbo].[EventTag]    Script Date: 24/04/2017 03:45:42 ******/
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
