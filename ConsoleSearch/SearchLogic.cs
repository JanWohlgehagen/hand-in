using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleSearch
{
    public class SearchLogic
    {
        private HttpClient api = new() { BaseAddress = new Uri("http://word-service") };

        Dictionary<string, int> mWords;

        public SearchLogic()
        {
            try
            {
                var response = api.GetAsync("Word").Result; // Make asynchronous call synchronous
                response.EnsureSuccessStatusCode(); // Ensure HTTP success status

                var content = response.Content.ReadAsStringAsync().Result;

                // Deserialize the content into the dictionary
                var words = JsonSerializer.Deserialize<Dictionary<string, int>>(content);

                // Optionally, you can assign the deserialized data to a member variable if needed
                // mWords = words;

                // Do something with the words
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exception
                Console.WriteLine($"HTTP Request Exception: {e.Message}");
            }
            catch (JsonException e)
            {
                // Handle JSON deserialization exception
                Console.WriteLine($"JSON Deserialization Exception: {e.Message}");
            }
            catch (Exception e)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // Make sure to dispose the HttpClient instance
                api.Dispose();
            }
        }

        public int GetIdOf(string word)
        {
            if (mWords.ContainsKey(word))
                return mWords[word];
            return -1;
        }

        public Dictionary<int, int> GetDocumentsByWordIds(IEnumerable<int> wordIds)
        {
            var url = "Document/GetByWordIds?wordIds=" + string.Join("&wordIds=", wordIds);

            try
            {
                var response = api.GetAsync(url).Result; // Make asynchronous call synchronous
                response.EnsureSuccessStatusCode(); // Ensure HTTP success status

                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(content);

                return JsonSerializer.Deserialize<Dictionary<int, int>>(content);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exception
                Console.WriteLine($"HTTP Request Exception: {e.Message}");
            }
            catch (JsonException e)
            {
                // Handle JSON deserialization exception
                Console.WriteLine($"JSON Deserialization Exception: {e.Message}");
            }
            catch (Exception e)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // Make sure to dispose the HttpClient instance
                api.Dispose();
            }

            return null; // Return null if an error occurred
        }

        public List<string> GetDocumentDetails(List<int> docIds)
        {
            var url = "Document/GetByDocIds?docIds=" + string.Join("&docIds=", docIds);

            try
            {
                var response = api.GetAsync(url).Result; // Make asynchronous call synchronous
                response.EnsureSuccessStatusCode(); // Ensure HTTP success status

                var content = response.Content.ReadAsStringAsync().Result;

                return JsonSerializer.Deserialize<List<string>>(content);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request exception
                Console.WriteLine($"HTTP Request Exception: {e.Message}");
            }
            catch (JsonException e)
            {
                // Handle JSON deserialization exception
                Console.WriteLine($"JSON Deserialization Exception: {e.Message}");
            }
            catch (Exception e)
            {
                // Handle other exceptions
                Console.WriteLine($"An unexpected error occurred: {e.Message}");
            }
            finally
            {
                // Make sure to dispose the HttpClient instance
                api.Dispose();
            }

            return null; // Return null if an error occurred
        }
    }
}