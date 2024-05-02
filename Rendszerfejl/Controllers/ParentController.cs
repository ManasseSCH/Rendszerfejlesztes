using Microsoft.AspNetCore.Mvc;

namespace Rendszerfejl.Controllers
{
	public class ParentController : Controller
	{
		public static async Task<string> getString(string url, string jwtToken)
		{
			string message = "";
			using (System.Net.Http.HttpClient client = new HttpClient())
			{
               
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

                var response = await client.GetAsync("https://localhost:7062/api/" + url);
				response.EnsureSuccessStatusCode();
				if (response.IsSuccessStatusCode)
				{
					message = await response.Content.ReadAsStringAsync();

				}
				return message;

			}
		}
	}
}
