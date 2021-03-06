USE [Volunteer]
GO
/****** Object:  StoredProcedure [dbo].[reverseTransaction]    Script Date: 24/04/2017 03:46:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[reverseTransaction] (@TransactionID INT)
AS
BEGIN
	
	SET NOCOUNT ON;
	
	DECLARE	@EventID INT,
		@Points SMALLINT,
		@Sender NVARCHAR(128),
		@Recipient NVARCHAR(128),
		@Complete BIT;

	SELECT	@EventID = t.EventID,
		@Points = t.Amount,
		@TransactionID = t.ID,
		@Recipient = t.RecipientID,
		@Sender = t.SenderID,
		@Complete = t.Complete
	FROM	[Transaction] t 
	WHERE	t.ID = @TransactionID

	UPDATE	[Transaction]
	SET	Cancelled = 1
	WHERE	ID = @TransactionID

	INSERT	[Transaction] (	SenderID, 
				RecipientID,
				Amount,
				EventID,
				Complete, 
				ParentTransaction)
	VALUES	(	@Recipient,
			@Sender,
			@Points,
			@EventID,
			1,
			@TransactionID
		)

	UPDATE	Profile
	SET	Balance = Balance + @Points
	WHERE	UserID = @Sender 

	IF @Complete = 1 
	BEGIN
		UPDATE	Profile
		SET	Balance = Balance - @Points
		WHERE	UserID = @Recipient
	END

	INSERT INTO	Audit (	UserID, 
				EventID, 
				AuditMessage)
	VALUES	(	@Recipient, 
			@EventID, 
			'Transaction Reversed: #' + @TransactionID + ' (Recipient)'),
		(	@Sender, 
			@EventID, 
			'Transaction Reversed: #' + @TransactionID + ' (Sender)')
END

GO
