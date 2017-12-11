using System;
using FakeItEasy;
using NUnit.Framework;
using ShareOnSocialsLib.UnitTests.TestHelpers;
using ShareOnSocialsLib.WebServices.Http;
using ShareOnSocialsLib.WebServices.Posters;
using ShareOnSocialsLib.WebServices.Authorizers;
using ShareOnSocialsLib.UIBridge;

namespace ShareOnSocialsLib.UnitTests.Components.WebServices.Posters
{
    /// <summary>
    /// Twitter poster test suite.
    /// </summary>
    [TestFixture]
    public class TwitterPosterTests
    {
        /// <summary>
        /// Posts the message and URL sends message to correct URL.
        /// </summary>
        [Test]
        public void Post_WithMessageAndURL_SendsMessageToCorrectURL()
        {
            string message = "some kind of tweet";
            string url = "https://www.youtube.com";
            string escapedmessage = Uri.EscapeDataString(message);

            IHttpManager mockmanager = A.Fake<IHttpManager>();

            ISocialPoster poster = GenerateTwitterPoster(mockmanager);

            poster.Post(message, url);

            string expectedurl = "https://api.twitter.com/1.1/statuses/update.json?status=" + message + "&attachment_url=" + url;

            A.CallTo(() => mockmanager.SendWithAuthHeader(A<string>.Ignored, expectedurl, A<string>.Ignored)).MustHaveHappened();
        }

        /// <summary>
        /// Posts the message sends message to correct URL.
        /// </summary>
        [Test]
        public void Post_WithMessage_SendsMessageToCorrectURL()
        {
            string message = "some kind of tweet";
            string escapedmessage = Uri.EscapeDataString(message);

            IHttpManager mockmanager = A.Fake<IHttpManager>();

            ISocialPoster poster = GenerateTwitterPoster(mockmanager);

            poster.Post(message);

            string expectedurl = "https://api.twitter.com/1.1/statuses/update.json?status=" + message;

            A.CallTo(() => mockmanager.SendWithAuthHeader(A<string>.Ignored, expectedurl, A<string>.Ignored)).MustHaveHappened();
        }

        /// <summary>
        /// Posts the message sends request as post.
        /// </summary>
        [Test]
        public void Post_WithMessage_SendsRequestAsPOST()
        {
            string message = "some kind of tweet";
            string escapedmessage = Uri.EscapeDataString(message);

            IHttpManager mockmanager = A.Fake<IHttpManager>();

            ISocialPoster poster = GenerateTwitterPoster(mockmanager);

            poster.Post(message);

            string expectedtype = "POST";

            A.CallTo(() => mockmanager.SendWithAuthHeader(expectedtype, A<string>.Ignored, A<string>.Ignored)).MustHaveHappened();
        }

        /// <summary>
        /// Posts the message sends request with correct auth header.
        /// </summary>
        [Test]
        public void Post_WithMessage_SendsRequestWithCorrectAuthHeader()
        {
            string message = "some kind of tweet";
            string expectedauthheader = "someauthheader";
            string escapedmessage = Uri.EscapeDataString(message);

            IAuthorizer fakeauthorizer = A.Fake<IAuthorizer>();
            A.CallTo(() => fakeauthorizer.GenerateHeader(A<string>.Ignored)).Returns(expectedauthheader);

            IHttpManager mockmanager = A.Fake<IHttpManager>();

            ISocialPoster poster = GenerateTwitterPoster(mockmanager, fakeauthorizer);

            poster.Post(message);

            A.CallTo(() => mockmanager.SendWithAuthHeader(A<string>.Ignored, A<string>.Ignored, expectedauthheader)).MustHaveHappened();
        }

