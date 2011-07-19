using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace Twitter_Watch
{
    class Program
    {
        static void Main(string[] args)
        {

            string auth_user_id = "";
            string auth_password = ";";

            string watch_user_ids = "77219798,86061782,16069066";

            var _Request = (HttpWebRequest)WebRequest.Create("http://stream.twitter.com/1/statuses/filter.json?follow=" + watch_user_ids);
            _Request.Credentials = new NetworkCredential(auth_user_id,auth_password);

            while (true)
            {
                var _text = new StreamReader(_Request.GetResponse().GetResponseStream()).ReadLine();
                if (_text != null)
                {
                    if (_text.Length > 0)
                    {
                        var _tweetdata = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(_text);
                        if (_tweetdata.ContainsKey("user") && _tweetdata.ContainsKey("text"))
                        {
                            var _userdata = _tweetdata["user"] as Dictionary<string, object>;

                            Console.WriteLine(" * Tweet Date : " + _tweetdata["created_at"]);
                            Console.WriteLine(" * Name : " + _userdata["name"] + " / ID: @" + _userdata["screen_name"]);
                            Console.WriteLine(_tweetdata["text"]);
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
