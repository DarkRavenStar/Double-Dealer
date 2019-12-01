﻿using UnityEngine;
using UnityEngine.UI;

namespace FoW
{
    [AddComponentMenu("FogOfWar/HideInFog")]
    public class HideInFog : MonoBehaviour
    {
        public int team = 0;

        [Range(0.0f, 1.0f)]
        public float minFogStrength = 0.2f;
        
        Transform _transform;
        Renderer _renderer;
        Graphic _graphic;
        Canvas _canvas;

        void Start()
        {
            _transform = transform;
            _renderer = GetComponent<Renderer>();
            _graphic = GetComponent<Graphic>();
            _canvas = GetComponent<Canvas>();
        }

        void Update()
        {
            FogOfWar fow = FogOfWar.GetFogOfWarTeam(team);
            if (fow == null)
            {
                return;
            }
            bool visible = !fow.IsInFog(_transform.position, minFogStrength);

            if (_renderer != null)
                _renderer.enabled = visible;
            if (_graphic != null)
                _graphic.enabled = visible;
            if (_canvas != null)
                _canvas.enabled = visible;
        }
    }
}
