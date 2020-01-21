USE PersonalFinance
GO

SET XACT_ABORT ON
GO

BEGIN TRANSACTION

DELETE FROM dbo.Categories
DBCC CHECKIDENT ([dbo.Categories], reseed, 0)

DELETE FROM dbo.Transactions
DBCC CHECKIDENT ([dbo.Transactions], reseed, 0)

INSERT INTO dbo.Categories (Name)
VALUES	('Clothing'),
		('Discretionary'),
		('Education'),
		('Financial'),
		('Food'),
		('Healthcare'),
		('Housing'),
		('Income'),
		('Other'),
		('Transportation'),
		('Utilities')

DECLARE @i INT = 0
WHILE @i < 100
BEGIN

	DECLARE @Date DATE = (SELECT DATEADD(DAY, RAND(CHECKSUM(NEWID())) * (1 + DATEDIFF(DAY, DATEADD(DAY, -30, GETDATE()), GETDATE())), DATEADD(DAY, -30, GETDATE())))
	DECLARE @CategoryId INT = (SELECT TOP 1 Id FROM dbo.Categories WHERE Name <> 'Income' ORDER BY NEWID())
	DECLARE @Amount DECIMAL(9, 2) = (SELECT FLOOR(RAND() * (50000 - 500 + 1)) + 500) / 100.00
	DECLARE @Memo NVARCHAR(500) = (SELECT REPLACE(LEFT(REPLICATE(NEWID(), 5), FLOOR(RAND() * (150 - 50 + 1)) + 50), '-', ' '))
	DECLARE @IsCredit BIT = 0
	
	IF (SELECT FLOOR(RAND() * (15 - 1 + 1)) + 1) = 1
	BEGIN
		SET @CategoryId = (SELECT Id FROM dbo.Categories WHERE Name = 'Income')
		SET @Amount = (SELECT FLOOR(RAND() * (300000 - 100000 + 1)) + 100000) / 100.00
		SET @IsCredit = 1
	END

	INSERT INTO dbo.Transactions (Date, CategoryId, Memo, Amount, IsCredit)
	VALUES (@Date, @CategoryId, @Memo, @Amount, @IsCredit)

	SET @i = @i + 1
END

COMMIT