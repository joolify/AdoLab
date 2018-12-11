----------------------------
-- Create Database
----------------------------
create database DBContact
go

use DBContact
go

----------------------------
-- Create Tables
----------------------------
create table Contact
(ID int primary key identity not null,
SSN varchar(32) unique not null,
FirstName varchar(32) not null,
LastName varchar(32) not null)
go

create table Address
(ID int primary key identity not null, 
Street varchar(32) not null, 
City varchar(32) not null, 
Zip varchar(32) not null, 
unique(Street, City, Zip)
)
go

create table ContactToAddress
(
	ID int primary key identity not null,
	ContactID int foreign key references Contact(ID) not null,
	AddressID int foreign key references Address(ID) not null,
	unique(ContactID, AddressID)
)
go

create table ContactInformation
(
	ID int primary key identity not null,
	Info varchar(32) unique not null,
	ContactID int foreign key references Contact(ID) null)
go

----------------------------
-- Create Contact Procedures
----------------------------
create procedure CreateContact @ssn varchar(32), @firstName varchar(32), @lastName varchar(32), @id int output as
begin
	insert into
		Contact
	values
		(@ssn, @firstName, @lastName)

	set @id = SCOPE_IDENTITY()
end
go

create procedure ReadContact @id int as
begin
	select 
		*
	from
		Contact as C
	where
		id = @id
end
go

create procedure UpdateContact @id int, @ssn varchar(32), @firstName varchar(32), @lastName varchar(32) as
begin
	update
		Contact
	set
		SSN = @ssn, 
		FirstName = @firstName,
		LastName = @lastName
	where
		id = @id
end
go

create procedure DeleteContact @id int as
begin
	delete from
		Contact
	where
		id = @id
end
go
----------------------------
-- Create Address Procedures
----------------------------
create procedure CreateAddress @street varchar(32), @city varchar(32), @zip varchar(32), @id int output as
begin
	insert into
		Address
	values
		(@street, @city, @zip)

	set @id = SCOPE_IDENTITY()
end
go

create procedure ReadAddress @id int as
begin
	select 
		*
	from
		Address
	where
		id = @id
end
go

create procedure UpdateAddress @id int, @street varchar(32), @city varchar(32), @zip varchar(32) as
begin
	update
		Address
	set
		street = @street, 
		city = @city,
		zip = @zip
	where
		id = @id
end
go

create procedure DeleteAddress @id int as
begin
	delete from
		Address
	where
		id = @id
end
go
----------------------------
-- Create ContactInformation Procedures
----------------------------
create procedure CreateContactInformation @info varchar(32), @contactId int=null, @id int output as
begin
	insert into
		ContactInformation
	values
		(@info, @contactId)

	set @id = SCOPE_IDENTITY()
end
go

create procedure ReadContactInformation @id int as
begin
	select 
		*
	from
		ContactInformation
	where
		id = @id
end
go

create procedure UpdateContactInformation @id int, @info varchar(32), @contactId int as
begin
	update
		ContactInformation
	set
		Info = @info, 
		ContactID = @contactId
	where
		id = @id
end
go

create procedure DeleteContactInformation @id int as
begin
	delete from
		ContactInformation
	where
		id = @id
end
go
----------------------------
-- Create ContactToAddress Procedures
----------------------------
create procedure CreateContactToAddress @contactId int, @addressId int, @id int output as
begin
	insert into
		ContactToAddress
	values
		(@contactId, @addressId)

	set @id = SCOPE_IDENTITY()
end
go

create procedure ReadContactToAddress @id int as
begin
	select 
		*
	from
		ContactToAddress
	where
		id = @id
end
go

create procedure UpdateContactToAddress @id int, @contactId int, @addressId int as
begin
	update
		ContactToAddress
	set
		ContactID = @contactId,
		AddressID = @addressId
	where
		id = @id
end
go

create procedure DeleteContactToAddress @id int as
begin
	delete from
		ContactToAddress
	where
		id = @id
end
go
