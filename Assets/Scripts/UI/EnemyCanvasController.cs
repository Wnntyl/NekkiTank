public class EnemyCanvasController : CanvasController
{
    private void Start()
    {
        MoveToTarget();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (_targetController == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _targetController.transform.position;
    }
}