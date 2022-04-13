using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UniRx;

namespace Project.TargetCamera
{
    public class CameraTargetDefeat : MonoBehaviour
    {
        [SerializeField] private List<CinemachineVirtualCamera> _camera = new List<CinemachineVirtualCamera>();
        public ReactiveProperty<Transform> Target;

        void Start()
        {
            Target
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    foreach (var camera in _camera)
                    {
                        camera.Follow = x;
                        camera.LookAt = x;
                    }
                });
        }
    }
}