CREATE TABLE import.CampaignImportMetadata (
    ID INT NOT NULL IDENTITY(1,1),
    SendID INT NULL,
    CampaignID INT NULL,
    IsImportComplete	BIT,
	ImportStartDate		DATETIME2,
	ImportEndDate		DATETIME2,
    CONSTRAINT PK_CampaignImportMetadata PRIMARY KEY (ID)
);
GO

CREATE INDEX IX_CampaignImportMetadata_SendID ON import.CampaignImportMetadata (SendID);
CREATE INDEX IX_CampaignImportMetadata_CampaignID ON import.CampaignImportMetadata (CampaignID);
GO

ALTER TABLE import.CampaignImportMetadata
ADD CONSTRAINT FK_CampaignImportMetadata_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);

ALTER TABLE import.CampaignImportMetadata
ADD CONSTRAINT FK_CampaignImportMetadata_Campaigns
    FOREIGN KEY (CampaignID) REFERENCES import.Campaigns (ID);
GO
