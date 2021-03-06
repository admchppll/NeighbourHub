USE [Volunteer]
GO
/****** Object:  Table [dbo].[Information]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Information](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[Data] [nvarchar](max) NOT NULL,
	[Edited] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Information] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Information] ADD  CONSTRAINT [DF_Information_Edited]  DEFAULT (getdate()) FOR [Edited]
GO
