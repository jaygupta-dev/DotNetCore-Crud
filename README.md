# DotNetCore-Crud
This project is built using C# .NET Core MVC. All business logic is implemented within the controller.

**Connection String**
The ConnectionString is located in appsettings.json.

**Project Structure**
Controller: Contains all logic and handles requests.
Model: Represents the data structure.
View: Provides the UI for the application.

**Technologies Used**
C#
.NET Core MVC
HTML, CSS, JavaScript (Frontend)

# Database Setup

**Run the following SQL script in SQL Server to set up the database:**
CREATE DATABASE DotNetCore_CRUD;
USE DotNetCore_CRUD;

CREATE TABLE tbl_CoreCrud (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    ProfilePath NVARCHAR(100) NULL,
    Gender NVARCHAR(100) NULL,
    Qualification NVARCHAR(100) NULL,
    Skills NVARCHAR(100) NULL,
    Message NVARCHAR(100) NULL,
    MobileNumber BIGINT NULL
);

**Stored Procedures**
**Insert Data**
create proc sp_InsertFormData
@Name nvarchar(100) = null,
@Email nvarchar(100) = null,
@ProfilePath nvarchar(100) = null,
@Gender nvarchar(100) = null,
@Qualification nvarchar(100) = null,
@Skills nvarchar(100) = null,
@Message nvarchar(100) = null,
@MobileNumber bigint  = null
as
begin
	insert into tbl_CoreCrud(Name,Email,ProfilePath,Gender,Qualification,Skills,Message,MobileNumber)values(@Name,@Email,@ProfilePath,@Gender,@Qualification,@Skills,@Message,@MobileNumber)
end

**Fetch All Data**
create proc sp_GetAllFormData
as
begin
	select Id, Name, Email, ProfilePath, Gender, Qualification, Skills, Message, MobileNumber from tbl_CoreCrud
end

**Delete Data**
create proc sp_DeleteFormData
@Id int = 0
as
begin
delete from tbl_CoreCrud where Id = @Id
end

**Fetch Data By Id For Update**
create proc sp_GetDataByID
@Id int = 0
as
begin
select Id, Name, Email, ProfilePath, Gender, Qualification, Skills, Message, MobileNumber from tbl_CoreCrud where Id = @Id
end

**Update Data**
create proc sp_UpdateFormData
@Id int = 0,
@Name nvarchar(100) = null,
@Email nvarchar(100) = null,
@ProfilePath nvarchar(100) = null,
@Gender nvarchar(100) = null,
@Qualification nvarchar(100) = null,
@Skills nvarchar(100) = null,
@Message nvarchar(100) = null,
@MobileNumber bigint  = null
as
begin
	update tbl_CoreCrud
	set
		Name = @Name,
		Email = @Email,
		ProfilePath = @ProfilePath,
		Gender = @Gender,
		Qualification = @Qualification,
		Skills = @Skills,
		Message = @Message,
		MobileNumber = @MobileNumber
	where Id = @Id
end








