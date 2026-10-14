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
GO

CREATE INDEX IX_ClickedContacts_SendContactID ON import.ClickedContacts (SendContactID);
GO
CREATE INDEX IX_ClickedContacts_LinkID ON import.ClickedContacts (LinkID);
GO
CREATE INDEX IX_ClickedContacts_UserAgentID ON import.ClickedContacts (UserAgentID);
GO
