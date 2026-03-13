CREATE TABLE dbo.BouncedEmails
(
	Id				BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId		INT NOT NULL,
	CampaignId		BIGINT NOT NULL,
	ContactEmail	VARCHAR(255) NOT NULL,
	BounceDate		DATETIME2 NOT NULL,
	BounceReason	VARCHAR(255) NOT NULL,
	BounceType		VARCHAR(255) NOT NULL,
	ResponseText	VARCHAR(MAX) NOT NULL,
	CONSTRAINT FK_BouncedEmails_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO