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
    NumberPlayer INT, --Số người tham gia
    Turn INT NOT NULL DEFAULT 1, --Lượt của player nào
    Status NVARCHAR(30)
);
GO


CREATE TABLE Player (
    IDPlayer INT IDENTITY(1,1) PRIMARY KEY,
    IDMatch INT NOT NULL,
    IDAccount INT NOT NULL,

    Rank INT NULL,
    CrashTime DATETIME NULL, --thời gian kết thúc của player
    Money INT NOT NULL DEFAULT 1500,
    Position INT NOT NULL DEFAULT 0, --Vị trí của player
    StatusPlayer NVARCHAR(30)

    FOREIGN KEY (IDMatch) REFERENCES Match(IDMatch),
    FOREIGN KEY (IDAccount) REFERENCES Account(IDAccount)
);
GO


CREATE TABLE Property (
    IDProperty INT IDENTITY(1,1) PRIMARY KEY,
    IDMatch INT NOT NULL,                -- property thuộc trận nào
    Name NVARCHAR(100) NOT NULL,
    Value INT NOT NULL,
    Level INT NOT NULL DEFAULT 0,
    TypeProperty NVARCHAR(20) NOT NULL,
    PlayerID INT NULL,                   -- ai đang sở hữu (nếu có)
    
    FOREIGN KEY (IDMatch) REFERENCES Match(IDMatch),
    FOREIGN KEY (PlayerID) REFERENCES Player(IDPlayer)
);
GO


drop table match
drop table Player
drop table Property



INSERT INTO Account (Username, PasswordHash, Email, DisplayName)
VALUES (
    N'hahatest',
    N'123456',
    N'hahatest@gmail.com',
    N'Haha Test'
);

select * from account