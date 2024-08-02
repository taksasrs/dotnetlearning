namespace TestAPI.Models
{
	public class ServiceResponse<T>
	{
		public T Data { get; set; }
		public bool Success { get; set; } = false;
		public string ErrorMessage { get; set; }
		public string RequestDate { get; set; } = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
	}
}

