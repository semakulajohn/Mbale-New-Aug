CREATE PROCEDURE [dbo].[Mark_LabourCost_AsDeleted]
	@inPutLabourCostId BIGINT,
	@userId NVARCHAR (128)
		
AS 


BEGIN TRY
 BEGIN TRANSACTION TRA_UpdateLabourCostRelatedDetails

	
	Update LabourCost
	SET Deleted =1,DeletedBy = @userId,DeletedOn = GETDATE()
	WHERE LabourCostId = @inPutLabourCostId AND Deleted = 0
	
 

 COMMIT TRANSACTION TRA_UpdateLabourCostRelatedDetails

		PRINT 'Operation Successful.'
		
 END TRY
 BEGIN CATCH 
		IF (@@TRANCOUNT > 0)
			BEGIN
				ROLLBACK TRANSACTION TRA_UpdateLabourCostRelatedDetails
				PRINT 'Error detected, all changes reversed'
			END
END CATCH

