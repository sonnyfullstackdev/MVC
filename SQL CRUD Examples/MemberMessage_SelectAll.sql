

ALTER PROCEDURE [dbo].[MemberMessage_Select_All]
			/*
##TEST##
Exec [dbo].[MemberMessage_Select_All]
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

	return 0



