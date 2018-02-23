
ALTER PROCEDURE [dbo].[MemberMessage_SelectByToMemberProfileId]
	@ToMemberProfileId int --pass Id as Parameter

			/*
##TEST##
Exec [dbo].[MemberMessage_SelectByToMemberProfileId]
	@ToMemberProfileId = 1
Go
*/
AS

    -- Select statements for procedure here
	Select	Id
			,ParentId
			,FromMemberProfileId	
			,ToMemberProfileId
			,MessageSubject
			,MessageText
			,CreatedDate
	
	From [dbo].[MemberMessage] With (NoLock)
	where ToMemberProfileId =@ToMemberProfileId
	return 0

