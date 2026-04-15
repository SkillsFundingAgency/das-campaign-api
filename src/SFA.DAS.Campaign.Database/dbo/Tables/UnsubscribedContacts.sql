CREATE TABLE dbo.UnsubscribedContacts
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT,
	CampaignId			BIGINT,
	ContactEmail		VARCHAR(255),
	UnsubscribedDate	DATETIME2,
	IsGlobalUnscribe	BIT,
	IsComplaint			BIT,
	CONSTRAINT FK_UnsubscribedContacts_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO