ALTER PROCEDURE [outlook].[GetCalculationDay]
	@startDate DATE,
	@endDate DATE
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [CalculateEmailsId],[Date],[MailCountAdd],[MailCountSent],[MailCountProcessed]
	,[TaskCountAdded],[TaskCountRemoved],[TaskCountFinished]
	FROM [outlook].[CalculateEmails]
	WHERE [Date] BETWEEN @startDate AND @endDate

END