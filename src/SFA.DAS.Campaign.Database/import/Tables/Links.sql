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

CREATE INDEX IX_Links_SendID ON import.Links (SendID);
GO

ALTER TABLE import.Links
ADD CONSTRAINT FK_Links_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);
GO
