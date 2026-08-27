CREATE TABLE import.ClickedContacts (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    ClickDate DATETIME2(3) NULL,
    LinkID INT NULL,
    SendContactID INT NULL,
    UserAgentID INT NULL,
    FriendlyName NVARCHAR(255) NULL,
    IsSuspectedBOT BIT NULL,
    CONSTRAINT PK_ClickedContacts PRIMARY KEY (ID)
);

CREATE INDEX IX_ClickedContacts_SendContactID ON import.ClickedContacts (SendContactID);
CREATE INDEX IX_ClickedContacts_LinkID ON import.ClickedContacts (LinkID);
CREATE INDEX IX_ClickedContacts_UserAgentID ON import.ClickedContacts (UserAgentID);

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
