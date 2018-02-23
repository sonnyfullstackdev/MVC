

ALTER PROCEDURE [dbo].[MemberMessage_Update]
	@Id int   -- pass Id of which message to delete as parameter
	,@ParentId int = null
	,@FromMemberProfileId int 
	,@ToMemberProfileId int 
	,@MessageSubject nvarchar(200) = null
	,@MessageText nvarchar(MAX) 
	
	
	
AS

	 /*
	EXEC	[dbo].[MemberMessage_Update]
	        @Id = 1,
			@ParentId = null,
			@FromMemberProfileId = 1,
			@ToMemberProfileId = 2,
			@MessageSubject = 'My new Subject',
			@MessageText = 'Hello world!!!'
			
     
     SELECT *
	 FROM MemberMessage
	 WHERE Id = 1
	 */

BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @ModifiedDate datetime2 = GetutcDate()

    -- Insert statements for procedure here
	Update	dbo.MemberMessage
	set
		ParentId = @ParentId,
		FromMemberProfileId = @FromMemberProfileId,
		ToMemberProfileId = @ToMemberProfileId,
		MessageSubject = @MessageSubject,
		MessageText = @MessageText,
		ModifiedDate =@ModifiedDate
	where Id = @id

End	
