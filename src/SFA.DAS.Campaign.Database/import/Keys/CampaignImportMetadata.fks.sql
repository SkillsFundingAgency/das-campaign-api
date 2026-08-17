ALTER TABLE import.CampaignImportMetadata
ADD CONSTRAINT FK_CampaignImportMetadata_Sends
    FOREIGN KEY (SendID) REFERENCES import.Sends (ID);

ALTER TABLE import.CampaignImportMetadata
ADD CONSTRAINT FK_CampaignImportMetadata_Campaigns
    FOREIGN KEY (CampaignID) REFERENCES import.Campaigns (ID);
GO