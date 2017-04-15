USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[clearNotifications]    Script Date: 15/04/2017 14:01:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[clearNotifications]
AS
BEGIN
	
	SET NOCOUNT ON;

	DELETE FROM	[Notification] 
	WHERE	Viewed = 1
END

GO
