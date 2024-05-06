using System;
using UnityEngine;

namespace Facade
{


    public interface IStatMenuProvider
    {
        StatsMenu MenuStats { get; }
    }

    public interface IPluginShopProvider
    {
        PluginShop MenuPlugin { get; }
    }

    public interface IGymManagerProvider
    {
        GymManager MenuGym { get; }
    }
public class MainLobbyCanvas : MonoBehaviour, IStatMenuProvider, IPluginShopProvider, IGymManagerProvider
{
        
        [SerializeField] private PluginShop _pluginshop;
        [SerializeField] private ColoMenu _coloMenu;
        [SerializeField] private GymManager _gymManager;
        [SerializeField] private StatsMenu _statsMenu;

        public static MainLobbyCanvas Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void ChoseEnemy(GameObject enemy)
        {
            _coloMenu.ComenzarPelea(enemy);
        }
        public void GymHP()
        {
            _gymManager.TrainHP();
        }
        public void GymWSD()
        {
            _gymManager.TrainWSD();
        }
        public void GymDMG()
        {
            _gymManager.TrainDMG();
        }
        public void BuyPlugin(string att)
        {
            _pluginshop.factoryPlugin(att);
        }

        public void BuyDV()
        {
            _pluginshop.pluginDV();
        }
        public void UpdateStatLevel(string att)
        {
            switch (att)
            {
                case "HP":
                    _statsMenu.TrainHP();
                    break;
                case "WSD":
                    _statsMenu.TrainWSD();
                    break;
                case "DMG":
                    _statsMenu.TrainDMG();
                    break;
            }
           
        }

        public StatsMenu MenuStats { get; }
        public PluginShop MenuPlugin { get; }
        public GymManager MenuGym { get; }
}
}