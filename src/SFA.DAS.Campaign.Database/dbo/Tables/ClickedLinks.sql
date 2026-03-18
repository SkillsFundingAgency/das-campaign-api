CREATE TABLE dbo.ClickedLinks
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT NOT NULL,
	CampaignId			BIGINT NOT NULL,
	ContactEmail		VARCHAR(255) NOT NULL,
	ClickedDate			DATETIME2 NOT NULL,
	FriendlyUrlName		VARCHAR(255),
	LinkId				INT NOT NULL,
	[Url]				VARCHAR(MAX) NOT NULL,
	IsMonitored			BIT NOT NULL,
	EmailFormat			VARCHAR(255) NOT NULL,
	IsSuspectedBot		BIT NOT NULL,
	Device				VARCHAR(255),
	ClientName			VARCHAR(255),
	Os					VARCHAR(255),
	OsFamily			VARCHAR(255),
	IpAddress			VARCHAR(15),
	ClientType			VARCHAR(255),
	ClientFamily		VARCHAR(255),
	CONSTRAINT FK_ClickedLinks_Campaigns FOREIGN KEY ( CampaignId ) REFERENCES dbo.Campaigns ( Id )
) ON [PRIMARY]
GO