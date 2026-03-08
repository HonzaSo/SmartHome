using FluentAssertions;
using NSubstitute;
using SmartHome.Application.Interfaces;
using SmartHome.Application.Operations.Homes.Queries.GetHomes;
using SmartHome.Domain.Domains;

namespace SmartHome.Test.UnitTests.Operations.Homes.Queries.GetAllHomes;

public class GetAllHomesQueryHandlerTests
{
    private readonly IHomeRepository _homeRepository;
    private readonly GetAllHomesQueryHandler _handler;

    public GetAllHomesQueryHandlerTests()
    {
        _homeRepository = Substitute.For<IHomeRepository>();
        _handler = new GetAllHomesQueryHandler(_homeRepository);
    }

    [Fact]
    public async Task Handle_ShouldReturnListOfHomes_WhenHomesExist()
    {
        var homes = new List<Home>
        {
            new() { Id = Guid.NewGuid(), Name = "Domov 1", Address = new Address { Street = "Ulice 1", City = "Praha", ZipCode = "14000" } },
            new() { Id = Guid.NewGuid(), Name = "Domov 2", Address = new Address { Street = "Ulice 2", City = "Praha", ZipCode = "16000" } }
        };

        _homeRepository.GetAllHomesAsync(Arg.Any<CancellationToken>()).Returns(homes);

        var query = new GetAllHomesQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().HaveCount(2);
        result.Should().ContainEquivalentOf(homes[0]);
        result.Should().ContainEquivalentOf(homes[1]);
        await _homeRepository.Received(1).GetAllHomesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoHomesExist()
    {
        _homeRepository.GetAllHomesAsync(Arg.Any<CancellationToken>()).Returns(new List<Home>());

        var query = new GetAllHomesQuery();

        var result = await _handler.Handle(query, CancellationToken.None);

        result.Should().BeEmpty();
        await _homeRepository.Received(1).GetAllHomesAsync(Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryOnce()
    {
        _homeRepository.GetAllHomesAsync(Arg.Any<CancellationToken>()).Returns(new List<Home>());

        var query = new GetAllHomesQuery();
        await _handler.Handle(query, CancellationToken.None);

        await _homeRepository.Received(1).GetAllHomesAsync(Arg.Any<CancellationToken>());
    }
}

