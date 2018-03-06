using System.Collections;
using System.Collections.Generic;

namespace CAVS.ProjectOrganizer.Project.Filtering
{
    public class YearFilter : Filter
    {
        protected string fieldToFilterOn;
        float value;

        public YearFilter(string fieldToFilterOn, float value)
        {
            this.fieldToFilterOn = fieldToFilterOn;
            this.value = value;
        }
        public override float FilterNewItem(Item item)
        {
            string val = item.GetValue(this.fieldToFilterOn);
            if (val == null)
            {
                return 0;
            }
            float result;
            float.TryParse(val, out result);
            return categorySelect(result);
        }

        private float categorySelect(float num)
        {
            float dif = value - num;

            if (dif < 7) return 1;
            else if (dif >= 7 & dif <= 14) return 2;
            else if (dif >= 15) return 3;
            else return 0;

        }
        public override bool FilterItem(Item item)
        {
            return false;
        }
    }
}
