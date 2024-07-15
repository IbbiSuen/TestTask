using APITests.APICore;
using APITests.APICore.Models;
using Newtonsoft.Json;
using System.Text;

namespace APITests.APITests
{
    public class Tests: APIBase
    {

        [Fact]
        public async Task TestGetActivities()
        {
            HttpResponseMessage response = await _client.GetAsync("Activities");

            Assert.True(response.IsSuccessStatusCode);

            var activities = JsonConvert.DeserializeObject<List<Activities>>(await response.Content.ReadAsStringAsync());

            Assert.Equal(30, activities.Count);

            Assert.All(activities, a => Assert.NotEqual(DateTime.Today.AddDays(-1), DateTime.Parse(a.DueDate)));

        }

        [Fact]
        public async Task TestPostAuthors()
        {
            var author = new Authors { Id = 1, IdBook = 2, FirstName = "John", LastName = "Garem" };
            string authorJson = JsonConvert.SerializeObject(author);
            
            StringContent httpContent = new StringContent(authorJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("Authors", httpContent);

            Assert.True(response.IsSuccessStatusCode);

            var returnedAuthor = JsonConvert.DeserializeObject<Authors>(await response.Content.ReadAsStringAsync());

            Assert.True(new PropertyEqualityComparer<Authors>().Equals(author, returnedAuthor));
        }

        [Fact]
        public async Task TestPutBooks()
        {
            var book = new Books { Id = 1, Title = "New Title", Description = "New Description", PageCount = 123, Excerpt = "New Excerpt", PublishDate = DateTime.Now };
            string bookJson = JsonConvert.SerializeObject(book);

            StringContent httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync($"Books/{book.Id}", httpContent);

            Assert.True(response.IsSuccessStatusCode);

            var returnedBook = JsonConvert.DeserializeObject<Books>(await response.Content.ReadAsStringAsync());

            Assert.True(new PropertyEqualityComparer<Books>().Equals(book, returnedBook));

        }

        [Theory]
        [InlineData(1, 100)]
        [InlineData(2, 200)]
        [InlineData(3, 300)]
        [InlineData(4, 400)]
        [InlineData(5, 500)]
        [InlineData(6, 600)]
        [InlineData(7, 700)]
        [InlineData(8, 800)]
        [InlineData(9, 900)]
        [InlineData(10, 1000)]
        public async Task TestGetBooks(int id, int pageCount)
        {
            HttpResponseMessage response = await _client.GetAsync($"Books/{id}");

            Assert.True(response.IsSuccessStatusCode);

            var returnedBook = JsonConvert.DeserializeObject<Books>(await response.Content.ReadAsStringAsync());

            Assert.Equal(id, returnedBook.Id);
            Assert.Equal(pageCount, returnedBook.PageCount);
        }

        [Fact]
        public async Task TestDeleteAuthor()
        {
            LoggerHelper loggerHelper = new LoggerHelper();

            var author = new Authors { FirstName = "John", LastName = "Doe" };
            string authorJson = JsonConvert.SerializeObject(author);

            loggerHelper.LogInfo($"Request Body: {authorJson}");

            StringContent httpContent = new StringContent(authorJson, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await _client.PostAsync("Authors", httpContent);
            loggerHelper.LogInfo($"POST URL: {_client.BaseAddress}Authors");

            loggerHelper.LogInfo($"Expected POST Response: Success Status Code");
            loggerHelper.LogInfo($"Actual POST Response: {postResponse.StatusCode}");

            Assert.True(postResponse.IsSuccessStatusCode, $"Fail in creating author. Code:{postResponse.StatusCode} Response:{postResponse.Content.ReadAsStringAsync().Result}");

            Authors returnedAuthorPost = JsonConvert.DeserializeObject<Authors>(await postResponse.Content.ReadAsStringAsync());

            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"Authors/{returnedAuthorPost.Id}");
            loggerHelper.LogInfo($"DELETE URL: {_client.BaseAddress}Authors/{returnedAuthorPost.Id}");

            loggerHelper.LogInfo($"Expected DELETE Response: Success Status Code");
            loggerHelper.LogInfo($"Actual DELETE Response: {deleteResponse.StatusCode}");

            Assert.True(deleteResponse.IsSuccessStatusCode, $"Fail in deleting author. Code:{deleteResponse.StatusCode} Response:{deleteResponse.Content.ReadAsStringAsync().Result}");
        }
    }
}
