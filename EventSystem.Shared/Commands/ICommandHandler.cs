using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.Shared.Commands
{
	public interface ICommandHandler<T, TR> : ICommand
	{
		Task<TR> Handle(T command);
		Dictionary<string, List<string>> GetErrors();
	}
}