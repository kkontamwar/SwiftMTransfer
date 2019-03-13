using Microsoft.VisualStudio.TestTools.UnitTesting;
using SwiftMTransfer.Controllers;
using SwiftMTransfer.Models;
using System.Net.Http;
using System.Web.Http;

namespace SwiftMTransfer.Tests.Controllers
{
	[TestClass]
	public class UserControllerTest
	{


		[TestMethod]
		public void Post()
		{
			// Arrange
			//UserController controller = new UserController();
			//User userInfo = new User();
			//// Act
			//HttpResponseMessage responseMessage = controller.Post(userInfo);

			var controller = new UserController();
			controller.Request = new HttpRequestMessage();
			controller.Configuration = new HttpConfiguration();
			User userInfo = new User
			{
				FirstName = "Ravi"
			};
			HttpResponseMessage response = controller.Post(userInfo);
			Assert.IsTrue(response.IsSuccessStatusCode);

		}

	}
}
