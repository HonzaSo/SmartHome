using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Commands.CreateHome;
using SmartHome.Domain.Domains;

namespace SmartHome.Test.UnitTests.Operations.Homes.Commands.CreateHome;

public class CreateHomeCommandHandlerTests
{
    private readonly IHomeRepository _homeRepository;
    private readonly CreateHomeCommandHandler _handler;

    public CreateHomeCommandHandlerTests()
    {
        _homeRepository = Substitute.For<IHomeRepository>();
        _handler = new CreateHomeCommandHandler(_homeRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnGuid_WhenCommandIsValid()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.AddAsync(Arg.Any<Home>(), Arg.Any<CancellationToken>())
            .Returns(homeId);

        var command = new CreateHomeCommand
        {
            Name = "My Home",
            Street = "Main St",
            City = "City",
            ZipCode = "12345"
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(homeId);
        await _homeRepository.Received(1).AddAsync(Arg.Is<Home>(h => 
            h.Name == "My Home" &&
            h.Address.Street == "Main St" &&
            h.Address.City == "City" &&
            h.Address.ZipCode == "12345"), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCreateHomeWithCorrectData()
    {
        var homeId = Guid.NewGuid();
        var capturedHome = (Home?)null;

        _homeRepository.AddAsync(Arg.Do<Home>(h => capturedHome = h), Arg.Any<CancellationToken>())
            .Returns(homeId);

        var command = new CreateHomeCommand
        {
            Name = "Test Home",
            Street = "Test St",
            City = "Test City",
            ZipCode = "54321"
        };

        await _handler.Handle(command, CancellationToken.None);

        capturedHome.Should().NotBeNull();
        capturedHome!.Name.Should().Be("Test Home");
        capturedHome.Address.Street.Should().Be("Test St");
        capturedHome.Address.City.Should().Be("Test City");
        capturedHome.Address.ZipCode.Should().Be("54321");
        capturedHome.Id.Should().NotBe(Guid.Empty);
    }
}

