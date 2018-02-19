using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventSystem.Shared.Commands
{
	public interface ICommandHandler<T> : ICommand
	{
		Task<ICommandResult> Handle(T command);
		Dictionary<string, List<string>> GetErrors();
	}
}