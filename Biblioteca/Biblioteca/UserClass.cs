using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca
{
    class UserClass
    {
        public int id_user { get; set; }

        public string username { get; set; }

        public string password { get; set; }

        public string confpass { get; set; }

        public int role { get; set; }

        public static string Encode(string original)
        {
            byte[] encodedBytes;

            using (var md5 = new MD5CryptoServiceProvider())
            {
                var originalBytes = Encoding.Default.GetBytes(original);
                encodedBytes = md5.ComputeHash(originalBytes);
            }

            return Convert.ToBase64String(encodedBytes);
        }

        public static void InregistrareUser(UserClass user)
        {

            string encryptPass = Encode(user.password);

            SqlConnection con = new SqlConnection(ConnectioString.LocalConnectionString);

            SqlCommand com = con.CreateCommand();

            com.CommandText = @"INSERT 
                                    INTO tUser
                                    ( username,
                                      password,
                                      role  
                                    ) 
                                    VALUES(
                                      @username,
                                      @password,
                                      @role  
                                    )";
            com.Parameters.AddWithValue("username", user.username);
            com.Parameters.AddWithValue("password", encryptPass);
            com.Parameters.AddWithValue("role", 2);

            try
            {
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                throw;
            }


        }


        public static UserClass Login(UserClass logUser)
        {

            UserClass user = new UserClass();

            SqlConnection con = new SqlConnection(ConnectioString.LocalConnectionString);

            SqlCommand com = con.CreateCommand();
            com.CommandText = @"SELECT * FROM tUser where username=@username ";
            com.Parameters.AddWithValue("username", logUser.username);
            try
            {
                con.Open();
                SqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        user.id_user = int.Parse(reader["id_user"].ToString());
                        user.username = reader["username"].ToString();
                        user.password = reader["password"].ToString();
                        user.confpass = reader["password"].ToString();
                        user.role = int.Parse(reader["role"].ToString());
                    }
                }
                else { return user = null; }

                reader.Close();
                con.Close();

                string pagePass = Encode(logUser.password);
                if (user.password == pagePass)
                {
                    return user;
                }

            }
            catch (Exception ex)
            {
                return user = null;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
            return user;





        }

    }
}