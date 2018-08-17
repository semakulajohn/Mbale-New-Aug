CREATE PROCEDURE [dbo].[Mark_MachineRepair_AsDeleted]
	@inPutRepairId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateMachineRepairDetails

	
	Update MachineRepair
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE MachineRepairId = @inPutRepairId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateMachineRepairDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateMachineRepairDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH


