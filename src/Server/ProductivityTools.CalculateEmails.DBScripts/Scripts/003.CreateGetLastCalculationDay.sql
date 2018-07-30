CREATE PROCEDURE [outlook].[GetLastCalculationDay]
	@date DATE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	IF NOT EXISTS (SELECT 1 FROM [outlook].CalculateEmails WHERE [Date]=@date)
		BEGIN
			INSERT INTO [outlook].CalculateEmails ([Date])
			VALUES (@date)
		END
	SELECT [CalculateEmailsId],[Date],[MailCountAdd],[MailCountSent],[MailCountProcessed]
	,[TaskCountAdded],[TaskCountRemoved],[TaskCountFinished]
	FROM [outlook].[CalculateEmails]
	WHERE [Date]=@date

END