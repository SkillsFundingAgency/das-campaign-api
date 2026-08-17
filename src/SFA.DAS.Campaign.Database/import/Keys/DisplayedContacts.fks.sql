ALTER TABLE import.DisplayedContacts
ADD CONSTRAINT FK_DisplayedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);

ALTER TABLE import.DisplayedContacts
ADD CONSTRAINT FK_DisplayedContacts_UserAgents
    FOREIGN KEY (UserAgentID) REFERENCES import.UserAgents (ID);
GO