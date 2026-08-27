CREATE TABLE import.DisplayedContacts (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    DisplayDate DATETIME2(3) NULL,
    Format NVARCHAR(20) NULL,
    SendContactID INT NULL,
    UserAgentID INT NULL,
    TimeInSecondsSpentReadingEmail INT NULL,
    IsSuspectedBOT BIT NULL,
    CONSTRAINT PK_DisplayedContacts PRIMARY KEY (ID)
);


CREATE INDEX IX_DisplayedContacts_SendContactID ON import.DisplayedContacts (SendContactID);
CREATE INDEX IX_DisplayedContacts_UserAgentID ON import.DisplayedContacts (UserAgentID);


ALTER TABLE import.DisplayedContacts
ADD CONSTRAINT FK_DisplayedContacts_SendContacts
    FOREIGN KEY (SendContactID) REFERENCES import.SendContacts (ID);

ALTER TABLE import.DisplayedContacts
ADD CONSTRAINT FK_DisplayedContacts_UserAgents
    FOREIGN KEY (UserAgentID) REFERENCES import.UserAgents (ID);
GO
