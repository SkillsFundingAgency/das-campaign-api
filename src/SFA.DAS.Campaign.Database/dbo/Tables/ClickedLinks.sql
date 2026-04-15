CREATE TABLE dbo.ClickedLinks
(
	Id					BIGINT IDENTITY (1, 1) PRIMARY KEY,
	ExternalId			INT,
	CampaignId			BIGINT,
	ContactEmail		VARCHAR(255),
	ClickedDate			DATETIME2,
	FriendlyUrlName		VARCHAR(255),
	LinkId				INT,
	[Url]				VARCHAR(MAX),
	IsMonitored			BIT,
	EmailFormat			VARCHAR(255),
	IsSuspectedBot		BIT,
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