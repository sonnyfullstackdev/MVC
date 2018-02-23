-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[MemberMessage_Insert]
	@Id int Output  -- same as columns for Author table. Output means the database will asign a Id number for us
	,@ParentId int
	,@FromMemberProfileId int 
	,@ToMemberProfileId int 
	,@MessageSubject nvarchar(200) = null
	,@MessageText nvarchar(MAX) 

	--------
/*

##### TEST


    Declare @IdOut int 
	,@ParentId int = null
	,@FromMemberProfileId int 
	,@ToMemberProfileId int 
	,@MessageSubject nvarchar(200) = null
	,@MessageText nvarchar(MAX) 
	,@CreatedDate datetime = GetutcDate()

	insert into [dbo].[MemberMessage] (ParentId, FromMemberProfileId, ToMemberProfileId, MessageSubject, MessageText, CreatedDate)
	values (1, 2, 1, 'Good Morning', 'Why Hello', @CreatedDate);

	select * from [dbo].[MemberMessage]

*/
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
declare @CreatedDate datetime = GetutcDate()

    -- Insert statements for procedure here
	Insert	[dbo].[MemberMessage_Insert]( 
			ParentId
			,FromMemberProfileId	
			,ToMemberProfileId
			,MessageSubject
			,MessageText
			,CreatedDate
	)
	Values (
		@ParentId
		,@FromMemberProfileId
		,@ToMemberProfileId
		,@MessageSubject
		,@MessageText
		,@CreatedDate
	);

	set @ID = SCOPE_IDENTITY()
End	
GO



