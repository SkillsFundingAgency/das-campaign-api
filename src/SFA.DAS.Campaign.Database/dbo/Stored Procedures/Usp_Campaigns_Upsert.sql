CREATE PROCEDURE Usp_Campaign_Upsert
(
    @CampaignId         BIGINT,
    @ExternalId         INT = NULL,
    @Name               VARCHAR(MAX) = NULL,
    @Type               VARCHAR(255) = NULL,
    @CreatedBy          VARCHAR(255) = NULL,
    @CreatedOn          DATETIME2(7) = NULL,
    @ModifiedBy         VARCHAR(255) = NULL,
    @ModifiedOn         DATETIME2(7) = NULL,
    @FirstSendDate      DATETIME2(7) = NULL,
    @LastSendDate       DATETIME2(7) = NULL,
    @FromEmailAddress   VARCHAR(255) = NULL,
    @FromName           VARCHAR(255) = NULL,
    @ReplyEmailAddress  VARCHAR(255) = NULL,
    @Subject            VARCHAR(MAX) = NULL,
    @SubStatus          VARCHAR(255) = NULL,
    @ContactCount       INT = NULL,
    @Account            VARCHAR(255) = NULL
)
AS
BEGIN

    MERGE INTO dbo.Campaigns AS [Target]
    USING (SELECT @CampaignId AS CampaignId) AS [Source] ON [Target].Id = [Source].CampaignId
    WHEN MATCHED THEN
        UPDATE SET
            [Name] = @Name,
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
        INSERT (ExternalId, [Name], [Type], CreatedBy, CreatedOn, ModifiedBy, ModifiedOn, 
                FirstSendDate, LastSendDate, FromEmailAddress, FromName, ReplyEmailAddress, 
                [Subject], SubStatus, ContactCount, Account)
        VALUES (@ExternalId, @Name, @Type, @CreatedBy, @CreatedOn, @ModifiedBy, @ModifiedOn,
                @FirstSendDate, @LastSendDate, @FromEmailAddress, @FromName, @ReplyEmailAddress,
                @Subject, @SubStatus, @ContactCount, @Account);

END
GO
