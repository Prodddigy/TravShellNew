using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravShell.Core.Entities.ViewModels;
using TravShell.Core.Repositories;
using TravShell.Infrastructure.Data;
using TravShell.Core.Entities;
using BCrypt.Net;
using Microsoft.EntityFrameworkCore;


namespace TravShell.Infrastructure.Repositories;
public class UsersRepository :IUsersRepository
{
	private readonly DataContext _dbContext;

	public UsersRepository(DataContext dbContext)
	{ 
		_dbContext = dbContext; 
	}

	//Task IUsersRepository.RegisterUserAsync(RegisterViewModel Model)
	//{
	//	throw new NotImplementedException();
	//}

	public async Task RegisterUserAsync(RegisterViewModel Model)
	{

		string passwordHash = BCrypt.Net.BCrypt.HashPassword(Model.Password);
		
		User newUser = new();
		newUser.Forename = Model.Forename;
		newUser.Surename = Model.Surename;
		newUser.Email = Model.Email;
		newUser.DOB = Model.DOB;
		newUser.PasswordHash = passwordHash;
		await _dbContext.Users.AddAsync(newUser);
		await _dbContext.SaveChangesAsync();

		return;
	}

	public async Task<User> FindUserAsync(string email)
	{

		User? user = await _dbContext.Users.Where(x => x.Email == email).FirstOrDefaultAsync();

		return user;
	}

	public async Task<User> FindUserByIdAsync(string id)
	{
		User? user = await _dbContext.Users.Where(x => x.Id == Int32.Parse(id)).FirstOrDefaultAsync();

		return user;
	}
}

