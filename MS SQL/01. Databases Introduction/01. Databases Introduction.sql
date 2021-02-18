-- 01. Create new database named Minions.
CREATE DATABASE Minions

-- 02. Add table Minions (Id, Name, Age) and table Towns (Id, Name); Don't use identity
CREATE TABLE Minions
(
	Id int NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL,
	Age int
)

CREATE TABLE Towns
(
	Id int NOT NULL PRIMARY KEY,
	Name nvarchar(50) NOT NULL
)

-- 03. Alter the Minions table to have new column TownId that would be of the same type as the Id column of
-- Towns table. Add new constraint that makes TownId foreign key and references to Id column of Towns table.
ALTER TABLE Minions
ADD TownId int NOT NULL

ALTER TABLE Minions
ADD FOREIGN KEY (TownId)
REFERENCES Towns (Id)

-- 04. Populate both tables with the sample records provided
INSERT INTO Towns (Id, Name) VALUES 
(1, 'Sofia'),
(2, 'Plovdiv'),
(3, 'Varna')

INSERT INTO Minions (Id, Name, Age, TownId) VALUES 
(1, 'Kevin', 22, 1),
(2, 'Bob', 15, 3),
(3, 'Steward', NULL, 2)

-- 05. Delete all the data from the Minions table using SQL query.
DELETE FROM [Minions].[dbo].[Minions]

-- 06. Delete all tables from the Minions database using SQL query.
DROP TABLE [Minions].[dbo].[Minions]
DROP TABLE [Minions].[dbo].[Towns]

-- 07. Create table people and insert 5 entries
CREATE TABLE People
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Name nvarchar(200) NOT NULL,
	Picture varchar(MAX),
	Height decimal(3, 2),
	Weight decimal(5, 2),
	Gender nvarchar(1),
	CHECK (Gender in ('m', 'f')),
	Birthdate date NOT NULL,
	Biography nvarchar(MAX)
)

INSERT INTO People (Name, Picture, Height, Weight, Gender, Birthdate, Biography) VALUES
('User1', 'Pic1', 1.10, 61.10, 'm', '2000/01/01', 'Bio 1'),
('User2', 'Pic2', 1.20, 62.20, 'f', '2001/02/02', 'Bio 2'),
('User3', 'Pic3', 1.30, 63.30, 'm', '2002/03/03', 'Bio 3'),
('User4', 'Pic4', 1.40, 64.40, 'f', '2003/04/04', 'Bio 4'),
('User5', 'Pic5', 1.50, 65.50, 'm', '2004/05/05', 'Bio 5')

-- 08. Create Table Users and insert 5 entries
CREATE TABLE Users
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Username varchar(30) UNIQUE NOT NULL,
	Password varchar(26) NOT NULL,
	ProfilePicture varchar(MAX),
	LastLoginTime datetime,
	IsDeleted bit
)

INSERT INTO Users (Username, Password, ProfilePicture, LastLoginTime, IsDeleted) VALUES
('User1', 'password1', 'PFP1', '2001/01/01 12:11:00', 0),
('User2', 'password2', 'PFP2', '2002/01/02 12:22:00', 0),
('User3', 'password3', 'PFP3', '2003/01/03 12:33:00', 0),
('User4', 'password4', 'PFP4', '2004/01/04 12:44:00', 0),
('User5', 'password5', 'PFP5', '2005/01/05 12:55:00', 0)

-- 09. Remove the current primary key then create one that is the combination of Id and Username.
ALTER TABLE Users
DROP CONSTRAINT PK__Users__3214EC0717069EE8

ALTER TABLE Users
ADD CONSTRAINT PK_IdUsername PRIMARY KEY (Id, Username)

-- 10. Add check constraint to ensure that the values in the Password field are at least 5 symbols long.
ALTER TABLE Users
ADD CONSTRAINT CH_MinPasswordLength CHECK (LEN(Password) > 5)

-- 11. Make the default value of LastLoginTime field to be the current time.
ALTER TABLE Users
ADD CONSTRAINT DF_LastLoginTime
DEFAULT GETDATE() FOR LastLoginTime

-- 12. Remove Username field from the primary key so only the field Id would be primary key.
--     Now add unique constraint to the Username field to ensure that the values there are at least 3 symbols long.
ALTER TABLE Users
DROP CONSTRAINT PK_IdUsername

