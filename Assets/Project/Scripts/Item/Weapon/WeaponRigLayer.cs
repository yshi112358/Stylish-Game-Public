using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Animations.Rigging;

public class WeaponRigLayer : MonoBehaviour
{
    [System.Serializable]
    private struct ParentWeight
    {
        [Range(0, 1)] public float Item0;
        [Range(0, 1)] public float Item1;
        [Range(0, 1)] public float Item2;
        [Range(0, 1)] public float Item3;

    }
    [System.Serializable]
    private struct AimWeight
    {
        [Range(0, 1)] public float Item0;
        [Range(0, 1)] public float Item1;
    }
    [SerializeField] private ParentWeight _parentWeight;
    [SerializeField] private AimWeight _aimWeight;
    private readonly ReactiveCollection<float> _parentWeightCollection = new ReactiveCollection<float> { 0, 0, 0, 0 };
    private readonly ReactiveCollection<float> _aimWeightCollection = new ReactiveCollection<float> { 0, 0 };

    void Start()
    {
        var parent = GetComponent<MultiParentConstraint>();
        var parentSources = parent.data.sourceObjects;
        var aim = GetComponent<MultiAimConstraint>();
        var aimSources = aim.data.sourceObjects;
        //値の更新
        this.UpdateAsObservable()
            .Subscribe(_ =>
            {
                _parentWeightCollection[0] = _parentWeight.Item0;
                _parentWeightCollection[1] = _parentWeight.Item1;
                _parentWeightCollection[2] = _parentWeight.Item2;
                _parentWeightCollection[3] = _parentWeight.Item3;
                _aimWeightCollection[0] = _aimWeight.Item0;
                _aimWeightCollection[1] = _aimWeight.Item1;
            });
        //MultiParentConstraintの更新
        _parentWeightCollection
            .ObserveReplace()
            .DistinctUntilChanged()
            .Subscribe(x =>
            {
                parentSources.SetWeight(x.Index, x.NewValue);
                parent.data.sourceObjects = parentSources;
            })
            .AddTo(this);
        //MultiAimConstraintの更新
        _aimWeightCollection
            .ObserveReplace()
            .DistinctUntilChanged()
            .Subscribe(x =>
            {
                aimSources.SetWeight(x.Index, x.NewValue);
                aim.data.sourceObjects = aimSources;
            })
            .AddTo(this);
    }
}
