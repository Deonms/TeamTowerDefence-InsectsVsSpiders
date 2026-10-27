using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;

public class InsectStun : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _insectAttackScript;

    private Coroutine _stunRoutine;

    public void ApplyStun(float duration)
    {
        if (_stunRoutine != null)
        {
            StopCoroutine(_stunRoutine);
        }

        _stunRoutine = StartCoroutine(StunRoutine(duration));
    }

    private IEnumerator StunRoutine(float duration)
    {
        if (_insectAttackScript != null)
        {
            _insectAttackScript.enabled = false;
        }

        yield return new WaitForSeconds(duration);

        if (_insectAttackScript != null)
        {
            _insectAttackScript.enabled = true;
        }

        _stunRoutine = null;
    }
}
