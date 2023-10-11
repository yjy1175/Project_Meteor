# Skill
## Bullet과 SpawnBullet
* [Bullet.cs](../../Assets/Scripts/Bullet/Bullet.cs)
* [SpawnBullet.cs](../../Assets/Scripts/Bullet/SpawnBullet.cs)
* 게임의 특성 상 발사형 Bullet과 제자리에 스폰되는 레이저같은 형식의 SpawnBullet을 따로 설계 하였습니다.

## Attack과 여타 Skill
* [PlayerController.cs](../../Assets/Scripts/Player/PlayerController.cs)에서 정적인 로컬 데이터를 기반으로 공격 속도에 따라서 기본 공격이 발사 되도록 구현하였으며,
여타 스킬같은 경우는 각 구현방법에 맞게 따로 설계하였으며, 기본 Bullet과 SpawnBullet 타입의 프리팹들을 인스턴스로 가지고 있어, 직접 발사 및 스폰하는 형식으로 설계하였습니다.