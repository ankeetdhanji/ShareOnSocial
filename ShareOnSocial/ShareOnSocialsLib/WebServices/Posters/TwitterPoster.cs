using System;
using ShareOnSocialsLib.WebServices.Http;
using ShareOnSocialsLib.WebServices.Authorizers;
using Newtonsoft.Json.Linq;
using ShareOnSocialsLib.UIBridge;
using System.Threading.Tasks;

namespace ShareOnSocialsLib.WebServices.Posters
{
    /// <summary>
    /// Twitter Poster.
    /// 
    /// This posts a tweet to twitter if the connected account has been authenticated.
    /// </summary>
    public class TwitterPoster : ISocialPoster
    {
        #region Private Variables

        /// <summary>
        /// The http manager.
        /// </summary>
        private IHttpManager mHttpManager;

        /// <summary>
        /// The authorizer.
        /// </summary>
        private IAuthorizer mAuthorizer;

        /// <summary>
        /// The UI Notifier.
        /// </summary>
        private IUINotifier mUINotifier;

        /// <summary>
        /// The twitter post status URL.
        /// </summary>
        private string mTwitterPostStatusURL = "https://api.twitter.com/1.1/statuses/update.json";

        #endregion

        #region Public Properties

        /// <summary>
        /// Indicates with the connected account is authorised to tweet.
        /// </summary>
        /// <returns><c>true</c>, if account is authorized, <c>false</c> otherwise.</returns>
        public bool IsAuthorized()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ShareOnSocialsLib.WebServices.Posters.TwitterPoster"/> class.
        /// </summary>
        /// <param name="httpmanager">Httpmanager.</param>
        /// <param name="authorizer">Authorizer.</param>
        /// <param name="uinotifier">UINotifier.</param>
        public TwitterPoster(IHttpManager httpmanager, IAuthorizer authorizer, IUINotifier uinotifier)
        {
            mHttpManager = httpmanager;
            mAuthorizer = authorizer;
            mUINotifier = uinotifier;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the url to allow the user to login to twitter.
        /// </summary>
        /// <returns>Twitter login url</returns>
        public Task<string> GetAuthorizeURL()
        {
            return mAuthorizer.GetAuthorizeURL();
        }

        /// <summary>
        /// Authorize this twitter account with specified pin.
        /// </summary>
        /// <returns>The authorize.</returns>
        /// <param name="pin">Pin.</param>
        public Task<bool> Authorize(string pin)
        {
            return mAuthorizer.Authorize(pin);
        }

        /// <summary>
        /// Post the specified message and url supplied.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="attachmenturl">Attachment url.</param>
        public async void Post(string message, string attachmenturl = null)
        {
            // build url to request the tweet.
            string url = string.Format("{0}?status={1}", mTwitterPostStatusURL, message);
            if (attachmenturl != null)
            {
                url = string.Format("{0}&attachment_url={1}", url, attachmenturl);
            }

            // get header for http request.
            string authheader = mAuthorizer.GenerateHeader(url);

            // send the request and wait for result
            string response = await mHttpManager.SendWithAuthHeader("POST", url, authheader);

            // check if errored.
            if (response.Contains("errors"))
            {
                // we got an error
                JToken tokens = JToken.Parse(response);
                JToken errorstoken = tokens["errors"].First;
                JToken messagetoken = errorstoken["message"];
                string valuetoken = messagetoken.Value<string>();
                if (valuetoken == "Could not authenticate you.")
                {
                    mUINotifier.Notify("Failed to authenticate twitter account, please try again.");
                }
                else
                {
                    // Error not recognised.
                    mUINotifier.Notify("Failed to post to twitter, something went wrong.");
                }

            }
            else if (response.Contains("created_at"))
            {
                // tweet has been posted
                mUINotifier.Notify("Posted to twitter");
            }
        }

        #endregion
    }
}
