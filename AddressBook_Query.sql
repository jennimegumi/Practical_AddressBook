CREATE PROCEDURE sp_DisplayInfo
AS 
BEGIN

	SELECT * FROM dbo.addressbook_db

END 

--READ ALL ADDRESS

CREATE PROCEDURE [DBO].[usp_ReadData]
AS BEGIN
	SELECT * FROM dbo.addressbook_db WITH (NOLOCK)
END


--GET BY CITY
CREATE PROCEDURE [DBO].[usp_GetCity]
( @City char )
AS BEGIN 
	SELECT City FROM dbo.addressbook_db WITH (NOLOCK)
	WHERE @City = City
END

--GET BY ID
CREATE PROCEDURE [DBO].[usp_GetPersonID]
( @ID int )
AS BEGIN 
	SELECT PersonID FROM dbo.addressbook_db WITH (NOLOCK)
	WHERE @ID = PersonID
END



-- INSERT 
CREATE PROC [DBO].[usp_InsertData]
(
	@PersonID		int,
	@PersonName		varchar(50),
	@PersonAddress	varchar(50),
	@City			varchar(50)
)
AS BEGIN
	INSERT INTO dbo.addressbook_db(PersonID, PersonName, PersonAddress, City)
	VALUES ( @PersonID, @PersonName, @PersonAddress, @City )
END

-- UPDATE
CREATE PROC [DBO].[usp_UpdateAddress]
(
	@PersonID		int,
	@PersonName		varchar(50),
	@PersonAddress	varchar(50),
	@City			varchar(50)
)
AS BEGIN
	Declare @RowCount INT = 0 
	
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM dbo.addressbook_db WITH (NOLOCK) WHERE @PersonID = PersonID)

		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN 
					UPDATE dbo.addressbook_db
					SET 
						PersonName = @PersonName,
						PersonAddress = @PersonAddress,
						City = @City
					WHERE PersonName = @PersonName
				COMMIT TRAN
		END
	END TRY
	BEGIN CATCH 
		ROLLBACK TRAN 
	END CATCH 
END


-- DELETE 

CREATE PROC [DBO].[usp_DeleteAddress]
(
	@PersonID	int
)

AS BEGIN
	Declare @RowCount INT = 0 
	
	BEGIN TRY
		SET @RowCount = (SELECT COUNT(1) FROM dbo.addressbook_db WITH (NOLOCK) WHERE @PersonID = PersonID)

		IF (@RowCount > 0)
			BEGIN
				BEGIN TRAN 
					DELETE FROM dbo.addressbook_db
					WHERE PersonID = @PersonID
				COMMIT TRAN
		END
	END TRY
	BEGIN CATCH 
		ROLLBACK TRAN 
	END CATCH 
END