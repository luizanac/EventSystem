using Flunt.Validations;

namespace EventSystem.Domain.Entities
{
	public class PointOfSale : User
	{
		
		public string Cnpj { get; set; }
		public string Phone { get; set; }
		
		public PointOfSale()
		{}
		
		public PointOfSale(string name, string email, string password, string cnpj, string phone) : base(name, email, password)
		{
			Cnpj = cnpj;
			Phone = phone;
			
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Cnpj, "cnpj", "Cnpj é um campo obrigatório" )
				.IsTrue(CheckCnpj(Cnpj), "cnpj", "Cnpj informado é inválido")
				.IsNotNullOrEmpty(Phone, "phone", "Telefone é um campo obrigatório" )
			);
		}

		private bool CheckCnpj(string cnpj)
		{
			int[] mt1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int[] mt2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
			int soma; int resto; string digito; string TempCNPJ;

			cnpj = cnpj.Trim();
			cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

			if (cnpj.Length != 14)
				return false;
    
			if (cnpj == "00000000000000" || cnpj == "11111111111111" ||
			    cnpj == "22222222222222" || cnpj == "33333333333333" ||
			    cnpj == "44444444444444" || cnpj == "55555555555555" ||
			    cnpj == "66666666666666" || cnpj == "77777777777777" ||
			    cnpj == "88888888888888" || cnpj == "99999999999999")
				return false;

			TempCNPJ = cnpj.Substring(0, 12);
			soma = 0;

			for (int i = 0; i < 12; i++)
				soma += int.Parse(TempCNPJ[i].ToString()) * mt1[i];

			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;

			digito = resto.ToString();

			TempCNPJ = TempCNPJ + digito;
			soma = 0;
			for (int i = 0; i < 13; i++)
				soma += int.Parse(TempCNPJ[i].ToString()) * mt2[i];

			resto = (soma % 11);
			if (resto < 2)
				resto = 0;
			else
				resto = 11 - resto;
			digito = digito + resto.ToString();

			return cnpj.EndsWith(digito);
		}
	}
}