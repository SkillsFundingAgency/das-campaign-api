CREATE TABLE dbo.DisplayedEmails
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT NOT NULL,
	CampaignId			BIGINT NOT NULL,
	ContactEmail		VARCHAR(255) NOT NULL,
	DisplayedDate		DATETIME2 NOT NULL,
	[Format]			VARCHAR(255) NOT NULL,
	TimeDisplayed		INT,
	IsSuspectedBot		BIT NOT NULL,
	Device				VARCHAR(255),
	ClientName			VARCHAR(255),
	Os					VARCHAR(255),
	OsFamily			VARCHAR(255),
	IpAddress			VARCHAR(15),
	ClientType			VARCHAR(255),
	ClientFamily		VARCHAR(255),
	CONSTRAINT FK_DisplayedEmails_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO