using System.Collections;
using UnityEngine;

public class VFXHandler : MonoBehaviour
{
    [SerializeField] private ObjectPooler redLaserPool;
    [SerializeField] private ObjectPooler blueLaserPool;
    [SerializeField] private Transform redBase;
    [SerializeField] private Transform blueBase;
    [SerializeField] private Transform border;

    [Space]
    [SerializeField] private float laserFadeTime = 0.5f;

    void OnEnable()
    {
        GameEvents.OnEnemyHit += HandleEnemyHit;
        GameEvents.OnSelfHit += HandleSelfHit;
    }

    void OnDisable()
    {
        GameEvents.OnEnemyHit -= HandleEnemyHit;
        GameEvents.OnSelfHit -= HandleSelfHit;
    }

    private void HandleEnemyHit(Faction faction, Vector3 targetPos)
    {
        if (faction == Faction.Red)
            SpawnRedLaser(targetPos);
        else if (faction == Faction.Blue)
            SpawnBlueLaser(targetPos);
    }

    private void HandleSelfHit(Faction faction)
    {
        if (faction == Faction.Red)
            SpawnRedLaserSelf();
        else if (faction == Faction.Blue)
            SpawnBlueLaserSelf();
    }

    public void SpawnRedLaser(Vector3 targetPos)
    { SpawnLaser(redBase.position, targetPos, redLaserPool); }

    public void SpawnBlueLaser(Vector3 targetPos)
    { SpawnLaser(blueBase.position, targetPos, blueLaserPool); }

    public void SpawnRedLaserSelf()
    { SpawnLaser(redBase.position, border.position, redLaserPool); }

    public void SpawnBlueLaserSelf()
    { SpawnLaser(blueBase.position, border.position, blueLaserPool); }

    private void SpawnLaser(Vector3 startPos, Vector3 endPos, ObjectPooler laserPool)
    {
        LineRenderer laser = laserPool.GetFromPool(false).GetComponent<LineRenderer>();
        if (laser == null) return;

        laser.SetPosition(0, startPos);
        laser.SetPosition(1, endPos);
        laser.gameObject.SetActive(true);
        StartCoroutine(LaserFade(laser));
    }

    private IEnumerator LaserFade(LineRenderer laser)
    {
        Color startColor = laser.startColor;
        Color endColor = laser.endColor;

        startColor.a = 1f;
        endColor.a = 1f;

        laser.startColor = startColor;
        laser.endColor = endColor;

        float timeElapsed = 0f;

        while (timeElapsed < laserFadeTime)
        {
            float currentAlpha = Mathf.Lerp(1f, 0f, timeElapsed / laserFadeTime);
            timeElapsed += Time.deltaTime;

            startColor.a = currentAlpha;
            endColor.a = currentAlpha;

            laser.startColor = startColor;
            laser.endColor = endColor;

            yield return null;
        }

        startColor.a = 0f;
        endColor.a = 0f;

        laser.startColor = startColor;
        laser.endColor = endColor;

        yield return null;
        laser.gameObject.SetActive(false);
    }
}
