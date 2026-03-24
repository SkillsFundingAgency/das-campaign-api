CREATE PROCEDURE Usp_CampaignImportMetadata_Upsert
(
    @CampaignId         BIGINT,
    @IsImportComplete   BIT,
    @ImportStartDate    DATETIME2(7),
    @ImportEndDate      DATETIME2(7) = NULL
)
AS
BEGIN

    MERGE INTO dbo.CampaignImportMetadata AS [Target]
    USING (SELECT @CampaignId AS CampaignId) AS [Source] ON [Target].CampaignId = [Source].CampaignId
    WHEN MATCHED THEN
        UPDATE SET
            IsImportComplete = @IsImportComplete,
            ImportStartDate = @ImportStartDate,
            ImportEndDate = @ImportEndDate
    WHEN NOT MATCHED THEN
        INSERT (CampaignId, IsImportComplete, ImportStartDate, ImportEndDate)
        VALUES (@CampaignId, @IsImportComplete, @ImportStartDate, @ImportEndDate);

END
GO
