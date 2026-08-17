ALTER TABLE import.ClickedContacts
ADD CONSTRAINT FK_ClickedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);

ALTER TABLE import.ClickedContacts
ADD CONSTRAINT FK_ClickedContacts_Links
    FOREIGN KEY (LinkID) REFERENCES import.Links (ID);

ALTER TABLE import.ClickedContacts
ADD CONSTRAINT FK_ClickedContacts_UserAgents
    FOREIGN KEY (UserAgentID) REFERENCES import.UserAgents (ID);
GO