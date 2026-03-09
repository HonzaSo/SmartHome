using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Commands.UpdateHome;
using SmartHome.Domain.Domains;

namespace SmartHome.Test.UnitTests.Operations.Homes.Commands.UpdateHome;

public class UpdateHomeCommandHandlerTests
{
    private readonly IHomeRepository _homeRepository;
    private readonly ILogger<UpdateHomeCommandHandler> _logger;
    private readonly UpdateHomeCommandHandler _handler;

    public UpdateHomeCommandHandlerTests()
    {
        _homeRepository = Substitute.For<IHomeRepository>();
        _logger = Substitute.For<ILogger<UpdateHomeCommandHandler>>();
        _handler = new UpdateHomeCommandHandler(_homeRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenHomeDoesNotExist()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>())
            .Returns((Home?)null);

        var command = new UpdateHomeCommand { Id = homeId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.NotFound);
        await _homeRepository.DidNotReceive().UpdateAsync(Arg.Any<Home>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenHomeNameIsUpdated()
    {
        var homeId = Guid.NewGuid();
        var home = new Home
        {
            Id = homeId,
            Name = "Old Name",
            Address = new Address { Street = "Street", City = "City", ZipCode = "12345" }
        };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);

        var command = new UpdateHomeCommand { Id = homeId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.Success);
        home.Name.Should().Be("New Name");
        await _homeRepository.Received(1).UpdateAsync(home, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenAddressIsUpdated()
    {
        var homeId = Guid.NewGuid();
        var home = new Home
        {
            Id = homeId,
            Name = "Home",
            Address = new Address { Street = "Old Street", City = "Old City", ZipCode = "12345" }
        };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);

        var command = new UpdateHomeCommand
        {
            Id = homeId,
            Street = "New Street",
            City = "New City",
            ZipCode = "54321"
        };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.Success);
        home.Address.Street.Should().Be("New Street");
        home.Address.City.Should().Be("New City");
        home.Address.ZipCode.Should().Be("54321");
        await _homeRepository.Received(1).UpdateAsync(home, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenPartialUpdateIsPerformed()
    {
        var homeId = Guid.NewGuid();
        var home = new Home
        {
            Id = homeId,
            Name = "Home",
            Address = new Address { Street = "Steet", City = "City", ZipCode = "12345" }
        };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);

        var command = new UpdateHomeCommand { Id = homeId, Street = "New Street" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.Success);
        home.Address.Street.Should().Be("New Street");
        home.Address.City.Should().Be("City");
        await _homeRepository.Received(1).UpdateAsync(home, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new UpdateHomeCommand { Id = homeId, Name = "New Name" };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.Error);
    }

    [Fact]
    public async Task Handle_ShouldNotUpdateWhenNoFieldsProvided()
    {
        var homeId = Guid.NewGuid();
        var originalName = "Home";
        var home = new Home
        {
            Id = homeId,
            Name = originalName,
            Address = new Address { Street = "Street", City = "City", ZipCode = "12345" }
        };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);

        var command = new UpdateHomeCommand { Id = homeId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(UpdateResultStatus.Success);
        home.Name.Should().Be(originalName);
        await _homeRepository.Received(1).UpdateAsync(home, Arg.Any<CancellationToken>());
    }
}

