using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LesMagouilleurs
{
    class BoardTile
    {
        public enum TileType{ Magouille, Malchance, Paye, Services }
        public enum EffectType { Magouille1, Magouille2, Malchance1, Malchance2, Paye, Service}

        public static string GetEffectTypeMessage(EffectType effectType)
        {
            switch (effectType)
            {
                case EffectType.Magouille1: return "Magouille : -1 PV aux autres joueurs.";
                case EffectType.Magouille2: return "Magouille : -20 $ aux autres joueurs.";
                case EffectType.Malchance1: return "Malchance : -2 PV.";
                case EffectType.Malchance2: return "Malchance : -40 $.";
                case EffectType.Paye: return "Paye : +40 $.";
                case EffectType.Service: return "Service : -20 $ et +1 PV.";
                default: return "";
            }
        }

        public static void ApplyEffect(Player currentPlayer, List<Player> otherPlayerList, EffectType effectType)
        {
            switch (effectType)
            {
                case EffectType.Magouille1:
                    foreach (Player p in otherPlayerList)
                        p.Hp -= 1;
                    break;
                case EffectType.Magouille2:
                    foreach (Player p in otherPlayerList)
                        p.Money -= 20;
                    break;
                case EffectType.Malchance1:
                    currentPlayer.Hp -= 2;
                    break;
                case EffectType.Malchance2:
                    currentPlayer.Money -= 40;
                    break;
                case EffectType.Paye:
                    currentPlayer.Money += 40;
                    break;
                case EffectType.Service:
                    currentPlayer.Money -= 20;
                    currentPlayer.Hp += 1;
                    break;
                default:
                    break;
            }
        }

        public static EffectType GetEffectType(int i)
        {
            return GetRandomEffectType(GetTileType(i));
        }

        private static EffectType GetRandomEffectType(TileType tileType) {

            Random random = new Random();
            int randomNumber = 1;

            switch (tileType)
            {
                case TileType.Magouille:
                    randomNumber = random.Next(1,3);
                    if (randomNumber == 1)
                    {
                        return EffectType.Magouille1;
                    }
                    else
                    {
                        return EffectType.Magouille2;
                    }
                case TileType.Malchance:
                    randomNumber = random.Next(1, 5);
                    if (randomNumber == 1)
                    {
                        return EffectType.Malchance1;
                    }
                    else
                    {
                        return EffectType.Malchance2;
                    }
                case TileType.Paye: return EffectType.Paye;
                case TileType.Services: return EffectType.Service;
                default: return EffectType.Magouille1;
            }
        }

        public static TileType GetTileType(int i)
        {
            if(i > 12) i = 12;
            else if (i < 1) i = 1;
            switch (i)
            {
                case 1: return TileType.Paye;
                case 2: return TileType.Magouille;
                case 3: return TileType.Malchance;
                case 4: return TileType.Services;
                case 5: return TileType.Magouille;
                case 6: return TileType.Malchance;
                case 7: return TileType.Services;
                case 8: return TileType.Magouille;
                case 9: return TileType.Malchance;
                case 10: return TileType.Services;
                case 11: return TileType.Magouille;
                case 12: return TileType.Malchance;
                default: return TileType.Magouille;
            }
        }
    }
}
