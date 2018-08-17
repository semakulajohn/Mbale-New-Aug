CREATE PROCEDURE [dbo].[Mark_OtherExpense_AsDeleted]
	@inPutOtherExpenseId BIGINT,
	@userId NVARCHAR (128)
		
AS 

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateOtherExpenseDetails

	
	Update OtherExpense
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE OtherExpenseId = @inPutOtherExpenseId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateOtherExpenseDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateOtherExpenseDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH



