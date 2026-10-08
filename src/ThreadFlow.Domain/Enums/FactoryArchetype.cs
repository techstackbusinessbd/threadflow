namespace ThreadFlow.Domain.Enums;

/// <summary>
/// Garment manufacturing factory operational archetypes
/// Customizes workflows, processes, and UI layout based on factory specialization
/// </summary>
public enum FactoryArchetype
{
    KnitDedicated = 1,
    WovenDedicated = 2,
    DenimDedicated = 3,
    SweaterDedicated = 4,
    WashDedicated = 5,
    EmbellishmentDedicated = 6,
    CompositeMulti = 7
}
