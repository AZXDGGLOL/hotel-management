using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;
using hotel_management.Domain;

namespace hotel_management.BLL;

public sealed class RoomService
{
    private readonly RoomRepository _repository = new();

    public DataTable GetRooms() => _repository.GetAll();
    public DataTable GetRoomLookup() => _repository.GetRoomLookup();
    public DataTable GetAvailableRooms(DateTime checkInDate, DateTime checkOutDate, string keyword) =>
        _repository.GetAvailableRooms(checkInDate, checkOutDate, keyword.Trim());
    public DataTable GetLevels() => _repository.GetLevelLookup();
    public DataTable GetCategories() => _repository.GetCategoryLookup();

    public ServiceResult Add(Room room)
    {
        ServiceResult validation = Validate(room);
        if (!validation.Success) return validation;

        try
        {
            _repository.Add(room);
            return ServiceResult.Ok("Room added successfully.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("Room ID already exists.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid Level or Room Category.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while adding room.");
        }
    }

    public ServiceResult Update(Room room)
    {
        ServiceResult validation = Validate(room);
        if (!validation.Success) return validation;

        try
        {
            int affected = _repository.Update(room);
            return affected > 0 ? ServiceResult.Ok("Room updated.") : ServiceResult.Fail("Room not found.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid Level or Room Category.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while updating room.");
        }
    }

    public ServiceResult Delete(string roomId)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            return ServiceResult.Fail("Room ID is required.");
        }

        try
        {
            int affected = _repository.Delete(roomId.Trim());
            return affected > 0 ? ServiceResult.Ok("Room deleted.") : ServiceResult.Fail("Room not found.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Cannot delete room because related stay details or room devices exist.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while deleting room.");
        }
    }

    private static ServiceResult Validate(Room room)
    {
        if (string.IsNullOrWhiteSpace(room.RoomId) ||
            string.IsNullOrWhiteSpace(room.LevelId) ||
            string.IsNullOrWhiteSpace(room.CategoryId))
        {
            return ServiceResult.Fail("Room ID, Level, and Category are required.");
        }

        if (room.Floor <= 0)
        {
            return ServiceResult.Fail("Floor must be greater than 0.");
        }

        return ServiceResult.Ok("Valid");
    }
}
