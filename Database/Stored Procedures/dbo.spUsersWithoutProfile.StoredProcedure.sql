USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[spUsersWithoutProfile]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spUsersWithoutProfile]
AS 
BEGIN

DECLARE	@Count INT

SELECT	@Count = ISNULL(COUNT(*),0) 
FROM	[User] u 
	LEFT JOIN Profile p on p.UserID = u.ID 
WHERE	p.ID IS NULL

SELECT TOP 1 @Count [Count]

END

GO
