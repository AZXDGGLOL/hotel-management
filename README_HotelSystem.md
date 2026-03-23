# Hotel Management System (WinForms + SQL Server)

## 1) Project Structure

```
hotel-management/
  BLL/
    CustomerService.cs
    EquipmentService.cs
    PaymentService.cs
    RoomService.cs
    ServiceResult.cs
    StayService.cs
  DAL/
    CustomerRepository.cs
    EquipmentRepository.cs
    PaymentRepository.cs
    RoomRepository.cs
    SqlDataAccess.cs
    StayRepository.cs
  Domain/
    Customer.cs
    Equipment.cs
    Receipt.cs
    Room.cs
    Stay.cs
    StayDetail.cs
  Forms/
    HotelSystemForm.cs
    ... (existing legacy forms)
  Data/
    HotelDb.cs
  Infrastructure/
    DbConnectionFactory.cs
  Program.cs
```

## 2) Notes

- This app **does not create or alter schema**.
- It uses `Microsoft.Data.SqlClient` and parameterized queries.
- Connection string is read from:
  - `HOTEL_DB_CONNECTION` environment variable, or
  - fallback in `Data/HotelDb.cs`.

## 3) Run

1. Ensure SQL Server is running and your schema already exists.
2. Set connection string (recommended):
   - PowerShell:
     - `$env:HOTEL_DB_CONNECTION="Data Source=.\SQLEXPRESS;Initial Catalog=HotelDB;Integrated Security=True;TrustServerCertificate=True;"`
3. Build and run:
   - `dotnet build`
   - `dotnet run`

## 4) Core SQL Examples Used

- Room list with joins:
```sql
SELECT r.RoomId, r.Floor, rl.LevelName, rc.CategoryName
FROM [Room] r
JOIN [RoomLevels] rl ON rl.LevelId = r.LevelId
JOIN [RoomCategories] rc ON rc.CategoryId = r.CategoryID;
```

- Room overlap check:
```sql
SELECT COUNT(1)
FROM [Stay List]
WHERE RoomNumber = @RoomNumber
  AND @CheckInDate < CheckOutDate
  AND @CheckOutDate > CheckInDate;
```

- Payment transaction:
```sql
INSERT INTO [Receipt] (...)
UPDATE [Stay List] SET PaymentStatus = 'Paid', ReceiptId = @ReceiptID
WHERE StayID = @StayID AND StaySequenceNo = @StaySequenceNo;
```
