CREATE DATABASE TripService
GO

USE TripService
GO

-- 01. DDL
CREATE TABLE Cities
(
	Id int PRIMARY KEY IDENTITY,
	Name nvarchar(20) NOT NULL,
	CountryCode char(2) NOT NULL
)

CREATE TABLE Hotels
(
	Id int PRIMARY KEY IDENTITY,
	Name nvarchar(30) NOT NULL,
	CityId int REFERENCES Cities(Id) NOT NULL,
	EmployeeCount int NOT NULL,
	BaseRate decimal(12, 2)
)

CREATE TABLE Rooms
(
	Id int PRIMARY KEY IDENTITY,
	Price decimal(12, 2) NOT NULL,
	Type nvarchar(20) NOT NULL,
	Beds int NOT NULL,
	HotelId int REFERENCES Hotels(Id) NOT NULL
)

CREATE TABLE Trips
(
	Id int PRIMARY KEY IDENTITY,
	RoomId int REFERENCES Rooms(Id) NOT NULL,
	BookDate date NOT NULL,
	ArrivalDate date NOT NULL,
	ReturnDate date NOT NULL,
	CancelDate date,
	CHECK (BookDate < ArrivalDate),
	CHECK (ArrivalDate < ReturnDate)
)

CREATE TABLE Accounts
(
	Id int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(20),
	LastName nvarchar(50) NOT NULL,
	CityId int REFERENCES Cities(Id) NOT NULL,
	BirthDate date NOT NULL,
	Email nvarchar(100) UNIQUE NOT NULL
)

CREATE TABLE AccountsTrips
(
	AccountId int REFERENCES Accounts(Id),
	TripId int REFERENCES Trips(Id),
	PRIMARY KEY (AccountId, TripId),
	Luggage int NOT NULL,
	CHECK (Luggage >= 0)
)

-- 02. Insert
INSERT INTO Accounts (FirstName, MiddleName, LastName, CityId, BirthDate, Email) VALUES
('John', 'Smith', 'Smith', 34, '1975/07/21', 'j_smith@gmail.com'),
('Gosho', NULL, 'Petrov', 11, '1978/05/16', 'g_petrov@gmail.com'),
('Ivan', 'Petrovich', 'Pavlov', 59, '1849/09/26', 'i_pavlov@softuni.bg'),
('Friedrich', 'Wilhelm', 'Nietzsche', 2, '1844/10/15', 'f_nietzsche@softuni.bg')

INSERT INTO Trips (RoomId, BookDate, ArrivalDate, ReturnDate, CancelDate) VALUES
(101, '2015/04/12', '2015/04/14', '2015/04/20', '2015/02/02'),
(102, '2015/07/07', '2015/07/15', '2015/07/22', '2015/04/29'),
(103, '2013/07/17', '2013/07/23', '2013/07/24', NULL),
(104, '2012/03/17', '2012/03/31', '2012/04/01', '2012/01/10'),
(109, '2017/08/07', '2017/08/28', '2017/08/29', NULL)

-- 03. Update
UPDATE Rooms
SET Price *= 1.14
WHERE HotelId IN (5, 7, 9)

-- 04. Delete
DELETE FROM AccountsTrips
WHERE AccountId = 47

-- 05. EEE-Mails
SELECT a.FirstName, a.LastName, FORMAT(a.BirthDate, 'MM-dd-yyyy') AS BirthDate, c.Name AS Hometown, a.Email
FROM Accounts a
JOIN Cities c ON a.CityId = c.Id
WHERE Email LIKE 'e%'
ORDER BY Hometown

-- 06. City Statistics
SELECT c.Name AS City, COUNT(*) AS Hotels
FROM Hotels h
JOIN Cities c ON h.CityId = c.Id
GROUP BY c.Name
ORDER BY Hotels DESC, c.Name

