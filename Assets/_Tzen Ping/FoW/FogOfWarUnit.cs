using UnityEngine;
using System.Collections.Generic;

namespace FoW
{
    public enum FogOfWarShapeType
    {
        Circle
    }

    [AddComponentMenu("FogOfWar/FogOfWarUnit")]
    public class FogOfWarUnit : MonoBehaviour
    {
        public int team = 0;

        [Header("Shape")]
        public FogOfWarShapeType shapeType = FogOfWarShapeType.Circle;
        public Vector2 offset = Vector2.zero;

        public float radius = 5.0f;
        [Range(0.0f, 1.0f)]
        public float innerRadius = 1;
        [Range(0.0f, 180.0f)]
        public float angle = 180;

        public Texture2D texture;
        public bool rotateToForward = false;

        [Header("Line of Sight")]
        public LayerMask lineOfSightMask = 0;
        public float lineOfSightPenetration = 0;
        public bool cellBased = false;
        public bool antiFlicker = false;

        float[] _distances = null;
        bool[] _visibleCells = null;

        Transform _transform;

        static List<FogOfWarUnit> _registeredUnits = new List<FogOfWarUnit>();
        public static List<FogOfWarUnit> registeredUnits { get { return _registeredUnits; } }

        void Awake()
        {
            _transform = transform;
        }

        void OnEnable()
        {
            registeredUnits.Add(this);
        }

        void OnDisable()
        {
            registeredUnits.Remove(this);
        }

        static bool CalculateLineOfSight2D(Vector2 eye, float radius, float penetration, LayerMask layermask, float[] distances)
        {
            bool hashit = false;
            float angle = 360.0f / distances.Length;
            RaycastHit2D hit;

            for (int i = 0; i < distances.Length; ++i)
            {
                Vector2 dir = Quaternion.AngleAxis(angle * i, Vector3.back) * Vector2.up;
                hit = Physics2D.Raycast(eye, dir, radius, layermask);
                if (hit.collider != null)
                {
                    distances[i] = (hit.distance + penetration) / radius;
                    if (distances[i] < 1)
                        hashit = true;
                    else
                        distances[i] = 1;
                }
                else
                    distances[i] = 1;
            }

            return hashit;
        }

        static bool CalculateLineOfSight3D(Vector3 eye, float radius, float penetration, LayerMask layermask, float[] distances, Vector3 up, Vector3 forward)
        {
            bool hashit = false;
            float angle = 360.0f / distances.Length;
            RaycastHit hit;

            for (int i = 0; i < distances.Length; ++i)
            {
                Vector3 dir = Quaternion.AngleAxis(angle * i, up) * forward;
                if (Physics.Raycast(eye, dir, out hit, radius, layermask))
                {
                    distances[i] = (hit.distance + penetration) / radius;
                    if (distances[i] < 1)
                        hashit = true;
                    else
                        distances[i] = 1;
                }
                else
                    distances[i] = 1;
            }

            return hashit;
        }

        public float[] CalculateLineOfSight(FogOfWarPhysics physicsmode, Vector3 eyepos, FogOfWarPlane plane)
        {
            if (lineOfSightMask == 0)
                return null;

            if (_distances == null)
                _distances = new float[256];

            if (physicsmode == FogOfWarPhysics.Physics2D)
            {
                if (CalculateLineOfSight2D(eyepos, radius, lineOfSightPenetration, lineOfSightMask, _distances))
                    return _distances;
            }
            return null;
        }

        static float Sign(float v)
        {
            if (Mathf.Approximately(v, 0))
                return 0;
            return v > 0 ? 1 : -1;
        }
        
        public bool[] CalculateLineOfSightCells(FogOfWar fow, FogOfWarPhysics physicsmode, Vector3 eyepos)
        {
            int rad = Mathf.RoundToInt(radius);
            int width = rad + rad + 1;
            if (_visibleCells == null || _visibleCells.Length != width * width)
                _visibleCells = new bool[width * width];

            Vector2 cellsize = (fow.mapResolution.vector2 * 1.1f) / fow.mapSize; 
            Vector2 playerpos = FogOfWarConversion.SnapWorldPositionToNearestFogPixel(fow, _transform.position, fow.mapOffset, fow.mapResolution, fow.mapSize);
            for (int y = -rad; y <= rad; ++y)
            {
                for (int x = -rad; x <= rad; ++x)
                {
                    Vector2i offset = new Vector2i(x, y);
                    Vector2 fogoffset = offset.vector2 - new Vector2(Sign(offset.x) * cellsize.x, Sign(offset.y) * cellsize.y) * 0.5f;
                    Vector2 worldoffset = FogOfWarConversion.FogToWorldSize(fogoffset, fow.mapResolution, fow.mapSize);
                    Vector2 worldpos = playerpos + worldoffset;

                    Debug.DrawLine(playerpos, worldpos);

                    int idx = (y + rad) * width + x + rad;

                    if (worldoffset.magnitude > radius)
                        _visibleCells[idx] = false;
                    else
                    {
                        _visibleCells[idx] = true;
                        RaycastHit2D hit = Physics2D.Raycast(playerpos, worldoffset.normalized, Mathf.Max(worldoffset.magnitude - lineOfSightPenetration, 0.00001f), lineOfSightMask);
                        _visibleCells[idx] = hit.collider == null;
                    }
                }
            }

            return _visibleCells;
        }

        void FillShape(FogOfWar fow, FogOfWarShape shape)
        {
            if (antiFlicker)
            {

            }
            else
            shape.eyePosition = _transform.position;
            shape.offset = offset;
            shape.radius = radius;
        }

        FogOfWarShape CreateShape(FogOfWar fow)
        {
            if (shapeType == FogOfWarShapeType.Circle)
            {
                FogOfWarShapeCircle shape = new FogOfWarShapeCircle();
                FillShape(fow, shape);
                shape.innerRadius = innerRadius;
                shape.angle = angle;
                return shape;
            }
          
            return null;
        }
       
        public FogOfWarShape GetShape(FogOfWar fow, FogOfWarPhysics physics, FogOfWarPlane plane)
        {
            FogOfWarShape shape = CreateShape(fow);
            if (shape == null)
                return null;

            if (cellBased)
            {
                shape.lineOfSight = null;
                shape.visibleCells = CalculateLineOfSightCells(fow, physics, shape.eyePosition);
            }
            else
            {
                shape.lineOfSight = CalculateLineOfSight(physics, shape.eyePosition, plane);
                shape.visibleCells = null;
            }
            return shape;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            if (shapeType == FogOfWarShapeType.Circle)
                Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}