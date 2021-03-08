using NWN.Core;

namespace NWN.Amia.Main.Managed.Objects.Types
{
    public class GameObject
    {
        public uint ObjectId { get; set; }
        public string ResRef { get; set; }
        public string Tag { get; set; }

        protected GameObject(uint objectId)
        {
            ObjectId = objectId;
            NWScript.GetResRef(objectId);
            NWScript.GetTag(objectId);
        }
    }
}