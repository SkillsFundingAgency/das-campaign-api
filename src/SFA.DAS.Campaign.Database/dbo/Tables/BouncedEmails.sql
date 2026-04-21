CREATE TABLE dbo.BouncedEmails
(
	Id				BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId		INT,
	CampaignId		BIGINT,
	ContactEmail	VARCHAR(255),
	BounceDate		DATETIME2,
	BounceReason	VARCHAR(255),
	BounceType		VARCHAR(255),
	ResponseText	VARCHAR(MAX),
	CONSTRAINT FK_BouncedEmails_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO