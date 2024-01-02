namespace SendMailApp.Helper
{
    public class Mailrequest
    {
        public string ToEmail { get; set; }

        public string Subject { get; set; }

        public string NombrePlantilla { get; set; }

        public byte[] Adjunto {  get; set; }
    }
}
