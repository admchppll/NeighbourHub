USE [Volunteer]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL CONSTRAINT [DF_Transaction_Date]  DEFAULT (getdate()),
	[SenderID] [nvarchar](128) NOT NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[Gift] [bit] NOT NULL CONSTRAINT [DF_Transaction_Gift]  DEFAULT ((0)),
	[Amount] [smallint] NOT NULL,
	[Cancelled] [bit] NOT NULL CONSTRAINT [DF_Transaction_Cancelled]  DEFAULT ((0)),
	[Complete] [bit] NOT NULL CONSTRAINT [DF_Transaction_Complete]  DEFAULT ((0)),
	[ParentTransaction] [int] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Event]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_RecipientUser] FOREIGN KEY([RecipientID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_RecipientUser]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_User] FOREIGN KEY([SenderID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_User]
GO
