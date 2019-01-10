namespace SSC.CustomSolution.CubansConexion.TuneUpResell.WebApplication.Data.Persistence.Configurations
{
    public class AccountEntityTypeConfigurator: SmartSolucionesCuba.SAPRESSC.Core.Persistence.Configurations.AbstractEntityTypeConfigurator<Entities.Account> 
    {
        protected override void ApplayRules(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Entities.Account> entityTypeBuilder)
        {
            base.ApplayRules(entityTypeBuilder);

            entityTypeBuilder.HasMany(entity => entity.Members).WithOne(entity => entity.Account);

            entityTypeBuilder.HasOne(entity => entity.Representative);
        }
    }
}
