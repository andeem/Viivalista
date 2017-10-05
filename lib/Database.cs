using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viivalista.lib
{
    public class Database
    {
        public static NpgsqlConnection connection()
        {
            Uri url;
            bool isUrl = Uri.TryCreate(Environment.GetEnvironmentVariable("DATABASE_URL"), UriKind.Absolute, out url);
            string connectionUrl = null;
            if (isUrl)
            {
                connectionUrl = $"host={url.Host};username={url.UserInfo.Split(':')[0]};password={url.UserInfo.Split(':')[1]};database={url.LocalPath.Substring(1)};pooling=true;sslmode=Require;trustservercertificate=True";
            }
            
            try
            {
                return new NpgsqlConnection(connectionUrl);

            }
            catch (Exception e){
                return null;
            }

         
        }
    }


}
