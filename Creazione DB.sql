CREATE DATABASE LeBarbier;
GO


USE LeBarbier;
GO


CREATE TABLE Roles (
    IdRole INT IDENTITY (1, 1) PRIMARY KEY,
    TypeRole VARCHAR(100) NOT NULL
);
GO


CREATE TABLE OfferedServices (
    IdOfferedServices INT IDENTITY (1, 1) PRIMARY KEY,
    ServiceName VARCHAR(100) NOT NULL,
	Description VARCHAR(200) 
);
GO


CREATE TABLE Users (
    IdUser INT IDENTITY (1, 1) PRIMARY KEY,
    FirstName VARCHAR(100) NOT NULL,
    LastName VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    PhoneNo VARCHAR(100)NOT NULL UNIQUE, 
    Password VARCHAR(100) NOT NULL UNIQUE,
    Id_Role INT,
    FOREIGN KEY (Id_Role) REFERENCES Roles(IdRole)
);
GO


CREATE TABLE Reservations (
    IdReservation INT IDENTITY (1, 1) PRIMARY KEY,
    Id_User INT,
	ReservationDate DATETIME NOT NULL,
	ReservationTime TIME NOT NULL,
    FOREIGN KEY (Id_User) REFERENCES Users(IdUser)
);
GO


CREATE TABLE ReservationServices (
    IdReservationService INT IDENTITY (1, 1) PRIMARY KEY,
    Id_Reservation INT,
    Id_OfferedServices INT,
    FOREIGN KEY (Id_Reservation) REFERENCES Reservations(IdReservation),
    FOREIGN KEY (Id_OfferedServices) REFERENCES OfferedServices(IdOfferedServices)
);
GO


INSERT INTO Roles (TypeRole) VALUES ('Admin'), ('User');
GO

INSERT INTO Users(FirstName, LastName, Email, PhoneNo, Password, Id_Role) VALUES ('Admin', 'Admin', 'Admin@admin.com', '1234567890' ,'Admin', 1)
GO


INSERT INTO OfferedServices (ServiceName, Description) VALUES ('Taglio', 'Taglio classico o moderno con strumenti professionali');
INSERT INTO OfferedServices (ServiceName, Description) VALUES ('Rasatura', 'Rasatura completa con crema e rasoio');
INSERT INTO OfferedServices (ServiceName, Description) VALUES ('Taglio Barba', 'Taglio e modellatura della barba');
INSERT INTO OfferedServices (ServiceName, Description) VALUES ('Trattamento Capelli', 'Trattamenti speciali per la cura dei capelli');
INSERT INTO OfferedServices (ServiceName, Description) VALUES ('Colore Capelli', 'Colorazione dei capelli con prodotti di alta qualità');
GO

SELECT * FROM Users;
SELECT * FROM Roles;
SELECT * FROM OfferedServices;
SELECT * FROM ReservationServices;
SELECT * FROM Reservations;
