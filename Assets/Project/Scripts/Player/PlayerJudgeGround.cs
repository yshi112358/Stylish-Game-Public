using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

namespace Project.Player
{
    public class PlayerJudgeGround : MonoBehaviour
    {
        void Start()
        {
            var animator = transform.root.GetComponent<Animator>();

            this.OnTriggerEnterAsObservable()
                .Where(x => x.gameObject.CompareTag("Terrain"))
                .Subscribe(_ => animator.SetBool("isGrounded", true));
            this.OnTriggerStayAsObservable()
                .Where(x => x.gameObject.CompareTag("Terrain"))
                .Subscribe(_ => animator.SetBool("isGrounded", true));
            this.OnTriggerExitAsObservable()
                .Where(x => x.gameObject.CompareTag("Terrain"))
                .Subscribe(_ => animator.SetBool("isGrounded", false));
        }
    }
}