ALTER TABLE Users
ADD PRIMARY KEY (Id)

ALTER TABLE Users
ADD CONSTRAINT CH_UsernameLength CHECK (LEN(Username) > 2)


-- 13. Movies database
CREATE DATABASE Movies

CREATE TABLE Directors
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	DirectorName nvarchar(50) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Genres
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	GenreName nvarchar(50) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	CategoryName nvarchar(50) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Movies
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	Title nvarchar(100) NOT NULL,
	DirectorId int FOREIGN KEY REFERENCES Directors(Id),
	CopyrightYear int NOT NULL,
	Length int NOT NULL,
	GenreId int FOREIGN KEY REFERENCES Genres(Id),
	CategoryId int FOREIGN KEY REFERENCES Categories(Id),
	Rating decimal(3, 1) NOT NULL,
	Notes nvarchar(500)
)

INSERT INTO Directors (DirectorName) VALUES
('Director 1'),
('Director 2'),
('Director 3'),
('Director 4'),
('Director 5')

INSERT INTO Genres (GenreName) VALUES
('Genre 1'),
('Genre 2'),
('Genre 3'),
('Genre 4'),
('Genre 5')

INSERT INTO Categories(CategoryName) VALUES
('Category 1'),
('Category 2'),
('Category 3'),
('Category 4'),
('Category 5')

INSERT INTO Movies (Title, DirectorId, CopyrightYear, Length, GenreId, CategoryId, Rating) VALUES
('Title 1', 1, 2005, 120, 1, 1, 9.5),
('Title 2', 2, 2006, 125, 2, 2, 7.4),
('Title 3', 3, 2007, 111, 3, 3, 6.2),
('Title 4', 4, 2008, 105, 4, 4, 9.9),
('Title 5', 5, 2009, 135, 5, 5, 8.1)

-- 14. Car rental database
CREATE DATABASE CarRental

CREATE TABLE Categories
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	CategoryName nvarchar(50) NOT NULL,
	DailyRate decimal(8, 2),
	WeeklyRate decimal(8, 2),
	MonthlyRate decimal(8, 2),
	WeekendRate decimal(8, 2)
)

CREATE TABLE Cars
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	PlateNumber smallint NOT NULL,
	Manufacturer nvarchar(30) NOT NULL,
	Model nvarchar(30) NOT NULL,
	CarYear smallint NOT NULL,
	CategoryId int FOREIGN KEY REFERENCES Categories(Id),
	Doors tinyint NOT NULL,
	Picture nvarchar(MAX),
	Condition nvarchar(20),
	Available bit
)

CREATE TABLE Employees
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Title nvarchar(50) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Customers
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	DriverLicenseNumber int NOT NULL,
	FullName nvarchar(100) NOT NULL,
	Address nvarchar(100) NOT NULL,
	City nvarchar(50) NOT NULL,
	ZIPCode int NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE RentalOrders
(
	Id int PRIMARY KEY IDENTITY NOT NULL,
	EmployeeId int FOREIGN KEY REFERENCES Employees(Id),
	CustomerId int FOREIGN KEY REFERENCES Customers(Id),
	CarId int FOREIGN KEY REFERENCES Cars(Id),
	TankLevel smallint NOT NULL,
	KilometrageStart int NOT NULL,
	KilometrageEnd int NOT NULL,
	TotalKilometrage int NOT NULL,
	StartDate date NOT NULL,
	EndDate date NOT NULL,
	TotalDays int NOT NULL,
	RateApplied bit NOT NULL,
	TaxRate decimal(6, 2),
	OrderStatus nvarchar(20),
	Notes nvarchar(500),
	CHECK (TotalDays = DATEDIFF(day, StartDate, EndDate) + 1),
	CHECK (TotalKilometrage = KilometrageEnd - KilometrageStart)
)

INSERT INTO Categories (CategoryName, DailyRate, WeeklyRate, MonthlyRate, WeekendRate) VALUES
('Category 1', 20, 100, 250, 40),
('Category 2', 22, 102, 252, 42),
('Category 3', 23, 103, 253, 43)

INSERT INTO Cars (PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available) VALUES
(1111, 'Manu 1', 'Model 1', 2000, 1, 4, NULL, 'New', 1),
(2222, 'Manu 2', 'Model 2', 2004, 2, 4, NULL, 'Old', 1),
(3333, 'Manu 3', 'Model 3', 2008, 3, 4, NULL, 'Old', 0)

