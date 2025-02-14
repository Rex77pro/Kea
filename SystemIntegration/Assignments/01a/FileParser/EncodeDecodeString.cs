using System;
using System.Text;

static class EncodeDecodeString {
    private static string text = "Hello World";

    public static void EncodeDecode(){
        byte[] bytes = Encoding.UTF8.GetBytes(text);
        string encodedString = Convert.ToBase64String(bytes);
        Console.WriteLine(encodedString);

        string decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(encodedString));
        Console.WriteLine(decodedString);
    }
}