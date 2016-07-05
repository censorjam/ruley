using System;
using System.IO;
using Newtonsoft.Json;

namespace Ruley.Core
{
    public class RuleProvider
    {
        private readonly Preprocessor _preprocessor;

        public RuleProvider(Preprocessor preprocessor)
        {
            _preprocessor = preprocessor;
        }

        private string LoadJson(string filename)
        {
            const int retries = 3;

            for (var attempt = 0; attempt < retries; attempt++)
            {
                try
                {
                    return File.ReadAllText(filename);
                }
                catch (Exception e)
                {
                    if (attempt == retries - 1)
                        throw;
                }
            }

            return null;
        }

        public Rule Create(string filename)
        {
            Console.WriteLine("Loading rule ({0})", filename);
            var json = LoadJson(filename);
            json = _preprocessor.Process(json);

            Rule rule = JsonConvert.DeserializeObject<Rule>(json, new JsonSerializerSettings() {TypeNameHandling = TypeNameHandling.Auto});
            rule.FileName = filename;
            rule.Validate();
            return rule;
        }
    }
}