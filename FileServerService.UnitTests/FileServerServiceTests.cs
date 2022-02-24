using Common;
using Common.Interfaces;
using Common.Interfaces.Actions;
using Common.Interfaces.MetaData;
using FileSystem.Resources;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace FileServerService.UnitTests
{
    public class FileServerServiceTests
    {
        private readonly Mock<IResourceFactory> factory;

        public FileServerServiceTests()
        {
            factory = new Mock<IResourceFactory>();
        }

        [Fact]
        public void Call_Get_Returns_RootResource()
        {
            // arrange
            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(new RootResource(factory.Object));

            //act
            var result = new fileserverapp.Services.FileServerService(factory.Object).Get(It.IsAny<string>());

            // arrange
            Assert.NotNull(result);
            Assert.IsType<RootResource>(result);
        }

        [Fact]
        public void Call_GetChildren_Returns_Collection()
        {
            // arrange
            var action = new Mock<IScanAction>();
            action
                .Setup(x => x.GetChildrenResources(It.IsAny<string>()))
                .Returns(
                new List<UnifiedResource>
                {
                    new FolderResource(It.IsAny<string>(), It.IsAny<string>(), factory.Object),
                    new FileResource(It.IsAny<string>(), It.IsAny<string>(), factory.Object)
                });

            var stub = new StubResource(factory.Object);
            stub.SetAction(action.Object);

            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(stub);

            //act
            var result = new fileserverapp.Services.FileServerService(factory.Object).GetChildren(It.IsAny<string>());

            // arrange
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Call_Delete_Verify_called()
        {
            // arrange
            var action = new Mock<IDeleteAction>();
            action.Setup(x => x.Delete(It.IsAny<string>()));

            var stub = new StubResource(factory.Object);
            stub.SetAction(action.Object);

            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(stub);


            //act
            new fileserverapp.Services.FileServerService(factory.Object).Delete(It.IsAny<string>());

            // arrange
            action.Verify(x => x.Delete(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public void Call_Copy_Resource_Name_Is_Changed()
        {
            // arrange
            var stub = new StubResource(factory.Object);
            stub.Name = "DUMMY";

            var action = new Mock<ICopyAction>();
            action.Setup(x => x.Copy(It.IsAny<string>()))
                .Returns("Copy of " + stub.Name);

            stub.SetAction(action.Object);

            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(stub);

            stub.Name = "Copy of " + stub.Name;
            //act
            var result = new fileserverapp.Services.FileServerService(factory.Object).Copy(It.IsAny<string>());

            // arrange
            Assert.True(result.Name == "Copy of DUMMY");
        }

        [Fact]
        public void Call_Rename_Resource_Name_Is_Changed()
        {
            // arrange
            var stub = new StubResource(factory.Object);
            stub.Name = "DUMMY";

            var action = new Mock<IRenameAction>();
            action.Setup(x => x.Rename(It.IsAny<string>(), It.IsAny<string>()))
                .Returns("Copy of " + stub.Name);

            stub.SetAction(action.Object);

            stub.Name = "Copy of " + stub.Name;

            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(stub);

            //act
            var result = new fileserverapp.Services.FileServerService(factory.Object).Rename(It.IsAny<string>(), It.IsAny<string>());

            // arrange
            Assert.True(result.Name == "Copy of DUMMY");
        }

        [Fact]
        public void Call_GetMetadata_()
        {
            // arrange
            var stub = new StubResource(factory.Object);
            stub.Name = "DUMMY";

            var action = new Mock<IMetadataAction>();
            action.Setup(x => x.GetMetadata(It.IsAny<string>()))
                .Returns(new Metadata(100));

            stub.SetAction(action.Object);

            factory.Setup(x => x.Create(It.IsAny<string>()))
                   .Returns(stub);

            //act
            var result = new fileserverapp.Services.FileServerService(factory.Object).GetMetadata(It.IsAny<string>());

            // arrange
            Assert.NotNull(result);
            Assert.IsType<Metadata>(result);
        }
    }
}
