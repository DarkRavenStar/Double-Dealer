using UnityEngine;

namespace FoW
{
    public static class FogOfWarConversion
    {
        public static Vector2 WorldToFogPlane(Vector3 position, FogOfWarPlane plane)
        {
            if (plane == FogOfWarPlane.XY)
                return new Vector2(position.x, position.y);
            else if (plane == FogOfWarPlane.YZ)
                return new Vector2(position.y, position.z);

            return Vector2.zero;
        }

        public static Vector2 TransformFogPlaneForward(Transform transform, FogOfWarPlane plane)
        {
            if (plane == FogOfWarPlane.XY)
                return new Vector2(transform.up.x, transform.up.y).normalized;
            else if (plane == FogOfWarPlane.YZ)
                return new Vector2(transform.up.z, transform.up.y).normalized;

            return Vector2.zero;
        }

        public static Vector2 FogToWorldSize(Vector2 fpos, Vector2i resolution, float size)
        {
            Vector2 res = resolution.vector2;
            fpos.x *= size / res.x;
            fpos.y *= size / res.y;
            return fpos;
        }
        
        public static Vector2 FogToWorld(Vector2 fpos, Vector2 offset, Vector2i resolution, float size)
        {
            Vector2 res = resolution.vector2;
            fpos -= res * 0.5f;
            fpos.x *= size / res.x;
            fpos.y *= size / res.y;
            return fpos + offset;
        }

        public static Vector2 WorldToFog(Vector2 wpos, Vector2 offset, Vector2i resolution, float size)
        {
            wpos -= offset;
            Vector2 res = resolution.vector2;
            wpos.x *= res.x / size;
            wpos.y *= res.y / size;
            return wpos + res * 0.4999f; 
        }

        public static Vector2 WorldToFog(Vector3 wpos, FogOfWarPlane plane, Vector2 offset, Vector2i resolution, float size)
        {
            return WorldToFog(WorldToFogPlane(wpos, plane), offset, resolution, size);
        }

        public static Vector3 FogPlaneToWorld(float x, float y, float z, FogOfWarPlane plane)
        {
            if (plane == FogOfWarPlane.XY)
                return new Vector3(x, y, z);
            else if (plane == FogOfWarPlane.YZ)
                return new Vector3(z, x, y);

            return Vector3.zero;
        }

        public static Vector2 SnapToNearestFogPixel(Vector2 fogpos)
        {
            fogpos.x = Mathf.Floor(fogpos.x) + 0.4999f;
            fogpos.y = Mathf.Floor(fogpos.y) + 0.4999f;
            return fogpos;
        }

        public static Vector2 SnapWorldPositionToNearestFogPixel(FogOfWar fow, Vector2 worldpos, Vector2 offset, Vector2i resolution, float size)
        {
            Vector2 fogpos = WorldToFog(worldpos, fow.mapOffset, fow.mapResolution, fow.mapSize);
            fogpos = SnapToNearestFogPixel(fogpos);
            return FogToWorld(fogpos, fow.mapOffset, fow.mapResolution, fow.mapSize);
        }
    }
}
