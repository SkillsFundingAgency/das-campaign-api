CREATE TABLE import.SendContacts (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    SendID INT NULL,
    ContactID INT NULL,
    PublicIP VARCHAR(45) NULL,
    CONSTRAINT PK_SendContacts PRIMARY KEY (ID)
);

CREATE INDEX IX_SendContacts_SendID ON import.SendContacts (SendID);
CREATE INDEX IX_SendContacts_ContactID ON import.SendContacts (ContactID);

ALTER TABLE import.SendContacts
ADD CONSTRAINT FK_SendContacts_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);

ALTER TABLE import.SendContacts
ADD CONSTRAINT FK_SendContacts_Contacts
    FOREIGN KEY (ContactID) REFERENCES import.Contacts (ID);
GO
