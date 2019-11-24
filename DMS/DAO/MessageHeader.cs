
namespace AA.DAO
{
    public class MessageHeader
    {
        public string To { get; set; }
        public string From { get; set; }
        public string Subject { get; set; }

        // DocumentType - Inward/Outward/Student/Principal
        public string DocumentType { get; set; }
        public string InwardNumber { get; set; }

        public string InwardDate { get; set; }

        public string OutwardNumber { get; set; }

        public string OutwardDate { get; set; }

        public string IsForwaded { get; set; }
        public string IsReceived { get; set; }

        public string ForwardDate { get; set; }
        public string ReceivedDate { get; set; }
    }
}
