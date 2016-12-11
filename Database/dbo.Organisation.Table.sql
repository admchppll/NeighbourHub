USE [Volunteer]
GO
/****** Object:  Table [dbo].[Organisation]    Script Date: 11/12/2016 14:31:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organisation](
	[ID] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Phone] [smallint] NOT NULL,
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
	[Approved] [bit] NULL,
 CONSTRAINT [PK_Organisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
