CREATE TABLE [outlook].[CalculateEmails]
(
	CalculateEmailsId INT PRIMARY KEY IDENTITY(1,1),
	Date DATE NOT NULL,
	MailCountAdd INTEGER NOT NULL DEFAULT (0),
	MailCountSent INTEGER NOT NULL DEFAULT (0),
	MailCountProcessed INTEGER NOT NULL DEFAULT (0),
	TaskCountAdded INTEGER NOT NULL DEFAULT (0),
	TaskCountRemoved INTEGER NOT NULL DEFAULT (0),
	TaskCountFinished INTEGER NOT NULL DEFAULT (0),
)

					
