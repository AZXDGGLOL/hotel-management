Use master
Go
If Exists (Select name
           From master.dbo.sysdatabases
           Where name = 'HotelDB')
    Drop Database HotelDB
Go

Create Database HotelDB

Alter Database HotelDB Collate Thai_CI_AS
Go

use HotelDB

Create Table [Device Category]
(
    CategoryId   Int IDENTITY (1,1),
    CategoryName Nvarchar(15) Not Null,
    Constraint PK_Device_Category
        Primary Key (CategoryId)
)
Go

Create Table Devices
(
    DeviceId      Int IDENTITY (1,1),
    DeviceName    Nvarchar(15) Not Null,
    Brand         Nvarchar(10),
    Model         Nvarchar(20),
    UnitPrice     Money,
    DevicePicture Varbinary(Max),
    DeviceStatus  Bit          Not Null,
    CategoryId    Int,
    Constraint PK_Devices
        Primary Key (DeviceId),
    Constraint FK_Devices
        Foreign Key (CategoryId)
            References [Device Category] (CategoryId)
            On Delete Set Null
            On Update Cascade
)

Go

Create Table [Room Levels]
(
    LevelId     Int IDENTITY (1,1),
    LevelName   Nvarchar(20) Not Null,
    PricePerDay Money,
    Constraint PK_Room_Levels
        Primary Key (LevelId)
)

Go

Create Table [Room Categories]
(
    CateGoryId   Int      Not Null IDENTITY (1,1),
    CategoryName Nvarchar Not Null,
    Constraint PK_Room_Categories
        Primary Key (CateGoryId)
)

Go

Create Table Rooms
(
    RoomId     Int,
    Floor      Int,
    LevelId    Int,
    CategoryId Int,
    Constraint PK_Rooms
        Primary Key (RoomId),
    Constraint FK_Rooms_Level
        Foreign Key (LevelId)
            References [Room Levels] (LevelId)
            On Delete Set Null
            On Update Cascade
)

Go

Create Table [Room Devices]
(
    DeviceId Int,
    RoomId   Int,
    Constraint PK_Room_Devices
        Primary Key (DeviceId, RoomId),
    Constraint PK_Device
        Foreign Key (DeviceId)
            References Devices (DeviceId)
            On Delete Cascade
            On Update Cascade,
    Constraint PK_Room
        Foreign Key (RoomId)
            References Rooms (RoomId)
            On Delete Cascade
            On Update Cascade
)

Go

Create Table Member
(
    MemberId    Int          Not Null IDENTITY (1,1),
    NationalId  Int          Not Null,
    Name        Nvarchar(25) Not Null,
    LastName    Nvarchar(25) Not Null,
    Nationality Nvarchar(15),
    Address     Nvarchar(60) Not Null,
    Email       Nvarchar(25),
    Phone       Nvarchar(10),
    Constraint PK_Member
        Primary Key (MemberId)
)

Go

Create Table Receipt
(
    ReceiptId    Int IDENTITY (1,1),
    Date         Datetime,
    CustomerName Nvarchar(50),
    Address      Nvarchar(60),
    Phone        Nvarchar(10),
    NetPrice     Money,
    Discount     Real,
    Constraint PK_Receipt
        Primary Key (ReceiptId)
)

Go

Create Table Stay
(
    StayId    Int IDENTITY (1,1),
    EntryDate Datetime,
    MemberId  Int,
    Constraint PK_Stay
        Primary Key (StayId),
    Constraint Fk_Member
        Foreign Key (MemberId)
            References Member (MemberId)
            On Delete Set Null
            On Update Cascade
)

Go

Create Table [Stay List]
(
    StayId        Int Not Null,
    StaySequence  Int Not Null IDENTITY (1,1),
    CheckInDate   Datetime,
    CheckOutDate  Datetime,
    Comment       Nvarchar(25),
    PaymentStatus Tinyint,
    ReceiptId     Int,
    RoomId        Int,
    Constraint PK_Stay_List
        Primary Key (StayId, StaySequence),
    Constraint FK_Stay
        Foreign Key (StayId)
            References Stay (StayId)
            On Delete Cascade
            On update Cascade,
    Constraint FK_Receipt
        Foreign Key (ReceiptId)
            References Receipt (ReceiptId)
            On Delete Cascade
            On update Cascade,
    Constraint FK_Room
        Foreign Key (RoomId)
            References Rooms (RoomId)
            On Delete Cascade
            On Update Cascade
)