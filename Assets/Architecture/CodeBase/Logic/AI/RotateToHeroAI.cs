using UnityEngine;

namespace CodeBase.Logic.AI
{
  public class RotateToHeroAI : FollowAIBase
  {
    [SerializeField] private float _rotateSpeed = 4f;
    
    private Vector3 _lookAt;
    

    private void Update()
    {
      if (HeroIsCreated())
        RotateToHero();
    }

    
    private void RotateToHero()
    {
      UpdateLookAt();

      transform.rotation = SmoothedRotation(currentRotation: transform.rotation, targetRotation: _lookAt);
    }

    private void UpdateLookAt()
    {
      Vector3 currentPos = transform.position;
      Vector3 targetPos = p_hero.transform.position;
      Vector3 lookDirection = targetPos - currentPos;
      
      _lookAt = new Vector3(lookDirection.x, currentPos.y, lookDirection.z);
    }

    private Quaternion SmoothedRotation(Quaternion currentRotation, Vector3 targetRotation) =>
      Quaternion.Lerp(currentRotation, Quaternion.LookRotation(targetRotation), _rotateSpeed * Time.deltaTime);
  }
}