CREATE TABLE import.UserAgents (
    ID INT NOT NULL,
    SubaccountID INT NULL,
    SendContactID INT NULL,
    DisplayDate DATETIME2(3) NULL,
    IPAddress VARCHAR(45) NULL,
    ClientName NVARCHAR(200) NULL,
    ClientType NVARCHAR(100) NULL,
    ClientFamily NVARCHAR(150) NULL,
    Device NVARCHAR(150) NULL,
    OperatingSystemFamily NVARCHAR(150) NULL,
    OperatingSystem NVARCHAR(150) NULL,
    IsSuspectedBOT BIT NULL,
    CONSTRAINT PK_UserAgents PRIMARY KEY (ID)
);
GO