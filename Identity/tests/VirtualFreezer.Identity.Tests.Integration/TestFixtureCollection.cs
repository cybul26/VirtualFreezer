using VirtualFreezer.Shared.Testing;
using Xunit;

namespace VirtualFreezer.Identity.Tests.Integration;

[CollectionDefinition(nameof(TestApplicationFactory<Program>))]
public class TestFixtureCollection : ICollectionFixture<TestApplicationFactory<Program>>
{
}