-- 07. Longest and Shortest Trips
SELECT at.AccountId,
	a.FirstName + ' ' + a.LastName AS FullName,
	MAX(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS LongestTrip,
	MIN(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS ShortestTrip
FROM Accounts a
JOIN AccountsTrips at ON a.Id = at.AccountId
JOIN Trips t ON at.TripId = t.Id
WHERE a.MiddleName IS NULL AND CancelDate IS NULL
GROUP BY at.AccountId, a.FirstName, a.LastName
ORDER BY LongestTrip DESC, ShortestTrip

-- 08. Metropolis
SELECT TOP(10) Result.Id, c.Name AS City, c.CountryCode AS Country, Result.Accounts
FROM
(
	SELECT CityId AS Id, COUNT(*) Accounts
	FROM Accounts
	GROUP BY CityId
) AS Result
JOIN Cities c ON Result.Id = c.Id
ORDER BY Result.Accounts DESC

-- 09. Romantic Getaways
SELECT a.Id, a.Email, c.Name, COUNT(*) AS Trips
FROM Accounts a
JOIN AccountsTrips at ON a.Id = at.AccountId
JOIN Trips t ON at.TripId = t.Id
JOIN Rooms r ON t.RoomId = r.Id
JOIN Hotels h ON r.HotelId = h.Id
JOIN Cities c ON h.CityId = c.Id AND a.CityId = h.CityId
GROUP BY a.Id, a.Email, c.Name
ORDER BY Trips DESC, a.Id

-- 10. GDPR Violation
SELECT 
	t.Id,
	CONCAT(a.FirstName, ' ', ISNULL(a.MiddleName + ' ', ''), a.LastName) AS [Full Name],
	c.Name AS [From],
	(
		SELECT c2.Name 
		FROM Trips t2
		JOIN Rooms r2 ON t2.RoomId = r2.Id
		JOIN Hotels h2 ON r2.HotelId = h2.Id
		JOIN Cities c2 ON h2.CityId = c2.Id
		WHERE t2.Id = t.Id
	) AS [To],
	IIF(t.CancelDate IS NOT NULL, 'Canceled', CONCAT(DATEDIFF(day, t.ArrivalDate, t.ReturnDate), ' days')) AS Duration
FROM Trips t
JOIN AccountsTrips at ON t.Id = at.TripId
JOIN Accounts a ON at.AccountId = a.Id
JOIN Cities c ON a.CityId = c.Id
ORDER BY [Full Name], TripId

-- 11. Available Room
GO
CREATE FUNCTION udf_GetAvailableRoom (@HotelId int, @Date date, @People int)
RETURNS nvarchar(100)
AS
BEGIN
	DECLARE @HotelRate decimal(10, 2) = (SELECT BaseRate FROM Hotels WHERE Id = @HotelId)

	DECLARE @RoomId int = 
	(
		SELECT TOP 1 r.Id
		FROM Rooms r
		JOIN Trips t ON r.Id = t.RoomId
		JOIN Hotels h ON r.HotelId = h.Id
		WHERE HotelId = @HotelId 
			AND @Date NOT BETWEEN t.ArrivalDate AND t.ReturnDate
			AND t.CancelDate IS NULL
			AND r.Beds >= @People
			AND YEAR(@Date) = YEAR(t.ArrivalDate)
		ORDER BY r.Price DESC
	)

	DECLARE @RoomType nvarchar(50) = (SELECT [Type] FROM Rooms WHERE Id = @RoomId)

	DECLARE @BedsCount int = (SELECT Beds FROM Rooms WHERE Id = @RoomId)

	DECLARE @RoomPrice decimal(10, 2) = (SELECT Price FROM Rooms WHERE Id = @RoomId)

	DECLARE @TotalCost decimal(10, 2) = (@HotelRate + @RoomPrice) * @People

	IF (@RoomId IS NULL)
		RETURN 'No rooms available'

	return CONCAT('Room ', @RoomId, ': ', @RoomType, ' (', @BedsCount, ' beds) - $', @TotalCost)
END
GO

SELECT dbo.udf_GetAvailableRoom(112, '2011-12-17', 2)

SELECT dbo.udf_GetAvailableRoom(94, '2015-07-26', 3)

-- 12. Switch Room
GO
CREATE PROC usp_SwitchRoom (@TripId int, @TargetRoomId int)
AS
	DECLARE @TripHotelId int = 
	(
		SELECT h.Id
		FROM Trips t
		JOIN Rooms r ON t.RoomId = r.Id
		JOIN Hotels h ON r.HotelId = h.Id
		WHERE t.Id = @TripId
	)

	DECLARE @TargetRoomHotelId int = (SELECT HotelId FROM Rooms WHERE Id = @TargetRoomId)

	IF (@TripHotelId != @TargetRoomHotelId)
		THROW 50001, 'Target room is in another hotel!', 1

	DECLARE @BedsNeeded int = (SELECT COUNT(*) FROM AccountsTrips WHERE TripId = @TripId)

	DECLARE @BedsCountTarget int = (SELECT Beds FROM Rooms WHERE Id = @TargetRoomId)

	IF (@BedsNeeded > @BedsCountTarget)
		THROW 50002, 'Not enough beds in target room!', 1

	UPDATE Trips
	SET RoomId = @TargetRoomId
	WHERE Id = @TripId
GO

EXEC usp_SwitchRoom 10, 11
SELECT RoomId FROM Trips WHERE Id = 10

EXEC usp_SwitchRoom 10, 7
EXEC usp_SwitchRoom 10, 8