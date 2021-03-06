USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[withdrawVolunteer]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[withdrawVolunteer] (@VolunteerID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Volunteer NVARCHAR(128),
		@Host NVARCHAR(128),
		@TransactionID INT;

	SELECT	@EventID = v.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Volunteer = V.VolunteerID,
		@Host = t.SenderID
	FROM	Volunteer v
		INNER JOIN [Transaction] t ON v.VolunteerID = t.RecipientID 
			AND t.EventID = v.EventID
	WHERE	v.ID = @VolunteerID

	UPDATE	[Transaction]
	SET	Cancelled = 1
	WHERE	ID = @TransactionID

	INSERT	[Transaction] (	SenderID, 
				RecipientID,
				Amount,
				EventID,
				Complete, 
				ParentTransaction)
	VALUES	(	@Volunteer,
			@Host,
			@Points,
			@EventID,
			1,
			@TransactionID
		)

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Host 
END

GO
