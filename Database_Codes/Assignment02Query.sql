CREATE DATABASE ASSIGNMENT02
GO
USE ASSIGNMENT02
GO

CREATE TABLE Account
(
	AccountID INT IDENTITY PRIMARY KEY,
	FullName VARCHAR (100),
	Email VARCHAR(100),
	AccountPassword VARCHAR(100),
	PersonAddress VARCHAR(100),
	PersonPhoneNo VARCHAR(100),
	Gender VARCHAR(100),
	BloodType VARCHAR(100),
	AccountRole VARCHAR(100),
	DOB DATE,
)

SELECT * FROM Account

DROP TABLE Booking

CREATE TABLE Booking
(
	BookingID INT IDENTITY PRIMARY KEY,
	BookingDate DATE,
	TimeSlot TIME,
	Department VARCHAR(100),
	DoctorName VARCHAR(100),
	DoctorEmail VARCHAR(100),
	AccountID INT, 
	PatientName VARCHAR (100),
	PatientEmail VARCHAR(100),
	BookingStatus VARCHAR(100),
	FOREIGN KEY (AccountID) REFERENCES Account(AccountID) 
)

SELECT * FROM Booking

