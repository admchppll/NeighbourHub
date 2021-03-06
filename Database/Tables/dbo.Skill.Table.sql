USE [Volunteer]
GO
/****** Object:  Table [dbo].[Skill]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skill](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Label] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL CONSTRAINT [DF_Skill_Active]  DEFAULT ((1)),
	[Created] [smalldatetime] NOT NULL CONSTRAINT [DF_Skill_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_Skill] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
