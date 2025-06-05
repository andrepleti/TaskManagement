using TaskManagement.Domain.Entities;

namespace TaskManagement.Tests.Unit.Domain
{
    public class BaseEntityTests
    {
        [Fact]
        public void Constructor_Should_Initialize_Properties()
        {
            // Act
            var entity = new BaseEntity();

            // Assert
            Assert.Equal(0, entity.Id);
            Assert.Equal(default, entity.CreateAt);
            Assert.Equal(default, entity.UpdateAt);
        }
    }
}
