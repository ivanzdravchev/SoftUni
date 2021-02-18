GO
CREATE DATABASE WMS
GO
USE WMS
GO

-- 01. DDL
CREATE TABLE Clients
(
	ClientId int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Phone char(12) NOT NULL
)

CREATE TABLE Mechanics
(
	MechanicId int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Address nvarchar(255) NOT NULL
)

CREATE TABLE Models
(
	ModelId int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL UNIQUE
)

CREATE TABLE Jobs
(
	JobId int PRIMARY KEY IDENTITY,
	ModelId int REFERENCES Models(ModelId) NOT NULL,
	Status nvarchar(11) NOT NULL DEFAULT 'Pending',
	ClientId int REFERENCES Clients(ClientId) NOT NULL,
	MechanicId int REFERENCES Mechanics(MechanicId),
	IssueDate date NOT NULL,
	FinishDate date,
	CHECK (Status IN ('Pending', 'In Progress', 'Finished'))
)

CREATE TABLE Vendors
(
	VendorId int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL UNIQUE
)

CREATE TABLE Parts
(
	PartId int PRIMARY KEY IDENTITY,
	SerialNumber nvarchar(50) NOT NULL UNIQUE,
	Description nvarchar(255),
	Price decimal(6, 2) NOT NULL,
	VendorId int REFERENCES Vendors(VendorId) NOT NULL,
	StockQty int NOT NULL DEFAULT 0,
	CHECK (Price > 0),
	CHECK (StockQty >= 0)
)

CREATE TABLE PartsNeeded
(
	JobId int REFERENCES Jobs(JobId),
	PartId int REFERENCES Parts(PartId),
	PRIMARY KEY (JobId, PartId),
	Quantity int NOT NULL DEFAULT 1,
	CHECK (Quantity > 0)
)

CREATE TABLE Orders
(
	OrderId int PRIMARY KEY IDENTITY,
	JobId int REFERENCES Jobs(JobId) NOT NULL,
	IssueDate date,
	Delivered bit NOT NULL DEFAULT 0
)

CREATE TABLE OrderParts
(
	OrderId int REFERENCES Orders(OrderId),
	PartId int REFERENCES Parts(PartId),
	PRIMARY KEY (OrderId, PartId),
	Quantity int NOT NULL DEFAULT 1,
	CHECK (Quantity > 0)
)

-- 02. Insert
INSERT INTO Clients (FirstName, LastName, Phone) VALUES
('Teri', 'Ennaco', '570-889-5187'),
('Merlyn', 'Lawler', '201-588-7810'),
('Georgene', 'Montezuma', '925-615-5185'),
('Jettie', 'Mconnell', '908-802-3564'),
('Lemuel', 'Latzke', '631-748-6479'),
('Melodie', 'Knipp', '805-690-1682'),
('Candida', 'Corbley', '908-275-8357')

INSERT INTO Vendors (Name) VALUES
('Name 1'),
('Name 2'),
('Name 3'),
('Name 4')

INSERT INTO Parts (SerialNumber, Description, Price, VendorId) VALUES
('WP8182119', 'Door Boot Seal', 117.86, 2),
('W10780048', 'Suspension Rod', 42.81, 1),
('W10841140', 'Silicone Adhesive ', 6.77, 4),
('WPY055980', 'High Temperature Adhesive', 13.94, 3)

-- 03. Update
-- Dataset needs to be included at this point
UPDATE Jobs
SET MechanicId = 3, Status = 'In Progress'
WHERE Status = 'Pending'

-- 04. Delete
DELETE FROM OrderParts
WHERE OrderId = 19

DELETE FROM Orders
WHERE OrderId = 19

-- Reset the DB to it's initial state from the dataset
-- 05. Mechanic Assignments
SELECT m.FirstName + ' ' + m.LastName AS Mechanic, j.Status, j.IssueDate
FROM Jobs j
JOIN Mechanics m ON j.MechanicId = m.MechanicId
ORDER BY m.MechanicId, j.IssueDate, j.JobId

-- 06. Current Clients
SELECT c.FirstName + ' ' + c.LastName AS Client,
	DATEDIFF(day, j.IssueDate, '2017/04/24') AS [Days going],
	j.Status
FROM Jobs j
JOIN Clients c ON j.ClientId = c.ClientId
WHERE j.Status != 'Finished'
ORDER BY [Days going] DESC, c.ClientId

