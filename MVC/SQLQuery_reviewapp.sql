CREATE TABLE Customer(
    Id int PRIMARY KEY IDENTITY(1, 1),
    FirstName varchar(50) not null,
    LastName varchar(50) not null,
    UserName varchar(10) not null,
    Email varchar(250) not null,
    Password varchar(100) not null,
);
Select * From Customer;

Create TABLE Restaurant(
    Id int PRIMARY KEY IDENTITY(1, 1),
    Name varchar(100) not null,
    Location varchar(255)not null,
    Contact varchar(15) not null,
);
SELECT * From Restaurant;

CREATE TABLE Review(
    Id int PRIMARY KEY IDENTITY(1, 1),
    Comment varchar(4000) not null,
	Time DateTime not null,
	Rating decimal(5) not null,
);
SELECT * From Review;

CREATE table ReviewJoin(
    Id int PRIMARY Key IDENTITY(1, 1),
    ReviewId int Foreign Key REFERENCES Review(Id) not null,
    RestaurantId int FOREIGN Key REFERENCES Restaurant(Id) not null,
    CustomerId int FOREIGN Key REFERENCES Customer(Id) not null,
);
SELECT * From ReviewJoin;

Insert Into Customer (FirstName, LastName, UserName, Email, Password) Values ('Emma', 'Wooden', 'emwoo', 'emma@gmail.com', '123');
Insert into Review (Comment, Time, Rating) Values ('interesting mission, menu is diverse', '2018-08-16', 4.5);
INSERT into Restaurant (Name, Location, Contact, Zipcode) Values('Ginza Ramen', '28th street, Grand Rapids, Michigan 49535', '616-345-8000', 'MI49544');
Insert into ReviewJoin (ReviewId, RestaurantId, CustomerId) VALUES (1, 1, 1);

SELECT Customer.FirstName, ReviewJoin.ReviewId, Review.Comment
From ((ReviewJoin
INNER JOIN Customer on Customer.Id = ReviewJoin.CustomerId)
INNER Join Review on Review.Id = ReviewJoin.ReviewId);

SELECT Customer.FirstName, Restaurant.Name, Review.Comment, Review.Rating
From (((ReviewJoin
INNER JOIN Customer on Customer.Id = ReviewJoin.CustomerId)
INNER JOIN Restaurant on Restaurant.Id = ReviewJoin.RestaurantId)
INNER Join Review on Review.Id = ReviewJoin.ReviewId);


ALTER TABLE Restaurant
Add Zipcode varchar(10);

ALTER TABLE Review
ALTER COLUMN Rating decimal(10, 2);

SELECT * From Review;
Select * From Customer;
SELECT * From Restaurant;
SELECT * From ReviewJoin;

ALTER TABLE ReviewJoin
ALTER COLUMN CustomerId
    int NULL;

ALTER TABLE ReviewJoin
ALTER COLUMN RestaurantId
    int NULL;

ALTER TABLE ReviewJoin
ALTER COLUMN ReviewId
    int NULL;
