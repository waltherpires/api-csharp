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
    DataContextEF _entityFramework;
    IMapper _mapper;
    public UserEFController(IConfiguration config)
    {
        _entityFramework = new DataContextEF(config);
        _mapper = new Mapper(new MapperConfiguration(cfg =>{
            cfg.CreateMap<UserToAddDto, User>();
            cfg.CreateMap<UserJobInfoDto, UserJobInfo>();
            cfg.CreateMap<UserSalaryDto, UserSalary>();
        }));
    }

    // Endpoints para Users
    [HttpGet("GetUsers")]
    public IEnumerable<User> GetUsers()
    {
        IEnumerable<User> users = _entityFramework.Users.ToList<User>();
        return users;
    }
    
    [HttpGet("GetSingleUser/{userId}")]
    public User GetSingleUser(int userId)
    {
        User? user = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();

        if(user != null)
        {
            return user;
        }

        throw new Exception("Failed to Get User");
    }

    [HttpPut("EditUser")]
    public IActionResult EditUser(User user)
    {
       User? userDb = _entityFramework.Users
            .Where(u => u.UserId == user.UserId)
            .FirstOrDefault<User>();
            
        if(userDb != null)
        {
            userDb.Active = user.Active;
            userDb.FirstName = user.FirstName;
            userDb.LastName = user.LastName;
            userDb.Email = user.Email;
            userDb.Gender = user.Gender;
            if(_entityFramework.SaveChanges() > 0)
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

        _entityFramework.Add(userDb);
        if(_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to Add User");
    }

    [HttpDelete("DeleteUser/{userId}")]
    public IActionResult DeleteUser(int userId)
    {
       User? userDb = _entityFramework.Users
            .Where(u => u.UserId == userId)
            .FirstOrDefault<User>();
            
        if(userDb != null)
        {
            _entityFramework.Users.Remove(userDb);
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to Delete User");
        }

        throw new Exception("Failed to Get User");
    }

    // Endpoints para UserJobInfo
    [HttpGet("GetUserJobInfo")]
    public IEnumerable<UserJobInfo> GetUserJobInfos()
    {
        IEnumerable<UserJobInfo> userJobInfos = _entityFramework
            .UserJobInfo.ToList<UserJobInfo>();
        return userJobInfos;
    }

    [HttpGet("GetSingleUserJobInfo/{userId}")]
    public UserJobInfo GetSingleUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault();
        
        if(userJobInfoDb != null)
        {
            return userJobInfoDb;
        }

        throw new Exception("Failed to Get UserJobInfo");
    }

    [HttpPut("EditUserJobInfo")]
    public IActionResult EditUserJobInfo(UserJobInfo userJobInfo)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userJobInfo.UserId)
            .FirstOrDefault();
        
        if(userJobInfoDb != null)
        {
            userJobInfoDb.JobTitle = userJobInfo.JobTitle;
            userJobInfoDb.Department = userJobInfo.Department;
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }

            throw new Exception("Failed to Edit UserJobInfo");
        }

        throw new Exception("Failed to Get UserJobInfo");
    }

    [HttpPost("AddUserJobInfo")]
    public IActionResult AddUserJobInfo(UserJobInfoDto userJobInfo)
    {
        UserJobInfo userJobInfoDb = _mapper.Map<UserJobInfo>(userJobInfo);

        _entityFramework.Add(userJobInfoDb);
        if(_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to Add UserJobInfo");
    }

    [HttpDelete("DeleteUserJobInfo")]
    public IActionResult DeleteUserJobInfo(int userId)
    {
        UserJobInfo? userJobInfoDb = _entityFramework.UserJobInfo
            .Where(u => u.UserId == userId)
            .FirstOrDefault();
        
        if(userJobInfoDb != null)
        {
            _entityFramework.Remove(userJobInfoDb);
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            throw new Exception("Failed to Delete UserJobInfo");
        }

        throw new Exception("Failed to Get UserJobInfo");
    }

    // Endpoints para UserSalary
    [HttpGet("GetUserSalary")]
    public IEnumerable<UserSalary> GetUserSalaries()
    {
        IEnumerable<UserSalary> userSalaries = _entityFramework.UserSalary.ToList<UserSalary>();
        return userSalaries;
    }

    [HttpGet("GetSingleUserSalary/{userId}")]
    public UserSalary GetUserSalary(int userId)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userId)
            .FirstOrDefault();
        
        if(userSalaryDb != null)
        {
            return userSalaryDb;
        }

        throw new Exception("Failed to Get UserSalary");
    }

    [HttpPut("EditUserSalary")]
    public IActionResult EditUserSalary(UserSalary userSalary)
    {
        UserSalary? userSalaryDb = _entityFramework.UserSalary
            .Where(u => u.UserId == userSalary.UserId)
            .FirstOrDefault<UserSalary>();
        
        if(userSalaryDb != null)
        {
            userSalaryDb.Salary = userSalary.Salary;
            if(_entityFramework.SaveChanges() > 0)
            {
                return Ok();
            }
            
            throw new Exception("Failed to Edit UserSalary");
        }

        throw new Exception("Failed to Get UserSalary");
    }

    [HttpPost("AddUserSalary")]
    public IActionResult AddUserSalary(UserSalaryDto user)
    {
        UserSalary userSalaryDb = _mapper.Map<UserSalary>(user);
        _entityFramework.Add(userSalaryDb);
        if(_entityFramework.SaveChanges() > 0)
        {
            return Ok();
        }

        throw new Exception("Failed to Add UserSalary");
    }
}