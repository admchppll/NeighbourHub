USE [Volunteer]
GO
/****** Object:  Table [dbo].[Message]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SenderID] [nvarchar](128) NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[Title] [nvarchar](50) NULL,
	[Body] [nvarchar](max) NULL,
	[Sent] [smalldatetime] NULL,
	[Read] [bit] NOT NULL CONSTRAINT [DF_Message_Read]  DEFAULT ((0)),
	[Saved] [bit] NOT NULL CONSTRAINT [DF_Message_Saved]  DEFAULT ((0)),
	[Admin] [bit] NULL,
	[ParentMessage] [int] NULL,
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User_Recipient] FOREIGN KEY([RecipientID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User_Recipient]
GO
ALTER TABLE [dbo].[Message]  WITH CHECK ADD  CONSTRAINT [FK_Message_User_Sender] FOREIGN KEY([SenderID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Message] CHECK CONSTRAINT [FK_Message_User_Sender]
GO
