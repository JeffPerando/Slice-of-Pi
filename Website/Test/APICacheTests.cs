
using Main.DAL.Mock;
using MongoDB.Driver;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    public class APICacheTests
    {
        /*
        It was at this point that I knew, I messed up.

        SO turns out, in order to properly implement filtering, you need to have BSON serial/deserializing.
        I have no idea how to do that. I also don't know how to actually get a filter to match with a 
        given document. At this point I'm 3 moq classes in and haven't fully implemented a single
        method that the API cache actually uses. Looking like I'll need another 1-2 to finish the job,
        and that's assuming that's straightforward. More likely, it'll take about half a dozen more
        moq objects. On top of all the methods in this spaghetti code I need to figure out and emulate.

        The big thing:
        I have no idea how to access the inner match predicate within a FilterDefinition. So, since that's
        how things are matched, it's looking like I'll need to:
        1. Convert every object iterated upon to BSON (somehow)
        2. (SOMEHOW) match the pattern to the document
        3. yield return that.

        Too bad I have no idea how to do #1 and #2. The documentation this deep in the MongoDB driver is
        so scarce I can't find anything. The latest SO questions are royally out of date (2012-2014):
        https://stackoverflow.com/questions/13257198/how-would-i-use-moq-to-test-a-mongodb-service-layer
        https://stackoverflow.com/questions/6903532/how-do-i-mock-mongodb-objects-to-test-my-data-models
        And they aren't helpful.

        Matching a BSON document filter to an actual document looks like it'll require a manual
        implementation. I can't directly access the actual predicate (lambda). Oh, and one more thing:
        Part of the MongoDB standard involves matching our old friend (/s) Regex! So assuming I get everything
        else working, I'll have to slap on a regex engine and hope it doesn't implode. And most likely,
        it'll also involve reflection. I did reflection back when I modded Minecraft, and it's not fun
        nor easy.

        in short: I give up. MongoDB is too difficult, if even feasible, to mock. Therefore, no unit
        tests for the API cache.

        --Jeff
         */

        /*
        private static IMongoCollection<T> MockCollection<T>(List<T> backing)
        {
            Mock<MockMongoCollection<T>> moq = new();

            moq.CallBase = true;

            moq.Setup(c => c.FindAsync(It.IsAny<FilterDefinition<T>>(),
                It.IsAny<FindOptions<T>>(), It.IsAny<CancellationToken>()))
                .Returns<FilterDefinition<T>>((foo) =>
                {
                    return null;
                });

            return moq.Object;
        }

        private static IMongoDatabase MockMongoDB<T>(IMongoCollection<T> collection)
        {
            Mock<MockMongoDB> moq = new();

            moq.CallBase = true;

            moq.Setup(db => db.GetCollection<T>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
                .Returns(collection);

            return moq.Object;
        }
        /*
        [SetUp]
        public void Setup() {}

        [TearDown]
        public void TearDown() {}
        
        [Test]
        public void APICacheTest_X() 
        {
            
        }
        */

    }

}
