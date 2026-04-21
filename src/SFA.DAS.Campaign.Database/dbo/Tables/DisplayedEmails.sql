CREATE TABLE dbo.DisplayedEmails
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT,
	CampaignId			BIGINT,
	ContactEmail		VARCHAR(255),
	DisplayedDate		DATETIME2,
	[Format]			VARCHAR(255),
	TimeDisplayed		INT,
	IsSuspectedBot		BIT DEFAULT(0) NOT NULL,
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