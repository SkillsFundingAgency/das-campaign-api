ALTER TABLE import.BouncedContacts
ADD CONSTRAINT FK_BouncedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);
GO