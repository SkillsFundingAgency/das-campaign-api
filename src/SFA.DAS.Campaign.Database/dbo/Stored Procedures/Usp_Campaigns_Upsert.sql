CREATE PROCEDURE Usp_Campaigns_Upsert
(
    @ExternalCampaignId     BIGINT,
    @ExternalSendId         INT = NULL,
    @CampaignName           VARCHAR(MAX) = NULL,
    @SendName               VARCHAR(MAX) = NULL,
    @Type                   VARCHAR(255) = NULL,
    @CreatedBy              VARCHAR(255) = NULL,
    @CreatedOn              DATETIME2(7) = NULL,
    @ModifiedBy             VARCHAR(255) = NULL,
    @ModifiedOn             DATETIME2(7) = NULL,
    @FirstSendDate          DATETIME2(7) = NULL,
    @LastSendDate           DATETIME2(7) = NULL,
    @FromEmailAddress       VARCHAR(255) = NULL,
    @FromName               VARCHAR(255) = NULL,
    @ReplyEmailAddress      VARCHAR(255) = NULL,
    @Subject                VARCHAR(MAX) = NULL,
    @SubStatus              VARCHAR(255) = NULL,
    @ContactCount           INT = NULL,
    @Account                VARCHAR(255) = NULL
)
AS
BEGIN

    DECLARE @Result TABLE (Id INT);

    MERGE INTO dbo.Campaigns AS [Target]
    USING (SELECT @ExternalCampaignId AS CampaignId, @ExternalSendId AS SendId) AS [Source] 
            ON [Target].ExternalCampaignId = [Source].CampaignId AND [Target].ExternalSendId = [Source].SendId

    WHEN MATCHED THEN
        UPDATE SET
            SendName = @SendName,
            CampaignName = @CampaignName,
            [Type] = @Type,
            CreatedBy = @CreatedBy,
            CreatedOn = @CreatedOn,
            ModifiedBy = @ModifiedBy,
            ModifiedOn = @ModifiedOn,
            FirstSendDate = @FirstSendDate,
            LastSendDate = @LastSendDate,
            FromEmailAddress = @FromEmailAddress,
            FromName = @FromName,
            ReplyEmailAddress = @ReplyEmailAddress,
            [Subject] = @Subject,
            SubStatus = @SubStatus,
            ContactCount = @ContactCount,
            Account = @Account

    WHEN NOT MATCHED THEN
        INSERT (
            ExternalSendId, ExternalCampaignId, SendName, CampaignName, [Type],
            CreatedBy, CreatedOn, ModifiedBy, ModifiedOn,
            FirstSendDate, LastSendDate, FromEmailAddress, FromName,
            ReplyEmailAddress, [Subject], SubStatus, ContactCount, Account
        )
        VALUES (
            @ExternalSendId, @ExternalCampaignId, @SendName, @CampaignName, @Type,
            @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn,
            @FirstSendDate, @LastSendDate, @FromEmailAddress, @FromName,
            @ReplyEmailAddress, @Subject, @SubStatus, @ContactCount, @Account
        )
    OUTPUT inserted.Id INTO @Result;

    -- Return the Id
    SELECT TOP 1 Id FROM @Result;
END
GO
