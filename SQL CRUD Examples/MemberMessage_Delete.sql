
CREATE PROCEDURE [dbo].[MemberMessage_Delete]
	@Id int   -- pass Id of which message to delete as parameter
	
AS

delete [dbo].[MemberMessage_Delete]
where [Id] = @Id

return 0 
go 

/*
	delete from [dbo].[MemberMessage]

*/