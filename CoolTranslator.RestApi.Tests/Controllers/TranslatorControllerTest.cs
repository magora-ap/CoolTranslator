using Microsoft.VisualStudio.TestTools.UnitTesting;
using CoolTranslator.RestApi.Controllers;
using CoolTranslator.RestApi.Models;
using CoolTranslator.RestApi.Tests.Mocks;

namespace CoolTranslator.RestApi.Tests.Controllers
{
    [TestClass]
    public class TranslatorControllerTest
    {
        private PathMapperMock _mapper;

        [TestInitialize]
        public void Startup()
        {
            _mapper = new PathMapperMock();
        }

        [TestMethod]
        public void TestEmptyModel()
        {
            var controller = new TranslatorController(_mapper);
            var request = new TranslationRequest();
            var response = controller.Get(request);
            Assert.AreEqual(System.Net.HttpStatusCode.NotFound, response.Code);
        }

        [TestMethod]
        public void TestCorrectTranslation()
        {
            var controller = new TranslatorController(_mapper);
            var request = new TranslationRequest
            {
                Language = "En",
                Text = "Hello, world!"
            };
            var response = controller.Get(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.Code);
            Assert.AreEqual("привет мир", response.Translation);
        }

        [TestMethod]
        public void TestDefaultTranslation()
        {
            var controller = new TranslatorController(_mapper);
            var request = new TranslationRequest
            {
                Language = "Fr",
                Text = "Hello, world!"
            };
            var response = controller.Get(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.Code);
            Assert.AreEqual("привет мир", response.Translation);
            Assert.AreEqual("En", response.Language);
        }
        
        [TestMethod]
        public void TestCaseInsensitive()
        {
            var controller = new TranslatorController(_mapper);
            var request = new TranslationRequest
            {
                Language = "eN",
                Text = "HeLlO, WoRlD!"
            };
            var response = controller.Get(request);
            Assert.AreEqual(System.Net.HttpStatusCode.OK, response.Code);
            Assert.AreEqual("привет мир", response.Translation);
        }
    }
}
