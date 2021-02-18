-- Part I – Queries for SoftUni Database

-- 01. Create stored procedure usp_GetEmployeesSalaryAbove35000 that returns all employees’ first and last names
--     for whose salary is above 35000.
GO
CREATE PROC usp_GetEmployeesSalaryAbove35000
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary > 35000
GO

-- 02. Create stored procedure usp_GetEmployeesSalaryAboveNumber that accept a number (of type DECIMAL(18,4)) as
--     parameter and returns all employees’ first and last names whose salary is above or equal to the given number.
GO
CREATE PROC usp_GetEmployeesSalaryAboveNumber (@SalaryTreshold decimal(18, 4) = 0)
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE Salary >= @salaryTreshold
GO

EXEC usp_GetEmployeesSalaryAboveNumber 70000

-- 03. Write a stored procedure usp_GetTownsStartingWith that accept string as parameter and returns all town names
--     starting with that string.
GO
CREATE PROC usp_GetTownsStartingWith (@Start varchar(50))
AS
	SELECT Name
	FROM Towns
	WHERE Name LIKE @start + '%'
GO

EXEC usp_GetTownsStartingWith 'S'

-- 04. Write a stored procedure usp_GetEmployeesFromTown that accepts town name as parameter and return the
--     employees’ first and last name that live in the given town.
GO
CREATE PROC usp_GetEmployeesFromTown (@Town varchar(50))
AS
	SELECT FirstName, LastName
	FROM Employees e
	JOIN Addresses a ON e.AddressID = a.AddressID
	JOIN Towns t ON a.TownID = t.TownID
	WHERE t.Name = @town
GO

EXEC usp_GetEmployeesFromTown 'Sofia'

-- 05. Write a function ufn_GetSalaryLevel(@salary DECIMAL(18,4)) that receives salary of an employee and returns
--     the level of the salary. If salary is < 30000 return "Low", if it is between 30000 and 50000 (inclusive)
--     return "Average" and if it is > 50000 return "High"
GO
CREATE FUNCTION ufn_GetSalaryLevel (@Salary decimal(18, 4))
RETURNS varchar(10)
AS
BEGIN
	DECLARE @level varchar(10)
	IF (@Salary < 30000)
		SET @level = 'Low'
	ELSE IF (@Salary < 50000)
		SET @level = 'Average'
	ELSE IF (@Salary > 50000)
		SET @level = 'High'
	RETURN @level
END
GO

SELECT Salary, dbo.ufn_GetSalaryLevel(Salary) AS SalaryLevel
FROM Employees

-- 06. Write a stored procedure usp_EmployeesBySalaryLevel that receive as parameter level of salary (low, average or
--     high) and print the names of all employees that have given level of salary. You should use the function -
--     "dbo.ufn_GetSalaryLevel(@Salary)", which was part of the previous task, inside your "CREATE PROCEDURE …" query.
GO
CREATE PROC usp_EmployeesBySalaryLevel (@SalaryLevel varchar(10))
AS
	SELECT FirstName, LastName
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = @SalaryLevel
GO

EXEC usp_EmployeesBySalaryLevel 'High'

-- 07. Define a function ufn_IsWordComprised(@setOfLetters, @word) that returns true or false depending on that if the
--    word is a comprised of the given set of letters.
GO
CREATE OR ALTER FUNCTION ufn_IsWordComprised (@setOfLetters nvarchar(100), @word nvarchar(100))
RETURNS bit
AS
BEGIN
	DECLARE @index int
	SET @index = 1
	WHILE (@index <= LEN(@setOfLetters))
	BEGIN
		SET @word = REPLACE(@word, SUBSTRING(@setOfLetters, @index, 1), '')
		SET @index += 1
	END

	IF (LEN(@word) = 0)
		RETURN 1
	RETURN 0
END
GO

SELECT 'oistmiahf' AS SetOfLetters,
	'Sofia' AS Word,
	dbo.ufn_IsWordComprised('oistmiahf', 'hal')

-- 08. Write a procedure with the name usp_DeleteEmployeesFromDepartment (@departmentId INT) which
--     deletes all Employees from a given department. Delete these departments from the Departments table too.
--     Finally SELECT the number of employees from the given department. If the delete statements are correct the select
--     query should return 0. After completing that exercise restore your database to revert all changes.
GO
CREATE PROC usp_DeleteEmployeesFromDepartment (@departmentId int)
AS
	ALTER TABLE Employees
	ALTER COLUMN DepartmentID int NULL

	ALTER TABLE Departments
	ALTER COLUMN ManagerID int NULL

	UPDATE Departments
	SET ManagerID = NULL
	WHERE DepartmentID = @departmentId

	UPDATE Employees
	SET DepartmentID = NULL
	WHERE DepartmentID = @departmentId

	UPDATE Employees
	SET DepartmentID = NULL
	WHERE DepartmentID = @departmentId

	DELETE FROM Departments
	WHERE DepartmentID = @departmentId

	DELETE FROM EmployeesProjects
	WHERE EmployeeID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID IS NULL)

	UPDATE Employees
	SET ManagerID = NULL
	WHERE ManagerID IN (SELECT EmployeeID FROM Employees WHERE DepartmentID IS NULL)

	DELETE FROM Employees
	WHERE DepartmentID IS NULL

	SELECT COUNT(*) AS Count
    FROM Employees
    WHERE DepartmentID = @departmentId
