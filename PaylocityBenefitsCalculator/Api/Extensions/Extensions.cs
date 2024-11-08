namespace Api.Extensions
{
    public static class Extensions
    {
        public static int Age(this DateTime sender)
        {
            var today = DateTime.Today;
            var age = today.Year - sender.Year;
            // this adjusts age if today is before the date of birth this year
            if (sender.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}