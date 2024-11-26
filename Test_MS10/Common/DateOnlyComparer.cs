using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Test_MS10.Common;

public class DateOnlyComparer : ValueComparer<DateOnly>
{
    public DateOnlyComparer() : base(
            (x, y) => x.DayNumber == y.DayNumber,
            dateOnly => dateOnly.GetHashCode())
    { }
}
