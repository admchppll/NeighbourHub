USE [Volunteer]
GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Balance] [smallint] NOT NULL CONSTRAINT [DF_Organisation_Balance]  DEFAULT ((0)),
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [nvarchar](20) NOT NULL,
	[Address1] [nvarchar](50) NOT NULL,
	[Address2] [nvarchar](50) NULL,
	[City] [nvarchar](50) NOT NULL,
	[County] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NOT NULL,
	[Postcode] [nvarchar](15) NOT NULL,
	[Facebook] [nvarchar](200) NULL,
	[Twitter] [nvarchar](200) NULL,
	[Google] [nvarchar](200) NULL,
	[Youtube] [nvarchar](200) NULL,
	[CharityNumber] [nvarchar](50) NOT NULL,
	[Approved] [bit] NOT NULL CONSTRAINT [DF_Organisation_Approved]  DEFAULT ((0)),
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
