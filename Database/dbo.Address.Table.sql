USE [Volunteer]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 11/12/2016 14:31:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NULL,
	[Name] [nchar](10) NULL,
	[Address1] [nvarchar](50) NULL,
	[Address2] [nvarchar](50) NOT NULL,
	[City] [nvarchar](50) NULL,
	[County] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Postcode] [nvarchar](15) NULL,
	[Notes] [nvarchar](max) NULL,
	[Long] [float] NOT NULL,
	[Lat] [float] NOT NULL,
	[GeoLocation] [geography] NULL,
	[Default] [bit] NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_User]
GO
