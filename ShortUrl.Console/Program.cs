using Base62;
using System.Security.Cryptography;
using System.Text;

var urls = new Dictionary<string, string>();

Console.WriteLine("Enter URL: ");
var urlString = Console.ReadLine();

using (var md5 = MD5.Create())
{
    var urlBytes = Encoding.ASCII.GetBytes(urlString);
    var withMd5 = md5.ComputeHash(urlBytes).ToBase62();
    Console.WriteLine(withMd5);

    urls.Add(withMd5, urlString);
}

Console.WriteLine("Find URL: ");
var url = Console.ReadLine();

Console.WriteLine(urls[url]);