# Wave
## Wave 구조
* 기본적으로 DataManager를 통하여 기본 웨이브 정보를 받아옵니다.
* [WaveManager.cs](../../Assets/Scripts/Wave/WaveManager.cs)에서 웨이브 데이터 정보와 현재 진행중인 웨이브의 진행을 컨트롤합니다.
* 또한 해당 웨이브의 수치에 맞게 직접 [Meteor.cs](../../Assets/Scripts/Wave/Meteor.cs)를 스폰시킵니다.