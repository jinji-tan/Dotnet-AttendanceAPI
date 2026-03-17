-- Run these queries on your local SQL Server to setup the database

CREATE DATABASE AttendanceDb;
GO
USE AttendanceDb;
GO

-- Table: Users
CREATE TABLE Users (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(255) NOT NULL,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(20) NOT NULL DEFAULT 'User' -- Can be 'User' or 'Admin'
);
GO

-- Table: AttendanceRecords
CREATE TABLE AttendanceRecords (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    TimeIn DATETIME NOT NULL,
    TimeOut DATETIME NULL,
    FOREIGN KEY (UserId) REFERENCES Users(Id) ON DELETE CASCADE
);
GO

CREATE INDEX IX_AttendanceRecords_UserId ON AttendanceRecords(UserId);
GO
