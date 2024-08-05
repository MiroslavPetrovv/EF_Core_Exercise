using System.ComponentModel.DataAnnotations;
using TravelAgency.Data;

namespace TravelAgency.DataProcessor
{
    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data format!";
        private const string DuplicationDataMessage = "Error! Data duplicated.";
        private const string SuccessfullyImportedCustomer = "Successfully imported customer - {0}";
        private const string SuccessfullyImportedBooking = "Successfully imported booking. TourPackage: {0}, Date: {1}";

        public static string ImportCustomers(TravelAgencyContext context, string xmlString)
        {
           StringBuilder sb = new StringBuilder();

            XmlHelper xmlHelper = new XmlHelper();
            const string XmlRoot = "Customers";

            ICollection<Customer> customersToImport = new List<Customer>();
            ImportCustomerDto[] CustomerDtos =
            xmlHelper.Deserialize<ImportCustomerDto[]>(xmlString, XmlRoot);
            foreach (ImportCustomerDto customerDto in CustomerDtos )
                {
                    if (!IsValid(customerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                        bool customerExists = customersToImport.Any(c =>
                           c.FullName == customerDto.FullName ||
                           c.Email == customerDto.Email ||
                           c.PhoneNumber == customerDto.PhoneNumber);

                    if (customerExists )
                        {
                            sb.AppendLine(DuplicationDataMessage);
                            continue;
                        }
                     Customer newCustomer = new Customer()
                    {
                        FullName = customerDto.FullName,
                        Email = customerDto.Email,
                        PhoneNumber = customerDto.PhoneNumber,
                     };
                    customersToImport.Add(newCustomer);
                    sb.AppendLine(string.Format(SuccessfullyImportedCustomer, customerDto.FullName));
            }
            context.Customers.AddRange(customersToImport);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportBookings(TravelAgencyContext context, string jsonString)
        {
            throw new NotImplementedException();
        }

        public static bool IsValid(object dto)
        {
            var validateContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(dto, validateContext, validationResults, true);

            foreach (var validationResult in validationResults)
            {
                string currValidationMessage = validationResult.ErrorMessage;
            }

            return isValid;
        }
    }
}
