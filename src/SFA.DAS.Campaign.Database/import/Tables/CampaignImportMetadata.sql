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