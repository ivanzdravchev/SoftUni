-- Part I – Queries for Gringotts Database

-- 01. Import the database and send the total count of records from the one and only table to Mr. Bodrog.
--     Make sure nothing got lost.
SELECT COUNT(*) AS Count
FROM WizzardDeposits

-- 02. Select the size of the longest magic wand. Rename the new column appropriately.
SELECT MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits

-- 03. For wizards in each deposit group show the longest magic wand. Rename the new column appropriately.
SELECT DepositGroup, MAX(MagicWandSize) AS LongestMagicWand
FROM WizzardDeposits
GROUP BY DepositGroup

-- 04. Select the two deposit groups with the lowest average wand size.
SELECT TOP 2 DepositGroup
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY AVG(MagicWandSize)

-- 05. Select all deposit groups and their total deposit sums.
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
GROUP BY DepositGroup

-- 06. Select all deposit groups and their total deposit sums but only for the wizards who have their
--     magic wands crafted by Ollivander family.
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

-- 07. Select all deposit groups and their total deposit sums but only for the wizards who have their
--     magic wands crafted by Ollivander family. Filter total deposit amounts lower than 150000.
--     Order by total deposit amount in descending order.
SELECT DepositGroup, SUM(DepositAmount) AS TotalSum
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY TotalSum DESC

-- 08. Create a query that selects: Deposit group, Magic wand creator, Minimum deposit charge for each group.
--     Select the data in ascending ordered by MagicWandCreator and DepositGroup.
SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge) AS MinDepositCharge
FROM WizzardDeposits
GROUP BY DepositGroup, MagicWandCreator
ORDER BY MagicWandCreator, DepositGroup

-- 09. Write down a query that creates 7 different groups based on their age. Age groups should be as follows:
--     [0-10], [11-20], [21-30], [31-40], [41-50], [51-60], [61+].
--     The query should return Age groups and Count of wizards in it
SELECT AgeGroup, COUNT(*) AS WizardCount
FROM
(
	SELECT
		CASE
			WHEN Age BETWEEN 0 AND 10 THEN '[0-10]'
			WHEN Age BETWEEN 11 AND 20 THEN '[11-20]'
			WHEN Age BETWEEN 21 AND 30 THEN '[21-30]'
			WHEN Age BETWEEN 31 AND 40 THEN '[31-40]'
			WHEN Age BETWEEN 41 AND 50 THEN '[41-50]'
			WHEN Age BETWEEN 51 AND 60 THEN '[51-60]'
			WHEN Age > 60 THEN '[61+]'
		END AS AgeGroup
	FROM WizzardDeposits
) AS Groups
GROUP BY AgeGroup

-- 10. Write a query that returns all unique wizard first letters of their first names only if they
--     have deposit of type Troll Chest. Order them alphabetically. Use GROUP BY for uniqueness.
SELECT SUBSTRING(FirstName, 1, 1) AS Letter
FROM WizzardDeposits
WHERE DepositGroup = 'Troll Chest'
GROUP BY SUBSTRING(FirstName, 1, 1)

-- 11. Get the average interest of all deposit groups split by whether the deposit has expired or not. Select deposits
--     with start date after 01/01/1985. Order the data descending by Deposit Group and ascending by Expiration Flag.
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS AverageInterest
FROM WizzardDeposits
WHERE DepositStartDate >= '1985/01/01'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired

-- 12. Compare the deposits of every wizard with the wizard after him. If a wizard is the last one in the database,
--     simply ignore it. In the end you have to sum the difference between the deposits.
--     At the end your query should return a single value: the SUM of all differences.
SELECT SUM(Diff) FROM
(
	SELECT
		--LEAD(DepositAmount, 1) OVER(ORDER BY Id) AS NextEl,
		DepositAmount - LEAD(DepositAmount, 1) OVER(ORDER BY Id) AS Diff
	FROM WizzardDeposits
) AS Result


-- Part II – Queries for SoftUni Database

-- 13. Create a query that shows the total sum of salaries for each department. Order by DepartmentID.
--     Your query should return: DepartmentID and the total salary TotalSalary
SELECT DepartmentID, SUM(Salary) AS TotalSalary
FROM Employees
GROUP BY DepartmentID
ORDER BY DepartmentID

-- 14. Select the minimum salary from the employees for departments with ID (2, 5, 7) but only for those hired after
--     01/01/2000. Your query should return: DepartmentID and the minimum salary MinimumSalary
SELECT DepartmentID, MIN(Salary) MinimumSalary
FROM Employees
WHERE DepartmentID IN (2, 5, 7) AND HireDate >= '2000/01/01'
GROUP BY DepartmentID

-- 15. Select all employees who earn more than 30000 into a new table. Then delete all employees who have
--     ManagerID = 42 (in the new table). Then increase the salaries of all employees with DepartmentID = 1 by 5000.
--     Finally, select the average salaries in each department.
SELECT * INTO NewTable
FROM Employees
WHERE Salary > 30000

DELETE FROM NewTable
WHERE ManagerID = 42

UPDATE NewTable
SET Salary += 5000
WHERE DepartmentID = 1

SELECT DepartmentID, AVG(Salary) AS AverageSalary
FROM NewTable
GROUP BY DepartmentID

-- 16. Find the max salary for each department. Filter those, which have max salaries NOT in the range 30000 – 70000.
SELECT DepartmentID, MAX(Salary) AS MaxSalary
FROM Employees
GROUP BY DepartmentID
HAVING MAX(Salary) NOT BETWEEN 30000 AND 70000

-- 17. Count the salaries of all employees who don’t have a manager.
SELECT COUNT(*) AS Count
FROM Employees
WHERE ManagerID IS NULL

-- 18. Find the third highest salary in each department if there is such.
SELECT DISTINCT DepartmentID, Salary AS ThirdHighestSalary FROM
(
	SELECT *,
		DENSE_RANK() OVER (PARTITION BY DepartmentID ORDER BY Salary DESC) Rank
	FROM Employees
) as Result
WHERE Rank = 3

-- 19. Write a query that returns: FirstName, LastName, DepartmentID.
--     Select all employees who have salary higher than the average salary of their respective departments.
--     Select only the first 10 rows. Order by DepartmentID.
SELECT TOP 10 FirstName, LastName, e.DepartmentID  FROM
(
	SELECT DepartmentID, AVG(Salary) AS AverageSalary
	FROM Employees
	GROUP BY DepartmentID
) AS Result
INNER JOIN Employees e ON Result.DepartmentID = e.DepartmentID
WHERE Salary > AverageSalary
ORDER BY DepartmentID