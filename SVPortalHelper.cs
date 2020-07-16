using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace SVPrayingAssistant_Windows {
    public class SVPortalHelper {
        private const string CORS_API_URL = @"https://cors-anywhere.herokuapp.com/";

        private SVPortalHelper() {}
        
        /// <summary>
        /// デッキコードからデッキを取得
        /// await FetchDeck(code);
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<Deck> FetchDeckFromDeckCodeAsync(string code) {
            try {
                var json = await FetchDeckHashAsync(code);
                var hash = json["data"]["hash"].ToString();
                var deck = await FetchDeckDataAsync(hash);
                return deck;
            } catch (Exception e) {
                MessageBox.Show(e.Message);
                return null;
            }
        }

        /// <summary>
        /// デッキコードからハッシュを取得
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static async Task<JObject> FetchDeckHashAsync(string code) {
            return await RequestOverCORSAsync(@"https://shadowverse-portal.com/api/v1/deck/import?format=json&deck_code=" + code);
        }

        /// <summary>
        /// ハッシュからデッキを取得
        /// </summary>
        /// <param name="hash"></param>
        /// <returns></returns>
        public static async Task<Deck> FetchDeckDataAsync(string hash) {
            var json = await RequestOverCORSAsync(@"https://shadowverse-portal.com/api/v1/deck?format=json&lang=ja&hash=" + hash);
            var cards = json["data"]["deck"]["cards"].Select(card => {
                var cardInfo = new Card(card["card_id"].ToObject<int>(),
                    card["card_name"].ToString(),
                    card["char_type"].ToObject<int>(),
                    card["clan"].ToObject<int>(),
                    card["cost"].ToObject<int>(),
                    card["atk"].ToObject<int>(),
                    card["life"].ToObject<int>(),
                    card["skill"].ToString(),
                    card["tribe_name"].ToString());
                return cardInfo;
            });
            var deck = new Deck(json["data"]["deck"]["clan"].ToObject<int>(), new List<Card>(cards));
            return deck;
        }

        /// <summary>
        /// OverCORSリクエストを送信
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<JObject> RequestOverCORSAsync(string url) {
            url = CORS_API_URL + url;
            JObject json;
            using (var client = new HttpClient()) {
                var req = new HttpRequestMessage(HttpMethod.Get, url);
                req.Headers.Add("ContentType", "application/json");
                req.Headers.Add("x-requested-with", "XMLHttpRequest");
                var res = await client.SendAsync(req);
                var str = await res.Content.ReadAsStringAsync();
                json = JObject.Parse(str);
            }
            return json;
        }
    }
}
