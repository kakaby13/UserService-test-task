using Moq;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using TestTask.Models.Dto;
using TestTask.Models.Entities;
using TestTask.Services;
using TestTask.Repositories.Interfaces;

namespace TestTask.Tests;

public class UserServiceTests
{
    private Mock<IUserRepository> userRepoMock = null!;
    private Mock<IUserRoleRepository> roleRepoMock = null!;
    private Mock<IMapper> mapperMock = null!;
    private Mock<IValidator<AddUserDto>> addUserValidatorMock = null!;
    private Mock<IValidator<UpdateUserDto>> updateUserValidatorMock = null!;
    private Mock<IValidator<UpdateUserRoleDto>> roleValidatorMock = null!;

    private UserService userService = null!;
    
    [SetUp]
    public void Setup()
    {       
        userRepoMock = new Mock<IUserRepository>();
        roleRepoMock = new Mock<IUserRoleRepository>();
        mapperMock = new Mock<IMapper>();
        addUserValidatorMock = new Mock<IValidator<AddUserDto>>();
        updateUserValidatorMock = new Mock<IValidator<UpdateUserDto>>();
        roleValidatorMock = new Mock<IValidator<UpdateUserRoleDto>>();

        userService = new UserService(
            userRepoMock.Object,
            roleRepoMock.Object,
            mapperMock.Object,
            roleValidatorMock.Object,
            updateUserValidatorMock.Object,
            addUserValidatorMock.Object
        );
    }

    [Test]
    public async Task CreateUser_ValidInput_CreatesUser()
    {
        // A
        var dto = new AddUserDto
        {
            Name = "Bob",
            Email = "bob@example.com",
            Password = "pass123",
            Role = "User"
        };

        var role = new UserRole { Id = 1, Name = "User" };
        var user = new User();

        addUserValidatorMock.Setup(v => v.ValidateAsync(dto, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        mapperMock.Setup(m => m.Map<User>(dto)).Returns(user);
        roleRepoMock.Setup(r => r.GetRoleByNameAsync("User")).ReturnsAsync(role);
        userRepoMock.Setup(r => r.AddNewUserAsync(user)).Returns(Task.FromResult(user));

        // A
        await userService.CreateUser(dto);

        // A
        Assert.IsNotNull(user.PasswordHash);
        Assert.That(user.UserRole, Is.EqualTo(role));
        userRepoMock.Verify(r => r.AddNewUserAsync(user), Times.Once);
    }
    
    [Test]
    public async Task UpdateUser_ValidInput_UpdatesUserAndReturnsDto()
    {
        // A
        var dto = new UpdateUserDto
        {
            Id = 1,
            Name = "Bob",
            Email = "bob@example.com",
            RoleName = "Admin"
        };

        var entity = new User { Id = 1 };
        var role = new UserRole { Id = 2, Name = "Admin" };

        updateUserValidatorMock.Setup(v => v.ValidateAsync(dto, CancellationToken.None))
            .ReturnsAsync(new ValidationResult());

        userRepoMock.Setup(r => r.GetUserByIdAsync(1)).ReturnsAsync(entity);
        mapperMock.Setup(m => m.Map(dto, entity)).Returns(entity);
        roleRepoMock.Setup(r => r.GetRoleByNameAsync("Admin")).ReturnsAsync(role);
        userRepoMock.Setup(r => r.UpdateUserAsync(entity)).Returns(Task.FromResult(entity));
        mapperMock.Setup(m => m.Map<UserDto>(entity)).Returns(new UserDto());

        // A
        var result = await userService.UpdateUser(dto);
        
        // A
        Assert.IsInstanceOf<UserDto>(result);
        userRepoMock.Verify(r => r.UpdateUserAsync(entity), Times.Once);
    }

    
    [Test]
    public async Task GetUsers_ReturnsUserNames()
    {
        // A
        var names = new List<string> { "Alice", "Bob" };
        userRepoMock.Setup(r => r.GetUsersNamesAsync()).ReturnsAsync(names);

        // A
        var result = await userService.GetUsers();

        // A
        Assert.That(result, Is.EqualTo(names));
    }
}