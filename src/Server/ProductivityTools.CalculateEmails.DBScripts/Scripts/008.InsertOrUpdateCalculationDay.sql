ALTER PROCEDURE [outlook].[UpdateLastCalculationDay]
	@date DATETIME,
	@mailCountAdd INT,
	@mailCountSent INT,
	@mailCountProcessed INT,
	@taskCountAdded INT,
	@taskCountRemoved INT,
	@taskCountFinished INT
AS
BEGIN
	SET NOCOUNT OFF;

	IF NOT EXISTS (SELECT 1 FROM [outlook].CalculateEmails WHERE [Date]=@date)
	BEGIN
		INSERT INTO [outlook].CalculateEmails ([Date],
		[MailCountAdd],
		[MailCountSent],
		[MailCountProcessed],
		[TaskCountAdded],
		[TaskCountRemoved],
		[TaskCountFinished]
		)
		VALUES (
		@date,
		@mailCountAdd,
		@mailCountSent,
		@mailCountProcessed,
		@taskCountAdded,
		@taskCountRemoved,
		@taskCountFinished)
	END
	ELSE
	BEGIN
		UPDATE [outlook].[CalculateEmails] SET
		[MailCountAdd]=@mailCountAdd,
		[MailCountSent]=@mailCountSent,
		[MailCountProcessed]=@mailCountProcessed,
		[TaskCountAdded]=@taskCountAdded,
		[TaskCountRemoved]=@taskCountRemoved,
		[TaskCountFinished]=@taskCountFinished
		WHERE  [Date]=@date
	END
END

GO