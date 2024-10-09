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
    GarageID NVARCHAR PRIMARY KEY,
    MosahNumber INT,
    GarageName NVARCHAR,
    TypeCode INT,
    GarageType NVARCHAR,
    GarageAddress NVARCHAR,
    City NVARCHAR,
    Phone INT,
    ZipCode INT,
    SpecializationCode INT,
    Specialization NVARCHAR,
    ManagerSpecialization NVARCHAR,
    License INT,
    TestTime DATE,
    WorkingHours VARCHAR
);

CREATE TABLE VEHICLE (
    LicensePlate INT PRIMARY KEY,
    Model NVARCHAR,
    YearVehicle INT,
    FuelType NVARCHAR,
    Color NVARCHAR,
    Manufacturer NVARCHAR,
    CurrentMileage INT
);

CREATE TABLE CARREPAIR (
    RepairID INT PRIMARY KEY,
    LicensePlate INT,
    GarageID INT,
    RepairDate DATETIME,
    DescriptionCar TEXT,
    Cost INT,
    FOREIGN KEY (LicensePlate) REFERENCES VEHICLE(LicensePlate),
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID)
);

CREATE TABLE VEHICLEPART (
    PartID INT PRIMARY KEY,
    PartName NVARCHAR,
    PartNumber INT,
    Cost INT,
    Supplier NVARCHAR,
    ImageURL NVARCHAR
);

CREATE TABLE GARAGE_PARTS (
    GaragePartID INT PRIMARY KEY,
    GarageID INT,
    PartID INT,
    QuantityAvailable INT,
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID),
    FOREIGN KEY (PartID) REFERENCES VEHICLEPART(PartID)
);

CREATE TABLE EMPLOYEE (
    EmployeeID INT PRIMARY KEY,
    NameEmployee NVARCHAR,
    Position NVARCHAR,
    GarageID INT,
    Phone INT,
    Email NVARCHAR,
    HireDate DATE,
    Specialization NVARCHAR,
    FOREIGN KEY (GarageID) REFERENCES GARAGE(GarageID)
);

CREATE TABLE REPAIRIMAGE (
    ImageID INT PRIMARY KEY,
    RepairID INT,
    ImageURL NVARCHAR,
    UploadDate DATETIME,
    FOREIGN KEY (RepairID) REFERENCES CARREPAIR(RepairID)
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY,
    ManagerName NVARCHAR,
    License INT,
    UserStatusID INT,
    FOREIGN KEY (UserStatusID) REFERENCES UserStatus(StatusID)
);

CREATE TABLE UserStatus (
    StatusID INT PRIMARY KEY,
    StatusName NVARCHAR
);

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



--scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=TheGarageManagerDB;User ID=TheGarageManagerAdminLogin;Password=thePassword;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context TheGarageManagerDbContext -DataAnnotations –force