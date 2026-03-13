CREATE TABLE dbo.DisplayedEmails
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT NOT NULL,
	CampaignId			BIGINT NOT NULL,
	ContactEmail		VARCHAR(255) NOT NULL,
	DisplayedDate		DATETIME2 NOT NULL,
	[Format]			VARCHAR(255) NOT NULL,
	TimeDisplayed		INT NOT NULL,
	IsSuspectedBot		BIT NOT NULL,
	Device				VARCHAR(255) NOT NULL,
	ClientName			VARCHAR(255) NOT NULL,
	Os					VARCHAR(255) NOT NULL,
	OsFamily			VARCHAR(255) NOT NULL,
	IpAddress			VARCHAR(15) NOT NULL,
	ClientType			VARCHAR(255) NOT NULL,
	ClientFamily		VARCHAR(255) NOT NULL,
	CONSTRAINT FK_DisplayedEmails_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO