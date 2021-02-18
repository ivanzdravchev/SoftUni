-- Part I – Queries for SoftUni Database

-- 01. Write a query that selects: EmployeeId, JobTitle, AddressId, AddressText.
--     Return the first 5 rows sorted by AddressId in ascending order.
SELECT TOP 5 e.EmployeeID, e.JobTitle, a.AddressID, a.AddressText
FROM Employees e
INNER JOIN Addresses a ON e.AddressID = a.AddressID
ORDER BY a.AddressID

-- 02. Write a query that selects: FirstName, LastName, Town, AddressText.
--     Sort by FirstName in ascending order then by LastName. Select first 50 employees.
SELECT TOP 50 e.FirstName, e.LastName, t.Name, a.AddressText
FROM Employees e
INNER JOIN Addresses a ON e.AddressID = a.AddressID
INNER JOIN Towns t ON a.TownID = t.TownID
ORDER BY e.FirstName, LastName

-- 03. Write a query that selects: EmployeeID, FirstName, LastName, DepartmentName.
--     Sort by EmployeeID in ascending order. Select only employees from "Sales" department.
SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE d.Name = 'Sales'
ORDER BY e.EmployeeID

-- 04. Write a query that selects: EmployeeID, FirstName, Salary, DepartmentName. Filter only employees
--     with salary higher than 15000. Return the first 5 rows sorted by DepartmentID in ascending order.
SELECT TOP 5 e.EmployeeID, e.FirstName, e.Salary, d.Name
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY d.DepartmentID

-- 05. Write a query that selects: EmployeeID, FirstName. Filter only employees without a project.
--     Return the first 3 rows sorted by EmployeeID in ascending order.
SELECT TOP 3 e.EmployeeID, e.FirstName
FROM Employees e
LEFT JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
WHERE ep.EmployeeID IS NULL
ORDER BY e.EmployeeID

-- 06. Write a query that selects: FirstName, LastName, HireDate, DeptName. Filter only employees hired after
--     1.1.1999 and are from either "Sales" or "Finance" departments, sorted by HireDate (ascending).
SELECT e.FirstName, e.LastName, e.HireDate, d.Name
FROM Employees e
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
WHERE e.HireDate > '1999/01/01' AND d.Name IN ('Sales', 'Finance')
ORDER BY e.HireDate

-- 07. Write a query that selects: EmployeeID, FirstName, ProjectName. Filter only employees
--     with a project which has started after 13.08.2002 and it is still ongoing (no end date).
--     Return the first 5 rows sorted by EmployeeID in ascending order.
SELECT TOP 5 e.EmployeeID, e.FirstName, p.Name
FROM Employees e
INNER JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects p ON ep.ProjectID = p.ProjectID
WHERE p.StartDate > '2002/08/13' AND p.EndDate IS NULL
ORDER BY e.EmployeeID

-- 08. Write a query that selects: EmployeeID, FirstName, ProjectName. Filter all the projects of employee
--     with Id 24. If the project has started during or after 2005 the returned value should be NULL.
SELECT e.EmployeeID, e.FirstName,
CASE
	WHEN p.StartDate >= '2005/01/01' THEN NULL
	ELSE p.Name
END AS ProjectName
FROM Employees e
INNER JOIN EmployeesProjects ep ON e.EmployeeID = ep.EmployeeID
INNER JOIN Projects p ON ep.ProjectID = p.ProjectID
WHERE e.EmployeeID = 24

-- 09. Write a query that selects: EmployeeID, FirstName, ManagerID, ManagerName. Filter all employees with
--     a manager who has ID equals to 3 or 7. Return all the rows, sorted by EmployeeID in ascending order.
SELECT e.EmployeeID, e.FirstName, e.ManagerID, m.FirstName AS ManagerName
FROM Employees e
INNER JOIN Employees m ON e.ManagerID = m.EmployeeID
WHERE e.ManagerID IN (3, 7)
ORDER BY e.EmployeeID

-- 10. Write a query that selects: EmployeeID, EmployeeName, ManagerName, DepartmentName. Show first 50 employees
--     with their managers and the departments they are in (show the departments of the employees). Order by EmployeeID.
SELECT TOP 50
	e.EmployeeID,
	e.FirstName + ' ' + e.LastName AS EmployeeName,
	m.FirstName + ' ' + m.LastName AS ManagerName,
	d.Name AS DepartmentName
FROM Employees e
INNER JOIN Employees m ON e.ManagerID = m.EmployeeID
INNER JOIN Departments d ON e.DepartmentID = d.DepartmentID
ORDER BY e.EmployeeID

-- 11. Write a query that returns the value of the lowest average salary of all departments.
-- Solution 1 - Group by
SELECT TOP 1 AVG(Salary) AS MinAverageSalary
FROM Employees
GROUP BY DepartmentID
ORDER BY MinAverageSalary

