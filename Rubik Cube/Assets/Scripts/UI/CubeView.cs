﻿using System;
using Assets.Scripts.Core.Model;
using Assets.Scripts.Core.Model.Enums;
using Assets.Scripts.UI.Events;
using UnityEngine;

namespace Assets.Scripts.UI
{
    [Serializable]
    [DisallowMultipleComponent]
    public sealed class CubeView : MonoBehaviour, IView
    {
        public event EventHandler<CubeSideDraggedEventArgs> OnCubeSideDragged = (sender, e) => { };

        public void Render(Piece[,,] pieces)
        {
            // TODO Render
        }

        public void Rotate(Faces face, bool clockwise)
        {
            OnCubeSideDragged(this, new CubeSideDraggedEventArgs(face, clockwise));
        }

        public void Solved()
        {
            Debug.LogWarning("Cube is solved!");
        }
    }
}
