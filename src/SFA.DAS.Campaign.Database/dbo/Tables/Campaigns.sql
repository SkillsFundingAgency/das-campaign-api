CREATE TABLE dbo.Campaigns
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT,
	[Name]				VARCHAR(MAX),
	[Type]				VARCHAR(255),
	CreatedBy			VARCHAR(255),
	CreatedOn			DATETIME2,
	ModifiedBy			VARCHAR(255),
	ModifiedOn			DATETIME2,
	FirstSendDate		DATETIME2,
	LastSendDate		DATETIME2,
	FromEmailAddress	VARCHAR(255),
	FromName			VARCHAR(255),
	ReplyEmailAddress	VARCHAR(255),
	[Subject]			VARCHAR(MAX),
	SubStatus			VARCHAR(255),
	ContactCount		INT,
	Account				VARCHAR(255)
) ON [PRIMARY]
GO
