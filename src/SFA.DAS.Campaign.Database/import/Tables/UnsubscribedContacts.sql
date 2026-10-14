CREATE TABLE import.UnsubscribedContacts (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    IsGlobalUnsubscribe BIT NULL,
    UnsubscribeDate DATETIME2(3) NULL,
    SendContactID INT NULL,
    IsComplaint BIT NULL,
    CONSTRAINT PK_UnsubscribedContacts PRIMARY KEY (ID)
);
GO

CREATE INDEX IX_UnsubscribedContacts_SendContactID ON import.UnsubscribedContacts (SendContactID);
GO

ALTER TABLE import.UnsubscribedContacts
ADD CONSTRAINT FK_UnsubscribedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);
GO
