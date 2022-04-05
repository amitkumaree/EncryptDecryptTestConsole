using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using EncryptDecryptTestConsole.Model;

namespace EncryptDecryptTestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine("Hello World!");

            var d = new Request
            {
                XPRIORITY = Constants.XPRIORITYNeft,
                tranRefNo = "",                
                amount = 1,
                senderAccNo = 0000000,
                beneAccNo = 000000,
                beneName = "",
                beneIFSC = Constants.beneIFSC,
                narration1 = "test",
                crpId = "",
                crpUsr = "",
                AggrId = "CUST0770",
                urn = "",
                AggrName = "JANTA",
                txnType = "RGS",
                WorkFlow_Reqd = "N"
            };
            // serealise JSON data
            var jsonData = JsonSerializer.Serialize(d);            
            Console.WriteLine("Test Data " + jsonData);           

            using (var client = new HttpClient())
            {
                var encrptedData = Encryption(jsonData);
                Console.WriteLine("Encrpted Data " + encrptedData);
                client.DefaultRequestHeaders.Add("Apikey", Constants.ApiKey);
                client.DefaultRequestHeaders.Add("BCID", Constants.BCID);
                client.DefaultRequestHeaders.Add("Passcode", Constants.Passcode);
                var response = client.PostAsync(Constants.URL, new StringContent(encrptedData));
                if (response.Result.IsSuccessStatusCode)
                {
                    var data = response.Result.Content.ReadAsStringAsync();
                    Console.WriteLine("Recieved data - " + data);
                }
                else
                {
                    Console.WriteLine(string.Format("No data recieved / Http Status {0} , http Status {1}", 
                        response.Result.StatusCode, response.Result.ReasonPhrase));
                }                
            }
            Console.WriteLine("http call ended");
        }

