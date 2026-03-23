using System.Data;
using Microsoft.Data.SqlClient;
using hotel_management.DAL;
using hotel_management.Domain;

namespace hotel_management.BLL;

public sealed class CustomerService
{
    private readonly CustomerRepository _repository = new();

    public DataTable GetCustomers(string keyword = "") => _repository.GetAll(keyword);
    public DataTable GetLookup() => _repository.GetLookup();

    public ServiceResult Add(Customer customer)
    {
        ServiceResult validation = Validate(customer);
        if (!validation.Success) return validation;

        try
        {
            _repository.Add(customer);
            return ServiceResult.Ok("Customer added successfully.");
        }
        catch (SqlException ex) when (ex.Number is 2627 or 2601)
        {
            return ServiceResult.Fail("Member ID already exists.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while adding customer.");
        }
    }

    public ServiceResult Update(Customer customer)
    {
        ServiceResult validation = Validate(customer);
        if (!validation.Success) return validation;

        try
        {
            int affected = _repository.Update(customer);
            return affected > 0 ? ServiceResult.Ok("Customer updated.") : ServiceResult.Fail("Customer not found.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while updating customer.");
        }
    }

    public ServiceResult Delete(string memberId)
    {
        if (string.IsNullOrWhiteSpace(memberId))
        {
            return ServiceResult.Fail("Member ID is required.");
        }

        try
        {
            int affected = _repository.Delete(memberId.Trim());
            return affected > 0 ? ServiceResult.Ok("Customer deleted.") : ServiceResult.Fail("Customer not found.");
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            return ServiceResult.Fail("Cannot delete customer because related stay records exist.");
        }
        catch (SqlException)
        {
            return ServiceResult.Fail("Database error while deleting customer.");
        }
    }

    private static ServiceResult Validate(Customer customer)
    {
        if (string.IsNullOrWhiteSpace(customer.MemberId) ||
            string.IsNullOrWhiteSpace(customer.NationalId) ||
            string.IsNullOrWhiteSpace(customer.Name) ||
            string.IsNullOrWhiteSpace(customer.LastName))
        {
            return ServiceResult.Fail("Member ID, National ID, Name, and Last Name are required.");
        }

        return ServiceResult.Ok("Valid");
    }
}
