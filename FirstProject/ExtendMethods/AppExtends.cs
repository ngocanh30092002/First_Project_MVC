using System.Net;

namespace FirstProject.ExtendMethods
{
    public static class AppExtends
    {
        public static void UseStatusCodePageWithCustomer(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseStatusCodePages(config =>
            {
                config.Run(async context =>
                {
                    var response = context.Response;
                    var code = response.StatusCode;

                    var content = $@"
                        <html>
                            <head>
                                <meta charset='UTF-8'/>
                                <title>Error - {code} </title>
                            </head>
                            <body>
                            <p style = 'color:red; font-size: 30px;'>
                                Có lỗi xảy ra: {code} - {(HttpStatusCode)code}
                            </p>
                            </body>
                        </html>
                    ";
                    await response.WriteAsync(content);
                });
            });
        }
    }
}
