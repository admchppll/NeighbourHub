USE [Volunteer]
GO
/****** Object:  Table [dbo].[Transaction]    Script Date: 11/12/2016 14:31:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[SenderID] [nvarchar](128) NOT NULL,
	[RecipientID] [nvarchar](128) NOT NULL,
	[EventID] [int] NULL,
	[Gift] [bit] NULL,
	[Amount] [smallint] NOT NULL,
	[Complete] [bit] NULL,
 CONSTRAINT [PK_Transaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_Date]  DEFAULT (getdate()) FOR [Date]
GO
ALTER TABLE [dbo].[Transaction] ADD  CONSTRAINT [DF_Transaction_Gift]  DEFAULT ((0)) FOR [Gift]
GO
ALTER TABLE [dbo].[Transaction]  WITH CHECK ADD  CONSTRAINT [FK_Transaction_Event] FOREIGN KEY([EventID])
REFERENCES [dbo].[Event] ([ID])
GO
ALTER TABLE [dbo].[Transaction] CHECK CONSTRAINT [FK_Transaction_Event]
GO