-- 07. Mechanic Performance
SELECT m.FirstName + ' ' + m.LastName AS Mechanic, AVG(DATEDIFF(day, j.IssueDate, j.FinishDate)) AS [Average Days]
FROM Jobs j
JOIN Mechanics m ON j.MechanicId = m.MechanicId
GROUP BY m.MechanicId, m.FirstName, m.LastName
ORDER BY m.MechanicId

-- 08. Available Mechanics
SELECT m.FirstName + ' ' + m.LastName AS Available FROM
(
	SELECT *
	FROM Jobs j
	WHERE Status != 'Finished'
) AS Result
RIGHT JOIN Mechanics m ON Result.MechanicId = m.MechanicId
WHERE IssueDate IS NULL
ORDER BY m.MechanicId

-- 09. Past Expenses
SELECT OuterResult.JobId, IIF(TotalCost IS NULL, 0, TotalCost) AS Total FROM
(
	SELECT Result.JobId, SUM(Result.PartQuantity * p.Price) AS TotalCost FROM
	(
		SELECT j.JobId, p.PartId, SUM(Quantity) AS PartQuantity
		FROM Jobs j
		LEFT JOIN Orders o ON j.JobId = o.JobId
		LEFT JOIN OrderParts op ON o.OrderId = op.OrderId
		LEFT JOIN Parts p ON op.PartId = p.PartId
		WHERE j.FinishDate IS NOT NULL
		GROUP BY j.JobId, p.PartId
	) AS Result
	LEFT JOIN Parts p ON Result.PartId = p.PartId
	GROUP BY Result.JobId
) AS OuterResult
ORDER BY OuterResult.TotalCost DESC, OuterResult.JobId

-- 10. Missing Parts
SELECT * FROM
(
	SELECT p.PartId, p.Description, pn.Quantity AS Required, p.StockQty AS [In Stock], IIF(o.OrderId IS NULL, 0, op.Quantity) AS Ordered
	FROM Jobs j
	JOIN PartsNeeded pn ON j.JobId = pn.JobId
	JOIN Parts p ON pn.PartId = p.PartId
	LEFT JOIN OrderParts op ON p.PartId = op.PartId
	LEFT JOIN Orders o ON j.JobId = o.JobId
	WHERE Status != 'Finished'
) AS Result
WHERE Result.Required > Result.[In Stock] + Result.Ordered

-- 11. Place Order
CREATE PROC usp_PlaceOrder (@JobId int, @SerialNumber nvarchar(50), @Quantity int)
AS
	DECLARE @JobStatus nvarchar(50) = (SELECT Status FROM Jobs WHERE JobId = @JobId)

	DECLARE @PartId nvarchar(50) = (SELECT PartId FROM Parts WHERE SerialNumber = @SerialNumber)

	IF (@Quantity <= 0)
		THROW 50012, 'Part quantity must be more than zero!', 1
	ELSE IF (@JobStatus = 'Finished')
		THROW 50011, 'This job is not active!', 1
	ELSE IF (@JobStatus IS NULL)
		THROW 50013, 'Job not found!', 1
	ELSE IF (@PartId IS NULL)
		THROW 50014, 'Part not found!', 1

	DECLARE @OrderId int = (SELECT OrderId FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL)

	IF (@OrderId IS NULL)
	BEGIN
		INSERT INTO Orders (JobId, IssueDate) VALUES
		(@JobId, NULL)
	END

	SET @OrderId = (SELECT OrderId FROM Orders WHERE JobId = @JobId AND IssueDate IS NULL)

	DECLARE @OrderPartExists int = (SELECT OrderId FROM OrderParts WHERE OrderId = @OrderId AND PartId = @PartId)

	IF (@OrderPartExists IS NULL)
	BEGIN
		INSERT INTO OrderParts (OrderId, PartId, Quantity) VALUES
		(@OrderId, @PartId, @Quantity)
	END
	ELSE
	BEGIN
		UPDATE OrderParts
		SET Quantity += @Quantity
		WHERE OrderId = @OrderId AND PartId = @PartId
	END
GO

-- 12. Cost Of Order
CREATE FUNCTION udf_GetCost (@JobId int)
RETURNS decimal(10, 2)
AS
BEGIN
	DECLARE @Result decimal(10, 2)

	SET @Result = 
	(
		SELECT SUM(p.Price * op.Quantity)
		FROM Jobs j
		JOIN Orders o ON j.JobId = o.JobId
		JOIN OrderParts op ON o.OrderId = op.OrderId
		JOIN Parts p ON op.PartId = p.PartId
		WHERE j.JobId = @JobId
	)

	RETURN IIF(@Result IS NULL, 0, @Result)
END

SELECT dbo.udf_GetCost(2)