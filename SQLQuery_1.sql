CREATE SCHEMA StoreApp;
GO


-- DROP TABLE StoreApp.Customer;
-- DROP TABLE StoreApp.Product;
-- DROP TABLE StoreApp.Location;
-- DROP TABLE StoreApp.Inventory;
-- DROP TABLE StoreApp.Orders;


CREATE TABLE StoreApp.Customer
(
    CustomerId INT NOT NULL PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(150) NOT NULL,
    LastName NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL UNIQUE
);

CREATE TABLE StoreApp.Product
(
    ProductId INT NOT NULL PRIMARY KEY IDENTITY,
    Name NVARCHAR(200) NOT NULL UNIQUE,
    Price DECIMAL NOT NULL CHECK ( Price > 0 )
);



CREATE TABLE StoreApp.Location
(
    LocationId INT NOT NULL PRIMARY KEY IDENTITY ,
    Name NVARCHAR(100) NOT NULL,
    Address NVARCHAR(200) NOT NULL UNIQUE,
    City NVARCHAR(200) NOT NULL,
    State NVARCHAR(2)

);

CREATE TABLE StoreApp.Inventory
(
    LocationId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE,
    ProductId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE,
    PRIMARY KEY ( LocationId, ProductId),
    Quantity INT NOT NULL CHECK (Quantity >= 0)
);


CREATE TABLE StoreApp.Orders
(
    OrderId INT NOT NULL PRIMARY KEY IDENTITY,
    LocationId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Location (LocationId) ON DELETE CASCADE ON UPDATE CASCADE,
    CustomerId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Customer (CustomerId) ON DELETE CASCADE ON UPDATE CASCADE,
    Date DATE NOT NULL DEFAULT GETDATE(),
);

CREATE TABLE StoreApp.OrderDetails (
    OrderId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Orders (OrderId) ON DELETE CASCADE ON UPDATE CASCADE,
    ProductId INT NOT NULL
        FOREIGN KEY REFERENCES StoreApp.Product (ProductId) ON DELETE CASCADE ON UPDATE CASCADE,
        PRIMARY KEY (OrderId, ProductId),
    Quantity INT NOT NULL CHECK (Quantity > 0),
);

INSERT INTO StoreApp.Customer (FirstName, LastName, Email) VALUES 
 ('Martin', 'Kyle', 'martink@outlook.com'),
 ('Jena', 'Finke', 'jfinke@live.com'),
 ('Antonio', 'Michalo', 'amichalo@verizon.net'),
 ('Gia', 'Fly', 'giafly@gmail.com');

INSERT INTO StoreApp.Location ( Name, Address, City, State ) VALUES 
 ('Discount BestBuy Southaven','820 Ashley Lane', 'Southaven','PA'),
 ('Discount BestBuy Winter Springs','44 Colonial Drive', 'Winter Springs','FL'),
 ('Discount BestBuy Soddy Daisy','86 Winding Way St','Soddy Daisy','TN');


INSERT INTO StoreApp.Product (Name, Price) VALUES 
('Samsung - Galaxy Tab', 169.99),
('Surface Pro X', 799.99),
('Insignia Smart Fire TV', 529.99);

INSERT INTO StoreApp.Inventory (LocationId, ProductId, Quantity) VALUES 
((SELECT LocationId FROM StoreApp.Location WHERE Address = '820 Ashley Lane'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Samsung - Galaxy Tab'), 100  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '820 Ashley Lane'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Surface Pro X'), 133  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '820 Ashley Lane'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Insignia Smart Fire TV'), 200  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '44 Colonial Drive'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Samsung - Galaxy Tab'), 150  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '44 Colonial Drive'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Surface Pro X'), 300  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '44 Colonial Drive'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Insignia Smart Fire TV'), 110  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '86 Winding Way St'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Samsung - Galaxy Tab'), 140  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '86 Winding Way St'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Surface Pro X'), 1900  ),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '86 Winding Way St'), (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Insignia Smart Fire TV'), 200  );

INSERT INTO StoreApp.Orders (LocationId, CustomerId) VALUES 
((SELECT LocationId FROM StoreApp.Location WHERE Address = '820 Ashley Lane'), (SELECT CustomerId FROM StoreApp.Customer WHERE Email = 'martink@outlook.com')),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '44 Colonial Drive'), (SELECT CustomerId FROM StoreApp.Customer WHERE Email = 'martink@outlook.com')),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '820 Ashley Lane'), (SELECT CustomerId FROM StoreApp.Customer WHERE Email = 'jfinke@live.com')),
((SELECT LocationId FROM StoreApp.Location WHERE Address = '86 Winding Way St'), (SELECT CustomerId FROM StoreApp.Customer WHERE Email = 'amichalo@verizon.net'));

INSERT INTO StoreApp.OrderDetails(OrderId, ProductId, Quantity) VALUES
    (1, (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Samsung - Galaxy Tab'), 3),
    (2, (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Samsung - Galaxy Tab'), 5),
    (2, (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Surface Pro X'), 7),
    (3, (SELECT ProductId FROM StoreApp.Product WHERE  Name = 'Insignia Smart Fire TV'), 11)