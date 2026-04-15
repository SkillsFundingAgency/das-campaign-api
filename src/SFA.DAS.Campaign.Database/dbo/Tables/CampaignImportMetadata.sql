CREATE TABLE dbo.CampaignImportMetadata
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	CampaignId			BIGINT,
	IsImportComplete	BIT,
	ImportStartDate		DATETIME2,
	ImportEndDate		DATETIME2,
	CONSTRAINT FK_CampaignImportMetadata_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO