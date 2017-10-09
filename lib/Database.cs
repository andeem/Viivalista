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
            bool isUrl = Uri.TryCreate("postgres://dmdojibxijzelm:943909b89c15c8358d310f00b3efa6a0974745be56d87e705cb1f606626a8a16@ec2-54-228-235-198.eu-west-1.compute.amazonaws.com:5432/dm51rqc6hcunp", UriKind.Absolute, out url);
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
