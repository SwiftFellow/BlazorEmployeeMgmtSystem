﻿using BaseLibrary.Responses;
using ClientLibrary.Helpers;
using ClientLibrary.Services.Contracts;
using System.Net.Http.Json;

namespace ClientLibrary.Services.Implementations;

public class GenericServiceImplementation<T>(GetHttpClient getHttpClient) : IGenericServiceInterface<T>
{
    // Create
    public async Task<GeneralResponse> Insert(T item, string baseUrl)
    {
        HttpClient httpClient = await getHttpClient.GetPrivateHttpClient();
        HttpResponseMessage response = await httpClient.PostAsJsonAsync($"{baseUrl}/add", item);
        GeneralResponse? result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    // Read All
    public async Task<List<T>> GetAll(string baseUrl)
    {
        HttpClient httpClient = await getHttpClient.GetPrivateHttpClient();
        var results = await httpClient.GetFromJsonAsync<List<T>>($"{baseUrl}/all");
        return results!;
    }

    // Read Single {id}
    public async Task<T> GetById(int id, string baseUrl)
    {
        HttpClient httpClient = await getHttpClient.GetPrivateHttpClient();
        var result = await httpClient.GetFromJsonAsync<T>($"{baseUrl}/single/{id}");
        return result!;
    }

    // Update {model}
    public async Task<GeneralResponse> Update(T item, string baseUrl)
    {
        HttpClient httpClient = await getHttpClient.GetPrivateHttpClient();
        HttpResponseMessage response = await httpClient.PutAsJsonAsync($"{baseUrl}/update", item);
        GeneralResponse? result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }

    // Delete {id}
    public async Task<GeneralResponse> DeleteById(int id, string baseUrl)
    {
        HttpClient httpClient = await getHttpClient.GetPrivateHttpClient();
        HttpResponseMessage response = await httpClient.DeleteAsync($"{baseUrl}/delete/{id}");
        GeneralResponse? result = await response.Content.ReadFromJsonAsync<GeneralResponse>();
        return result!;
    }
}