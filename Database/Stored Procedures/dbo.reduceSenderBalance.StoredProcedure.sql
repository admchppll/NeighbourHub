USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[reduceSenderBalance]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[reduceSenderBalance] (@TransactionID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@Points SMALLINT,
		@Sender NVARCHAR(128);

	SELECT	@Points = t.Amount,
		@Sender = t.SenderID
	FROM	[Transaction] t 
	WHERE	t.ID = @TransactionID

	UPDATE	Profile
	SET	Balance = Balance - @Points
	WHERE	UserID = @Sender
END
GO
