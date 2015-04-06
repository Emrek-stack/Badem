using System;
using System.Reflection;
using Microsoft.Win32;

namespace Generator.CodeGenerators.InfrastructureGenerator
{
    public class RegistryAccess
    {
        private RegistryKey _Software;
        private RegistryKey _Company;
        private RegistryKey _Product;

        public RegistryAccess()
            : this(((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyCompanyAttribute), true)).Company, ((AssemblyProductAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(AssemblyProductAttribute), true)).Product)
        {
        }

        public RegistryAccess(string CompanyName, string ProductName)
        {
            try
            {
                this._Software = Registry.LocalMachine.OpenSubKey("Software");
                this._Company = this._Software.OpenSubKey(CompanyName);
                if (this._Company == null)
                    this._Company = Registry.LocalMachine.CreateSubKey("Software\\" + CompanyName);
                this._Product = this._Company.OpenSubKey(ProductName, true);
                if (this._Product != null)
                    return;
                this._Product = Registry.LocalMachine.CreateSubKey(string.Format("Software\\{0}\\{1}", (object)CompanyName, (object)ProductName));
            }
            catch (Exception ex)
            {
            }
        }

        public void WriteRegistryValue(string ValueName, string Value)
        {
            try
            {
                this._Product.SetValue(ValueName, (object)Value);
            }
            catch (Exception ex)
            {
            }
        }

        public string ReadRegistryValue(string ValueName)
        {
            try
            {
                return (string)this._Product.GetValue(ValueName);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
