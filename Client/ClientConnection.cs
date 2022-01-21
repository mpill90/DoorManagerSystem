using Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client
{
    public class ClientConnection : IDisposable
    {
        private HttpClient HttpClient;
        private string IPAddress;
        public ClientConnection(string ipAddress)
        {
            IPAddress = ipAddress;
            Open();
        }

        private void Open()
        {
            try
            {
                this.HttpClient = new HttpClient();
                this.HttpClient.BaseAddress = new Uri(IPAddress);
                this.HttpClient.DefaultRequestHeaders.Accept.Clear();
                this.HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task CreateDoor(Door door)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.PostAsJsonAsync("api/door/", door).ConfigureAwait(false);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<Door> GetDoor(int id)
        {
            var door = new Door();
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync($@"api/door/{id}").ConfigureAwait(true);
                if (response.IsSuccessStatusCode)
                {
                    List<MediaTypeFormatter> mediaTypeFormatter = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() };

                    door = await response.Content.ReadAsAsync<Door>(mediaTypeFormatter).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return door;
        }

        public async Task<List<Door>> GetDoors()
        {
            var doors = new List<Door>();
            try
            {
                HttpResponseMessage response = await this.HttpClient.GetAsync("api/door/").ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    List<MediaTypeFormatter> mediaTypeFormatter = new List<MediaTypeFormatter>() { new JsonMediaTypeFormatter() };

                    doors = await response.Content.ReadAsAsync<List<Door>>(mediaTypeFormatter).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return doors;
        }

        public async Task UpdateDoor(Door selectedDoor)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.PutAsJsonAsync("api/door/", selectedDoor).ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task DeleteDoor(int id)
        {
            try
            {
                HttpResponseMessage response = await this.HttpClient.DeleteAsync($"api/door/{id}").ConfigureAwait(false);

                response.EnsureSuccessStatusCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
