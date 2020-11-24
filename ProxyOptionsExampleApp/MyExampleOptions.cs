namespace ProxyOptionsExampleApp
{
    using System.Net;
    using Microsoft.Extensions.Options;

    public class MyExampleOptions : IOptions<MyExampleOptions>
    {
        public string ExampleSetting1
        {
            get; set;
        }

        //
        // პროქსის პარამეტრები:
        //
        public string? ProxyHost
        {
            get; set;
        }

        public int ProxyPort
        {
            get; set;
        }

        public NetworkCredential? ProxyCredentials
        {
            get; set;
        }

        public MyExampleOptions Value => this;
    }
}
