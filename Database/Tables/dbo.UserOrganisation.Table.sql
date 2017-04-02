USE [Volunteer]
GO
/****** Object:  Table [dbo].[UserOrganisation]    Script Date: 02/04/2017 03:55:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserOrganisation](
	[ID] [int] NOT NULL,
	[UserID] [nvarchar](128) NOT NULL,
	[OrganisationID] [smallint] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Denied] [bit] NOT NULL,
	[Admin] [bit] NOT NULL,
	[Created] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_UserOrganisation] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserOrganisation] ADD  CONSTRAINT [DF_UserOrganisation_Approved]  DEFAULT ((0)) FOR [Approved]
GO
ALTER TABLE [dbo].[UserOrganisation] ADD  CONSTRAINT [DF_UserOrganisation_Denied]  DEFAULT ((0)) FOR [Denied]
GO
ALTER TABLE [dbo].[UserOrganisation] ADD  CONSTRAINT [DF_UserOrganisation_Admin]  DEFAULT ((0)) FOR [Admin]
GO
ALTER TABLE [dbo].[UserOrganisation] ADD  CONSTRAINT [DF_UserOrganisation_Created]  DEFAULT (getdate()) FOR [Created]
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
