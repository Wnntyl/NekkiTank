public class EnemyCanvasController : CanvasController
{
    private void Update()
    {
        if(_targetController == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = _targetController.transform.position;
    }
}