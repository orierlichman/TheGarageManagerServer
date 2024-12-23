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



INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Alternator', 101, 150, 'https://highlinecarcare.com/wp-content/uploads/2023/07/the-importance-of-alternator-repair-for-vehicle-performance.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Brake Pads', 102, 50, 'https://www.boschautoparts.com/documents/647135/656978/BlueDiscPads_PDP_Carousel.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Spark Plugs', 103, 30, 'https://images.goapr.com/c1dd3a9e309da6fa9510de532ff312f1e85a5e9b.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Oil Filter', 104, 15, 'https://www.pgfilters.com/wp-content/uploads/2023/02/What-is-the-Oil-Filters-Primary-Job_-1000x675-1.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Radiator', 105, 200, 'https://s19528.pcdn.co/wp-content/uploads/2019/08/Radiator.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Timing Belt', 106, 120, 'https://www.krishnaautoelectric.com/blog/wp-content/uploads/2018/04/What-is-a-Timing-Belt.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Battery', 107, 80, 'https://antigravitybatteries.com/wp-content/uploads/2019/02/antigravity-group-51r-lithium-auto-battery.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Brake Discs', 108, 90, 'https://www.championautoparts.com/content/loc-emea/loc-eu/fmmp-champion/en_GB/products/light-vehicles/brakes/brake-discs/_jcr_content/main-par/product_feature/featureContent1/image.img.png/brake-disc-main-1513849824310.png');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Windshield Wiper Blades', 109, 20, 'https://elegantautoretail.com/cdn/shop/files/wipers-universal_1000x.jpg?v=1692946381');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Air Filter', 110, 25, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRuoFw4AH-cYt84uXcgocwD-gP87qQCKuKNbn57br73S2mtQ_wc9qekomlDBIHmDr1HRXs&usqp=CAU');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Fuel Pump', 111, 180, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQMVUdZ5EVf-LiieSq8KYmIL9TEDKBHwI8wIg&s');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Headlights', 112, 100, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRdiJOE56NCDH0JJeYQGFsiawD2aDanvG9ciw&s');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Exhaust System', 113, 250, 'https://www.japspeed.co.uk/wp-content/uploads/2022/02/products-zz00405_1_-wpp1652793646385.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Suspension Springs', 114, 110, 'https://bumper-blog-prod.s3.eu-west-1.amazonaws.com/cars_coil_springs_0a8ebc2027.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Clutch Kit', 115, 150, 'https://res.cloudinary.com/firstinternet/image/upload/v1504599644/Exedy/CLUTCH%20KIT-b1O9b.png');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'CV Joint', 116, 130, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRzcgapPVaEKJuzyMIDK3O3mRrB8LiGmgDsRA&s');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Power Steering Pump', 117, 170, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRYPFrAUzo1-0umNcHzB_NSvmJE1dCJo_79aQ&s');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Engine Mount', 118, 90, 'https://d2hucwwplm5rxi.cloudfront.net/wp-content/uploads/2022/01/21095425/Cover-200122-1.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Oil Pan', 119, 100, 'https://images-cdn.ubuy.co.in/64f2f909f458d90caa357cc1-a-premium-engine-oil-pan-sump-with-drain.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Tire', 120, 75, 'https://hmr.ph/images/postings/2023/11/e8xk21vlggloqzv5p7.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Timing Chain', 121, 140, 'https://lh3.googleusercontent.com/proxy/GMacJlujipv-IGTmjChk9TNMr6JgGgcnc1KrsWOhVgENEgWtC8PQFLgTdYxxhANtJl3alPmPO9Eq-K1Xx4nvBRbSnNWciIa0uJch-KeJhH3dMcyYppzN-dReXVMQPnDe');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Throttle Body', 122, 160, 'https://cfx-wp-images.imgix.net/2024/06/eic05Fin-GettyImages-1369405546-scaled.jpg?auto=compress%2Cformat&fit=scale&h=683&ixlib=php-3.3.1&w=1024&wpsize=large&s=42c248087f631be65dcd5b4fc712b9f3');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Fuel Injector', 123, 180, 'https://www.bosch-mobility.com/media/global/solutions/two-wheeler-and-powersports/powertrain/combustion_engine/fuel-injector/fuel_injector_thumbnail.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Drive Belt', 124, 45, 'https://static.mrosupply.com/cache/7f/c0/7fc0d0e4f2dcc97b60e051cec9cf48cd.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Oil Pressure Switch', 125, 35, 'https://m.media-amazon.com/images/I/51NUON0W0QL.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Windshield', 126, 200, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSuzYR_5LSrE89NIlJWdaasHZ3O9Rlc-82Cj3HSi8U2NbzaaqVvCTYq6UXkLsU3JO7Y7JA&usqp=CAU');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Carburetor', 127, 160, 'https://www.carandbike.com/_next/image?url=https%3A%2F%2Fc.ndtvimg.com%2F2022-03%2F61j1as8_car_625x300_21_March_22.jpeg&w=3840&q=75');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Fender', 128, 120, 'https://5.imimg.com/data5/EY/UJ/MY-23761876/car-fender-500x500.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Bumper', 129, 180, 'https://m.media-amazon.com/images/I/51UlvSA-LPS.jpg');
INSERT INTO GarageParts (GarageID, PartName, PartNumber, Cost, ImageURL) VALUES (103, 'Radiator Fan', 130, 90, 'https://grimmermotors.co.nz/wp-content/uploads/2019/04/radiator-fan-replacement-hamilton-nz.jpg');


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