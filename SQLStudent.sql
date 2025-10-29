Create database SRMS2025;
use SRMS2025;


CREATE TABLE Role (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(20) NOT NULL UNIQUE
);
GO


INSERT INTO Role (RoleName)
VALUES ('Invigilator'), ('Student');
GO

CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(30) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    RoleId INT NOT NULL FOREIGN KEY REFERENCES Role(RoleId)
);
GO


CREATE TABLE Students (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    RollNumber AS RIGHT('00000' + CAST(Id AS VARCHAR(5)), 5) PERSISTED,
    Name NVARCHAR(30) NOT NULL,
    Maths INT CHECK (Maths BETWEEN 1 AND 100),
    Physics INT CHECK (Physics BETWEEN 1 AND 100),
    Chemistry INT CHECK (Chemistry BETWEEN 1 AND 100),
    English INT CHECK (English BETWEEN 1 AND 100),
    Programming INT CHECK (Programming BETWEEN 1 AND 100),
    IsActive BIT DEFAULT 1
);
GO


-- 4.1 Add Student
CREATE PROCEDURE sp_AddStudent
    @Name NVARCHAR(30),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT
AS
BEGIN
    INSERT INTO Students (Name, Maths, Physics, Chemistry, English, Programming)
    VALUES (@Name, @Maths, @Physics, @Chemistry, @English, @Programming);
END
GO

-- 2 Update Student Marks
CREATE PROCEDURE sp_UpdateStudentMarks
    @RollNumber NVARCHAR(5),
    @Maths INT,
    @Physics INT,
    @Chemistry INT,
    @English INT,
    @Programming INT
AS
BEGIN
    UPDATE Students
    SET Maths = @Maths,
        Physics = @Physics,
        Chemistry = @Chemistry,
        English = @English,
        Programming = @Programming
    WHERE RollNumber = @RollNumber AND IsActive = 1;
END
GO

-- 3 Disable (Soft Delete) Student
CREATE PROCEDURE sp_DisableStudent
    @RollNumber NVARCHAR(5)
AS
BEGIN
    UPDATE Students
    SET IsActive = 0
    WHERE RollNumber = @RollNumber;
END
GO

-- 4 Get All Active Students
CREATE PROCEDURE sp_GetAllStudents
AS
BEGIN
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming
    FROM Students
    WHERE IsActive = 1;
END
GO

-- 5 Get Student by Roll Number
CREATE PROCEDURE sp_GetStudentByRoll
    @RollNumber NVARCHAR(5)
AS
BEGIN
    SELECT Id, RollNumber, Name, Maths, Physics, Chemistry, English, Programming
    FROM Students
    WHERE RollNumber = @RollNumber AND IsActive = 1;
END
GO

-- 6 User Login
CREATE PROCEDURE sp_UserLogin
    @Username NVARCHAR(30),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT U.UserId, U.Username, R.RoleName
    FROM Users U
    INNER JOIN Role R ON U.RoleId = R.RoleId
    WHERE U.Username = @Username AND U.Password = @Password;
END
GO


INSERT INTO Users (Username, Password, RoleId)
VALUES 
('admin', 'admin123', (SELECT RoleId FROM Role WHERE RoleName='Invigilator')),
('student1', 'stud123', (SELECT RoleId FROM Role WHERE RoleName='Student'));
GO