        /// <summary>
        /// when trying to post and the account is not authorized, then notify the user interface
        /// of the error.
        /// </summary>
        [Test]
        public void Post_AccountNotAuthorized_NotifyUI()
        {
            string errorresponse = "{\"errors\":[{\"code\": 32, \"message\": \"Could not authenticate you.\"}]}";

            IHttpManager fakemanager = A.Fake<IHttpManager>();
            A.CallTo(() => fakemanager.SendWithAuthHeader(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(errorresponse);

            IUINotifier mocknotifier = A.Fake<IUINotifier>();


            ISocialPoster poster = GenerateTwitterPoster(fakemanager, mocknotifier);

            poster.Post("message", "someurl");

            string expectednotificationmessage = "Failed to authenticate twitter account, please try again.";
            A.CallTo(() => mocknotifier.Notify(expectednotificationmessage)).MustHaveHappened();

        }

        /// <summary>
        /// When the account is not already authorised and the user tries to authorise,
        /// then Authorizer is used.
        /// </summary>
        [Test]
        public void Authorize_NotCurrentlyAuthorized_AuthorizeIsCalled()
        {
            IAuthorizer mockauthorizer = A.Fake<IAuthorizer>();
            ISocialPoster poster = GenerateTwitterPoster(mockauthorizer);

            poster.GetAuthorizeURL();

            A.CallTo(() => mockauthorizer.GetAuthorizeURL()).MustHaveHappened();
        }

        /// <summary>
        /// When Authorising the account with pin supplied then authorizer is used.
        /// </summary>
        [Test]
        public void Authorize_PinSupplied_AuthorizeWithPinIsCalled()
        {
            IAuthorizer mockauthorizer = A.Fake<IAuthorizer>();
            ISocialPoster poster = GenerateTwitterPoster(mockauthorizer);

            string pin = "some kind of pin";
            poster.Authorize(pin);

            A.CallTo(() => mockauthorizer.Authorize(pin)).MustHaveHappened();
        }

        /// <summary>
        /// When posting the tweet and twitter replies with success, notify user interface.
        /// </summary>
        [Test]
        public void Post_TwitterRepliesWithSuccess_NotifyUI()
        {
            string response = "{\"created_at\": \"Sun Dec 10 18:23:12 +0000 2017\", \"id\": 939923468115685381\"}";
            IHttpManager fakemanager = A.Fake<IHttpManager>();
            A.CallTo(() => fakemanager.SendWithAuthHeader(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(response);

            IUINotifier mocknotifier = A.Fake<IUINotifier>();
            ISocialPoster poster = GenerateTwitterPoster(mocknotifier, fakemanager);

            poster.Post("some message");

            string expectedmessage = "Posted to twitter";
            A.CallTo(() => mocknotifier.Notify(expectedmessage)).MustHaveHappened();
        }

        /// <summary>
        /// When Posting the tweet and twitter replies with error not known, notify user interface.
        /// </summary>
        [Test]
        public void Post_TwitterRepliesWithErrorNotKnown_NotifyUI()
        {
            string errorresponse = "{\"errors\":[{\"code\": 32, \"message\": \"Some random error\"}]}";

            IHttpManager fakemanager = A.Fake<IHttpManager>();
            A.CallTo(() => fakemanager.SendWithAuthHeader(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored)).Returns(errorresponse);

            IUINotifier mocknotifier = A.Fake<IUINotifier>();


            ISocialPoster poster = GenerateTwitterPoster(fakemanager, mocknotifier);

            poster.Post("message", "someurl");

            string expectednotificationmessage = "Failed to post to twitter, something went wrong.";
            A.CallTo(() => mocknotifier.Notify(expectednotificationmessage)).MustHaveHappened();
        }

        /// <summary>
        /// Generates the twitter poster.
        /// </summary>
        /// <returns>The twitter poster.</returns>
        /// <param name="fakes">Fakes.</param>
        private ISocialPoster GenerateTwitterPoster(params object[] fakes)
        {
            IHttpManager fakehttpmanager = FakeFactory.FindOrCreate<IHttpManager>(fakes);
            IAuthorizer fakeauthorizer = FakeFactory.FindOrCreate<IAuthorizer>(fakes);
            IUINotifier fakeuinotifier = FakeFactory.FindOrCreate<IUINotifier>(fakes);
            return new TwitterPoster(fakehttpmanager, fakeauthorizer, fakeuinotifier);
        }
    }
}
