using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using TflChallenge.API;
using TflChallenge.Services;
using TflChallenge.Tests.Mocks;

namespace TflChallenge.Tests
{
    [TestClass]
    public class RoadServiceTests
    {
        //Given a valid road ID is specified
        //When the RoadService is called
        //Then the RoadStatusResponse with return Success as true
        [TestMethod]
        public void GivenAValidRoadIdTheRoadStatusResponseReturnsSuccesAsTrue()
        {
            //Arrange
            var api = createRoadApiWithValidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Success);
        }

        //Given a valid road ID is specified
        //When the RoadService is called
        //Then the road ‘displayName’ should be returned
        [TestMethod]
        public void GivenAValidRoadIdTheDisplayNameShouldBeReturned()
        {
            //Arrange
            var api = createRoadApiWithValidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.AreEqual("Blackwall Tunnel", result.RoadDisplayName);
        }

        //Given a valid road ID is specified
        //When the RoadService is called
        //Then the road ‘statusSeverity’ should be returned
        [TestMethod]
        public void GivenAValidRoadIdTheStatusSeverityShouldBeReturned()
        {
            //Arrange
            var api = createRoadApiWithValidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.AreEqual("Good", result.Status.StatusSeverity);
        }

        //Given a valid road ID is specified
        //When the RoadService is called
        //Then the road ‘statusSeverityDescription’ should be returned
        [TestMethod]
        public void GivenAValidRoadIdTheStatusSeverityDescriptionShouldBeReturned()
        {
            //Arrange
            var api = createRoadApiWithValidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.AreEqual("No Exceptional Delays", result.Status.StatusSeverityDescription);
        }

        //Given an invalid road ID is specified
        //When the RoadService is called
        //Then the RoadStatusResponse returns Success as true
        [TestMethod]
        public void GivenAnInValidRoadIdTheRoadStatusResponseReturnsSuccesAsTrue()
        {
            //Arrange
            var api = createApiWithInvalidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("invalidRoad").GetAwaiter().GetResult();

            //Assert
            Assert.IsTrue(result.Success);
        }

        //Given an invalid road ID is specified
        //When the RoadService is called
        //Then the RoadStatusResponse does not contain a Status object
        [TestMethod]
        public void GivenAnInValidRoadIdTheRoadStatusResponseDoesNotContainAStatusObject()
        {
            //Arrange
            var api = createApiWithInvalidRoadIdResponse();
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("invalidRoad").GetAwaiter().GetResult();

            //Assert
            Assert.IsNull(result.Status);
        }

        //Given the API returns an unexpected HTTP Status
        //When the RoadService is called
        //Then the RoadStatusResponse will return Success as false
        [TestMethod]
        public void GivenTheApiReturnsAnUnexpecedHttpStatusTheRoadStatusResponseReturnsSuccesAsFalse()
        {
            //Arrange
            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.Unauthorized
            };
            var api = new RoadApiStub(response);
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Success);
        }

        //Given an exception is thrown calling the API
        //When the RoadService is called
        //Then the RoadStatusResponse will return Success as false
        [TestMethod]
        public void GivenAnExceptionIsThrownCallingTheApiTheRoadStatusResponseReturnsSuccesAsFalse()
        {
            //Arrange
            var exception = new Exception();
            var api = new RoadApiStub(exception);
            var service = new RoadService(api);

            //Act
            var result = service.GetRoadStatusById("validRoad").GetAwaiter().GetResult();

            //Assert
            Assert.IsFalse(result.Success);
        }

        private IRoadApi createRoadApiWithValidRoadIdResponse()
        {
            var roadCorridor = @"[{""$type"":""Tfl.Api.Presentation.Entities.RoadCorridor, Tfl.Api.Presentation.Entities"",""id"":""blackwall tunnel"",""displayName"":""Blackwall Tunnel"",""statusSeverity"":""Good"",""statusSeverityDescription"":""No Exceptional Delays"",""bounds"":""[[-0.01405,51.47478],[0.02452,51.52971]]"",""envelope"":""[[-0.01405,51.47478],[-0.01405,51.52971],[0.02452,51.52971],[0.02452,51.47478],[-0.01405,51.47478]]"",""url"":""/Road/blackwall+tunnel""}]";
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(roadCorridor, Encoding.UTF8, "application/json")
            };

            return new RoadApiStub(response);
        }

        private IRoadApi createApiWithInvalidRoadIdResponse()
        {
            var apiError = @"{""$type"":""Tfl.Api.Presentation.Entities.ApiError, Tfl.Api.Presentation.Entities"",""timestampUtc"":""2018-02-24T23:45:06.7462416Z"",""exceptionType"":""EntityNotFoundException"",""httpStatusCode"":404,""httpStatus"":""NotFound"",""relativeUri"":""/Road/notARoad"",""message"":""The following road id is not recognised: notARoad""}";
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(apiError, Encoding.UTF8, "application/json")
            };

            return new RoadApiStub(response);
        }

    }
}
