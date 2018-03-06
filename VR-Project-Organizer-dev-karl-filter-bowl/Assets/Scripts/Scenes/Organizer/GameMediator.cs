using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CAVS.ProjectOrganizer.Project;


namespace CAVS.ProjectOrganizer.Scenes.Organizer
{

    /// <summary>
    /// Meant for keeping up with everything inside of the scene currently and
    /// providing a common interface for them all to talk with.
    /// 
    /// Design Pattern:
    /// https://sourcemaking.com/design_patterns/mediator
    /// </summary>
    public class GameMediator : MonoBehaviour
    {

        /// <summary>
        /// The material that will be applied to all line renders
        /// </summary>
        [SerializeField]
        private Material lineRendererMaterial;

        /// <summary>
        /// File path/name we used to load this scene.
        /// </summary>
        private string sceneLoaded = "test.vpo";

        /// <summary>
        /// All items currentely being interacted with inside of the scene.
        /// </summary>
        List<ItemBehaviour> items;

        Dictionary<LineRenderer, ItemBehaviour[]> linesToItems;

        // Use this for initialization
        void Start()
        {
            linesToItems = new Dictionary<LineRenderer, ItemBehaviour[]>();
            buildProjectFromCSV("CarData.csv");

            //BuildSceneFromProject(Project.Space.LoadFromFile(sceneLoaded));
        }

        void buildProjectFromCSV(string path)
        {
            Item[] items = ProjectFactory.buildItemsFromCSV(path);
			int i = 0;
            foreach (Item item in items)
            {
				ItemBehaviour itemBehavior = item.Build(new Vector3(Mathf.Sin(i)*10, i/5, Mathf.Cos(i)*10), Vector3.zero);
				itemBehavior.transform.LookAt(Vector3.zero);
				i++;
            }
        }

        /// <summary>
        /// Called when the scene is unloaded (changing scenes, quiting applications, etc)
        /// </summary>
        void OnDestroy()
        {
            Save(sceneLoaded);
        }

        /// <summary>
        /// Loads in all items and builds lines between them
        /// </summary>
        /// <param name="project">Project.</param>
        private void BuildSceneFromProject(Project.Space project)
        {
            items = new List<ItemBehaviour>();
            Dictionary<Item, Vector3> positions = project.GetItemPositions();

            // Build items and lines appropriatly
            foreach (Item item in project.GetItems())
            {
                ItemBehaviour itemBehavior = item.Build(positions[item], Vector3.zero);
                items.Add(itemBehavior);

                // Create line to rest of items.
                foreach (ItemBehaviour lineTo in items)
                {
                    linesToItems.Add(
                        BuildLineBetweenNodes(itemBehavior, lineTo),
                        new ItemBehaviour[] { itemBehavior, lineTo }
                    );
                }
            }

        }

        private LineRenderer BuildLineBetweenNodes(ItemBehaviour node, ItemBehaviour otherNode)
        {
            GameObject lineObject = new GameObject("line");
            LineRenderer line = lineObject.AddComponent<LineRenderer>();

            line.positionCount = 10;
            line.SetPositions(BuildVerticesFromItems(node, otherNode, 10));

            line.material = lineRendererMaterial;

            // line.widthCurve = new AnimationCurve (new Keyframe[]{
            // 	new Keyframe(0, 1),
            // 	new Keyframe(0.25f, .5f),
            // 	new Keyframe(0.5f, 0),
            // 	new Keyframe(0.75f, .5f),
            // 	new Keyframe(1, 1)
            // });

            line.startWidth = 0.1f;
            line.endWidth = 0.1f;

            return line;
        }

        private void UpdateItemPositions()
        {
            if (linesToItems.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<LineRenderer, ItemBehaviour[]> entry in linesToItems)
            {
                entry.Key.SetPositions(BuildVerticesFromItems(entry.Value[0], entry.Value[1], 10));
                float dist = Vector3.Distance(entry.Value[0].transform.position, entry.Value[1].transform.position);
                float pos = Mathf.Clamp(.25f * dist, 0, .25f);
                entry.Key.widthCurve = new AnimationCurve(new Keyframe[]{
                    new Keyframe(0, .5f),
                    new Keyframe(0.25f-pos, .15f),
                    new Keyframe(0.5f, 0),
                    new Keyframe(0.75f+pos, .15f),
                    new Keyframe(1, .5f)
                });
            }
        }

        private Vector3[] BuildVerticesFromItems(ItemBehaviour item, ItemBehaviour other, int vertCount)
        {
            Vector3[] verts = new Vector3[vertCount];
            Vector3 dir = other.transform.position - item.transform.position;
            for (int i = 0; i < vertCount - 1; i++)
            {
                verts[i] = item.transform.position + (dir * (1f / (float)vertCount) * i);
            }
            verts[vertCount - 1] = other.transform.position;
            return verts;
        }

        void Update()
        {
            UpdateItemPositions();
        }

        private void Save(string name)
        {
            Debug.Log(string.Format("saving: {0}", name));
        }

    }

}