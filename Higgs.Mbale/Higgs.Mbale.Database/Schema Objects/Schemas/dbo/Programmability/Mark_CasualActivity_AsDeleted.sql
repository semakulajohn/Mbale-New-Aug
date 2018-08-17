CREATE PROCEDURE [dbo].[Mark_CasualActivity_AsDeleted]
	@inPutCasualActivityId BIGINT,
	@userId NVARCHAR (128)
		
AS 

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateCasualActivityDetails

	
	Update CasualActivity
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE CasualActivityId = @inPutCasualActivityId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateCasualActivityDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateCasualActivityDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH


