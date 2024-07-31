namespace TestAPI.Models
{
	public class MCommon<T>
	{
		public List<T> Data { get; set; }
		public bool Success { get; set; } = false;
		public string ErrorMessage { get; set; }
		public string SuccessDate { get; set; } = DateTime.Now.ToString("dd/mm/yyyy, hh:mm");
	}
}

