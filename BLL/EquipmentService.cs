using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;
using hotel_management.Domain;

namespace hotel_management.BLL;

public sealed class EquipmentService
{
    private readonly EquipmentRepository _equipmentRepository = new();
    private readonly RoomRepository _roomRepository = new();

    public DataTable GetEquipments() => _equipmentRepository.GetAll();
    public DataTable GetCategories() => _equipmentRepository.GetCategoryLookup();
    public DataTable GetDeviceLookup() => _equipmentRepository.GetDeviceLookup();
    public DataTable GetRoomLookup() => _roomRepository.GetRoomLookup();
    public DataTable GetEquipmentByRoom(string roomId) => _equipmentRepository.GetEquipmentByRoom(roomId);

    public ServiceResult Add(Equipment equipment)
    {
        ServiceResult validation = ValidateEquipment(equipment);
        if (!validation.Success) return validation;

        try
        {
            _equipmentRepository.Add(equipment);
            return ServiceResult.Ok("Equipment added successfully.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("Device ID already exists.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid equipment category.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while adding equipment.");
        }
    }

    public ServiceResult Update(Equipment equipment)
    {
        ServiceResult validation = ValidateEquipment(equipment);
        if (!validation.Success) return validation;

        try
        {
            int affected = _equipmentRepository.Update(equipment);
            return affected > 0 ? ServiceResult.Ok("Equipment updated.") : ServiceResult.Fail("Device not found.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid equipment category.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while updating equipment.");
        }
    }

    public ServiceResult Delete(string deviceId)
    {
        if (string.IsNullOrWhiteSpace(deviceId))
        {
            return ServiceResult.Fail("Device ID is required.");
        }

        try
        {
            int affected = _equipmentRepository.Delete(deviceId.Trim());
            return affected > 0 ? ServiceResult.Ok("Equipment deleted.") : ServiceResult.Fail("Device not found.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Cannot delete equipment because it is assigned to rooms.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while deleting equipment.");
        }
    }

    public ServiceResult AssignToRoom(string roomId, string deviceId)
    {
        if (string.IsNullOrWhiteSpace(roomId) || string.IsNullOrWhiteSpace(deviceId))
        {
            return ServiceResult.Fail("Room and Device are required.");
        }

        try
        {
            _equipmentRepository.AssignToRoom(roomId.Trim(), deviceId.Trim());
            return ServiceResult.Ok("Equipment assigned to room.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("This equipment is already assigned to the selected room.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid room or device.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while assigning equipment.");
        }
    }

    public ServiceResult RemoveFromRoom(string roomId, string deviceId)
    {
        try
        {
            int affected = _equipmentRepository.RemoveFromRoom(roomId.Trim(), deviceId.Trim());
            return affected > 0 ? ServiceResult.Ok("Equipment removed from room.") : ServiceResult.Fail("Assignment not found.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while removing equipment assignment.");
        }
    }

    private static ServiceResult ValidateEquipment(Equipment equipment)
    {
        if (string.IsNullOrWhiteSpace(equipment.DeviceId) ||
            string.IsNullOrWhiteSpace(equipment.DeviceName) ||
            string.IsNullOrWhiteSpace(equipment.CategoryId))
        {
            return ServiceResult.Fail("Device ID, Device Name, and Category are required.");
        }

        if (equipment.UnitPrice < 0)
        {
            return ServiceResult.Fail("Unit price must be >= 0.");
        }

        if (equipment.DeviceStatus is < 0 or > 1)
        {
            return ServiceResult.Fail("DeviceStatus must be 0 (valid) or 1 (invalid).");
        }

        return ServiceResult.Ok("Valid");
    }
}
