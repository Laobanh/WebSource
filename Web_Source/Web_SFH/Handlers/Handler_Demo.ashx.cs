using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace HTT
{
    /// <summary>
    /// Summary description for Handler_Demo
    /// </summary>
    public class Handler_Demo : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            /*
            var client = new MongoClient("mongodb+srv://dbhealth:68Gpsvn.vn86@cluster0.wl0zo.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<BsonDocument>("Demo");
            var document = new BsonDocument
            {
                { "student_id", 10002 },
                { "scores", new BsonArray
                    {
                    new BsonDocument{ {"type", "exam"}, {"score", 88.12334193287023 } },
                    new BsonDocument{ {"type", "quiz"}, {"score", 74.92381029342834 } },
                    new BsonDocument{ {"type", "homework"}, {"score", 89.97929384290324 } },
                    new BsonDocument{ {"type", "homework"}, {"score", 82.12931030513218 } }
                    }
                },
                { "class_id", 480}
            };

            // collection.InsertOne(document);


            //var firstDocument = collection.Find(new BsonDocument()).ToList();
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10001);
            var firstDocument = collection.Find(filter).FirstOrDefault();
            String kq = String.Empty;
            kq = firstDocument.ToString();

            context.Response.ContentType = "text/plain";
            context.Response.Write(kq);
            */
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}