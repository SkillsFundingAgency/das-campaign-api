CREATE TABLE import.BouncedContacts (
    ID INT NOT NULL,
    BounceReason NVARCHAR(255) NULL,
    BounceType NVARCHAR(100) NULL,
    BounceTypeID INT NULL,
    BounceDate DATETIME2(3) NULL,
    SubaccountID INT NULL,
    SendContactID INT NULL,
    ResponseText NVARCHAR(2000) NULL,
    CONSTRAINT PK_BouncedContacts PRIMARY KEY (ID)
);
GO

CREATE INDEX IX_BouncedContacts_SendContactID ON import.BouncedContacts (SendContactID);
GO

ALTER TABLE import.BouncedContacts
ADD CONSTRAINT FK_BouncedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);
GO
