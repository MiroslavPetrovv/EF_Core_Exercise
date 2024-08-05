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
            StringBuilder sb = new StringBuilder();
 ICollection<Booking> bookings = new List<Booking>();
 ImportBookingDto[] importBookingDtos = 
     JsonConvert.DeserializeObject<ImportBookingDto[]>(jsonString);
 foreach (ImportBookingDto bookingDto in importBookingDtos)
 {
     if (!IsValid(bookingDto))
     {
         sb.AppendLine(ErrorMessage);
         continue;
     }
     DateTime bookingDate;
     if (!DateTime.TryParseExact(bookingDto.BookingDate,
         "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out bookingDate))
     {
         sb.AppendLine(ErrorMessage);
         continue;
     }
     int customerId = context.Customers.FirstOrDefault(c => c.FullName == bookingDto.CustomerName).Id;
     int tourPackageId = context.TourPackages.FirstOrDefault(
            tp => tp.PackageName == bookingDto.TourPackageName).Id;
     if (customerId ==0 || tourPackageId ==0 )
     {
         sb.AppendLine(ErrorMessage);
         continue;
     }
     Booking booking = new Booking()
     {
         CustomerId = customerId,
         TourPackageId = tourPackageId,
         BookingDate = bookingDate,
     };
     bookings.Add(booking);
     sb.AppendLine(string.Format(SuccessfullyImportedBooking,bookingDto.TourPackageName,
         bookingDto.BookingDate));
 }
 context.Bookings.AddRange(bookings);
 context.SaveChanges();
 return sb.ToString().TrimEnd();
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
