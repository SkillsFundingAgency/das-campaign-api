CREATE PROCEDURE Usp_CampaignImportMetadata_Upsert
(
    @SendId             INT,
    @CampaignId         BIGINT,
    @IsImportComplete   BIT,
    @ImportStartDate    DATETIME2(7),
    @ImportEndDate      DATETIME2(7) = NULL
)
AS
BEGIN

    DECLARE @Result TABLE (Id INT);

    IF ISNULL(@SendId, 0) = 0
    BEGIN
        SELECT TOP 1 @SendId = ExternalId FROM dbo.Campaigns WHERE Id = @CampaignId;
    END

    MERGE INTO dbo.CampaignImportMetadata AS [Target]
    USING (SELECT @CampaignId AS CampaignId, @SendId AS SendId) AS [Source] 
            ON [Target].CampaignId = [Source].CampaignId AND [Target].SendId = [Source].SendId
    
    WHEN MATCHED THEN
        UPDATE SET
            IsImportComplete = @IsImportComplete,
            ImportStartDate = @ImportStartDate,
            ImportEndDate = @ImportEndDate

    WHEN NOT MATCHED THEN
        INSERT (SendId, CampaignId, IsImportComplete, ImportStartDate, ImportEndDate)
        VALUES (@SendId, @CampaignId, @IsImportComplete, @ImportStartDate, @ImportEndDate)
    OUTPUT inserted.SendId INTO @Result;

    -- Return the Id
    SELECT TOP 1 Id FROM @Result;
END
GO
