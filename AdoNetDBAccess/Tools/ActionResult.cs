using AdoNetDBAccess.Enumerators;

namespace AdoNetDBAccess.Tools
{
    public class ActionResult<T>
    {
        public double linesAffected { get; set; }
        public double linesResulted { get; set; }
        public string msgResulted { get; set; }
        public enumResult actionStatus { get; set; }
    }

    public class ActionResul
    {
        public double linesAffected { get; set; }
        public double linesResulted { get; set; }
        public string msgResulted { get; set; }
        public enumResult actionStatus { get; set; }
    }
}