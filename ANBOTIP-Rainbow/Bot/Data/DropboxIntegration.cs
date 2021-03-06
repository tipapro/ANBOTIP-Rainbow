﻿using Dropbox.Api;
using Dropbox.Api.Files;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ANBOTIP_Rainbow.Bot.Data
{
    class DropboxIntegration
    {
        private static DropboxClient DBClient;
        public static bool IsAuthorized;

        public static void Authorize(string apiKey)
        {
            DBClient = new DropboxClient(apiKey);
            IsAuthorized = true;
        }

        public static async Task<bool> UploadAsync(string UploadfileName, string str)
        {
            try
            {
                using (var stream = GenerateStreamFromString(str))
                {
                    await DBClient.Files.UploadAsync("/" + UploadfileName, WriteMode.Overwrite.Instance, body: stream);
                }
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("UploadAsync Error: " + UploadfileName);
                return false;
            }
        }

        public static async Task<string> DownloadAsync(string DropboxFileName)
        {
            try
            {
                var response = await DBClient.Files.DownloadAsync("/" + DropboxFileName);
                string result = await response.GetContentAsStringAsync();
                Console.WriteLine("DownloadAsync Success: " + DropboxFileName);
                return result;
            }
            catch (Exception)
            {
                Console.WriteLine("DownloadAsync Error: " + DropboxFileName);
                return "";
            }

        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}