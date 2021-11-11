using Brightcove.Core.Models;
using Brightcove.Core.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Brightcove.Core.Tests
{
    [TestClass]
    public class UnitTest1
    {
        static string accountId = "5718741447001";
        static string clientId = "5e000783-0f31-43d0-873b-be08fb7234d6";
        static string clientSecret = "vUhjn2o4EFw1mtOgMOoNrnIZQBAH4PXnKhKJ4niDGnypcZovFG-JcBoAtcDCjrUPvKg4MTPLYZ1X9CMglRKPZg";

        [TestMethod]
        public void TestMethod1()
        {
            BrightcoveAuthenticationService service = new BrightcoveAuthenticationService(clientId, clientSecret);

            Assert.AreEqual("", service.CreateAuthenticationHeader().Parameter);
        }

        [TestMethod]
        public void TestMethod2()
        {
           BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

           Assert.AreEqual(0, service.VideosCount());
        }

        [TestMethod]
        public void TestMethod3()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            Assert.AreEqual(0, service.GetVideos(0).Count());
        }

        [TestMethod]
        public void TestMethod4()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            Assert.AreEqual(0, service.GetPlayLists().Count());
        }

        [TestMethod]
        public void TestMethod5()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);
            Video video;

            Assert.AreEqual(true, service.TryGetVideo("aosidjfoij", out video));
            Assert.AreEqual(null, video);
        }

        [TestMethod]
        public void TestMethod6()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);
            Video video;

            service.TryGetVideo("6257819058001", out video);
            var ret = service.UpdateVideo(video);
            Assert.AreEqual(null, ret);
        }

        [TestMethod]
        public void TestMethod7()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);
            PlayList video;

            service.TryGetPlaylist("1684261519670224819", out video);
            //var ret = service.UpdateVideo(video);
            Assert.AreEqual(null, video);
        }

        [TestMethod]
        public void TestMethod8()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);
            PlayList video;

            service.TryGetPlaylist("1684261519670224819", out video);
            var ret = service.UpdatePlaylist(video);
            Assert.AreEqual(null, video);
        }

        [TestMethod]
        public void TestMethod9()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            service.DeleteVideo("6215726169001");
        }

        [TestMethod]
        public void TestMethod10()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            service.CreatePlaylist("test123");
        }

        [TestMethod]
        public void TestMethod11()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            service.CreateVideo("testvideo");
        }

        [TestMethod]
        public void TestMethod12()
        {
            BrightcoveService service = new BrightcoveService(accountId, clientId, clientSecret);

            Assert.AreEqual(null, service.IngestVideo("6279873159001", "https://ingestion-upload-production.s3.amazonaws.com/5718741447001/6279873159001/e5368f6c-cd63-4da1-b05e-c82f4f2ae7e4/6279873159001"));
        }
    }
}
