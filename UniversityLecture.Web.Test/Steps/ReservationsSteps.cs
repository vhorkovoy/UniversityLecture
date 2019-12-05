using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TechTalk.SpecFlow;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using UniversityLecture.WEB.Models;
using TechTalk.SpecFlow.Assist;
using System.Net.Http.Headers;

namespace UniversityLecture.Web.Test.Steps
{
    [Binding]
    class ReservationsSteps : WebApplicationFactory<StartupTest>
    {
        private class CereateResult
        {
            public int ID { get; set; }
        }
        public ReservationsSteps(WebApplicationFactory<StartupTest> factory)
        {
            _factory = factory;
        }

        private WebApplicationFactory<StartupTest> _factory;
        private HttpClient _Client;
        private HttpResponseMessage _Response;
        private CereateResult _CreateResult;
        [Given(@"I am a user")]
        public void GivenIAmAUser()
        {
            _Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                BaseAddress = new Uri($"http://localhost/")
            });
        }

        [Given(@"I make a post request to '(.*)' to get token with the following data")]
        public virtual async Task GivenIMakeAPostRequestToToGetTokenWithTheFollowingData(string endPoint, Table table)
        {
            var auth = table.CreateInstance<AuthenticationDto>();
            var jsnauth = JsonConvert.SerializeObject(auth);
            var endPointUri = new Uri(endPoint, UriKind.Relative);
            var content = new StringContent(jsnauth, Encoding.UTF8, "application/json");
            _Response = await _Client.PostAsync(endPointUri, content).ConfigureAwait(false);
        }

        [Given(@"the response status code is '(.*)'")]
        public void GivenTheResponseStatusCodeIs(int expectedCode)
        {
            Assert.IsTrue(_Response.StatusCode.Equals((HttpStatusCode)expectedCode));
        }

        [Given(@"the response data should be '(.*)'")]
        public void GivenTheResponseDataShouldBe(string p0)
        {
            var responseData = _Response.Content.ReadAsStringAsync().Result;
            _Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", responseData);
            Assert.IsFalse(string.IsNullOrEmpty(responseData));
        }

        [When(@"I make a post request to '(.*)' with the following data")]
        public virtual async Task WhenIMakeAPostRequestToWithTheFollowingData(string endPoint, Table table)
        {
            var reserv = table.CreateInstance<ReservationDto>();
            var jsnReserv = JsonConvert.SerializeObject(reserv);
            var endPointUri = new Uri(endPoint, UriKind.Relative);
            var content = new StringContent(jsnReserv, Encoding.UTF8, "application/json");
            _Response = await _Client.PostAsync(endPointUri, content).ConfigureAwait(false);
        }

        [Then(@"the response status code is '(.*)'")]
        public void ThenTheResponseStatusCodeIs(int expectedCode)
        {
            Assert.IsTrue(_Response.StatusCode.Equals((HttpStatusCode)expectedCode));
        }

        [Then(@"the response data should be '(.*)'")]
        public void ThenTheResponseDataShouldBe(string expectedResponse)
        {
            var responseData = _Response.Content.ReadAsStringAsync().Result;
            _CreateResult = JsonConvert.DeserializeObject<CereateResult>(responseData);
            Assert.IsTrue(_CreateResult.ID > 0);
        }

        [Then(@"I make delete request '(.*)' to delete created reservation")]
        public virtual async Task ThenIMakeDeleteRequestToDeleteCreatedReservation(string endPoint)
        {
            var endPointUri = new Uri($"{endPoint}/{_CreateResult.ID}", UriKind.Relative);
            _Response = await _Client.DeleteAsync(endPointUri);
            Assert.IsTrue(_Response.StatusCode.Equals(HttpStatusCode.OK));
        }
    }
}
