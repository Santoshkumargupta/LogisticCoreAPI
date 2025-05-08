

Create Table Roles(
 RoleId		int not null Primary Key Identity(1,1),
 RoleName	nvarchaR(50)
 )

 Create Table [User] (
    Id               int Identity(1,1) Primary Key not null,
    FullName           nvarchar(250) not null,
	Email              nvarchar(50) not null Unique,
	Password        nvarchar(50) not null,
	UserName       nvarchar(50) unique,
	RoleId  int not null References Roles(RoleId),
    Dob       DateTime,
	RememberToken   nvarchar(max),
	CreatedAt    DateTime,
	loggedInAt        DateTime
   )


   -- Supplier Table
CREATE TABLE Supplier (
    SupplierID       INT IDENTITY(1,1) PRIMARY KEY,
    SupplierName     NVARCHAR(100) NOT NULL,
    ContactNumber    NVARCHAR(20),
    Email            NVARCHAR(100),
    Address          NVARCHAR(200),
    City             NVARCHAR(100),
    Country          NVARCHAR(100),
    CreatedDate      DATETIME DEFAULT GETDATE()
);

-- Product Table
CREATE TABLE Product (
    ProductID         INT IDENTITY(1,1) PRIMARY KEY,
    ProductName       NVARCHAR(100) NOT NULL,
    Category          NVARCHAR(100),
    Price             DECIMAL(18,2) NOT NULL,
    StockQuantity     INT NOT NULL,
    SupplierID        INT NOT NULL FOREIGN KEY REFERENCES Supplier(SupplierID),
    ManufacturedDate  DATE,
    ExpiryDate        DATE,
    Description       NVARCHAR(MAX),
    IsActive          BIT DEFAULT 1,
    CreatedDate       DATETIME DEFAULT GETDATE()
);

-- Order Table
CREATE TABLE [Order] (
    OrderID        INT IDENTITY(1,1) PRIMARY KEY,
    OrderDate      DATETIME NOT NULL,
    CustomerName   NVARCHAR(100) NOT NULL,
    CustomerEmail  NVARCHAR(100),
    TotalAmount    DECIMAL(18,2) NOT NULL,
    OrderStatus    NVARCHAR(50),
    CreatedDate    DATETIME DEFAULT GETDATE()
);

-- OrderDetails Table
CREATE TABLE OrderDetails (
    OrderDetailID  INT IDENTITY(1,1) PRIMARY KEY,
    OrderID        INT NOT NULL FOREIGN KEY REFERENCES [Order](OrderID),
    ProductID      INT NOT NULL FOREIGN KEY REFERENCES Product(ProductID),
    Quantity       INT NOT NULL,
    SubTotal       DECIMAL(18,2) NOT NULL
);

CREATE TABLE RefreshToken (
    Id                INT IDENTITY(1,1) PRIMARY KEY,
    UserId            INT NOT NULL,
    Token             NVARCHAR(MAX) NOT NULL,
    ExpiresAt         DATETIME NOT NULL,
    IsExpired         BIT NOT NULL,
    IsActive          BIT NOT NULL,
    CreatedAt         DATETIME NOT NULL DEFAULT GETDATE(),
    CreatedByIp       NVARCHAR(50),
    RevokedAt         DATETIME NULL,
    RevokedByIp       NVARCHAR(50),
    ReplacedByToken   NVARCHAR(MAX)
);