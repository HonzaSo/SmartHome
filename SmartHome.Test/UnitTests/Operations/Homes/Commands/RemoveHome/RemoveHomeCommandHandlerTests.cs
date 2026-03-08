using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using SmartHome.Application.Enums;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Commands.RemoveHome;
using SmartHome.Domain.Domains;

namespace SmartHome.Test.UnitTests.Operations.Rooms.Commands.RemoveRoom;

public class RemoveHomeCommandHandlerTests
{
    private readonly IHomeRepository _homeRepository;
    private readonly ILogger<RemoveHomeCommandHandler> _logger;
    private readonly RemoveHomeCommandHandler _handler;

    public RemoveHomeCommandHandlerTests()
    {
        _homeRepository = Substitute.For<IHomeRepository>();
        _logger = Substitute.For<ILogger<RemoveHomeCommandHandler>>();
        _handler = new RemoveHomeCommandHandler(_homeRepository, _logger);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFound_WhenHomeDoesNotExist()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>())
            .Returns((Home?)null);

        var command = new RemoveHomeCommand {Id = homeId};

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.NotFound);
        await _homeRepository.DidNotReceive().RemoveByIdAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task Handle_ShouldReturnDeleted_WhenEverythingIsOk()
    {
        var homeId = Guid.NewGuid();
        var home = new Home { Id = homeId };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);
        _homeRepository.HasRoomsAsync(homeId, Arg.Any<CancellationToken>()).Returns(false);

        var command = new RemoveHomeCommand { Id = homeId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Deleted);
        await _homeRepository.Received(1).RemoveByIdAsync(homeId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenExceptionOccurs()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>())
            .Throws(new Exception("Database error"));

        var command = new RemoveHomeCommand { Id = homeId };

        var result = await _handler.Handle(command, CancellationToken.None);

        result.Should().Be(DeleteResultStatus.Error);
    }
}