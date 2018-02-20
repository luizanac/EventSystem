using EventSystem.Shared.Commands;
using Flunt.Notifications;
using Flunt.Validations;

namespace EventSystem.Domain.Commands.PointOfSaleCommands.Input
{
	public class CreatePointOfSaleCommand : Notifiable, ICommand
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Cnpj { get; set; }
		public string Phone { get; set; }
		
		public bool IsValid()
		{
			AddNotifications(new Contract()
				.Requires()
				.IsNotNullOrEmpty(Password, "password", "A senha não pode ser vazia")
				.HasMinLen(Password, 6, "password", "A senha não pode ter menos que 6 caracteres")
				.IsEmail(Email, "email", "O E-mail deve ser em um formato válido")
				.HasMinLen(Name, 3,"name", "O nome de usuário deve ter no minimo 3 caracteres")
				.HasMaxLen(Name, 45,"name", "O nome de usuário deve ter no máximo 45 caracteres")
				.IsNotNullOrEmpty(Name, "name", "O nome de usuário não pode ser vazio")
				.IsNotNullOrEmpty(Cnpj, "cnpj", "Cnpj é um campo obrigatório" )
				.IsTrue(CheckCnpj(Cnpj), "cnpj", "Cnpj informado é inválido")
				.IsNotNullOrEmpty(Phone, "phone", "Telefone é um campo obrigatório" )
			);
			
			return Valid;
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