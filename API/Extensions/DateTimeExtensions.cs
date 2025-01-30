using System;

namespace API.Extensions;

public static class DateTimeExtensions
{
    public static int CalculateAge(this DateOnly dob)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        var age = today .Year - dob.Year;
        //if you subtract the years in age from today and are less than
        //the dob, your bday hasn't happened yet this year.
        if(dob > today.AddYears(-age)) age--;
        return age; //this doesn't account for leap years on feb 29... who cares
    }
}
