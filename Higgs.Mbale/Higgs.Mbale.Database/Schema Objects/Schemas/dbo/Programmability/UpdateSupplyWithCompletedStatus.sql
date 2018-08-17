CREATE PROCEDURE [dbo].[UpdateSupplyWithCompletedStatus]
	@inPutSupplyId BIGINT,
	@statusId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateSupplyDetails

	
	Update Supply
	SET StatusId = @statusId,UpdatedBy = @userId,[TimeStamp] = GETDATE()
	WHERE SupplyId = @inPutSupplyId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateSupplyDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateSupplyDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH
