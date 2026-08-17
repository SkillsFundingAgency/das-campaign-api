ALTER TABLE import.UserAgents
ADD CONSTRAINT FK_UserAgents_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);
GO