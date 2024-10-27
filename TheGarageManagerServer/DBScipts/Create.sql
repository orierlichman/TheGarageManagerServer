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

CREATE TABLE GARAGE (
    GarageID INT PRIMARY KEY IDENTITY,
    MosahNumber INT,
    GarageName NVARCHAR(150),
    TypeCode INT,
    GarageType NVARCHAR(100),
    GarageAddress NVARCHAR(100),
    City NVARCHAR(70),
    Phone INT,
    ZipCode INT,
    SpecializationCode INT,
    Specialization NVARCHAR(150),
    ManagerSpecialization NVARCHAR(100),
    License INT,
    TestTime DATE,
    WorkingHours VARCHAR
);

CREATE TABLE VEHICLE (
    LicensePlate INT PRIMARY KEY,
    Model NVARCHAR(80),
    YearVehicle INT,
    FuelType NVARCHAR(70),
    Color NVARCHAR(30),
    Manufacturer NVARCHAR(100),
    CurrentMileage INT
);

CREATE TABLE CARREPAIR (
    RepairID INT PRIMARY KEY IDENTITY,
    LicensePlate INT,
    GarageID INT,
    RepairDate DATETIME,
    DescriptionCar TEXT,
    Cost INT,
    FOREIGN KEY (LicensePlate) REFERENCES VEHICLE(LicensePlate),
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID)
);

CREATE TABLE VEHICLEPART (
    PartID INT PRIMARY KEY IDENTITY,
    PartName NVARCHAR(30),
    PartNumber INT,
    Cost INT,
    Supplier NVARCHAR(50),
    ImageURL NVARCHAR(350)
);

CREATE TABLE GARAGE_PARTS (
    GaragePartID INT PRIMARY KEY IDENTITY,
    GarageID INT,
    PartID INT,
    QuantityAvailable INT,
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID),
    FOREIGN KEY (PartID) REFERENCES VEHICLEPART(PartID)
);

CREATE TABLE EMPLOYEE (
    EmployeeID INT PRIMARY KEY IDENTITY,
    NameEmployee NVARCHAR(50),
    Position NVARCHAR(70),
    GarageID INT,
    Phone INT,
    Email NVARCHAR(50),
    HireDate DATE,
    Specialization NVARCHAR,
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID)
);

CREATE TABLE REPAIRIMAGE (
    ImageID INT PRIMARY KEY IDENTITY,
    RepairID INT,
    ImageURL NVARCHAR(350),
    UploadDate DATETIME,
    FOREIGN KEY (RepairID) REFERENCES CARREPAIR(RepairID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    ManagerName NVARCHAR(50),
    License INT,
    UserStatusID INT,
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(StatusID)
);

CREATE TABLE UserStatus (
    StatusID INT PRIMARY KEY  IDENTITY,
    StatusName NVARCHAR(80)
);

Insert Into UserStatus Values('User')
Insert Into UserStatus Values('Manager')


Insert Into Users Values('admin', 23345567, 1)
Go
-- Create a login for the admin user
CREATE LOGIN [TheGarageManagerAdminLogin] WITH PASSWORD = 'admin123';
Go

-- Create a user in the YourProjectNameDB database for the login
CREATE USER [TheGarageManagerAdminUser] FOR LOGIN [TheGarageManagerAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [TheGarageManagerAdminUser];
Go

DROP DATABASE YourProjectNameDB



--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=TheGarageManagerDB;User ID=TheGarageManagerAdminUser;Password=admin123;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context TheGarageManagerDbContext -DataAnnotations –force