-- Solution 2 - Subqueries
SELECT TOP 1
(
	SELECT AVG(Salary) AS AverageSalary FROM Employees e
	WHERE d.DepartmentID = e.DepartmentID
) AS MinAverageSalary
FROM Departments d
ORDER BY MinAverageSalary


-- Part II – Queries for Geography Database

-- 12. Write a query that selects: CountryCode, MountainRange, PeakName, Elevation. Filter all peaks in Bulgaria
--     with elevation over 2835. Return all the rows sorted by elevation in descending order.
SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries c
INNER JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
	AND c.CountryCode = 'BG'
INNER JOIN Mountains m ON m.Id = mc.MountainId
INNER JOIN Peaks p ON p.MountainId = m.Id
WHERE p.Elevation > 2835
ORDER BY p.Elevation DESC

-- 13. Write a query that selects: CountryCode, MountainRanges. 
--     Filter the count of the mountain ranges in the United States, Russia and Bulgaria.
SELECT CountryCode, COUNT(MountainId)
FROM MountainsCountries
WHERE CountryCode IN ('US', 'RU', 'BG')
GROUP BY CountryCode

-- Solution 2 without group by
SELECT CountryCode,
(
	SELECT COUNT(mc.CountryCode) FROM MountainsCountries mc
	WHERE c.CountryCode = mc.CountryCode
)
FROM Countries c
WHERE c.CountryCode IN ('US', 'RU', 'BG')

-- 14. Write a query that selects: CountryName, RiverName. Find the first 5 countries
--     with or without rivers in Africa. Sort them by CountryName in ascending order.
SELECT TOP 5 CountryName, RiverName
FROM Countries c
LEFT JOIN CountriesRivers cr ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers r ON cr.RiverId = r.Id
WHERE c.ContinentCode = 'AF'
ORDER BY c.CountryName

-- 15. Write a query that selects: ContinentCode, CurrencyCode, CurrencyUsage. Find all continents and their most
--     used currency. Filter any currency that is used in only one country. Sort your results by ContinentCode.
SELECT ContinentCode, CurrencyCode, CurrencyUsage FROM
(
	SELECT ContinentCode, CurrencyCode, COUNT(*) AS CurrencyUsage,
		DENSE_RANK() OVER (PARTITION BY ContinentCode ORDER BY COUNT(CurrencyCode) DESC) as Rank
	FROM Countries
	GROUP BY ContinentCode, CurrencyCode
) as k
WHERE Rank = 1 AND CurrencyUsage > 1
ORDER BY ContinentCode

-- 16. Find all the count of all countries, which don’t have a mountain.
SELECT COUNT(*) AS Count
FROM Countries c
LEFT JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL

-- 17. For each country, find the elevation of the highest peak and the length of the longest river, sorted by the highest
--     peak elevation (from highest to lowest), then by the longest river length (from longest to smallest), then by country
--     name (alphabetically). Display NULL when no data is available in some of the columns. Limit only the first 5 rows.
SELECT TOP 5
	c.CountryName,
	MAX(p.Elevation) AS HighestPeakElevation,
	MAX(r.Length) AS LongestRiverLength
FROM Countries c
LEFT JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
LEFT JOIN Mountains m ON mc.MountainId = m.Id
LEFT JOIN Peaks p ON p.MountainId = m.Id
LEFT JOIN CountriesRivers cr ON c.CountryCode = cr.CountryCode
LEFT JOIN Rivers r ON cr.RiverId = r.Id
GROUP BY c.CountryName
ORDER BY HighestPeakElevation DESC, LongestRiverLength DESC, c.CountryName

-- 18. For each country, find the name and elevation of the highest peak, along with its mountain. When no peaks are
--     available in some country, display elevation 0, "(no highest peak)" as peak name and "(no mountain)" as mountain
--     name. When multiple peaks in some country have the same elevation, display all of them. Sort the results by
--     country name alphabetically, then by highest peak name alphabetically. Limit only the first 5 rows.
SELECT TOP 5
	Country,
	ISNULL(PeakName, '(no highest peak)') AS [Highest Peak Name],
	ISNULL(Elevation, 0) AS [Highest Peak Elevation],
	ISNULL(MountainRange, '(no mountain)') AS Mountain
FROM
(
	SELECT
		c.CountryName AS Country,
		p.PeakName,
		p.Elevation,
		m.MountainRange,
		DENSE_RANK() OVER (PARTITION BY c.CountryName ORDER BY p.Elevation DESC) AS Rank
	FROM Countries c
	LEFT JOIN MountainsCountries mc ON c.CountryCode = mc.CountryCode
	LEFT JOIN Mountains m ON mc.MountainId = m.Id
	LEFT JOIN Peaks p ON p.MountainId = m.Id
) AS k
WHERE Rank = 1
ORDER BY Country, [Highest Peak Name]