using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Entities;

namespace TravShell.Core.Repositories;
public interface IUsersRepository
{

	Task RegisterUserAsync(RegisterViewModel Model);

	Task<User> FindUserAsync(string email);
	Task<User> FindUserByIdAsync(string id);
}

