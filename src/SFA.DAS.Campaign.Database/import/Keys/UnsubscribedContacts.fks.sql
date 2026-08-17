ALTER TABLE import.UnsubscribedContacts
ADD CONSTRAINT FK_UnsubscribedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);
GO