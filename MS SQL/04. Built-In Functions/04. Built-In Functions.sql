-- Part I – Queries for SoftUni Database

-- 01. Write a SQL query to find first and last names of all employees whose first name starts with "SA".
SELECT FirstName, LastName FROM Employees
WHERE LEFT(FirstName, 2) = 'SA'

-- 02. Write a SQL query to find first and last names of all employees whose last name contains "ei".
SELECT FirstName, LastName FROM Employees
WHERE CHARINDEX('ei', LastName) > 0

-- 03. Write a SQL query to find the first names of all employees in the departments with ID 3 or 10 and 
--     whose hire year is between 1995 and 2005 inclusive.
SELECT FirstName FROM Employees
WHERE DepartmentID IN (3, 10) AND
DATEPART(YEAR, HireDate) BETWEEN 1995 AND 2005

-- 04. Write a SQL query to find the first and last names of all employees whose job titles does not contain "engineer".
SELECT FirstName, LastName FROM Employees
WHERE CHARINDEX('engineer', JobTitle) = 0

-- 05. Write a SQL query to find town names that are 5 or 6 symbols long and order them alphabetically by town name.
SELECT Name FROM Towns
WHERE LEN(Name) IN (5, 6)
ORDER BY Name

-- 06. Write a SQL query to find all towns that start with letters M, K, B or E. Order them alphabetically by town name.
SELECT TownID, Name FROM Towns
WHERE SUBSTRING(Name, 1, 1) IN ('M', 'K', 'B', 'E')
ORDER BY Name

-- 07. Write a SQL query to find all towns that does not start with letters R, B or D. Order them alphabetically by name.
SELECT TownId, Name FROM Towns
WHERE SUBSTRING(Name, 1, 1) NOT IN ('R', 'B', 'D')
ORDER BY Name

-- 08. Write a SQL query to create view V_EmployeesHiredAfter2000 with first and last name to all employees hired after 2000 year.
CREATE VIEW V_EmployeesHiredAfter2000 AS
SELECT FirstName, LastName FROM Employees
WHERE DATEPART(YEAR, HireDate) > 2000

-- 09. Write a SQL query to find the names of all employees whose last name is exactly 5 characters long.
SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5

-- 10. Write a query that ranks all employees using DENSE_RANK. In the DENSE_RANK function, employees need to be
--     partitioned by Salary and ordered by EmployeeID. You need to find only the employees whose Salary is between
--     10000 and 50000 and order them by Salary in descending order.
SELECT EmployeeID, FirstName, LastName, Salary,
	DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS Rank
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000
ORDER BY Salary DESC

-- 11. Use the query from the previous problem and upgrade it, so that it finds only the employees whose Rank is 2 and
--     again, order them by Salary (descending).
SELECT * FROM
	(SELECT EmployeeID, FirstName, LastName, Salary,
		DENSE_RANK() OVER (PARTITION BY Salary ORDER BY EmployeeID) AS Rank
	FROM Employees
	WHERE Salary BETWEEN 10000 AND 50000) AS Result
WHERE Rank = 2
ORDER BY Salary DESC


-- Part II – Queries for Geography Database

-- 12. Find all countries that holds the letter "A" in their name at least 3 times (case insensitively),
--     sorted by ISO code. Display the country name and ISO code.
SELECT CountryName, IsoCode FROM Countries
WHERE CountryName LIKE '%a%a%a%'
ORDER BY IsoCode

--Solution 2
--SELECT [CountryName], [IsoCode] FROM Countries
--    WHERE LEN([CountryName]) - LEN(REPLACE([CountryName], 'A', '')) >= 3 
--        ORDER BY [IsoCode] ASC

-- 13. Combine all peak names with all river names, so that the last letter of each peak name is the same as the first letter
--     of its corresponding river name. Display the peak names, river names, and the obtained mix (mix should be in
--     lowercase). Sort the results by the obtained mix.
SELECT PeakName, RiverName, LOWER(PeakName + SUBSTRING(RiverName, 2, LEN(RiverName))) AS Mix
FROM Peaks, Rivers
WHERE RIGHT(PeakName, 1) = LEFT(RiverName, 1)
ORDER BY Mix


-- Part III – Queries for Diablo Database

-- 14. Find the top 50 games ordered by start date, then by name of the game. Display only games from 2011 and 2012
--     year. Display start date in the format "yyyy-MM-dd".
SELECT TOP 50 Name, FORMAT(Start, 'yyyy-MM-dd') AS Start FROM Games
WHERE DATEPART(YEAR, Start) IN (2011, 2012)
ORDER BY Start, Name

-- 15. Find all users along with information about their email providers. Display the username and email provider. Sort the
--     results by email provider alphabetically, then by username.
SELECT Username, SUBSTRING(Email, CHARINDEX('@', Email) + 1, LEN(Email)) AS Provider FROM Users
ORDER BY Provider, Username

-- 16. Find all users along with their IP addresses sorted by username alphabetically. Display only rows that IP address
--     matches the pattern: "***.1^.^.***".
SELECT Username, IpAddress FROM Users
WHERE IpAddress LIKE '___.1%.%.___'
ORDER BY Username

-- 17. Find all games with part of the day and duration sorted by game name alphabetically then by duration
--     (alphabetically, not by the timespan) and part of the day (all ascending). Parts of the day should be Morning (time is
--     >= 0 and < 12), Afternoon (time is >= 12 and < 18), Evening (time is >= 18 and < 24). Duration should be Extra Short
--     (smaller or equal to 3), Short (between 4 and 6 including), Long (greater than 6) and Extra Long (without duration).
SELECT Name,
	CASE
		WHEN DATEPART(HOUR, Start) BETWEEN 0 AND 11 THEN 'Morning'
		WHEN DATEPART(HOUR, Start) BETWEEN 12 AND 17 THEN 'Afternoon'
		WHEN DATEPART(HOUR, Start) BETWEEN 18 AND 23 THEN 'Evening'
	END AS [Part of the Day],
	CASE
		WHEN Duration BETWEEN 0 AND 3 THEN 'Extra Short'
		WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
		WHEN Duration > 6 THEN 'Long'
		WHEN Duration IS NULL THEN 'Extra Long'
	END AS Duration
FROM Games
ORDER BY Name, Duration


-- Part IV – Date Functions Queries

-- 18. You are given a table Orders(Id, ProductName, OrderDate) filled with data. Consider that the payment for that
--     order must be accomplished within 3 days after the order date. Also the delivery date is up to 1 month. Write a
--     query to show each product’s name, order date, pay and deliver due dates.
SELECT ProductName, OrderDate, DATEADD(DAY, 3, OrderDate) AS [Pay Due], DATEADD(MONTH, 1, OrderDate) AS [Deliver Due]
FROM Orders