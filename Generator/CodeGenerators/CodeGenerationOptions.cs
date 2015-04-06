namespace Generator.CodeGenerators
{
    public class CodeGenerationOptions : CommonGenerationOptions
    {
        public string InsertPrefix = "Insert";
        public string UpdatePrefix = "Update";
        public string DeletePrefix = "Delete";
        public string SelectPrefix = "Select";
        public string GetPrefix = "Get";
        public string AllGeneratedSpPrefix = "";
        public string DataLayerClassPrefix = "Dat";
        public string DataLayerClassSuffix = "Repository";
        public string DomainLogicLayerPrefix = "Bus";
        public string DomainLogicLayerSuffix = "Service";
        public bool GenerateDeleteByForeignKey = true;
        public bool GenerateDropAllTableSPs;
    }
}
