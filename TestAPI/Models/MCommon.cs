namespace TestAPI.Models
{
	public class MCommon<T>
	{
		public List<T> Data { get; set; }
		public bool Success { get; set; } = false;
		public string ErrorMessage { get; set; }
		public DateTime RequestDate { get; set; } = DateTime.Now;
	}
}

