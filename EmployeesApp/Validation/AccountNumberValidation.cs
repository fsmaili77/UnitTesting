namespace EmployeesApp.Validation
{
    public class AccountNumberValidation
    {
        // Define constants for the lengths of different parts in the account number.
        private const int startingPartLength = 3;
        private const int middlePartLength = 10;
        private const int lastPartLength = 2;

        // Define a public method 'IsValid' that checks if a given account number is valid.
        public bool IsValid(string accountNumber)
        {
            // Find the index of the first occurrence of '-' in the account number.
            var firstDelimiter = accountNumber.IndexOf('-');

            // Find the index of the last occurrence of '-' in the account number.
            var secondDelimiter = accountNumber.LastIndexOf('-');

            // Check if there is no '-' in the account number or if the first and last '-' are the same.
            if (firstDelimiter == -1 || (firstDelimiter == secondDelimiter))
                throw new ArgumentException(); // Throw an ArgumentException if the account number is invalid.

            // Extract the first part of the account number from the beginning up to the first '-'.
            var firstPart = accountNumber.Substring(0, firstDelimiter);

            // Check if the length of the first part matches the specified starting part length.
            if (firstPart.Length != startingPartLength)
                return false; // Return false if the length is not as expected.

            // Remove the first part and the first '-' from the account number to isolate the middle part.
            var tempPart = accountNumber.Remove(0, startingPartLength + 1);
            var middlePart = tempPart.Substring(0, tempPart.IndexOf('-'));

            // Check if the length of the middle part matches the specified middle part length.
            if (middlePart.Length != middlePartLength)
                return false; // Return false if the length is not as expected.

            // Extract the last part of the account number from the second '-' to the end.
            var lastPart = accountNumber.Substring(secondDelimiter + 1);

            // Check if the length of the last part matches the specified last part length.
            if (lastPart.Length != lastPartLength)
                return false; // Return false if the length is not as expected.

            // If all checks passed, the account number is considered valid, so return true.
            return true;
        }
    }

}
