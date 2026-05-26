CREATE TABLE dbo.CampaignImportMetadata
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	SendId				INT,
	CampaignId			BIGINT,
	IsImportComplete	BIT,
	ImportStartDate		DATETIME2,
	ImportEndDate		DATETIME2
) ON [PRIMARY]
GO