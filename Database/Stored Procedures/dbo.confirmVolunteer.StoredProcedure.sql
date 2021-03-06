USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[confirmVolunteer]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[confirmVolunteer] (@VolunteerID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Volunteer NVARCHAR(128),
		@TransactionID INT;

	SELECT	@EventID = v.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Volunteer = V.VolunteerID
	FROM	Volunteer v
		INNER JOIN [Transaction] t ON v.VolunteerID = t.RecipientID 
			AND t.EventID = v.EventID
	WHERE	v.ID = @VolunteerID

	UPDATE	[Transaction]
	SET	Complete = 1
	WHERE	ID = @TransactionID

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Volunteer 
END

GO
