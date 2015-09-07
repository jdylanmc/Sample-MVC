using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;

namespace Sample.Domain.Model.System
{
    public class HttpAudit
    {
        public HttpAudit() { }

        public HttpAudit(HttpRequestMessage request, string requestData, HttpResponseMessage response)
        {
            // todo: make request and response headers be backed by a dictionary so serialization is cleaner on the ToString method
            this.Timestamp = DateTime.Now;

            this.RequestMethod = request.Method.Method.ToUpper();
            this.RequestUri = request.RequestUri.AbsoluteUri;

            var requestHeaders = request.Headers.ToDictionary(header => header.Key, header => header.Value.ToArray());
            this.RequestHeaders = JsonConvert.SerializeObject(requestHeaders);

            RequestContent = requestData;

            var responseHeaders = response.Headers.ToDictionary(header => header.Key, header => header.Value.ToArray());
            this.ResponseHeaders = JsonConvert.SerializeObject(responseHeaders);

            if (response.Content != null)
            {
                ResponseContent = response.Content.ReadAsStringAsync().Result;
            }
            

            ResponseStatusCode = (int)response.StatusCode;
        }

        public int Id { get; set; }

        public DateTime Timestamp { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(2000)]
        public string RequestMethod { get; set; }

        [Column(TypeName = "VARCHAR")]
        [MaxLength(2000)]
        public string RequestUri { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        public string RequestHeaders { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        public string RequestContent { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        public string ResponseHeaders { get; set; }

        [Column(TypeName = "NVARCHAR(MAX)")]
        [MaxLength]
        public string ResponseContent { get; set; }

        public int ResponseStatusCode { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
