CREATE TABLE Accounts (
    CardNumber TEXT PRIMARY KEY,
    CardPIN TEXT NOT NULL,
    FirstName TEXT NOT NULL,
    LastName TEXT NOT NULL,
    Email TEXT,
    Phone TEXT,
    Balance REAL NOT NULL,
    FailedLoginAttempts INT DEFAULT 0,
    IsLocked BIT DEFAULT 0,
    LockTime DATETIME NULL
);

CREATE TABLE Transactions (
    Id INT IDENTITY PRIMARY KEY,
    CardNumber NVARCHAR(16),
    Type NVARCHAR(50),          -- "Поповнення", "Зняття", "Переказ"
    Amount FLOAT,
    Timestamp DATETIME DEFAULT GETDATE()
);