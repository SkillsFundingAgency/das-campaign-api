CREATE PROCEDURE Usp_Campaign_Upsert
(
    @CampaignId         BIGINT,
    @ExternalId         INT,
    @Name               VARCHAR(MAX),
    @Type               VARCHAR(255) = NULL,
    @CreatedBy          VARCHAR(255),
    @CreatedOn          DATETIME2(7),
    @ModifiedBy         VARCHAR(255) = NULL,
    @ModifiedOn         DATETIME2(7) = NULL,
    @FirstSendDate      DATETIME2(7),
    @LastSendDate       DATETIME2(7) = NULL,
    @FromEmailAddress   VARCHAR(255),
    @FromName           VARCHAR(255),
    @ReplyEmailAddress  VARCHAR(255),
    @Subject            VARCHAR(MAX),
    @SubStatus          VARCHAR(255),
    @ContactCount       INT,
    @Account            VARCHAR(255)
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
