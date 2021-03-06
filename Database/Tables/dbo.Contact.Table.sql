USE [Volunteer]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Date] [datetime] NOT NULL CONSTRAINT [DF_Contact_Date]  DEFAULT (getdate()),
	[Replied] [bit] NOT NULL CONSTRAINT [DF_Contact_Replied]  DEFAULT ((0)),
	[LinkedEmail] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
