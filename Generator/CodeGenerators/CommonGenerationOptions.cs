namespace Generator.CodeGenerators
{
    public class CommonGenerationOptions
    {
        public Platform TargetPlatform = Platform.netFramework20;
        public bool ValidationAndReplaceRequired = true;
        public string MarkPrefix = "Mark";
        public string WithMarkPostfix = "WithMark";
        public string IsDeletedColumn = "IsDeleted";
        public string IsDisabledColumn = "IsSiteWideDisabled";
        public string DeletedPhrase = "Deleted";
        public string DisabledPhrase = "Disabled";
        public string NotDeletedPhrase = "NotDeleted";
        public string NotDisabledPhrase = "NotDisabled";
    }
}
