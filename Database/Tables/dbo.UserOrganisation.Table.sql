USE [Volunteer]
GO
/****** Object:  Table [dbo].[UserOrganisation]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrganisation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[OrganisationID] [smallint] NOT NULL,
	[Approved] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Approved]  DEFAULT ((0)),
	[Denied] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Denied]  DEFAULT ((0)),
	[Admin] [bit] NOT NULL CONSTRAINT [DF_UserOrganisation_Admin]  DEFAULT ((0)),
	[Created] [smalldatetime] NOT NULL CONSTRAINT [DF_UserOrganisation_Created]  DEFAULT (getdate()),
 CONSTRAINT [PK_UserOrganisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisation_Organisation] FOREIGN KEY([OrganisationID])
REFERENCES [dbo].[Organisation] ([ID])
GO
ALTER TABLE [dbo].[UserOrganisation] CHECK CONSTRAINT [FK_UserOrganisation_Organisation]
GO
ALTER TABLE [dbo].[UserOrganisation]  WITH CHECK ADD  CONSTRAINT [FK_UserOrganisation_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserOrganisation] CHECK CONSTRAINT [FK_UserOrganisation_User]
GO
