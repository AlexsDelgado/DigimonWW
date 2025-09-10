using AbstractFactory;
using UnityEngine;

namespace AbstractFactory
{
    public abstract class AbstractFactory<T> where T : IProduct
    {
        public abstract T CreateProduct(string stat);
    }

    public interface IProduct
    {
    }
    public class PluginFactory : AbstractFactory<Plugin>
    {
        public override Plugin CreateProduct(string stat)
        {
            return GetPlugin(stat);
        }

        public Plugin GetPlugin(string stat)
        {
            int ammount = GameManager.Instance.costPlugin;
            PluginTier aux;
            if (ammount < 10)
            {
                aux = PluginTier.Basic;
            }else if (ammount<20)
            {
                aux = PluginTier.Intermediate;
            }else if (ammount<40)
            {
                aux = PluginTier.Advanced;
            }
            else
            {
                aux = PluginTier.Ultimate;
            }
            
            switch (stat)
            {
                case "HP":
                    return new Plugin(aux, "HP", ammount);
                case "DMG":
                    return new Plugin(aux, "DMG", ammount);
                case "WSD":
                    return new Plugin(aux, "WSD", ammount);
            }

            return default;
        }

        
        
    }

    
}
    
    public class Plugin : IProduct
    {
        public PluginTier tier { get; }
        public string stat{ get; }
        public int ammount{ get; }
        
        public Plugin(PluginTier tier, string stat, int ammount)
        {
            this.tier = tier;
            this.stat = stat;
            this.ammount = ammount;
        }
    }
    public enum PluginTier
    {
        Basic,
        Intermediate,
        Advanced,
        Ultimate
    }
    
    
    public class Shop
    {
        public void Shopping()
        {
            var boughtPlugin = new PluginFactory();
            Plugin producedPlugin = boughtPlugin.GetPlugin("HP");
            // DEBUG_REMOVED: Debug.Log($"Se obtuvo plugin {producedPlugin.tier.ToString()}, de {producedPlugin.stat}, que aumenta un {producedPlugin.ammount} ");

        }
    }

