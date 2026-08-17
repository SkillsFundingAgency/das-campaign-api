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
GO