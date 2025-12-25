CREATE DATABASE MonopolyDB;
GO

USE MonopolyDB;
GO


CREATE TABLE Account (
    IDAccount INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(200) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    DisplayName NVARCHAR(100),
    CreatedDate DATETIME DEFAULT GETDATE()
);
GO


CREATE TABLE Match (
    IDMatch INT IDENTITY(1,1) PRIMARY KEY,
    StartTime DATETIME NOT NULL DEFAULT GETDATE(),
    EndTime DATETIME NULL,
    NumberPlayer INT, -- Số người tham gia
    Turn INT NOT NULL DEFAULT 1, -- Lượt của player nào
    Status NVARCHAR(30)  -- Waiting | Playing | End
);
GO


CREATE TABLE Player (
    IDPlayer INT IDENTITY(1,1) PRIMARY KEY,
    IDMatch INT NOT NULL,
    IDAccount INT NOT NULL,

    Rank INT NULL,
    CrashTime DATETIME NULL, -- Thời gian kết thúc của player
    Status NVARCHAR(30), -- Waiting | Playing | End

    FOREIGN KEY (IDMatch) REFERENCES Match(IDMatch),
    FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount)
);
GO

