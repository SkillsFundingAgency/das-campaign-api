CREATE TABLE import.Links (
    ID INT NOT NULL,
    URL NVARCHAR(2048) NULL,
    FriendlyName NVARCHAR(255) NULL,
    IsMonitored BIT NULL,
    ReceivedInMessageFormat NVARCHAR(20) NULL,
    SendID INT NULL,
    CONSTRAINT PK_Links PRIMARY KEY (ID)
);
GO