CREATE PROCEDURE [outlook].[UpdateLastCalculationDay]
	@calculateEmailsId INT,
	@mailCountAdd INT,
	@mailCountSent INT,
	@mailCountProcessed INT,
	@taskCountAdded INT,
	@taskCountRemoved INT,
	@taskCountFinished INT


AS
BEGIN
	SET NOCOUNT OFF;
	UPDATE [outlook].[CalculateEmails] SET
    [MailCountAdd]=@mailCountAdd,
	[MailCountSent]=@mailCountSent,
	[MailCountProcessed]=@mailCountProcessed,
	[TaskCountAdded]=@taskCountAdded,
	[TaskCountRemoved]=@taskCountRemoved,
	[TaskCountFinished]=@taskCountFinished
	WHERE  [CalculateEmailsId]=@calculateEmailsId

END

GO