using NWN.Amia.Main.Managed.Economy.Types;
using NWN.Core;
using NWN.Core.NWNX;

namespace NWN.Amia.Main.Managed.Economy
{
    public class EconomyStorageLoader : IStorageLoader
    {
        private readonly uint _storageObject;
        
        public EconomyStorageLoader(uint storageObject)
        {
            _storageObject = storageObject;
        }
        
        public void Load()
        {
            string entityName = NWScript.GetLocalString(_storageObject, "repository_name");
            
            
        }
    }
}