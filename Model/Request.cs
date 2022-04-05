namespace EncryptDecryptTestConsole.Model
{
    public sealed class Request
    {
        #region Mandatory
        public string XPRIORITY { get; set; }
        public string tranRefNo { get; set; }
        public int amount { get; set; }
        public int senderAccNo { get; set; }
        public int beneAccNo { get; set; }
        public string beneName { get; set; }
        public string beneIFSC { get; set; }
        public string narration1 { get; set; }
        public string crpId { get; set; }
        public string crpUsr { get; set; }
        public string AggrId { get; set; }
        public string AggrName { get; set; }
        public string urn {get;set;}
        public string txnType { get;set;}

        #endregion  Mandatory

        #region  Optional
        public string narration2 { get; set; }
        #endregion  Optional

        #region  Computational
        public string WorkFlow_Reqd { get; set; } // Conditional for nodal account user with PWT flow
        #endregion  Optional

    }

}