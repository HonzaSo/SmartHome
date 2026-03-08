using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Queries.GetHomeById;
using SmartHome.Domain.Domains;

namespace SmartHome.Test.UnitTests.Operations.Homes.Queries.GetHomeById;

public class GetHomeByIdQueryHandlerTests
{
    private readonly IHomeRepository _homeRepository;
    private readonly GetHomeByIdQueryHandler _handler;

    public GetHomeByIdQueryHandlerTests()
    {
        _homeRepository = Substitute.For<IHomeRepository>();
        _handler = new GetHomeByIdQueryHandler(_homeRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnHome_WhenHomeExists()
    {
        var homeId = Guid.NewGuid();
        var home = new Home
        {
            Id = homeId,
            Name = "Test Home",
            Address = new Address { Street = "Street", City = "City", ZipCode = "12345" }
        };

        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns(home);

        var query = new GetHomeByIdQuery { Id = homeId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().NotBeNull();
        result!.Id.Should().Be(homeId);
        result.Name.Should().Be("Test Home");
        await _homeRepository.Received(1).GetByIdAsync(homeId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnNull_WhenHomeDoesNotExist()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns((Home?)null);

        var query = new GetHomeByIdQuery { Id = homeId };

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeNull();
        await _homeRepository.Received(1).GetByIdAsync(homeId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryWithCorrectId()
    {
        var homeId = Guid.NewGuid();
        _homeRepository.GetByIdAsync(homeId, Arg.Any<CancellationToken>()).Returns((Home?)null);

        var query = new GetHomeByIdQuery { Id = homeId };
        await _handler.Handle(query, CancellationToken.None);

        await _homeRepository.Received(1).GetByIdAsync(homeId, Arg.Any<CancellationToken>());
    }
}

