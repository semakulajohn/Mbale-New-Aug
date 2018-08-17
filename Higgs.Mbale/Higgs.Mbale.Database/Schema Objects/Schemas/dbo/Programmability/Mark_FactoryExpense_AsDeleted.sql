CREATE PROCEDURE [dbo].[Mark_FactoryExpense_AsDeleted]
	@inPutFactoryExpenseId BIGINT,
	@userId NVARCHAR (128)
		
AS 

BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateFactoryExpenseDetails

	
	Update FactoryExpense
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE FactoryExpenseId = @inPutFactoryExpenseId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateFactoryExpenseDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateFactoryExpenseDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH


