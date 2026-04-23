CREATE PROCEDURE Usp_CampaignImportMetadata_Get
(
    @SendIds        VARCHAR(MAX) = NULL,
    @CampaignIds    VARCHAR(MAX) = NULL
)
AS
BEGIN

    SELECT  Id, SendId, CampaignId, IsImportComplete, ImportStartDate, ImportEndDate
    FROM    dbo.CampaignImportMetadata
    WHERE   (@SendIds IS NULL OR SendId IN (SELECT value FROM STRING_SPLIT(@SendIds, ',') WHERE RTRIM(value) <> ''))
            AND (@CampaignIds IS NULL OR CampaignId IN (SELECT value FROM STRING_SPLIT(@CampaignIds, ',') WHERE RTRIM(value) <> ''))    

END
GO
