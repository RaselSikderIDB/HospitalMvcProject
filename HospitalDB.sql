drop  database HospitalDBMvcProject
create database HospitalDBMvcProject
use HospitalDBMvcProject;

Create table Department(
DepartmentID int primary key identity(1,1)not null,
DepartmentName varchar(50)
);
Create table Doctor(
DoctorID int primary key identity(1,1)not null,
DoctorName varchar(50),
PhoneNo varchar(50),
Email varchar (50)not null,
JoiningDate datetime not null,
ImageName NVARCHAR(600)NULL,
ImageUrl NVARCHAR(600)NULL
);

Create table Patient(
PatientID int primary key identity(1,1)not null,
PatientName varchar(50),
Gender varchar(50),
Age varchar(50),
BirthDate Date,
Address varchar(50),
Picture varchar(250),
DepartmentID int references Department(DepartmentID),
ReferDoctor int references Doctor(DoctorID)
);

CREATE TABLE TblUser(
	UserID int primary key NOT NULL,
	Username varchar(50) NULL,
	UserPass varchar(50) NULL,
	UserType varchar(50) NULL,
	)
CREATE TABLE TblUserRole(
	UserRoleID int primary key NOT NULL,
	UserID int references TblUser(UserID) NULL,
	PageName varchar(50) NULL,
	IsCreate bit NULL,
	IsRead bit NULL,
	IsUpdate bit NULL,
	IsDelete bit NULL,
	)
	select * from Patient