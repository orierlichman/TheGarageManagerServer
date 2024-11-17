Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'TheGarageManagerDB')
BEGIN
    DROP DATABASE TheGarageManagerDB;
END
Go
Create Database TheGarageManagerDB
Go
Use TheGarageManagerDB
Go

CREATE TABLE Garage (
    GarageID INT PRIMARY KEY,
    MosahNumber INT,
    GarageName NVARCHAR(150),
    TypeCode INT,
    GarageType NVARCHAR(100),
    GarageAddress NVARCHAR(100),
    City NVARCHAR(70),
    Phone NVARCHAR(15),
    ZipCode NVARCHAR(10),
    SpecializationCode INT,
    Specialization NVARCHAR(150),
    ManagerSpecialization NVARCHAR(100),
    License INT
);

CREATE TABLE Vehicle (
    LicensePlate NVARCHAR(80) PRIMARY KEY,
    Model NVARCHAR(80),
    YearVehicle INT,
    FuelType NVARCHAR(70),
    Color NVARCHAR(30),
    Manufacturer NVARCHAR(100),
    CurrentMileage INT
);

CREATE TABLE CarRepair (
    RepairID INT PRIMARY KEY IDENTITY,
    LicensePlate NVARCHAR(80),
    GarageID INT,
    RepairDate DATETIME,
    DescriptionCar TEXT,
    Cost INT,
    FOREIGN KEY (LicensePlate) REFERENCES Vehicle(LicensePlate),
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID)
);

CREATE TABLE GarageParts (
    PartID INT PRIMARY KEY IDENTITY,
    GarageID INT,
    PartName NVARCHAR(30),
    PartNumber INT,
    Cost INT,
    ImageURL NVARCHAR(350),
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID)
);
CREATE TABLE UserStatus (
    StatusID INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(80)
);
CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    Email  NVARCHAR(70),
    UserPassword NVARCHAR(50),
    UserFirstName NVARCHAR(50) ,
	UserLastName NVARCHAR(50),
    UserGarageID INT NOT NULL,
    UserStatusID INT,
    FOREIGN KEY (UserGarageID) REFERENCES Garage(GarageID),
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(StatusID)
);




CREATE TABLE Appointment (
    AppointmentID INT PRIMARY KEY IDENTITY,
    AppointmentDate DATETIME,
    GarageID INT,
    LicensePlate NVARCHAR(80),
    AppointmentStatus NVARCHAR(50),
    ConfirmDate DATETIME,
    FOREIGN KEY (GarageID) REFERENCES Garage(GarageID),
    FOREIGN KEY (LicensePlate) REFERENCES Vehicle(LicensePlate)
);

Insert Into UserStatus Values('User')
Insert Into UserStatus Values('Manager')
Insert Into Garage Values(103,2,'benigarage',3456,'tipol', 'gigi', 'hod hasharon', '056277899', 'ori06', 8, 'mosahnik', 'king', 538903749)

Insert into Vehicle Values('4444', 'Civic', '2013', 'gas', 'black','Honda',500)
Insert into Vehicle Values('55555', 'Q3', '2023', 'gas', 'black','Audi',500)
Insert into CarRepair Values('4444',103,(convert(datetime,'18-06-25 10:34:09 PM',5)),'Engine problem',1000)
Insert into CarRepair Values('55555',103,(convert(datetime,'22-06-25 10:34:09 PM',5)),'tyre blow',1000)
Go

Insert Into Users Values('email2@2gmail.com','0610','ori','erlichman',103, 1)
Go

select * from CarRepair
-- Create a login for the admin user
CREATE LOGIN [TheGarageManagerAdminLogin] WITH PASSWORD = 'admin123';
Go

-- Create a user in the YourProjectNameDB database for the login
CREATE USER [TheGarageManagerAdminUser] FOR LOGIN [TheGarageManagerAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [TheGarageManagerAdminUser];
Go

select * from Users

--DROP DATABASE YourProjectNameDB



--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=TheGarageManagerDB;User ID=TheGarageManagerAdminLogin;Password=admin123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context TheGarageManagerDbContext -DataAnnotations –force