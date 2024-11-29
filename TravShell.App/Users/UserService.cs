using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Repositories;

namespace TravShell.App.Users;
public class UserService :IUserService
{

	private readonly IUsersRepository _usersRepository;

	public UserService(IUsersRepository usersRepository)
	{
		_usersRepository = usersRepository;
	}

	async Task<User> IUserService.FindUserAsync(string email)
	{
		return await _usersRepository.FindUserAsync(email);
	}

	async Task<User> IUserService.FindUserByIdAsync(string id)
	{
		return await _usersRepository.FindUserByIdAsync(id);
	}

	async Task IUserService.RegisterUserAsync(RegisterViewModel Model)
	{
		 await _usersRepository.RegisterUserAsync(Model);
	}

	//public Task RegisterUserAsync(RegisterViewModel Model)
	//{
	//	return _userService.RegisterUserAsync( Model);
	//}

}

