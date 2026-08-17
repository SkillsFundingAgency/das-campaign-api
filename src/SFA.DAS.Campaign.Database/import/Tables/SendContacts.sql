CREATE TABLE import.SendContacts (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    SendID INT NULL,
    ContactID INT NULL,
    PublicIP VARCHAR(45) NULL,
    CONSTRAINT PK_SendContacts PRIMARY KEY (ID)
);
GO