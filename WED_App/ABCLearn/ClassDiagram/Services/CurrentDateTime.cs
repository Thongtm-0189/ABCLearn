namespace ABCLearn.Services
{
	public class CurrentDateTime
	{
		public static DateTime GetcurrentDateTime
		{
			get
			{
				DateTime nowUtc = DateTime.UtcNow;
				TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time"); // Múi giờ của Việt Nam
				DateTime vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, vietnamTimeZone);
				return vietnamTime;
			}
		}
	}
}
