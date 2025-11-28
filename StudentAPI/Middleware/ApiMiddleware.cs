using Polly;
using Polly.Extensions.Http;
using StudentAPI.Services;
using System.Net;
using System.Text;


namespace StudentAPI.Middleware
{
    public static class ApiMiddleware
    {
        public static void AddGradeServicesToApiContainer(this IServiceCollection services,
                                                       ConfigurationManager Configuration)
        {


            services.AddHttpClient<IGradeService, GradeService>(options =>
            {
                options.BaseAddress = new Uri(Configuration["GradeApiConfig:BaseUrl"]);
            })
            .AddPolicyHandler(GetFallbackPolicy())
            .AddPolicyHandler(GetRetryPolicy())
            .AddPolicyHandler(GetCircuitBreakerPolicy());
            //.AddPolicyHandler(GetCircuitBreakerPolicy()); // LAST (innermost);
            //.AddPolicyHandler(GetRetryPolicy());

        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            //return HttpPolicyExtensions
            //               .HandleTransientHttpError()
            //               .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            //               .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(2));
            return HttpPolicyExtensions
                          .HandleTransientHttpError()
                          .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                          .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2,retryAttempt)));
        }
        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                           .HandleTransientHttpError()
                           .CircuitBreakerAsync(3,
                            TimeSpan.FromSeconds(5));


        }
        //private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        //{
        //    return Policy<HttpResponseMessage>
        //        .Handle<HttpRequestException>()
        //        .OrResult(response => !response.IsSuccessStatusCode)
        //        .FallbackAsync(
        //            fallbackValue: new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        //            {
        //                Content = new StringContent(
        //                    "{\"message\":\"Fallback executed - service unavailable, returning default\"}",
        //                    System.Text.Encoding.UTF8,
        //                    "application/json")
        //            },
        //            onFallbackAsync: async (outcome, context) =>
        //            {
        //                Console.WriteLine("🔥 FALLBACK TRIGGERED (Service Down)");
        //                Console.WriteLine($"Reason: {outcome.Exception?.Message ?? outcome.Result.StatusCode.ToString()}");
        //                await Task.CompletedTask;
        //            });
        //}
        //private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        //{
        //    // Sample fallback grade returned as JSON (plain integer is valid JSON for GetFromJsonAsync<int>())
        //    var fallbackResponse = new HttpResponseMessage(HttpStatusCode.OK)
        //    {
        //        Content = new StringContent("7", Encoding.UTF8, "application/json")
        //    };

        //    return HttpPolicyExtensions
        //               .HandleTransientHttpError()
        //               .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
        //               .FallbackAsync(fallbackResponse, onFallbackAsync: async (outcome, context) =>
        //               {
        //                   // Optional: log fallback occurrence here. Keep lightweight to avoid DI complexity in this static helper.
        //                   await Task.CompletedTask;
        //               });
        //}
        //private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        //{
        //    return Policy<HttpResponseMessage>
        //        .Handle<HttpRequestException>()
        //        .OrResult(r => !r.IsSuccessStatusCode)
        //        .FallbackAsync(
        //            fallbackValue: new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        //            {
        //                Content = new StringContent(
        //                    "{\"message\":\"Fallback – Grade service unavailable\"}",
        //                    System.Text.Encoding.UTF8,
        //                    "application/json")
        //            },
        //            onFallbackAsync: async (outcome, ctx) =>
        //            {
        //                Console.WriteLine("🔥 FALLBACK TRIGGERED for Grade API");
        //                await Task.CompletedTask;
        //            });
        //}

        //private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        //{
        //    return Policy<HttpResponseMessage>
        //        .Handle<Exception>()
        //        .OrResult(r => !r.IsSuccessStatusCode)
        //        .FallbackAsync(
        //            fallbackValue: new HttpResponseMessage(HttpStatusCode.OK)
        //            {
        //                Content = new StringContent("Fallback: returning default grade")
        //            },
        //            onFallbackAsync: (outcome, context) =>
        //            {
        //                Console.WriteLine(">>> FALLBACK TRIGGERED <<<");
        //                return Task.CompletedTask;
        //            }
        //        );
        //}
        private static IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
        {
            return Policy<HttpResponseMessage>
                .Handle<Exception>()                           // retries & circuit errors
                .OrResult(r => !r.IsSuccessStatusCode)         // API 500
                .FallbackAsync(
                    fallbackValue: new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent("2000")     // <<<<<<<< FALLBACK VALUE HERE
                    },
                    onFallbackAsync: (outcome, context) =>
                    {
                        Console.WriteLine("🔥🔥🔥 FALLBACK ACTIVATED 🔥🔥🔥");

                        if (outcome.Exception != null)
                            Console.WriteLine("Reason: " + outcome.Exception.Message);
                        else
                            Console.WriteLine("Reason: HTTP Failure " + outcome.Result.StatusCode);

                        Console.WriteLine("Returning fallback grade = 2000");
                        return Task.CompletedTask;
                    }
                );
        }




    }
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
