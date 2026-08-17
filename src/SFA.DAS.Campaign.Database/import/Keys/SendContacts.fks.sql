ALTER TABLE import.SendContacts
ADD CONSTRAINT FK_SendContacts_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);

ALTER TABLE import.SendContacts
ADD CONSTRAINT FK_SendContacts_Contacts
    FOREIGN KEY (ContactID) REFERENCES import.Contacts (ID);
GO