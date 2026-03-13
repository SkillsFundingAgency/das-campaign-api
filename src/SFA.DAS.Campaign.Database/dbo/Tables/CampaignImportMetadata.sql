CREATE TABLE dbo.CampaignImportMetadata
(
	Id					INT IDENTITY (1, 1) PRIMARY KEY,
	CampaignId			INT NOT NULL,
	IsImportComplete	BIT NOT NULL,
	ImportStartDate		DATETIME2 NOT NULL,
	ImportEndDate		DATETIME2 NOT NULL
) ON [PRIMARY]
GO