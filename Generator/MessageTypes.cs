namespace Generator
{
    public enum MessageTypes
    {
        Error,
        NoDuplicates,
        NoRecordInTable,
        IncompleteServerInfo,
        IncompleteDuplicateInfo,
        IncompleteDeleteDuplicateInfo,
        IncompleteQuery,
        ConnectionTest,
        ConnectionSuccesful,
        ConnectionFailed,
        UnknownMessage,
    }
}
