using System;
using Connector.CcvShop.Interface;
using Newtonsoft.Json;

namespace Connector.CcvShop.Api.Execute
{
    internal class ExecuteParams
    {
        public IConnectionCcvShop Connection { get; internal set; }
        public string Uri { get; internal set; }
        public string Method { get; internal set; }
        public string TargetLanguage { get; internal set; }

        public bool HasData => Data != null;
        private object _data;
        public object Data
        {
            get { return _data; }
            internal set
            {
                _data = value;
                if (_data != null)
                    _dataJson = JsonConvert.SerializeObject(Data);
            }
        }

        private string _dataJson;
        public string DataJson => HasData ? _dataJson : string.Empty;

        internal void SetUri(string uri, Base.ParameterBase parameters = null)
        {
            if (parameters == null)
                Uri = uri;
            else
                Uri = $"{uri}?{parameters.ConstructQueryString()}";
        }

        public ExecuteParams(IConnectionCcvShop connection, string lan = null)
        {
            if (connection == null)
            {
                string errorMessage = "Connection should be available when you want to execute an API call.";
                Error.ErrorLogger.ErrorOccurred(errorMessage);
                throw new NotSupportedException(errorMessage);
            }
            Connection = connection;
            TargetLanguage = lan;
        }
    }
}
