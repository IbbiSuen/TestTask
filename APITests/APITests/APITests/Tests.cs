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
            _loggerHelper.LogHttpRequest("GET", "Activities", response);

            Assert.True(response.IsSuccessStatusCode);

            var activities = JsonConvert.DeserializeObject<List<Activities>>(await response.Content.ReadAsStringAsync());
            _loggerHelper.LogInfo($"Expected Count: 30");
            _loggerHelper.LogInfo($"Actual Count: {activities.Count}");
            Assert.Equal(30, activities.Count);

            string activitiesJson = JsonConvert.SerializeObject(activities);
            _loggerHelper.LogInfo($"Activities : {activitiesJson}");

            Assert.All(activities, a => Assert.NotEqual(DateTime.Today.AddDays(-1), DateTime.Parse(a.DueDate)));
        }


        [Fact]
        public async Task TestPostAuthors()
        {
            var author = new Authors { Id = 1, IdBook = 2, FirstName = "John", LastName = "Garem" };
            string authorJson = JsonConvert.SerializeObject(author);
            _loggerHelper.LogInfo($"Request Body: {authorJson}");

            StringContent httpContent = new StringContent(authorJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PostAsync("Authors", httpContent);
            _loggerHelper.LogHttpRequest("Post", "Authors", response, authorJson);

            Assert.True(response.IsSuccessStatusCode);

            var returnedAuthor = JsonConvert.DeserializeObject<Authors>(await response.Content.ReadAsStringAsync());

            string returnedAuthorJson = JsonConvert.SerializeObject(returnedAuthor);
            _loggerHelper.LogInfo($"Expected Author: {authorJson}");
            _loggerHelper.LogInfo($"Returned Author: {returnedAuthorJson}");

            Assert.True(new PropertyEqualityComparer<Authors>().Equals(author, returnedAuthor));
        }


        [Fact]
        public async Task TestPutBooks()
        {
            var book = new Books { Id = 1, Title = "New Title", Description = "New Description", PageCount = 123, Excerpt = "New Excerpt", PublishDate = DateTime.Now };
            string bookJson = JsonConvert.SerializeObject(book);
            _loggerHelper.LogInfo($"Request Body: {bookJson}");

            StringContent httpContent = new StringContent(bookJson, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _client.PutAsync($"Books/{book.Id}", httpContent);
            _loggerHelper.LogHttpRequest("PUT", $"Books/{book.Id}", response, bookJson);

            Assert.True(response.IsSuccessStatusCode);

            var returnedBook = JsonConvert.DeserializeObject<Books>(await response.Content.ReadAsStringAsync());

            string returnedBookJson = JsonConvert.SerializeObject(returnedBook);
            _loggerHelper.LogInfo($"Expected Book: {bookJson}");
            _loggerHelper.LogInfo($"Returned Book: {returnedBookJson}");

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

            _loggerHelper.LogHttpRequest("GET", $"Books/{id}", response);

            Assert.True(response.IsSuccessStatusCode);

            var returnedBook = JsonConvert.DeserializeObject<Books>(await response.Content.ReadAsStringAsync());

            _loggerHelper.LogInfo($"Expected Book ID: {id}, Expected Page Count: {pageCount}");
            _loggerHelper.LogInfo($"Actual Book ID: {returnedBook.Id}, Actual Page Count: {returnedBook.PageCount}");

            Assert.Equal(id, returnedBook.Id);
            Assert.Equal(pageCount, returnedBook.PageCount);
        }

        [Fact]
        public async Task TestDeleteAuthor()
        {
            var author = new Authors { FirstName = "John", LastName = "Doe" };
            string authorJson = JsonConvert.SerializeObject(author);

            StringContent httpContent = new StringContent(authorJson, Encoding.UTF8, "application/json");

            HttpResponseMessage postResponse = await _client.PostAsync("Authors", httpContent);
            _loggerHelper.LogHttpRequest("POST", "/Authors", postResponse, authorJson);

            Assert.True(postResponse.IsSuccessStatusCode);

            Authors returnedAuthorPost = JsonConvert.DeserializeObject<Authors>(await postResponse.Content.ReadAsStringAsync());

            HttpResponseMessage deleteResponse = await _client.DeleteAsync($"Authors/{returnedAuthorPost.Id}");
            _loggerHelper.LogHttpRequest("DELETE", $"Authors/{returnedAuthorPost.Id}", deleteResponse);

            Assert.True(deleteResponse.IsSuccessStatusCode);
        }
    }
    }
