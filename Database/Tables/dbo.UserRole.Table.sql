USE [Volunteer]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 24/04/2017 03:45:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
	[IdentityUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.User_IdentityUser_Id] FOREIGN KEY([IdentityUser_Id])
REFERENCES [dbo].[User] ([ID])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.User_IdentityUser_Id]
GO
