using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Enemy
{
    public class EnemyPlayerDistance : MonoBehaviour
    {
        [SerializeField] Transform target;

        public float Enemyplayerdistance;
        void Start()
        {
            target = GameObject.Find("Player").transform;
            var animator = GetComponent<Animator>();
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    Enemyplayerdistance = Vector3.Distance(gameObject.transform.position, target.position);
                    animator.SetFloat("Distance", Enemyplayerdistance);
                }
                );
            Ray ray;
            int layerMask = 1 << 3;
            this.UpdateAsObservable()
                .Subscribe(_ =>
                {
                    ray = new Ray(transform.position, transform.TransformDirection(Vector3.forward)); foreach (RaycastHit hit in Physics.SphereCastAll(transform.position, 5, transform.TransformDirection(Vector3.forward), 20, layerMask))
                    {
                        if (hit.collider.CompareTag("Player"))
                        {
                            animator.SetTrigger("Ray");
                        }
                    }
                }
                );
        }
    }
}