GO

EXEC usp_DeleteEmployeesFromDepartment 10


-- Part II – Queries for Bank Database

-- 09. You are given a database schema with tables AccountHolders(Id (PK), FirstName, LastName, SSN) and Accounts(Id
--     (PK), AccountHolderId (FK), Balance). Write a stored procedure usp_GetHoldersFullName that selects the full
--     names of all people.
GO
CREATE PROC usp_GetHoldersFullName
AS
	SELECT FirstName + ' ' + LastName AS FullName FROM AccountHolders
GO

EXEC usp_GetHoldersFullName

-- 10. Your task is to create a stored procedure usp_GetHoldersWithBalanceHigherThan that accepts a number as a
--     parameter and returns all people who have more money in total of all their accounts than the supplied number.
--     Order them by first name, then by last name
GO
CREATE PROC usp_GetHoldersWithBalanceHigherThan (@minBalance decimal(18, 4))
AS
	SELECT FirstName AS [First Name], LastName AS [Last Name] FROM
	(
		SELECT a.AccountHolderId, SUM(Balance) AS TotalMoney
		FROM Accounts a
		JOIN AccountHolders ah ON a.AccountHolderId = ah.Id
		GROUP BY a.AccountHolderId
	) AS Result
	JOIN AccountHolders ah ON Result.AccountHolderId = ah.Id
	WHERE TotalMoney > @minBalance
	ORDER BY [First Name], [Last Name]
GO

EXEC usp_GetHoldersWithBalanceHigherThan 50000.500

-- 11. Your task is to create a function ufn_CalculateFutureValue that accepts as parameters – sum (decimal), yearly
--     interest rate (float) and number of years(int). It should calculate and return the future value of the
--     initial sum rounded to the fourth digit after the decimal delimiter. Using the following formula:
--     I – Initial sum, R – Yearly interest rate, T – Number of years
GO
CREATE FUNCTION ufn_CalculateFutureValue (@sum decimal(18, 4), @rate float, @years int)
RETURNS decimal(18, 4)
AS
BEGIN
	DECLARE @result decimal(18, 4)
	SET @result = @sum * (POWER(1 + @rate, @years))
	return @result
END
GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)

-- 12. Your task is to create a stored procedure usp_CalculateFutureValueForAccount that uses the function from the
--     previous problem to give an interest to a person's account for 5 years, along with information about his/her
--     account id, first name, last name and current balance. It should take the AccountId and the interest rate as
--     parameters. Again you are provided with “dbo.ufn_CalculateFutureValue” function which was part of the previous task.
GO
CREATE PROC usp_CalculateFutureValueForAccount (@AccountId int, @InterestRate float)
AS
	SELECT 
		a.AccountHolderId AS [Account Id],
		ah.FirstName AS [First Name],
		ah.LastName AS [Last Name],
		a.Balance AS [Current Balance],
		dbo.ufn_CalculateFutureValue (Balance, @InterestRate, 5) AS [Balance in 5 years]
	FROM Accounts a
	JOIN AccountHolders ah ON a.AccountHolderId = ah.Id
	WHERE a.Id = @AccountId
GO

EXEC usp_CalculateFutureValueForAccount 1, 0.1


-- Part III – Queries for Diablo Database

-- 13. Create a function ufn_CashInUsersGames that sums the cash of odd rows. Rows must be ordered by cash in
--     descending order. The function should take a game name as a parameter and return the result as table. Submit
--     only your function in. Execute the function over the following game names, ordered exactly like: "Love in a mist".
GO
CREATE FUNCTION ufn_CashInUsersGames (@GameName varchar(50))
RETURNS TABLE
AS
	RETURN
	(
		SELECT SUM(Cash) AS SumCash FROM
		(
			SELECT Cash, ROW_NUMBER() OVER (ORDER BY ug.Cash DESC) AS Row
			FROM UsersGames ug
			JOIN Games g ON g.Id = ug.GameId
			WHERE g.Name = @GameName
		) AS Result
		WHERE Row % 2 = 1
	)
GO

SELECT * FROM dbo.ufn_CashInUsersGames ('Rose Lavanda')