namespace API;

public static class DateTimeExtensions
{
    public static int CalculageAge(this DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth > today.AddYears(-age)) age--;

        return age;
    }
}
