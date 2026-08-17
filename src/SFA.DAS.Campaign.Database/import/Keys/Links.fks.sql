ALTER TABLE import.Links
ADD CONSTRAINT FK_Links_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);
GO