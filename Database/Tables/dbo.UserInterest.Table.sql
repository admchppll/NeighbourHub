USE [Volunteer]
GO
/****** Object:  Table [dbo].[UserInterest]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserInterest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[InterestID] [int] NOT NULL,
 CONSTRAINT [PK_UserInterest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserInterest]  WITH CHECK ADD  CONSTRAINT [FK_UserInterest_Interest] FOREIGN KEY([InterestID])
REFERENCES [dbo].[Interest] ([ID])
GO
ALTER TABLE [dbo].[UserInterest] CHECK CONSTRAINT [FK_UserInterest_Interest]
GO
ALTER TABLE [dbo].[UserInterest]  WITH CHECK ADD  CONSTRAINT [FK_UserInterest_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserInterest] CHECK CONSTRAINT [FK_UserInterest_User]
GO
