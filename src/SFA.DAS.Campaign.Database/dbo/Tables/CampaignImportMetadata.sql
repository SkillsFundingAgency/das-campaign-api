CREATE TABLE dbo.CampaignImportMetadata
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	CampaignId			BIGINT NOT NULL,
	IsImportComplete	BIT NOT NULL,
	ImportStartDate		DATETIME2 NOT NULL,
	ImportEndDate		DATETIME2,
	CONSTRAINT FK_CampaignImportMetadata_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO