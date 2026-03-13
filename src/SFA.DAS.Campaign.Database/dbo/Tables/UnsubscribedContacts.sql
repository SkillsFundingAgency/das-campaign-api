CREATE TABLE dbo.UnsubscribedContacts
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT NOT NULL,
	CampaignId			BIGINT NOT NULL,
	ContactEmail		VARCHAR(255) NOT NULL,
	UnsubscribedDate	DATETIME2 NOT NULL,
	IsGlobalUnscribe	BIT NOT NULL,
	IsComplaint			BIT NOT NULL,
	CONSTRAINT FK_UnsubscribedContacts_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO