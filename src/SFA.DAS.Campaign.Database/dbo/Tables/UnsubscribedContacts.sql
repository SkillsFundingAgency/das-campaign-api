CREATE TABLE dbo.UnsubscribedContacts
(
	Id					INT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT NOT NULL,
	CampaignId			INT NOT NULL,
	ContactEmail		VARCHAR(255) NOT NULL,
	UnsubscribedDate	DATETIME2 NOT NULL,
	IsGlobalUnscribe	BIT NOT NULL,
	IsComplaint			BIT NOT NULL
) ON [PRIMARY]
GO