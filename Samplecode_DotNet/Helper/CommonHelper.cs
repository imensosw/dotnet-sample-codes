using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

namespace Samplecode_DotNet.Helper
{
    public class CommonHelper
    {
        public static readonly string Key = ConfigurationManager.AppSettings["Key"];
        public static readonly Encoding Encoder = Encoding.UTF8;
        //Generate encrypt password
        public static string GeneratePassword()
        {
            string allowedChars = "";
            allowedChars = "a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,x,y,z,";
            allowedChars += "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,";
            allowedChars += "1,2,3,4,5,6,7,8,9,0,!,@,#,$,%,&,?";
            char[] sep = { ',' };
            string[] arr = allowedChars.Split(sep);
            string passwordString = "";
            string temp = "";
            Random rand = new Random();
            for (int i = 0; i < 8; i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                passwordString += temp;
            }
            return passwordString;
        }
        //Encrypt string.
        public static string Encrypt_Password(string password)
        {
            string pswstr = string.Empty;
            byte[] psw_encode = new byte[password.Length];
            psw_encode = Encoding.UTF8.GetBytes(password);
            pswstr = Convert.ToBase64String(psw_encode);
            return pswstr;
        }
        //Encrypt string for use MD5 methology.
        public static string Encrypt_Password_MD5(string password)
        {
            string pswstr = string.Empty;
            var TempPassword = System.Text.Encoding.ASCII.GetBytes(password);
            TempPassword = System.Security.Cryptography.MD5.Create().ComputeHash(TempPassword);
            pswstr = Convert.ToBase64String(TempPassword);
            return pswstr;
        }
        //Decrypt encrypt string for use MD5 methology.
        public static string Decrypt_Password_MD5(string encryptpassword)
        {
            string pswstr = string.Empty;
            var TempPassword = System.Text.Encoding.ASCII.GetBytes(encryptpassword);
            TempPassword = System.Security.Cryptography.MD5.Create().ComputeHash(TempPassword);
            pswstr = Convert.ToBase64String(TempPassword);
            return pswstr;
        }
        //Decrypt encrypt string .
        public static string Decrypt_Password(string encryptpassword)
        {
            string pswstr = string.Empty;
            UTF8Encoding encode_psw = new UTF8Encoding();
            Decoder Decode = encode_psw.GetDecoder();
            byte[] todecode_byte = Convert.FromBase64String(encryptpassword);
            int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
            char[] decoded_char = new char[charCount];
            Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
            pswstr = new String(decoded_char);
            return pswstr;
        }

        

    }
   
}