namespace Generator
{
    public class MessageTextProviderEnglish : IMessageTextProvider
    {
        public virtual string GetMessage(MessageTypes messageType)
        {
            string str;
            switch (messageType)
            {
                case MessageTypes.Error:
                    str = "Error";
                    break;
                case MessageTypes.NoDuplicates:
                    str = "There are no duplicate records with the provided criteria.";
                    break;
                case MessageTypes.NoRecordInTable:
                    str = "There are no records in table.";
                    break;
                case MessageTypes.IncompleteServerInfo:
                    str = "Server, User Name and Password fields must be filled.";
                    break;
                case MessageTypes.IncompleteDuplicateInfo:
                    str = "Table name, duplicate column name, primary key values must be provided.";
                    break;
                case MessageTypes.IncompleteDeleteDuplicateInfo:
                    str = "Before deleting duplicates you must first list them. Use the 'List Duplicates' button.";
                    break;
                case MessageTypes.IncompleteQuery:
                    str = "There is no query context.";
                    break;
                case MessageTypes.ConnectionTest:
                    str = "Connection Test";
                    break;
                case MessageTypes.ConnectionSuccesful:
                    str = "Connection Sucessful!";
                    break;
                case MessageTypes.ConnectionFailed:
                    str = "Connection Failed!";
                    break;
                default:
                    str = "Bilinmeyen Mesaj";
                    break;
            }
            return str;
        }
    }
}
