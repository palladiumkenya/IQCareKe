using Entities.CCC.Interoperability;

namespace Interface.CCC.Interoperability
{
    public interface IViralLoadMessageManager
    {
        ViralLoadMessage GetViralLoadMessageByEntityId(int entityId);
    }
}