public static string Encryption(string strText)
        {           
           var publicKey = @"<RSAKeyValue><Modulus>
                MIIE7jCCAtagAwIBAgIIWmFBujLqylAwDQYJKoZIhvcNAQEMBQAwFTETMBEGA1UEAwwKcnNhX2Fw
                aWtleTAeFw0xODEwMzAwNDQ3MThaFw0yMzEwMjkwNDQ3MThaMBUxEzARBgNVBAMMCnJzYV9hcGlr
                ZXkwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQCwjBVK1CLppIwsFm7e+Fp85Hk1Mw2n
                5Nc/DKT/pWhpJB8OdlpJA9iF23hrxfbXkrBfCkgvV4Ek4fY1byOnkA7hZq4dYTASCAm89oLwWDNm
                0OGNh7E6T7/JoNtjtT0Gh8lJTvpUgHFGg3tiYCScAqul+fS6Rc8+5THk3L9zLzme6eqjkzwBx/ZV
                XBIZlAwFkVKbfLFg51LiVoOUz6zXD7nAsMyNhKAgybvqulV07eGzafZ1IBgzpcw5qo0PAd1mTqfy
                U+CK9hVeNPPspT16qQWd5xa+fa6BEjuGCumVnFLTbSTRAF5h3QAfvMlkpLdejlXJwvTVQ79Zg5C8
                Hu/yWB7tOJBncIKue7KSpwn+vkMws79wpAB5mL4tD3kVCDf2Og7wbtt87v5rcazxF7eZFbsADzHV
                oSftdkw5S7iXgh82/CHbRXhzPfG8Zd2v1ksW+Bfnn3czEIMGOSJrKfMbyCYtVMihoi0/L6SHA7++
                N9aRrQvfK9PeXnlHgf8pErGUdpjnwdV0tu5atSgf/iBuRgVgUL6t6MFbnBsTQUmZYiQRcsqxOVdy
                yfp4DOLgFHGJ1D/isgR/ypalIXMmhuK8GdZ7hukEDX2Dc3js8OkPnFLq6Ps4NIGESfbZSeyINoZX
                5GGxdgD/GpokKMHr5bsI3TQujCvzuxShPhUArzCs6TgPmwIDAQABo0IwQDAdBgNVHQ4EFgQUyNoW
                eeLVSzVybz7gcZnZlj01cv4wHwYDVR0jBBgwFoAUyNoWeeLVSzVybz7gcZnZlj01cv4wDQYJKoZI
                hvcNAQEMBQADggIBADuwEh31OI66oSMB6a79Pd6WSqiyD2NBskdRF7st7CRP5vqeH4P/4srNFAqC
                9CjsOmXmSpZFckYQ4zgtqnVQBY7jQlCuSHmg8/Lr1qIzRsMvQmhvp6DJ+bEfQgqcJ+a6tR9cH6hD
                VahoMZDEpt3J0fIp30z+O7wJ03K6q5Di/rNey6Ac3GoZwlCi8OFCTmwihcn56I+ssxAqzlq53hzO
                iBLLmcMTrWSJWePPkYEhrbBxywg1qJRRGWwkfr1dbRZ22umLHU0R/QdK+jQtqyzghqJpd3T/lHzK
                uzAsa0s1R+qMqurKu6mulcLp/XmZpY+Fm4T0WRXzcZBf9trkCSO2Z3VvkCTeGu/WAi3UQpx4HfGr
                x02m/h8CHCPPO+PKYthpvSR+0jmiVBaaBo029UG0i2oYBTckng2sy0fx0E+rHnR7pk5Worv8BMm5
                sewPUkDDJMZhLtm/bd/VxlI/b56vEA7HvupSWzc7xXV8lZOHVEUAotrlXz+Je2MkEEQIDnYUOYhw
                78yFMJJddK9tJVRy8tr8I2j6Zi62jQp/Zltq5JOwpOw/9poovd9wgeRBjuFnscoR/YWrNdPjsjpJ
                g/CCb6mthz4R2Mu4enD1YghW7w5darrlUHaYAk+SnwWhMwDwZWWfrVNeEaNq/t/gRm/Ljy+Of3lA
                nztA1PrT4bk1KvZX
               </Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";


            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    // client encrypting data with public key issued by server                    
                    rsa.FromXmlString(publicKey.ToString());

                    var encryptedData = rsa.Encrypt(testData, false);

                    var base64Encrypted = Convert.ToBase64String(encryptedData);

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static string Decryption(string strText)
        {
            var privateKey = @"<RSAKeyValue><Modulus>21wEnTU+mcD2w0Lfo1Gv4rtcSWsQJQTNa6gio05AOkV/Er9w3Y13Ddo5wGtjJ19402S71HUeN0vbKILLJdRSES5MHSdJPSVrOqdrll/vLXxDxWs/U0UT1c8u6k/Ogx9hTtZxYwoeYqdhDblof3E75d9n2F0Zvf6iTb4cI7j6fMs=
                            </Modulus><Exponent>AQAB</Exponent><P>/aULPE6jd5IkwtWXmReyMUhmI/nfwfkQSyl7tsg2PKdpcxk4mpPZUdEQhHQLvE84w2DhTyYkPHCtq/mMKE3MHw==</P><Q>3WV46X9Arg2l9cxb67KVlNVXyCqc/w+LWt/tbhLJvV2xCF/0rWKPsBJ9MC6cquaqNPxWWEav8RAVbmmGrJt51Q==</Q><DP>8TuZFgBMpBoQcGUoS2goB4st6aVq1FcG0hVgHhUI0GMAfYFNPmbDV3cY2IBt8Oj/uYJYhyhlaj5YTqmGTYbATQ==</DP><DQ>FIoVbZQgrAUYIHWVEYi/187zFd7eMct/Yi7kGBImJStMATrluDAspGkStCWe4zwDDmdam1XzfKnBUzz3AYxrAQ==</DQ><InverseQ>QPU3Tmt8nznSgYZ+5jUo9E0SfjiTu435ihANiHqqjasaUNvOHKumqzuBZ8NRtkUhS6dsOEb8A2ODvy7KswUxyA==</InverseQ><D>cgoRoAUpSVfHMdYXW9nA3dfX75dIamZnwPtFHq80ttagbIe4ToYYCcyUz5NElhiNQSESgS5uCgNWqWXt5PnPu4XmCXx6utco1UVH8HGLahzbAnSy6Cj3iUIQ7Gj+9gQ7PkC434HTtHazmxVgIR5l56ZjoQ8yGNCPZnsdYEmhJWk=</D></RSAKeyValue>";


            var testData = Encoding.UTF8.GetBytes(strText);

            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    var base64Encrypted = strText;

                    // server decrypting data with private key                    
                    rsa.FromXmlString(privateKey);

                    var resultBytes = Convert.FromBase64String(base64Encrypted);
                    var decryptedBytes = rsa.Decrypt(resultBytes, true);
                    var decryptedData = Encoding.UTF8.GetString(decryptedBytes);
                    return decryptedData.ToString();
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }

        public static byte[] ByteArrayLeftPad(byte[] input, byte padValue, int len)
        {
            var temp = Enumerable.Repeat(padValue, len).ToArray();
            for (var i = 0; i < input.Length; i++)
                temp[i] = input[i];

            return temp;
        }

        // private static string Decrypt(string stringTodecrypt)
        // {
        //     X509Certificate2 x509 = FindCertBySerialNumber("63a33966d19cebe37369cf29caa9c7d2dc0e8841");

        //     if (x509 == null || string.IsNullOrEmpty(stringTodecrypt))
        //         throw new Exception("A x509 certificate and string for decryption must be provided");

        //     if (!x509.HasPrivateKey)
        //         throw new Exception("x509 certicate does not contain a private key for decryption");

        //     RSACryptoServiceProvider rsa = (RSACryptoServiceProvider)x509.PrivateKey;
        //     byte[] bytestodecrypt = Convert.FromBase64String(stringTodecrypt);
        //     byte[] plainbytes = rsa.Decrypt(bytestodecrypt, false);
        //     System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
        //     return enc.GetString(plainbytes);
        // }

        // private static X509Certificate2 FindCertBySerialNumber(string serialNumber)
        // {            
        //     if (string.IsNullOrWhiteSpace(serialNumber)) throw new ArgumentException("Certificate serial number is required");
        //     var certStore = new X509Store(StoreLocation.LocalMachine);
        //     certStore.Open(OpenFlags.ReadOnly | OpenFlags.IncludeArchived);
        //     foreach (var x in certStore.Certificates)
        //     {
        //         if (string.Equals(x.SerialNumber, serialNumber, StringComparison.CurrentCultureIgnoreCase)) return x;
        //         // if (x.SerialNumber.ToLower().Contains(serialNumber.ToLower())) return x;
        //     }
        //     throw new Exception(string.Format("Certificate with the serial number '{0}' is not installed on the current machine.", serialNumber));
        // }

    }
}
