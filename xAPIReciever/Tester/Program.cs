using System;
using Newtonsoft.Json;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            String XAPIbody = "{\"id\":\"f3f9612e - 0e7e - 4d1d - 8047 - c4dcb2f01bb1\",\"actor\":{\"name\":\"Danny Devito\",\"mbox\":\"mailto: danny@example.org\"},\"verb\":{\"id\":\"http://adlnet.gov/expapi/verbs/completed\"},\"object\":{\"id\":\"https://example.org/block/1047831219\",\"definition\":{\"type\":\"https://w3id.org/xapi/cmi5/activities/block\"}},\"context\":{\"contextActivities\":{\"category\":[{\"id\":\"https://w3id.org/xapi/cmi5/context/categories/moveon\"}]},\"registration\":\"4a971479-af76-424c-a523-89a04476e7fd\"},\"timestamp\":\"2019-11-18T11:39:42.621Z\",\"result\":{\"completion\":true,\"duration\":\"PT13H33M26S\"}}";
            dynamic msg = JsonConvert.DeserializeObject<dynamic>(XAPIbody);
            Console.WriteLine(msg.GetType());
            Console.WriteLine(msg.id);
            Console.WriteLine(msg.actor.name);
            Console.WriteLine(msg.actor.mbox);
            Console.WriteLine(msg.verb.id);
            Console.WriteLine(msg["object"].id);
            Console.WriteLine(msg.context.contextActivities.category[0].id);
            Console.WriteLine(msg.timestamp);
        }
    }
}
