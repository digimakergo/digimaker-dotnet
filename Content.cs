using System.Net.Http.Headers;
using Google.Apis.Json;
using GraphQL;
using GraphQL.Client;
using GraphQL.Client.Http;
using Newtonsoft.Json;
using GraphQL.Client.Serializer.Newtonsoft;
using System.Net.Http;

namespace Dcp
{

    public class ContentData
    {
       public List<Article> Article { get; set; }
    }

    public class Article
    {
        public string Title { get; set; }
    }

    public class Content
    {

        private static string host = "http://dmdemo2.dev.digimaker.no/api";
        private static string apiKey = "ddddxxxx7383423424sjfshfgfysifsik";

        public static ContentData List()
        {
            var heroRequest = new GraphQLRequest
            {
                Query = @"query{
                            article(filter:{cid: 473})
                            {
                            cid,
                            title,
                            summary
                            }
                          }"
            };

            var graphQLClient = new GraphQLHttpClient(host+"/graphql", new GraphQL.Client.Serializer.Newtonsoft.NewtonsoftJsonSerializer());


            var httpClient = graphQLClient.HttpClient;
            httpClient.DefaultRequestHeaders.Add("apiKey", apiKey);
         
            var result = new ContentData();
            var resp = Task.Run(() => graphQLClient.SendQueryAsync<ContentData>(heroRequest)).Result;
            result = resp.Data;
            return result;

        }

    }
}