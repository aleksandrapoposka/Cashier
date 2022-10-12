CREATE TRIGGER [dbo].[OrderDetailsTrigger] ON [dbo].[OrderDetails]
    AFTER INSERT, UPDATE, DELETE
AS
    BEGIN
        SET NOCOUNT ON;
		DECLARE @Operation VARCHAR(15)

		IF EXISTS (SELECT 0 FROM inserted)
		BEGIN
		   IF EXISTS (SELECT 0 FROM deleted)
		   BEGIN 
			  SELECT @Operation = 'UPDATE'
		   END ELSE
		   BEGIN
			  SELECT @Operation = 'INSERT'
		   END
		END ELSE 
		BEGIN
		   SELECT @Operation = 'DELETE'
		END 
	
	--declare variables
        DECLARE @id BIGINT ,
			@OrderId BIGINT,
            @ArticleId BIGINT ,
            @Count DECIMAL(18,6),
		    @Price DECIMAL(18,6),
			@ModifiedBy nvarchar(MAX)

	--set variables
       IF @Operation = 'INSERT'
	   BEGIN
	   SELECT  @id = i.id ,
                @OrderId = i.OrderId ,
				@ArticleId = i.ArticleId ,
                @Count = i.Count ,
                @Price = i.Price ,
				@ModifiedBy = i.ModifiedBy
        FROM    inserted i ;
        END
		ELSE IF @Operation = 'UPDATE'
	   BEGIN
	   SELECT  @id = i.id ,
                @OrderId = i.OrderId ,
				@ArticleId = i.ArticleId ,
                @Count = i.Count ,
                @Price = i.Price ,
				@ModifiedBy = i.ModifiedBy
        FROM    deleted i ;
        END
		ELSE IF @Operation = 'DELETE'
	    BEGIN
	    SELECT  @id = i.id ,
                @OrderId = i.OrderId ,
				@ArticleId = i.ArticleId ,
                @Count = i.Count ,
                @Price = i.Price ,
				@ModifiedBy = i.ModifiedBy
        FROM    deleted i ;
        END

     Insert Into OrderDetailsHistory
	 ( 
		[Id],
		[OrderId],
		[ArticleId],
		[Count],
		[Price],
		[ModifiedBy],
		[ModifiedOn],
		[Operation]
		)

		Select @id,@OrderId,@ArticleId,@Count,@Price,@ModifiedBy,GETUTCDATE(),@Operation
	 
	 END