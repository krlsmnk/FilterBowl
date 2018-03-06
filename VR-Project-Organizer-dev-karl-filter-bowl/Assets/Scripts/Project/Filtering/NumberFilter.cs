using System.Collections;
using System.Collections.Generic;

namespace CAVS.ProjectOrganizer.Project.Filtering
{

    public class NumberFilter : Filter
    {

        public enum Operator
        {
            /// <summary>
            /// Return all items less than a certain value
            /// </summary>
            LessThan,

            /// <summary>
            /// Return all items greater than a certain value
            /// </summary>
            GreaterThan,

            /// <summary>
            /// Return all items equal to a certain value
            /// </summary>
            EqualTo
        }

        /// <summary>
        /// How we're going to perform the filter
        /// </summary>
        Operator op;

        float value;

        /// <summary>
        /// The field the item might contain that we want to determine whether or not it get's pruned 
        /// </summary>
        protected string fieldToFilterOn;


        public NumberFilter(string fieldToFilterOn, Operator op, float value)
        {
            this.fieldToFilterOn = fieldToFilterOn;
            this.op = op;
            this.value = value;
        }

        public override bool FilterItem(Item item)
        {
            string val = item.GetValue(this.fieldToFilterOn);
            if (val == null)
            {
                return false;
            }
            float result;
            float.TryParse(val, out result);
            return passes(result);
        }

        private bool passes(float num)
        {
            switch (op)
            {
                case Operator.GreaterThan:
                    return num > value;

                case Operator.LessThan:
                    return num < value;

                case Operator.EqualTo:
                    return num == value;
            }
            return false;
        }

        // public override string ToString()
        // {
        //     string operatorSymbol = "";
        //     switch (this.op)
        //     {
        //         case Operator.EqualTo:
        //             operatorSymbol = "";
        //     }
        //     return string.Format("Filtering {0} by numbers {1} {2}", fieldToFilterOn);
        // }

    }

}