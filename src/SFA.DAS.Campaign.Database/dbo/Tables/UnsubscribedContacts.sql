CREATE TABLE dbo.UnsubscribedContacts
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT,
	CampaignId			BIGINT,
	ContactEmail		VARCHAR(255),
	UnsubscribedDate	DATETIME2,
	IsGlobalUnscribe	BIT DEFAULT(0),
	IsComplaint			BIT DEFAULT(0)
) ON [PRIMARY]
GO