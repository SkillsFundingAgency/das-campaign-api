ALTER TABLE import.Sends
ADD CONSTRAINT FK_Sends_Campaigns
    FOREIGN KEY (CampaignID) REFERENCES import.Campaigns (ID);
GO