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