INSERT INTO Employees (FirstName, LastName, Title) VALUES
('FName 1', 'LName 1', 'Title 1'),
('FName 2', 'LName 2', 'Title 2'),
('FName 3', 'LName 3', 'Title 3')

INSERT INTO Customers (DriverLicenseNumber, FullName, Address, City, ZIPCode) VALUES
(11111, 'FullName 1', 'Address 1', 'City 1', 1111),
(22222, 'FullName 2', 'Address 2', 'City 2', 2222),
(22222, 'FullName 3', 'Address 3', 'City 3', 3333)

INSERT INTO RentalOrders (EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd,
						  TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus) VALUES
(1, 1, 1, 100, 5037, 5137, 100, '2020/01/01', '2020/01/02', 2, 1, 100, 'Complete'),
(2, 2, 2, 90, 5037, 5137, 100, '2020/01/01', '2020/01/02', 2, 1, 100, 'Complete'),
(3, 3, 3, 80, 5037, 5137, 100, '2020/01/01', '2020/01/02', 2, 1, 100, 'Complete')

-- 15. Hotel Database
CREATE DATABASE Hotel

CREATE TABLE Employees
(
	Id int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	Title nvarchar(30) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Customers
(
	AccountNumber int PRIMARY KEY,
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL,
	PhoneNumber nvarchar(20) NOT NULL,
	EmergencyName nvarchar(50) NOT NULL,
	EmergencyNumber nvarchar(20) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE RoomStatus
(
	RoomStatus nvarchar(50) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE RoomTypes
(
	RoomType nvarchar(100) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE BedTypes
(
	BedType nvarchar(100) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Rooms
(
	RoomNumber int PRIMARY KEY,
	RoomType nvarchar(100) NOT NULL,
	BedType nvarchar(100) NOT NULL,
	Rate int NOT NULL,
	RoomStatus bit NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Payments
(
	Id int PRIMARY KEY IDENTITY,
	EmployeeId int FOREIGN KEY REFERENCES Employees(Id),
	PaymentDate datetime NOT NULL,
	AccountNumber nvarchar(20),
	FirstDateOccupied date NOT NULL,
	LastDateOccupied date NOT NULL,
	TotalDays smallint NOT NULL,
	AccountCharged decimal(15, 2) NOT NULL,
	TaxRate int,
	TaxAmount decimal(15, 2),
	PaymentTotal decimal(16, 2) NOT NULL,
	Notes nvarchar(500)
)

CREATE TABLE Occupancies
(
	Id int PRIMARY KEY IDENTITY,
	EmployeeId int FOREIGN KEY REFERENCES Employees(Id),
	DateOccupied date NOT NULL,
	AccountNumber nvarchar(20),
	RoomNumber int FOREIGN KEY REFERENCES Rooms(RoomNumber),
	RateApplied int,
	PhoneCharge decimal(10, 2),
	Notes nvarchar(500)
)

INSERT INTO Employees (FirstName, LastName, Title) VALUES
('FName 1', 'LName 1', 'Title 1'),
('FName 2', 'LName 2', 'Title 2'),
('FName 3', 'LName 3', 'Title 3')

INSERT INTO Customers (AccountNumber, FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber) VALUES
(1, 'FName 1', 'LName 1', '+359345789000', 'Emergency name 1', '+121121251212'),
(2, 'FName 2', 'LName 2', '+359345789000', 'Emergency name 2', '+123344421212'),
(3, 'FName 3', 'LName 3', '+359345789000', 'Emergency name 3', '+121121333212')

INSERT INTO RoomStatus (RoomStatus) VALUES
('Status 1'),
('Status 2'),
('Status 3')

INSERT INTO RoomTypes (RoomType) VALUES
('Room type 1'),
('Room type 2'),
('Room type 3')

INSERT INTO BedTypes (BedType) VALUES
('Bed type 1'),
('Bed type 2'),
('Bed type 3')

INSERT INTO Rooms (RoomNumber, RoomType, BedType, Rate, RoomStatus) VALUES
(101, 'Room type 1', 'Bed type 1', 10, 1),
(205, 'Room type 2', 'Bed type 2', 10, 1),
(302, 'Room type 3', 'Bed type 3', 10, 1)

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AccountCharged, TaxRate, TaxAmount, PaymentTotal) VALUES
(1, '2020/01/05', 'NUM123123123', '2020/01/01', '2020/01/05', 5, 780, 10, 78, 858),
(2, '2020/01/05', 'NUM123123123', '2020/01/01', '2020/01/05', 5, 780, 10, 78, 858),
(3, '2020/01/05', 'NUM123123123', '2020/01/01', '2020/01/05', 5, 780, 10, 78, 858)

INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge) VALUES
(1, '2020/01/05', 'NUM123123123', 101, 10, 15.50),
(2, '2020/01/05', 'NUM123123123', 205, 10, 15.50),
(3, '2020/01/05', 'NUM123123123', 302, 10, 15.50)

-- 16. SoftUni database
CREATE DATABASE SoftUniDB

CREATE TABLE Towns
(
	Id int PRIMARY KEY IDENTITY,
	Name nvarchar(50)
)

CREATE TABLE Addresses
(
	Id int PRIMARY KEY IDENTITY,
	AddressText nvarchar(100),
	TownId int FOREIGN KEY REFERENCES Towns(Id)
)

CREATE TABLE Departments
(
	Id int PRIMARY KEY IDENTITY,
	Name nvarchar(50)
)

CREATE TABLE Employees
(
	Id int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50) NOT NULL,
	MiddleName nvarchar(50),
	LastName nvarchar(50) NOT NULL,
	JobTitle nvarchar(50) NOT NULL,
	DepartmentId int FOREIGN KEY REFERENCES Departments(Id),
	HireDate date NOT NULL,
	Salary decimal(6, 2),
	AddressId int FOREIGN KEY REFERENCES Addresses(Id)
)

-- 17. Backup the database SoftUni from the previous task into a file named “softuni-backup.bak”. Delete your database
--	   from SQL Server Management Studio. Then restore the database from the created backup.

USE SoftUniDB;
GO
BACKUP DATABASE SoftUniDB
TO DISK = 'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS01\MSSQL\Backup\SoftUniDB.bak'
   WITH FORMAT,
      MEDIANAME = 'SQLServerBackups',
      NAME = 'Full Backup of SoftUniDB';
GO

-- 18. Use the SoftUni database and insert some data using SQL queries.
INSERT INTO Towns (Name) VALUES
('Sofia'),
('Plovdiv'),
('Varna'),
('Burgas')

INSERT INTO Addresses (AddressText, TownId) VALUES
('Address text 1', 1)

INSERT INTO Departments (Name) VALUES
('Engineering'),
('Sales'),
('Marketing'),
('Software Development'),
('Quality Assurance')

INSERT INTO Employees (FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary, AddressId) VALUES
('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013/02/01', 3500.00, 1),
('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004/03/02', 4000.00, 1),
('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016/08/28', 525.25, 1),
('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007/12/09', 3000.00, 1),
('Peter', 'Pan', 'Pan', 'Intern', 3, '2016/08/28', 599.88, 1)

-- 19. Use the SoftUni database and select all records from the Towns, Departments and finally from Employees table.
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

-- 20. Sort Towns alphabetically by name, Departments alphabetically by name and Employees - descending by salary
SELECT * FROM Towns ORDER BY Name ASC
SELECT * FROM Departments ORDER BY Name ASC
SELECT * FROM Employees ORDER BY Salary DESC

-- 21. Show only some of the columns: For Towns – Name, For Departments – Name, For Employees – FirstName, LastName, JobTitle, Salary
--     Keep the ordering from last problem
SELECT Name FROM Towns ORDER BY Name ASC
SELECT Name FROM Departments ORDER BY Name ASC
SELECT FirstName, LastName, JobTitle, Salary FROM Employees ORDER BY Salary DESC

-- 22. Use SoftUni database and increase the salary of all employees by 10%. Then show only Salary column for all in the Employees table.
UPDATE Employees
SET Salary *= 1.1

SELECT Salary FROM Employees

-- 23. Use Hotel database and decrease tax rate by 3% to all payments. Then select only TaxRate column from the Payments table.
UPDATE Payments
SET TaxRate *= 0.97

SELECT TaxRate FROM Payments

-- 24. Use Hotel database and delete all records from the Occupancies table.
TRUNCATE TABLE Occupancies