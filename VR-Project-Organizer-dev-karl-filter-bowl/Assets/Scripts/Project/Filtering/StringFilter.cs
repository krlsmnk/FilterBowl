using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CAVS.ProjectOrganizer.Project.Filtering
{

    public class StringFilter : Filter
    {

        public enum Operator
        {
            Equal,
            NotEqual
        }

        private string fieldName;

        private string stringToFilterOn;

        private Operator op;

        public StringFilter(string fieldName, Operator op, string stringToFilterOn)
        {
            this.fieldName = fieldName;
            this.stringToFilterOn = stringToFilterOn;
            this.op = op;
        }

        public override bool FilterItem(Item item)
        {
            string val = item.GetValue(this.fieldName);
            switch (op)
            {
				case Operator.Equal:
					return val == stringToFilterOn;
				case Operator.NotEqual:
					return val != stringToFilterOn;
				default:
					throw new System.Exception("Not Implemented");
            }
        }

    }

}