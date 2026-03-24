CREATE PROCEDURE Usp_Campaigns_Get
(
    @CampaignIds    VARCHAR(MAX) = NULL
)
AS
BEGIN

    SELECT  Id, ExternalId, [Name], [Type], CreatedBy, CreatedOn, ModifiedBy, ModifiedOn,
            FirstSendDate, LastSendDate, FromEmailAddress, FromName, ReplyEmailAddress,
            [Subject], SubStatus, ContactCount, Account
    FROM    dbo.Campaigns WITH (NOLOCK)
    WHERE   (@CampaignIds IS NULL OR Id IN (SELECT id FROM STRING_SPLIT(@CampaignIds, ',') WHERE RTRIM(id) <> ''))

END
GO
