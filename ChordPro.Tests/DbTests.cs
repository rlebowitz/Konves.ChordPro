using ChordPro.DB;
using ChordPro.Library;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace ChordPro.Tests
{
    public class DbTests
    {

       // [Fact]
        public void DeleteDb()
        {
            // Arrange
            using var context = new ChordProContext();
            Assert.True(context.Database.EnsureDeleted());
        }

        // [Fact]
        public void CreateDb()
        {
            // Arrange
            using var context = new ChordProContext();
            Assert.True(context.Database.EnsureCreated());
        }

    }
}
