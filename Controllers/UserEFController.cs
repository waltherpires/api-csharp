using AutoMapper;
using DotnetAPI.Data;
using DotnetAPI.Dtos;
using DotnetAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UserEFController : ControllerBase
{
    IUserRepository _userRepository;
    IMapper _mapper;
    public UserEFController(IConfiguration config, IUserRepository userRepository)
    {
        _userRepository = userRepository;

        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserToAddDto, User>();
        }));
    }

    // Endpoints para Users
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _userRepository.GetUsers();
        return users;
    }
    
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        // User? user = _entityFramework.Users
        //     .Where(u => u.UserId == userId)
        //     .FirstOrDefault<User>();

        // if(user != null)
        // {
        //     return user;
        // }

        // throw new Exception("Failed to Get User");
        return _userRepository.GetSingleUser(userId);
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
       User? userDb = _userRepository.GetSingleUser(user.UserId);
            
        if(userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Update User");
        }

        throw new Exception("Failed to Get User");
    }

    [HttpPost("AddUser")]
    public IActionResult AddUser(UserToAddDto user)
    {
        User userDb = _mapper.Map<User>(user);

        _userRepository.AddEntity<User>(userDb);
        if(_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
       User? userDb = _userRepository.GetSingleUser(userId);
            
        if(userDb != null)
        {
            _userRepository.RemoveEntity<User>(userDb);
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");
        }

        throw new Exception("Failed to Get User");
    }

    // Endpoints para UserJobInfo

    [HttpGet("UserJobInfo/{userId}")]
    public UserJobInfo GetUserJobInfoEF(int userId)
    {
        return _userRepository.GetSingleUserJobInfo(userId);
    }

    [HttpPut("UserJobInfo")]
    public IActionResult PutUserJobInfoEF(UserJobInfo userForUpdate)
    {
        UserJobInfo? userToUpdate = _userRepository.GetSingleUserJobInfo(userForUpdate.UserId);
        
        if(userToUpdate != null)
        {
            userToUpdate.JobTitle = userForUpdate.JobTitle;
            userToUpdate.Department = userForUpdate.Department;
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Edit UserJobInfo");
        }

        throw new Exception("Failed to Get UserJobInfo");
    }

    [HttpPost("UserJobInfo")]
    public IActionResult PostUserJobInfoEF(UserJobInfo userToAdd)
    {
        _userRepository.AddEntity<UserJobInfo>(userToAdd);
        if(_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to Add UserJobInfo");
    }

    [HttpDelete("UserJobInfo/{userId}")]
    public IActionResult DeleteUserJobInfoEF(int userId)
    {
        UserJobInfo? userToDelete = _userRepository.GetSingleUserJobInfo(userId);
        
        if(userToDelete != null)
        {
            _userRepository.RemoveEntity<UserJobInfo>(userToDelete);
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }
            throw new Exception("Failed to Delete UserJobInfo");
        }

        throw new Exception("Failed to Get UserJobInfo");
    }

    // Endpoints para UserSalary

    [HttpGet("UserSalary/{userId}")]
    public UserSalary GetUserSalaryEF(int userId)
    {
        return _userRepository.GetSingleUserSalary(userId);
    }

    [HttpPut("EditUserSalary")]
    public IActionResult PutUserSalaryEF(UserSalary userForUpdate)
    {
        UserSalary? userToUpdate = _userRepository.GetSingleUserSalary(userForUpdate.UserId);
        
        if(userToUpdate != null)
        {
            userToUpdate.Salary = userForUpdate.Salary;
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }
            
            throw new Exception("Failed to Edit UserSalary");
        }

        throw new Exception("Failed to Get UserSalary");
    }

    [HttpPost("UserSalary")]
    public IActionResult PostUserSalaryEF(UserSalary userForInsert)
    {
        _userRepository.AddEntity<UserSalary>(userForInsert);
        if(_userRepository.SaveChanges())
        {
            return Ok();
        }

        throw new Exception("Failed to Add UserSalary");
    }

    [HttpDelete("UserSalary/{userId}")]
    public IActionResult DeleteUserSalaryEF(int userId)
    {
        UserSalary? userToDelete = _userRepository.GetSingleUserSalary(userId);

        if(userToDelete != null)
        {
            _userRepository.RemoveEntity<UserSalary>(userToDelete);
            if(_userRepository.SaveChanges())
            {
                return Ok();
            }

            throw new Exception("Failed to Remove UserSalary");
        }

        throw new Exception("Failed to Get UserSalary");
    }
}