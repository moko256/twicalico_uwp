using CoreTweet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Credentials;

namespace twicalico
{
    class LocalTokenManager
    {
        private string resourceName = "twicalico";

        public Tokens getToken(uint position=0)
        {
            PasswordCredential loginCredential;

            try
            {
                loginCredential = GetCredentialFromPosition(position);
            }
            catch
            {
                return null;
            }

            if (loginCredential != null)
            {
                string[] name = loginCredential.UserName.Split("@".ToArray());
                loginCredential.RetrievePassword();
                string[] pass = loginCredential.Password.Split(" ".ToArray());
                var consumer = new TwitterConsumerImpl() as TwitterConsumer;
                return Tokens.Create(consumer.ConsumerKey(), consumer.ConsumerSecret(), pass[0], pass[1], long.Parse(name[1]), name[0]);

            }
            else
            {
                return null;
            }

        }

        private Windows.Security.Credentials.PasswordCredential GetCredentialFromPosition(uint position)
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            var credentialList = vault.FindAllByResource(resourceName);
            if (credentialList.Count > 0)
            {
                if (credentialList.Count == 1)
                {
                    return credentialList[(int)position];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void registerToken(Tokens newToken)
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Add(new Windows.Security.Credentials.PasswordCredential(
                resourceName,
                newToken.ScreenName+"@"+newToken.UserId.ToString(),
                newToken.AccessToken+" "+newToken.AccessTokenSecret)
            );
        }

        public void removeToken(uint position)
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            vault.Remove(vault.FindAllByResource(resourceName)[(int)position]);
        }
    }
}
