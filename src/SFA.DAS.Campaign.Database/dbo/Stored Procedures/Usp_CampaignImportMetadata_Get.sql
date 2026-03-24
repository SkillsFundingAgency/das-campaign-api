CREATE PROCEDURE Usp_CampaignImportMetadata_Get
(
    @CampaignIds    VARCHAR(MAX) = NULL
)
AS
BEGIN

    SELECT  Id, CampaignId, IsImportComplete, ImportStartDate, ImportEndDate
    FROM    dbo.CampaignImportMetadata
    WHERE   (@CampaignIds IS NULL OR CampaignId IN (SELECT value FROM STRING_SPLIT(@CampaignIds, ',') WHERE RTRIM(value) <> ''))

END
GO
