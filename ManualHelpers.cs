using ff14bot.Enums;
using ff14bot.Managers;
using System.Collections.Generic;
using System.Linq;

namespace ManualPlugin
{
    public static class Helpers
    {
        private static bool IsManualItem(this BagSlot slot)
        {
            return (slot.Item.EquipmentCatagory == ItemUiCategory.Other && (slot.EnglishName.Contains("Engineering") || slot.EnglishName.Contains("Survival")));
        }

        public static IEnumerable<BagSlot> GetManualItems(this IEnumerable<BagSlot> bags)
        {
            return bags
                .Where(s => s.IsManualItem());
        }

        public static bool ContainsManualitem(this IEnumerable<BagSlot> bags, uint id)
        {
            return bags
                .Select(s => s.TrueItemId)
                .Contains(id);
        }

        public static BagSlot GetManualItem(this IEnumerable<BagSlot> bags, uint id)
        {
            return bags.First(s => s.TrueItemId == id);
        }
    }
}
