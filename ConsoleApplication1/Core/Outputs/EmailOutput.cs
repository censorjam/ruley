using System;
using System.Dynamic;
using SmartFormat;

namespace Ruley.Core.Outputs
{
    public class EmailOutput : Output
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public override void Do(ExpandoObject x)
        {
            var email = "--------------" + 
                Environment.NewLine + "To: " + To + 
                Environment.NewLine + "Subject: " + 
                Template(Subject, x) + Environment.NewLine + 
                "Body: " + Template(Body, x);

            Logger.Debug(email);
        }
    }
}