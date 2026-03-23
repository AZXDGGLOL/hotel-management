using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;
using hotel_management.Domain;

namespace hotel_management.BLL;

public sealed class StayService
{
    private readonly StayRepository _stayRepository = new();
    private readonly CustomerRepository _customerRepository = new();
    private readonly RoomRepository _roomRepository = new();

    public DataTable GetStays() => _stayRepository.GetStays();
    public DataTable GetStayLookup() => _stayRepository.GetStayLookup();
    public DataTable GetStayDetails(string stayId = "") => _stayRepository.GetStayDetails(stayId);
    public DataTable GetCustomerLookup() => _customerRepository.GetLookup();
    public DataTable GetRoomLookup() => _roomRepository.GetRoomLookup();

    public ServiceResult CreateStay(Stay stay)
    {
        if (string.IsNullOrWhiteSpace(stay.StayId) || string.IsNullOrWhiteSpace(stay.MemberId))
        {
            return ServiceResult.Fail("Stay ID and Member are required.");
        }

        try
        {
            _stayRepository.AddStay(stay);
            return ServiceResult.Ok("Stay created successfully.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("Stay ID already exists.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Selected customer does not exist.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while creating stay.");
        }
    }

    public ServiceResult CheckRoomAvailability(string roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        if (string.IsNullOrWhiteSpace(roomId))
        {
            return ServiceResult.Fail("Room is required.");
        }

        if (checkOutDate.Date <= checkInDate.Date)
        {
            return ServiceResult.Fail("Check-out date must be after check-in date.");
        }

        try
        {
            bool available = _stayRepository.IsRoomAvailable(roomId.Trim(), checkInDate, checkOutDate);
            return available
                ? ServiceResult.Ok("Room is available.")
                : ServiceResult.Fail("Room is occupied in the selected date range.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while checking room availability.");
        }
    }

    public ServiceResult AddStayDetail(StayDetail detail)
    {
        if (string.IsNullOrWhiteSpace(detail.StayId) || string.IsNullOrWhiteSpace(detail.RoomNumber))
        {
            return ServiceResult.Fail("Stay and Room are required.");
        }

        if (detail.CheckOutDate.Date <= detail.CheckInDate.Date)
        {
            return ServiceResult.Fail("Check-out date must be after check-in date.");
        }

        try
        {
            bool available = _stayRepository.IsRoomAvailable(detail.RoomNumber, detail.CheckInDate, detail.CheckOutDate);
            if (!available)
            {
                return ServiceResult.Fail("Cannot add detail: room already occupied in this range.");
            }

            detail.StaySequenceNo = _stayRepository.GetNextSequence(detail.StayId);
            _stayRepository.AddStayDetail(detail);
            return ServiceResult.Ok("Stay detail added.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Invalid Stay ID or Room Number.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while adding stay detail.");
        }
    }
}
