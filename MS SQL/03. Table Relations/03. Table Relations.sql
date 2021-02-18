-- 01. One-To-One Relationship
CREATE TABLE Passports
(
	PassportID int PRIMARY KEY,
	PassportNumber nvarchar(20)
)

CREATE TABLE Persons
(
	PersonID int PRIMARY KEY IDENTITY,
	FirstName nvarchar(50),
	Salary decimal(10, 2),
	PassportID int REFERENCES Passports(PassportID) UNIQUE
)

INSERT INTO Passports (PassportID, PassportNumber) VALUES
(101, 'N34FG21B'),
(102, 'K65LO4R7'),
(103, 'ZE657QP2')

INSERT INTO Persons (FirstName, Salary, PassportID) VALUES
('Roberto', 43300.00, 102),
('Tom', 56100.00, 103),
('Yana', 60200.00, 101)


-- 02. One-To-Many Relationship
CREATE TABLE Manufacturers
(
	ManufacturerID int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
	EstablishedOn date NOT NULL
)

CREATE TABLE Models
(
	ModelID int PRIMARY KEY IDENTITY (101, 1),
	Name nvarchar(50) NOT NULL,
	ManufacturerID int REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers (Name, EstablishedOn) VALUES
('BMW', '1916/03/07'),
('Tesla', '2003/01/01'),
('Lada', '1966/05/01')

INSERT INTO Models (Name, ManufacturerID) VALUES
('X1', 1),
('i6', 1),
('Model S', 2),
('Model X', 2),
('Model 3', 2),
('Nova', 3)


-- 03. Many-To-Many Relationship
CREATE TABLE Students
(
	StudentID int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL
)

CREATE TABLE Exams
(
	ExamID int PRIMARY KEY IDENTITY (101, 1),
	Name nvarchar(50) NOT NULL
)

CREATE TABLE StudentsExams
(
	StudentID int REFERENCES Students(StudentID),
	ExamID int REFERENCES Exams(ExamID),
	PRIMARY KEY (StudentID, ExamID)
)

INSERT INTO Students (Name) VALUES
('Mila'),
('Tony'),
('Ron')

INSERT INTO Exams (Name) VALUES
('SpringMVC'),
('Neo4j'),
('Oracle 11g')

INSERT INTO StudentsExams (StudentID, ExamID) VALUES
(1, 101),
(1, 102),
(2, 101),
(3, 103),
(2, 102),
(2, 103)


-- 04. Self-Referencing
CREATE TABLE Teachers
(
	TeacherID int PRIMARY KEY IDENTITY (101, 1),
	Name nvarchar(50) NOT NULL,
	ManagerID int REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers (Name, ManagerID) VALUES
('John', NULL),
('Maya', 106),
('Silvia', 106),
('Ted', 105),
('Mark', 101),
('Greta', 101)


-- 05. Online Store Database
-- CREATE DATABASE OnlineStore
CREATE TABLE ItemTypes
(
	ItemTypeID int PRIMARY KEY IDENTITY,
	Name varchar(50) NOT NULL
)

CREATE TABLE Items
(
	ItemID int PRIMARY KEY IDENTITY,
	Name varchar(50) NOT NULL,
	ItemTypeID int REFERENCES ItemTypes(ItemTypeID)
)

CREATE TABLE Cities
(
	CityID int PRIMARY KEY IDENTITY,
	Name varchar(50) NOT NULL
)

CREATE TABLE Customers
(
	CustomerID int PRIMARY KEY IDENTITY,
	Name varchar(50) NOT NULL,
	Birthday date,
	CityID int REFERENCES Cities(CityID)
)

CREATE TABLE Orders
(
	OrderID int PRIMARY KEY IDENTITY,
	CustomerID int REFERENCES Customers(CustomerID)
)

CREATE TABLE OrderItems
(
	OrderID int REFERENCES Orders(OrderID),
	ItemID int REFERENCES Items(ItemID),
	PRIMARY KEY (OrderID, ItemID)
)


-- 06. University Database
-- CREATE DATABASE University
CREATE TABLE Majors
(
	MajorID int PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
)

CREATE TABLE Students
(
	StudentID int PRIMARY KEY IDENTITY,
	StudentNumber nvarchar(20) NOT NULL,
	StudentName nvarchar(80) NOT NULL,
	MajorID int REFERENCES Majors(MajorID)
)

CREATE TABLE Payments
(
	PaymentID int PRIMARY KEY IDENTITY,
	PaymentDate date NOT NULL,
	PaymentAmount decimal(6, 2) NOT NULL,
	StudentID int REFERENCES Students(StudentID)
)

CREATE TABLE Subjects
(
	SubjectID int PRIMARY KEY IDENTITY,
	SubjectName nvarchar(50)
)

CREATE TABLE Agenda
(
	StudentID int REFERENCES Students(StudentID),
	SubjectID int REFERENCES Subjects(SubjectID),
	PRIMARY KEY (StudentID, SubjectID)
)


-- 09. *Peaks in Rila
SELECT MountainRange, PeakName, Elevation FROM Peaks p
JOIN Mountains m ON p.MountainId = m.Id
WHERE MountainRange = 'Rila'
ORDER BY Elevation DESC