namespace EventSystem.Domain.Entities
{
	public class BoxOffice : User
	{
		public BoxOffice()
		{}
		
		public BoxOffice(string name, string email, string password) :
			base(name, email, password)
		{}
	}
}