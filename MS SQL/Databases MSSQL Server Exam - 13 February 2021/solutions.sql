GO
CREATE DATABASE BitBucket
GO
USE BitBucket
GO

-- 01. DDL
CREATE TABLE Users
(
	Id int PRIMARY KEY IDENTITY,
	Username varchar(30) NOT NULL,
	Password varchar(30) NOT NULL,
	Email varchar(50) NOT NULL
)

CREATE TABLE Repositories
(
	Id int PRIMARY KEY IDENTITY,
	Name varchar(50) NOT NULL
)

CREATE TABLE RepositoriesContributors
(
	RepositoryId int REFERENCES Repositories(Id),
	ContributorId int REFERENCES Users(Id),
	PRIMARY KEY (RepositoryId, ContributorId)
)

CREATE TABLE Issues
(
	Id int PRIMARY KEY IDENTITY,
	Title varchar(255) NOT NULL,
	IssueStatus char(6) NOT NULL,
	RepositoryId int REFERENCES Repositories(Id) NOT NULL,
	AssigneeId int REFERENCES Users(Id)
)

CREATE TABLE Commits
(
	Id int PRIMARY KEY IDENTITY,
	Message varchar(255) NOT NULL,
	IssueId int REFERENCES Issues(Id),
	RepositoryId int REFERENCES Repositories(Id) NOT NULL,
	ContributorId int REFERENCES Users(Id)
)

CREATE TABLE Files
(
	Id int PRIMARY KEY IDENTITY,
	Name varchar(100) NOT NULL,
	Size decimal(12, 2) NOT NULL,
	ParentId int REFERENCES Files(Id),
	CommitId int REFERENCES Commits(Id) NOT NULL
)

-- 02. Insert
INSERT INTO Files (Name, Size, ParentId, CommitId) VALUES
('Trade.idk', 2598.0, 1, 1),
('menu.net', 9238.31, 2, 2),
('Administrate.soshy', 1246.93, 3, 3),
('Controller.php', 7353.15, 4, 4),
('Find.java', 9957.86, 5, 5),
('Controller.json', 14034.87, 3, 6),
('Operate.xix', 7662.92, 7, 7)

INSERT INTO Issues (Title, IssueStatus, RepositoryId, AssigneeId) VALUES
('Critical Problem with HomeController.cs file', 'open', 1, 4),
('Typo fix in Judge.html', 'open', 4, 3),
('Implement documentation for UsersService.cs', 'closed', 8, 2),
('Unreachable code in Index.cs', 'open', 9, 8)

-- 03. Update
UPDATE Issues
SET IssueStatus = 'closed'
WHERE AssigneeId = 6

-- 04. Delete
DELETE FROM RepositoriesContributors
WHERE RepositoryId = 3

DELETE FROM Issues
WHERE RepositoryId = 3

-- 05. Commits
SELECT Id, Message, RepositoryId, ContributorId
FROM Commits
ORDER BY Id, Message, RepositoryId, ContributorId

-- 06. Front-end
SELECT Id, Name, Size
FROM Files
WHERE Size > 1000 AND Name LIKE '%html%'
ORDER BY Size DESC, Id, Name

-- 07. Issue Assignment
SELECT i.Id, Username + ' : ' + Title AS IssueAssignee
FROM Issues i
JOIN Users u ON i.AssigneeId = u.Id
ORDER BY i.Id DESC, i.AssigneeId

-- 08. Single Files
SELECT f1.Id, f1.Name, CONCAT(f1.Size, 'KB') AS Size
FROM Files f1
LEFT JOIN Files f2 ON f2.ParentId = f1.Id
WHERE f2.Id IS NULL
ORDER BY f1.Id, f1.Name, Size DESC

-- 09. Commits in Repositories
SELECT TOP 5 r.Id, r.Name, COUNT(*) AS Commits
FROM RepositoriesContributors rc
JOIN Repositories r ON rc.RepositoryId = r.Id
JOIN Commits c ON r.Id = c.RepositoryId
GROUP BY r.Id, r.Name
ORDER BY Commits DESC, r.Id, r.Name

-- 10. Average Size
SELECT u.Username, AVG(f.Size) AS Size
FROM Commits c
JOIN Users u ON c.ContributorId = u.Id
JOIN Files f ON c.Id = f.CommitId
GROUP BY u.Id, u.Username
ORDER BY Size DESC, u.Username

-- 11. All User Commits
GO
CREATE FUNCTION udf_AllUserCommits (@Username varchar(50))
RETURNS int
AS
BEGIN
	RETURN 
	(
		SELECT COUNT(*)
		FROM Users u
		JOIN Commits c ON u.Id = c.ContributorId
		WHERE u.Username = @Username
	)
END
GO

SELECT dbo.udf_AllUserCommits('UnderSinduxrein')

-- 12. Search for Files
GO
CREATE PROC usp_SearchForFiles(@fileExtension varchar(10))
AS
	SELECT Id, Name, CONCAT(Size, 'KB') AS Size
	FROM Files
	WHERE Name LIKE '%' + @fileExtension
	ORDER BY Id, Name, Size DESC
GO

EXEC usp_SearchForFiles 'json'