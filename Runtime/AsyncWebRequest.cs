using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WattanaGaming.WebRequest
{
    public static class AsyncWebRequest
    {
        public static async Task<UnityWebRequest> GET(string uri)
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(uri);
            UnityWebRequestAsyncOperation asyncOperation = webRequest.SendWebRequest();
            while (!asyncOperation.isDone) { await Task.Yield(); }

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                    throw new WebRequestError("A connection error has occured.");
                case UnityWebRequest.Result.DataProcessingError:
                    throw new WebRequestError("A data processing error has occured.");
                case UnityWebRequest.Result.ProtocolError:
                    throw new WebRequestError("A protocol error has occured.");
            }

            return webRequest;
        }

        public class WebRequestError : System.Exception
        {
            public WebRequestError(string message) : base(message) { }
        }
    }
}
